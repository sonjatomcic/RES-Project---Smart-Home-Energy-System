
using SmartHomeEnergySystem.SOLARNI_PANEL;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.DODATNE_TABELE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;

namespace SmartHomeEnergySystem.SHES
{
    public class FakeShesRepozitorijum : IShes
    {
        public static List<SolarniPanel> solarniPaneli = new List<SolarniPanel>();
        public static List<PanelIzmereneVrednosti> izmereneVrednostiPanel = new List<PanelIzmereneVrednosti>();
        List<Baterija> Baterije = new List<Baterija>();
        List<IzmereneVrednostiBaterije> izmerenevrednostiBat = new List<IzmereneVrednostiBaterije>();
        List<ElektrodistribucijaPodaci> elektrodistribucijaPod = new List<ElektrodistribucijaPodaci>();
        Shes shes = Shes.Instance();
        List<PotrosaciStanje> listaStanjePotrosaca = new List<PotrosaciStanje>();
        List<Potrosac> potrosaci = new List<Potrosac>();
        public IBaterija IBaterija = new BaterijaMetode();

        #region SolarniPanel
        public void SacuvajIzmereneSnagePanela(PanelIzmereneVrednosti izmereno)
        {
            izmereneVrednostiPanel.Add(izmereno);
        }

        public List<SolarniPanel> PreuzmiPanele()
        {
            List<SolarniPanel> paneli = solarniPaneli; 
            return paneli;
        }

        public int VratiBrojIzmerenihSnagaPanela()
        {
            return izmereneVrednostiPanel.Count();
        }

        public List<PanelIzmereneVrednosti> VratiIzmereneVrednostiPanela()
        {
            return izmereneVrednostiPanel;
        }
        #endregion

       

        #region Baterija
        public int BrojIzmerenihKapaciteta()
        {
            return izmerenevrednostiBat.Count();
        }

        public void SacuvajIzmereneVrednostiBaterije(IzmereneVrednostiBaterije izmerene)
        {
            Baterija bat = new Baterija();
            bat.Ime = izmerene.NazivBaterije;
            bat.Kapacitet = 20.0;
            bat.MaxSnaga = 100;

            izmerene.Baterija = bat;
            izmerenevrednostiBat.Add(izmerene);

        }

        public List<Baterija> PreuzmiBaterije()
        {
          
            return Baterije;
        }

        public void AzurirajBaterijuRepozitorijum(Baterija baterija)
        {
            for (int i = 0; i < Baterije.Count; i++)
            {
                if (baterija.Ime == Baterije[i].Ime)
                {
                    Baterije[i] = baterija;
                }
            }
        }

        public List<IzmereneVrednostiBaterije> VratiListuIzerenihVrednostibaterija()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Potrosac
        public void DodajPotrosacaUBazu(Potrosac potrosac)
        {

            potrosaci.Add(potrosac);
        }

        public List<Potrosac> VratiListuPotrosacaRepo()
        {
            return potrosaci;
        }

       public void UkljuciPotrosacUBazi(Potrosac potrosac)
        {
            for (int i = 0; i < potrosaci.Count; i++)
            {
                if (potrosac.Ime == potrosaci[i].Ime)
                {
                    potrosaci[i] = potrosac;
                }
            }
        }

        public void IskljuciPotrosacUBazi(Potrosac potrosac)
        {
            for (int i = 0; i < potrosaci.Count; i++)
            {
                if (potrosac.Ime== potrosaci[i].Ime)
                {
                    potrosaci[i] = potrosac;
                }
            }
        }

        public void ObrisiPotrosacUBazi(Potrosac potrosac)
        {
            potrosaci.Remove(potrosac);
        }
        #endregion


        #region Elektrodistribucija
        public void DodajElektrodistribucijaPodatke(ElektrodistribucijaPodaci e)
        {
            elektrodistribucijaPod.Add(e);
        }

        public int BrojPodatakaElektrodistribucija()
        {
            return elektrodistribucijaPod.Count();
        }

        public List<ElektrodistribucijaPodaci> VratiPodatkeElektrodistribucija()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region StanjaPotrosaca

        public void DodajStanjePotrosaca(PotrosaciStanje s)
        {
            listaStanjePotrosaca.Add(s);
        }

        public List<PotrosaciStanje> VratiListuStanjaPotrosaca()
        {
            throw new NotImplementedException();
        }

        
        public int VratiBrojStanjaPotrosaca()
        {
            return listaStanjePotrosaca.Count;
        }
        #endregion
    }
}
