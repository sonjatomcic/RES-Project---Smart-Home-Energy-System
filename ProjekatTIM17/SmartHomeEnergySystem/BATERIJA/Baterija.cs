using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.BATERIJA
{
    public class Baterija
    {
        [Key]
        public string Ime { get; set; }
        public int MaxSnaga { get; set; }
        public double Kapacitet { get; set; }

        public Baterija() { }

        public Baterija(string ime, int ? maxSnaga, double ? kapacitet)
        {
            if (ime == null || maxSnaga==null || kapacitet==null)
            {
                throw new ArgumentNullException("Parameti ne mogu da budu NULL!");
            }

            if (ime.Trim() == "")
            {
                throw new ArgumentException("Ime ne sme biti prazno polje.");
            }

            if (kapacitet < 0)
            {
                throw new ArgumentException("Kapacitet ne sme imati negativnu vrednost!");
            }

            if (maxSnaga < 0)
            {
                throw new ArgumentException("MaxSnaga ne sme imati negativnu vrednost!");
            }

            Ime = ime;
            MaxSnaga =(int)maxSnaga;
            Kapacitet =(double)kapacitet;
        }
    }
}
