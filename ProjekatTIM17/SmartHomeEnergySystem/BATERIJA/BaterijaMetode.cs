using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.BATERIJA
{
    public class BaterijaMetode : IBaterija
    {
        private int Rezim { get; set; }
        private Object baterijaLock = new object();

        public BaterijaMetode()
        {
            
        }

        public void PreuzmiRezim(int? rezim) 
        {
            
            if(rezim==null)
            {
                throw new ArgumentNullException("Rezim ne sme biti NULL!");
            }
            if(rezim<0 || rezim>2)
            {
                throw new ArgumentException("Nepostojeci rezim! Moze biti 0,1 ili 2.");
            }
            
            this.Rezim = (int)rezim;
               
           
        }

        public void RukovanjeKapacitetom()
        {
           

            IShes IShess = new ShesRepozitorijum();
            ShesMetode shes = new ShesMetode(IShess); 
            while (true)
            {
                RukovanjeKapacitetom2(shes);
            }
        }
        public void RukovanjeKapacitetom2(ShesMetode shes)
        {
            if(shes==null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }
            int a = this.Rezim;
            Shes sss = Shes.Instance();
            List<Baterija> baterije = shes.PreuzmiBaterijeIzBaze();
            double kapMin = 0;
            lock (baterijaLock)
            {
                for (int i = 0; i < baterije.Count; i++)
                {
                    kapMin = baterije[i].Kapacitet * 60;
                    if (a == 1)
                    {
                        kapMin++;
                    }
                    else if (a == 2)
                    {
                        kapMin--;
                    }
                    kapMin = kapMin / 60;
                    baterije[i].Kapacitet = kapMin;

                    shes.AzurirajBateriju(baterije[i]);
                    shes.PreuzmiPodatkeOdBaterije(baterije[i].Kapacitet, a, baterije[i].Ime, sss.Vreme);
                }
            }
            Thread.Sleep(1000);
        }

    }
}

