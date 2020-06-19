using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.SOLARNI_PANEL
{
    public class SolarniPanel
    {
        [Key]
        public String Ime { get; set; }
        public int MaxSnaga { get; set; }

        public SolarniPanel()
        {

        }

        public SolarniPanel(String ime, int? maxSnaga)
        {
          
            if (ime == null || maxSnaga==null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null");
            }

            if (ime.Trim() == "")
            {
                throw new ArgumentException("Ime ne sme biti prazno");
            }

            Ime = ime;

            if (maxSnaga < 10)
            {
                throw new ArgumentException("Maksimalna snaga ne sme biti manja od 10W");
            }

            MaxSnaga = (int)maxSnaga;

        }
    }
}
