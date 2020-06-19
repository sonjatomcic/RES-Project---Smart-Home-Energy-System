using SmartHomeEnergySystem.POTROSAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.COMMON
{
   public interface IPotrosac
    {
        void DodajPotrosacPrekoKonzole();
        void UkljuciPotrosac();
        void IskljuciPotrosac();
        void ObrisiPotrosacPrekoKonzole();
        bool ValidacijaDodatogPotrosaca(List<Potrosac> lista, string naziv, string potrosnjaString);
        bool ValidacijaUkljuci(List<Potrosac> lista, string naziv);
        bool ValidacijaIskljuci(List<Potrosac> lista, string naziv);
        bool ValidacijaObrisiPotrosaca(List<Potrosac> lista, string naziv);

    }
}
