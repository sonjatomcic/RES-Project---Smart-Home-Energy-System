using SmartHomeEnergySystem.SOLARNI_PANEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.DODATNE_TABELE
{
    public class PanelIzmereneVrednosti
    {
        [Key]
        public int Id { get; set; }
        public String NazivPanela { get; set; }
        public SolarniPanel Panel { get; set; }
        public DateTime Datum { get; set; }
        public double IzmerenaSnaga { get; set; }

        public PanelIzmereneVrednosti()
        {

        }

        public PanelIzmereneVrednosti(string naziv, DateTime? dat, double? snaga)
        {
            if (naziv == null || dat == null || snaga == null)
                throw new ArgumentNullException("parametri ne mogu biti null");
            if (naziv.Trim() == "")
                throw new ArgumentException("naziv ne sme biti prazan");
            if (snaga < 0)
                throw new ArgumentException("snaga ne moze biti manja od 0");

            this.NazivPanela = naziv;
            this.Datum = (DateTime)dat;
            this.IzmerenaSnaga = (double)snaga;
        }

    }
}
