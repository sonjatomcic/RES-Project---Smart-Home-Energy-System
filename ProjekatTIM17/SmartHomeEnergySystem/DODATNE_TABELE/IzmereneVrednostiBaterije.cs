using SmartHomeEnergySystem.BATERIJA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.DODATNE_TABELE
{
   public class IzmereneVrednostiBaterije
    {
        [Key]
        public int Id { get; set; }

        public string NazivBaterije { get; set; }

        public Baterija Baterija { get; set; }

        public double Kapacitet { get; set; }

        public int Rezim { get; set; }

        public DateTime Datum { get; set; }

        public IzmereneVrednostiBaterije() { }

        public IzmereneVrednostiBaterije(string naziv, double? kapacitet, int? rezim, DateTime? dat)
        {
            if (naziv == null || kapacitet == null || rezim == null || dat == null)
                throw new ArgumentNullException("parametri ne mogu biti null");
            if (naziv.Trim() == "")
                throw new ArgumentException("naziv ne moze biti prazan");
            if (rezim != 0 && rezim != 1 && rezim != 2)
                throw new ArgumentException("rezim mora biit 0,1 ili 2");
            if (kapacitet < 0)
                throw new ArgumentException("kapacitet ne moze biti manji od 0");

            DateTime datum = (DateTime)dat;
            this.NazivBaterije = naziv;
            this.Kapacitet = (double)kapacitet;
            this.Rezim = (int)rezim;
            this.Datum = datum;
                
        }


    }
}
