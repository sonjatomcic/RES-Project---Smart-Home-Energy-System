
using SmartHomeEnergySystem.SOLARNI_PANEL;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.BAZA;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.DODATNE_TABELE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;
using System.Configuration;
using SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA;
using System.Globalization;

namespace SmartHomeEnergySystem.SHES
{
    public class ShesMetode 
    {
        
        public IShes repozitorijum { get; set; }
        public Shes shes = Shes.Instance();

        private Object aLock = new object();

        public ShesMetode(IShes repo)
        {
            repozitorijum = repo;
        }

        #region SolarniPanel
        public void IzmereneSnagePanelaDodaj(string nazivPanela, DateTime datum, double snaga)
        {
            PanelIzmereneVrednosti vrednosti = new PanelIzmereneVrednosti() { NazivPanela = nazivPanela, Datum = shes.Vreme, IzmerenaSnaga = snaga };
            repozitorijum.SacuvajIzmereneSnagePanela(vrednosti);
        }

        public List<SolarniPanel> PreuzmiSolarnePanele()
        {
            
           return repozitorijum.PreuzmiPanele();
        }

        public int BrojIzmerenihSnagaSPanela()
        {
            return repozitorijum.VratiBrojIzmerenihSnagaPanela();
        }

        public List<PanelIzmereneVrednosti> PreuzmiIzmereneVrednostiPanela()
        {
            return repozitorijum.VratiIzmereneVrednostiPanela();
        }

        #endregion


        #region Baterija
        public void PosaljiKomanduNaBateriju(IBaterija ibaterija)
        {
            if (ibaterija == null)
                throw new ArgumentNullException("parametar ne moze biti null");


            while (true)
            {
                int rezim = ValidacijaVremenaZaRezim();
                ibaterija.PreuzmiRezim(rezim);
                Thread.Sleep(1000);
                
            }

        }

        public void PreuzmiPodatkeOdBaterije(double kapacijet, int rezim, string ime,DateTime datum) 
        {
            IzmereneVrednostiBaterije izmerene = new IzmereneVrednostiBaterije();
            izmerene.Kapacitet = kapacijet;
            izmerene.Rezim = rezim;
            izmerene.NazivBaterije = ime;
          
            izmerene.Datum = datum;
            repozitorijum.SacuvajIzmereneVrednostiBaterije(izmerene);
            
        }

        public int ValidacijaVremenaZaRezim() 
        {
            Shes s = Shes.Instance();
            DateTime vreme = s.Vreme;
            int rezim = 0;      //0 - mirovanje , 1 - punjenje  , 2 - praznjenje

            int sati = Int32.Parse(vreme.TimeOfDay.Hours.ToString());
            int minuti = Int32.Parse(vreme.TimeOfDay.Minutes.ToString());

            if (sati >= 3 && sati <= 6)
            {
                if (sati == 6 && minuti >= 1)
                    rezim = 0;
                else
                    rezim = 1;
            }
            else if (sati >= 14 && sati <= 17)
            {
                if (sati == 17 && minuti >= 1)
                    rezim = 0;
                else rezim = 2;
            }
            else rezim = 0;

            return rezim;
        }

        public List<Baterija> PreuzmiBaterijeIzBaze() 
        {
            return repozitorijum.PreuzmiBaterije();
        }

        public int BrojIzmerenihKapacitetaBaterija() 
        {
            return repozitorijum.BrojIzmerenihKapaciteta();
        }

        public void AzurirajBateriju(Baterija baterija)
        {
            repozitorijum.AzurirajBaterijuRepozitorijum(baterija);
        }
        #endregion

        #region Elektrodistribucija
       
