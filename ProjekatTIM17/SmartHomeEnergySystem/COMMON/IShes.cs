
using SmartHomeEnergySystem.SOLARNI_PANEL;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.DODATNE_TABELE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;

namespace SmartHomeEnergySystem.COMMON
{
    public interface IShes
    {
        #region SolarniPaneli
        List<SolarniPanel> PreuzmiPanele();    
        void SacuvajIzmereneSnagePanela(PanelIzmereneVrednosti izmereno);
        int VratiBrojIzmerenihSnagaPanela();
        List<PanelIzmereneVrednosti> VratiIzmereneVrednostiPanela();
        #endregion

        #region Baterija
        List<Baterija> PreuzmiBaterije();
        void SacuvajIzmereneVrednostiBaterije(IzmereneVrednostiBaterije izmerene);
        int BrojIzmerenihKapaciteta();
        void AzurirajBaterijuRepozitorijum(Baterija baterija);
        List<IzmereneVrednostiBaterije> VratiListuIzerenihVrednostibaterija();
        #endregion

        #region Potrosac
        void DodajPotrosacaUBazu(Potrosac potrosac);
        List<Potrosac> VratiListuPotrosacaRepo();
        void UkljuciPotrosacUBazi(Potrosac potrosac);
        void IskljuciPotrosacUBazi(Potrosac potrosac);
        void ObrisiPotrosacUBazi(Potrosac potrosac);
        #endregion

        #region Elektrodistribucija
        void DodajElektrodistribucijaPodatke(ElektrodistribucijaPodaci e);
        int BrojPodatakaElektrodistribucija();
        List<ElektrodistribucijaPodaci> VratiPodatkeElektrodistribucija();
        #endregion


        #region StanjePotrosaca
        void DodajStanjePotrosaca(PotrosaciStanje s);
        List<PotrosaciStanje> VratiListuStanjaPotrosaca();
        int VratiBrojStanjaPotrosaca();
        #endregion

        


    }
}
