using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.DODATNE_TABELE
{
    public class PotrosaciStanje
    {
        [Key]
        public int Id { get; set; }
        public double Snaga { get; set; }
        public DateTime Datum { get; set; }

        public PotrosaciStanje()
        {

        }

        public PotrosaciStanje(double? snaga, DateTime? dat)
        {
            if (snaga == null || dat == null)
                throw new ArgumentNullException("parametri ne mogu biti null");
            if (snaga < 0)
                throw new ArgumentException("snaga ne moze biti negativna");

            DateTime datum = (DateTime)dat;
            this.Datum = datum;
            this.Snaga = (double)snaga;

        }
    }
}