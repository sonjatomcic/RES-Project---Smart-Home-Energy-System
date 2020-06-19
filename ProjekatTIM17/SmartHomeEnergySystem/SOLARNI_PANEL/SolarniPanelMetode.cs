using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.SOLARNI_PANEL
{
    public class SolarniPanelMetode : ISolarniPanel
    {
        IShes shesRepo = new ShesRepozitorijum();  
        private int SnagaSunca { get; set; }

        private Object panelLock = new object();

        public SolarniPanelMetode()
        {
            SnagaSunca = 0;
        }
        
        public void UcitajSnaguSunca()
        {
            
            
                Console.WriteLine("Unesite snagu sunca (0-100): ");
                string str = Console.ReadLine();
                int snagaSunca = 0;
                if (ValidacijaSnageSunca(str))
                {
                    snagaSunca = Int32.Parse(str);
                    this.SnagaSunca = snagaSunca;
                   Console.WriteLine("Snaga sunca je: " + this.SnagaSunca);

                }
                else
                {
                    Console.WriteLine("Uneli ste pogresnu vrednost. Pokusajte ponovo. Trenutna snaga je i dalje " + this.SnagaSunca);
                        
                }
                
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine();
           
        }

        public bool ValidacijaSnageSunca(string str)
        {

            string pattern = @"^[0-9][0-9]{0,2}$";
            
            if (Regex.IsMatch(str,pattern))
            {
                Int32.TryParse(str, out int snaga);
                if (snaga < 0 || snaga > 100)
                {
                    Console.WriteLine("Snaga sunca mora biti u opsegu 0-100 %");
                    return false;
                }
                return true;
            }
            Console.WriteLine("Snaga sunca mora biti broj");
            return false;
           
        }
        
        public void IzmeriSnagePanela()
        {      
            ShesMetode shes = new ShesMetode(shesRepo);

            while (true)
            {
                List<SolarniPanel> paneli = shes.PreuzmiSolarnePanele();
                Shes s = Shes.Instance();
                
                double izmereno = 0;
                
                lock (panelLock)
                {
                    for(int i = 0; i < paneli.Count; i++)
                    {
                        
                        izmereno = paneli[i].MaxSnaga * this.SnagaSunca / 100;
                        shes.IzmereneSnagePanelaDodaj(paneli[i].Ime, s.Vreme, izmereno); 
                        
                    }
                    
                  
                }
               
               
                Thread.Sleep(1000);  
            }
            
        }

        public int PosaljiTrenutnuSnaguSunca()
        {
            lock (panelLock)
            {
                return this.SnagaSunca;
          
            }
        }
    }
}
