
using SmartHomeEnergySystem.BAZA;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.DODATNE_TABELE;
using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using SmartHomeEnergySystem.POTROSAC;
using System.Data.Entity;

namespace SmartHomeEnergySystem.SHES
{
    public class ShesRepozitorijum : IShes
    {
        public ApplicationContext repozitorijum = new ApplicationContext();
        private Object repozitorijumLock = new object();
        public IBaterija IBaterija = new BaterijaMetode();

        #region SolarniPanel
        public void SacuvajIzmereneSnagePanela(PanelIzmereneVrednosti izmereno)
        {

           
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                var panel = repozitorijum.SolarniPaneli.FirstOrDefault(p => p.Ime.Equals(izmereno.NazivPanela));
                izmereno.Panel = panel;

                repozitorijum.PanelIzmereno.Add(izmereno);
                repozitorijum.SaveChanges();
            }
           
        }

        public List<SolarniPanel> PreuzmiPanele()
        {
           
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.SolarniPaneli.ToList<SolarniPanel>();
            }
               
            
        }

        public int VratiBrojIzmerenihSnagaPanela()
        {
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.PanelIzmereno.Count();
            }
            
        }

        public List<PanelIzmereneVrednosti> VratiIzmereneVrednostiPanela()
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                List<PanelIzmereneVrednosti> izmerene = repozitorijum.PanelIzmereno.ToList<PanelIzmereneVrednosti>();
                return izmerene;
            }


        }
        #endregion


        #region Baterija
        public int BrojIzmerenihKapaciteta()
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.IzmenereVrednostiBaterija.Count();
            }
        }

        public void SacuvajIzmereneVrednostiBaterije(IzmereneVrednostiBaterije izmerene)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                List<Baterija> lista = repozitorijum.Baterije.ToList<Baterija>();

                Baterija baterija = new Baterija();
                foreach (Baterija b in lista)
                {
                    if (izmerene.NazivBaterije == b.Ime)
                    {
                        baterija = b;
                    }
                }
                izmerene.Baterija = baterija;
                repozitorijum.IzmenereVrednostiBaterija.Add(izmerene);
                repozitorijum.SaveChanges();
            }

        }

        public List<IzmereneVrednostiBaterije> VratiListuIzerenihVrednostibaterija()
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                List<IzmereneVrednostiBaterije> lista = repozitorijum.IzmenereVrednostiBaterija.ToList<IzmereneVrednostiBaterije>();
                int cnt = lista.Count;
                List<Baterija> baterije = repozitorijum.Baterije.ToList<Baterija>();
                int cnt2 = baterije.Count;
               
                for(int i = 0; i < cnt; i++)
                {
                    for(int j = 0; j < cnt2; j++)
                    {
                        if (lista[i].NazivBaterije.Equals(baterije[j].Ime))
                        {
                            lista[i].Baterija = baterije[j];
                            break;
                        }
                    }
                }

                return lista;
            }
        }

        public List<Baterija> PreuzmiBaterije()
        {
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.Baterije.ToList<Baterija>();
            }
            
           
        }

        public void AzurirajBaterijuRepozitorijum(Baterija baterija)
        {
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                repozitorijum.Entry(baterija).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    repozitorijum.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            
        }
        #endregion

        #region Potrosac
        public void DodajPotrosacaUBazu(Potrosac potrosac)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                repozitorijum.Potrosaci.Add(potrosac);
                repozitorijum.SaveChanges();
            }
        }

        public List<Potrosac> VratiListuPotrosacaRepo()
        {
           
           using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.Potrosaci.ToList<Potrosac>();
            }
           
            
        }


        public void UkljuciPotrosacUBazi(Potrosac potrosac)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                repozitorijum.Entry(potrosac).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    repozitorijum.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
        }

        public void IskljuciPotrosacUBazi(Potrosac potrosac)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                repozitorijum.Entry(potrosac).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    repozitorijum.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
        }

        public void ObrisiPotrosacUBazi(Potrosac potrosac)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
               
                repozitorijum.Entry(potrosac).State = EntityState.Deleted;        
                repozitorijum.SaveChanges();
            }
        }
        #endregion

        #region Elektrodistribucija
        public void DodajElektrodistribucijaPodatke(ElektrodistribucijaPodaci e)
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                try
                {
                    repozitorijum.ElektroDistribucijaPodaci.Add(e);
                    repozitorijum.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("ne moze da se doda");
                }
            }
            
        }

        public int BrojPodatakaElektrodistribucija()
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.ElektroDistribucijaPodaci.Count();
            }
        }

        public List<ElektrodistribucijaPodaci> VratiPodatkeElektrodistribucija()
        {
             using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.ElektroDistribucijaPodaci.ToList<ElektrodistribucijaPodaci>();
            }
               
            
        }
        #endregion


        #region StanjaPotrosaca
        public void  DodajStanjePotrosaca(PotrosaciStanje s)
        {
           
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                repozitorijum.stanjePotrosaca.Add(s);
                repozitorijum.SaveChanges();
            }
                
            
        }

        public List<PotrosaciStanje> VratiListuStanjaPotrosaca()
        {
            using(ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.stanjePotrosaca.ToList<PotrosaciStanje>();
            }
            
        }

        public int VratiBrojStanjaPotrosaca()
        {
            using (ApplicationContext repozitorijum = new ApplicationContext())
            {
                return repozitorijum.stanjePotrosaca.Count();
            }
        }
        #endregion

    }
}