        public void RacunanjeSnageRazmene(ISolarniPanel panel, Elektrodistribucija e)
        {
            //shes treba da izracuna razliku potrosnje i proizvodnje / pozitivno - trosak za shes / negativno - prodaja
            double proizvodnjaPanela; double potrosnjaPotrosaca; double potrosnjaProizvodnjaBaterije; double razlikaPotrosnjaProizvodnja;       
            IElektrodistribucija Ielektrodistr = new ElektrodistribucijaMetode();
            int snagaRazmene;
            List<SolarniPanel> solarniPaneli; List<Potrosac> potrosaci; List<Baterija> baterije;
            int rezim;
            double vreme = Double.Parse(ConfigurationManager.AppSettings["sekunde"]) / 3600;

            while (true)
            {
                solarniPaneli = PreuzmiSolarnePanele();
                potrosaci = VratiListuPotrosaca();
                baterije = PreuzmiBaterijeIzBaze();
                rezim = ValidacijaVremenaZaRezim();

                StanjePotrosacaa(potrosaci);

                proizvodnjaPanela = ProizvodnjaSolarnihPanela(panel, solarniPaneli, vreme);
                potrosnjaPotrosaca = PotrosnjaPotrosaca(potrosaci);
                potrosnjaProizvodnjaBaterije = PotrosnjaProizvodnjaBaterije(baterije,vreme);

                if(rezim==1)    //ako je rezim 1 baterija je potrosac
                    razlikaPotrosnjaProizvodnja = (potrosnjaProizvodnjaBaterije + potrosnjaPotrosaca) - proizvodnjaPanela;
                else if(rezim==2)    //ako je rezim 2 baterija je proizvodjac
                    razlikaPotrosnjaProizvodnja =  potrosnjaPotrosaca - (proizvodnjaPanela + potrosnjaProizvodnjaBaterije);
                else     //ako je rezim 0 nista
                    razlikaPotrosnjaProizvodnja = potrosnjaPotrosaca - proizvodnjaPanela;

                Ielektrodistr.PreuzmiRazliku(razlikaPotrosnjaProizvodnja, e);//saljem u elektrodistr                            
                double cena = Ielektrodistr.PosaljiCenu();//primi cenu

                if (razlikaPotrosnjaProizvodnja >= 0)  //ako je snaga razmene pozitivna to je trosak 
                    snagaRazmene = 1;
                else
                    snagaRazmene = -1;

                SacuvajElektroDistribucijaPodatke(shes.Vreme, snagaRazmene, cena);

                

                 Thread.Sleep(1000);
            }
        }

        public void SacuvajElektroDistribucijaPodatke(DateTime datum, int snagaRazmene, double cena)
        {
            ElektrodistribucijaPodaci e = new ElektrodistribucijaPodaci(datum, snagaRazmene, cena);
            repozitorijum.DodajElektrodistribucijaPodatke(e);
        }

        public int BrojElektrodistribucijaPodatakaBaza() 
        {
            return repozitorijum.BrojPodatakaElektrodistribucija();
        }

        
        public double ProizvodnjaSolarnihPanela(ISolarniPanel panel, List<SolarniPanel> solarniPaneli, double? vreme)
        {
            if (panel == null || solarniPaneli==null || vreme==null)
                throw new ArgumentNullException("parametari ne mogu biti null");

            double proizvodnja = 0;



            int trenutnaSnagaSunca = panel.PosaljiTrenutnuSnaguSunca();
            double sn;
            foreach(var p in solarniPaneli)
            {
                sn = (double)p.MaxSnaga / 1000;
                proizvodnja += (sn * (double)trenutnaSnagaSunca / 100) * (double)vreme;
            }

            return proizvodnja;
        }
        
        public double PotrosnjaPotrosaca(List<Potrosac> potrosaci)
        {
            if(potrosaci==null)
                throw new ArgumentNullException("parametar ne moze biti null");

            double potrosnja = 0;
            
            foreach(var p in potrosaci)
            {
                if (p.Upaljeno)
                    potrosnja += p.Potrosnja;
            }
            return potrosnja;
        }

        public double PotrosnjaProizvodnjaBaterije(List<Baterija> baterije,double? vreme)
        {
            if (baterije == null || vreme==null)
                throw new ArgumentNullException("parametar ne moze biti null");

            double potrosnja = 0;
           
            foreach (var b in baterije)
            {

                potrosnja += ((double)b.MaxSnaga/1000) * (double)vreme;

            }
           
           
            return potrosnja;
        }
        #endregion


        #region Potrosac
        public void DodajPotrosac(string naziv, double potrosnja)
        {
            if (naziv == null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }

            Potrosac potrosac = new Potrosac(naziv, potrosnja, false);
            repozitorijum.DodajPotrosacaUBazu(potrosac);
        }

        public List<Potrosac> VratiListuPotrosaca()
        {
           
            return repozitorijum.VratiListuPotrosacaRepo();
        }

        public void Ukljuci(string naziv)
        {
            if(naziv==null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }
            List<Potrosac> lista = repozitorijum.VratiListuPotrosacaRepo();
            foreach(Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    p.Upaljeno = true;
                    repozitorijum.UkljuciPotrosacUBazi(p);
                    break;
                }
            }
        }

