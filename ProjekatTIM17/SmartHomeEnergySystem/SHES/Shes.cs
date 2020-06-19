using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.POTROSAC;
using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.SHES
{
    public class Shes
    {
        private static Shes instance;     //singleton
        public  DateTime Vreme { get; set; }
        private static object syncLock = new object();
       

        ~Shes()
        {

        }

        public Shes()
        {
            Vreme = DateTime.Now;
        }

        public static Shes Instance()
        {
            lock (syncLock)
            {
                if (instance == null)
                {
                    instance = new Shes();
                }
            }
            return instance;
        }

        public void UbrzajVreme(string sekundaa){
            

            if (sekundaa == null)
                throw new ArgumentNullException("Sekunda ne moze bit null");
            if (sekundaa == "")
                throw new ArgumentException("Sekunda ne moze biti prazan string");
            if (sekundaa.Contains("-"))
                throw new ArgumentException("sekunda ne moze biti negativan broj");

            int sekunda;
            while (true)
            {
                try
                {
                  
                     sekunda= Int32.Parse(sekundaa);

                    Vreme = Vreme.AddSeconds(sekunda);
                    Thread.Sleep(1000);

                }
                catch(Exception e)
                {
                    Console.WriteLine("Greska priklikom simulacije vremena");
                    throw new Exception(e.ToString());
                }
            }
           
        }

        public void KorisnickiMeni(ISolarniPanel IsolarniPanel, ShesMetode shes, IPotrosac Ipotrosac, double e)
        {
           

            while (true)
            {

                Console.WriteLine("\nMeni:");
                Console.WriteLine("\t1. Unesi snagu sunca");
                Console.WriteLine("\t2. Dodaj potrosaca");
                Console.WriteLine("\t3. Obrisi potrosaca");
                Console.WriteLine("\t4. Ukljuci potrosaca");
                Console.WriteLine("\t5. Iskljuci potrosaca");
                Console.WriteLine("\t6. Prikazi grafik za datum");
                Console.WriteLine("\t7. Prikazi troskove za datum");
                Console.WriteLine("\nIzaberite opciju: ");
            

                string a = Console.ReadLine();
                int izbor;
                if (!Int32.TryParse(a, out izbor))
                {
                    Console.WriteLine("Pogresan unos");
                }
                else
                {
                    switch (izbor)
                        {
                            case 1: IsolarniPanel.UcitajSnaguSunca(); break;
                            case 2: Ipotrosac.DodajPotrosacPrekoKonzole(); break;
                            case 3: Ipotrosac.ObrisiPotrosacPrekoKonzole(); break;
                            case 4: Ipotrosac.UkljuciPotrosac(); break;
                            case 5: Ipotrosac.IskljuciPotrosac(); break;
                            case 6: shes.VrednostiZaGrafik(e); break;
                            case 7: shes.IzracunajTroskove(); break;
                            default: Console.WriteLine("Opcija ne postoji"); break;
                        }
                    

                }

            }


        }
    }
}
