
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.SHES;
using SmartHomeEnergySystem.SOLARNI_PANEL;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;
using SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA;

namespace SmartHomeEnergySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //main je takodje tred
            Console.WriteLine("SHES POKRENUT");
            Thread.Sleep(5000);

            Shes s1 = Shes.Instance();
            //novi tred za vreme
            Thread vreme = new Thread(() => s1.UbrzajVreme(ConfigurationManager.AppSettings["sekunde"]));
            vreme.Start();
            

            IShes Ishes = new ShesRepozitorijum();
            ShesMetode shes = new ShesMetode(Ishes);

            ISolarniPanel ISolarniPanel = new SolarniPanelMetode();
            
            IPotrosac Ipotrosac = new PotrosacMetode();

            //novi tred za merenje snage panela
             Thread merenjeSnagePanela = new Thread(ISolarniPanel.IzmeriSnagePanela);
             merenjeSnagePanela.Start();

            
            IBaterija IBaterija = new BaterijaMetode();

            //tred za komande bateriji
            Thread posaljiKomandu = new Thread(() => shes.PosaljiKomanduNaBateriju(IBaterija));
            posaljiKomandu.Start();
            
            //tred za bateriju        
            Thread baterija = new Thread(IBaterija.RukovanjeKapacitetom);
            baterija.Start();

            //tred za merenje snage razmene
            Elektrodistribucija e = new Elektrodistribucija(3.8); //cena
            Thread snagaRazmene = new Thread(() => shes.RacunanjeSnageRazmene(ISolarniPanel,e));
            snagaRazmene.Start();

            Thread a = new Thread(() => s1.KorisnickiMeni(ISolarniPanel, shes, Ipotrosac, e.Cena));
            a.Start();
            
           

        }
    }
}