        public void Iskljuci(string naziv)
        {
            if (naziv == null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }
            List<Potrosac> lista = repozitorijum.VratiListuPotrosacaRepo();
            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    p.Upaljeno = false;
                    repozitorijum.IskljuciPotrosacUBazi(p);
                    break;
                }
            }
        }

        public void ObrisiPotrosac(string naziv)
        {
            if (naziv == null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }
            List<Potrosac> lista = repozitorijum.VratiListuPotrosacaRepo();
            
            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                   
                    repozitorijum.ObrisiPotrosacUBazi(p);
                    break;
                }
            }
        }
        #endregion

        #region grafik i racunanje troskova
        public void IzracunajTroskove()
        {
          
            Console.WriteLine("Unesite zeljeni datum (dd/MM/yyyy): ");
            string d = Console.ReadLine();

            List<DateTime> datumi = repozitorijum.VratiPodatkeElektrodistribucija().Select(a => a.Datum).ToList<DateTime>();
            if (datumi.Count > 0)
            {
                if (ValidacijaDatuma(d, datumi[0], datumi.Last()))
                {
                    var cultureInfo = new CultureInfo("de-DE");
                    var dateTime = DateTime.Parse(d, cultureInfo);

                    double troskovi = 0;

                    List<double> lista = new List<double>();
                    lista = repozitorijum.VratiPodatkeElektrodistribucija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.SnagaRazmene == 1).Select(a => a.Cena).ToList<double>();
                    int cnt = lista.Count();

                    for (int i = 0; i < cnt; i++)
                    {
                        troskovi += lista[i];
                    }

                    Console.WriteLine("Troskovi za datum " + dateTime.ToString("dd/MM/yyyy") + " su " + troskovi.ToString());
                }
            }
            else
            {
                Console.WriteLine("u tabelu nije upisan nijedan datum");

            }
                
           
        }

        public bool ValidacijaDatuma(string datum, DateTime? prvi, DateTime? poslednji)
        {

            if (datum == null || prvi == null || poslednji == null)
                throw new ArgumentNullException("parametri ne mogu biti null");

            DateTime prviDatum = (DateTime)prvi;
            DateTime poslednjiDatum = (DateTime)poslednji;

            var cultureInfo = new CultureInfo("de-DE");
            
            DateTime dateTime;

            try
            {
                dateTime = DateTime.Parse(datum, cultureInfo);

            }
            catch
            {
                Console.WriteLine("Pogresan format datuma");
                return false;
            }
           
               
            if(DateTime.Compare(dateTime.Date, prviDatum.Date) < 0)
            {
                Console.WriteLine("Datum " + dateTime.ToString("dd/MM/yyyy") + " ne postoji u bazi");
                return false;
            }
            if(DateTime.Compare(dateTime.Date,poslednjiDatum.Date) > 0)
            {
                Console.WriteLine("Datum " + dateTime.ToString("dd/MM/yyyy") + " ne postoji u bazi");
                return false;
            }
            return true;
            

        }

        public void StanjePotrosacaa(List<Potrosac> lista)
        {
            if(lista==null)
            {
                throw new ArgumentNullException("Parametri ne mogu da budu NULL!");
            }
           
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Upaljeno)
                    {
                        PotrosaciStanje ps = new PotrosaciStanje();
                        ps.Snaga = lista[i].Potrosnja;
                        ps.Datum = shes.Vreme;
                        repozitorijum.DodajStanjePotrosaca(ps);
                    }
                }
             
        }

        public List<PotrosaciStanje> VratiStanjaPotrosacaIzBaze()
        {
            return repozitorijum.VratiListuStanjaPotrosaca();
        }

        public void VrednostiZaGrafik(double e)
        {
           
            Console.WriteLine("Unesite zeljeni datum [dd/MM/yyyy]");
            string dat = Console.ReadLine();

           
            List<DateTime> datumi = repozitorijum.VratiPodatkeElektrodistribucija().Select(a => a.Datum).ToList<DateTime>();
            if (ValidacijaDatuma(dat, datumi[0], datumi.Last()))
            {
                var cultureInfo = new CultureInfo("de-DE");
                var dateTime = DateTime.Parse(dat, cultureInfo);

                List<double> ListaPaneliSnaga = new List<double>();
                ListaPaneliSnaga = repozitorijum.VratiIzmereneVrednostiPanela().Where(a => a.Datum.Date.Equals(dateTime.Date)).Select(a => a.IzmerenaSnaga).ToList<double>();
                List<IzmereneVrednostiBaterije> ListaBatEnergija1 = repozitorijum.VratiListuIzerenihVrednostibaterija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.Rezim == 1).ToList<IzmereneVrednostiBaterije>();
                List<IzmereneVrednostiBaterije> ListaBatEnergija2 = repozitorijum.VratiListuIzerenihVrednostibaterija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.Rezim == 2).ToList<IzmereneVrednostiBaterije>();
                List<IzmereneVrednostiBaterije> ListaBatEnergija3 = repozitorijum.VratiListuIzerenihVrednostibaterija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.Rezim == 0).ToList<IzmereneVrednostiBaterije>();

                List<double>ListaEDpoz = new List<double>();
                List<double>ListaEDneg = new List<double>();
                ListaEDneg = repozitorijum.VratiPodatkeElektrodistribucija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.SnagaRazmene == 1).Select(a => a.Cena).ToList<double>();
                ListaEDpoz = repozitorijum.VratiPodatkeElektrodistribucija().Where(a => a.Datum.Date.Equals(dateTime.Date) && a.SnagaRazmene == -1).Select(a => a.Cena).ToList<double>();

                List<double> ListaPot = repozitorijum.VratiListuStanjaPotrosaca().Where(a => a.Datum.Date.Equals(dateTime.Date)).Select(a => a.Snaga).ToList<double>();



                double proizvodnjaPanela = GrafikProizvodnjaPanela(dateTime,ListaPaneliSnaga);
                double energijaBat = GrafikEnergijaBaterija(dateTime,ListaBatEnergija1,ListaBatEnergija2,ListaBatEnergija3);
                double uvozed = GrafikUvozED(dateTime, e,ListaEDpoz,ListaEDneg);
                double potrosnja = GrafikPotrosnja(dateTime,ListaPot);
                ShesGrafik sg = new ShesGrafik(proizvodnjaPanela, energijaBat, uvozed, potrosnja, dat);
                sg.ShowDialog();
            }

            

        }

        public double GrafikProizvodnjaPanela(DateTime dateTime,List<double> listaPaneliSnaga)
        {
            
                double izmereno = 0;
                int cnt = listaPaneliSnaga.Count;
                

                for (int i = 0; i < cnt; i++)
                {
                    izmereno = izmereno + listaPaneliSnaga[i];
                }

                if(dateTime.Date!=shes.Vreme.Date)
                {
                     izmereno = izmereno / 1000 * 24;
                }
                else
                {
                    double sati = ((double)shes.Vreme.TimeOfDay.TotalMinutes) / 60.0;
                    izmereno = izmereno / 1000 * sati;

                }
                                           
                return izmereno;
            
            
        }
        
        public double GrafikEnergijaBaterija(DateTime dateTime,List<IzmereneVrednostiBaterije> listaBatEnergija1,
            List<IzmereneVrednostiBaterije> listaBatEnergija2, List<IzmereneVrednostiBaterije> listaBatEnergija3)
        {
           
            double rez1 = 0, rez2 = 0, rez3=0;
           
            if (dateTime.Date != shes.Vreme.Date)
            {
                for (int i = 0; i < listaBatEnergija1.Count; i++)
                {
                    rez1 = ((double)listaBatEnergija1[i].Baterija.MaxSnaga / (double)1000 * 24) + rez1;
                }

                for (int i = 0; i < listaBatEnergija2.Count; i++)
                {
                    rez2 = ((double)listaBatEnergija2[i].Baterija.MaxSnaga / (double)1000 * 24) + rez2;
                }

                for (int i = 0; i < listaBatEnergija3.Count; i++)
                {
                    rez3 = ((double)listaBatEnergija3[i].Baterija.MaxSnaga / (double)1000 * 24) + rez3;
                }

            }
            else
            {
                double sati = ((double)shes.Vreme.TimeOfDay.TotalMinutes) / 60.0;
                for (int i = 0; i < listaBatEnergija1.Count; i++)
                {

                    rez1 = ((double)listaBatEnergija1[i].Baterija.MaxSnaga / (double)1000 * sati) + rez1;
                }

                for (int i = 0; i < listaBatEnergija2.Count; i++)
                {
                    rez2 = ((double)listaBatEnergija2[i].Baterija.MaxSnaga / (double)1000 * sati) + rez2;
                }

                for (int i = 0; i < listaBatEnergija3.Count; i++)
                {
                    rez3 = ((double)listaBatEnergija3[i].Baterija.MaxSnaga / (double)1000 * sati) + rez3;
                }

            }
            
            double rez = (double)rez2 - (double)rez1 + (double)rez3;
            return rez;
            
        }

        public double GrafikUvozED(DateTime dateTime,double e,List<double> listaEDpoz,List<double>listaEDneg)
        {

                double cena = e;
                double neg = 0;
                double poz = 0;
               

                for (int i = 0; i < listaEDneg.Count; i++)
                {
                    neg = (listaEDneg[i] / cena) + neg;
                }

                for (int i = 0; i < listaEDpoz.Count; i++)
                {
                    poz = (listaEDpoz[i] / cena) + poz;
                }

                return neg - poz;
            
            
        }

        public double GrafikPotrosnja(DateTime dateTime,List<double> listaPot)
        {
            
              double rez = 0;
               
                for (int i = 0; i < listaPot.Count; i++)
                {
                    rez = listaPot[i] + rez;
                }

                return rez;
            
            
        }

        
        public int BrojStanjaPotrosacaIzBaza()
        {
            return repozitorijum.VratiBrojStanjaPotrosaca();
        }
        #endregion
    }
}
