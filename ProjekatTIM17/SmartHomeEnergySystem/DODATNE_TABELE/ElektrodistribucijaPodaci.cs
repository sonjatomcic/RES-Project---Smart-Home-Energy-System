using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.DODATNE_TABELE
{
    public class ElektrodistribucijaPodaci
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int SnagaRazmene { get; set; }
        public double Cena { get; set; }

        public ElektrodistribucijaPodaci()
        {

        }

        public ElektrodistribucijaPodaci(DateTime? datum, int? snagaR, double? cena)
        {
            if (datum == null || snagaR == null || cena == null)
                throw new ArgumentNullException("parametri ne mogu biti null");
            if (snagaR != -1 && snagaR != 1)
                throw new ArgumentException("snaga razmene mora biti -1 ili 1");
            if (cena < 0)
                throw new ArgumentException("cena ne moze biti manja od 0");

            DateTime datumm = (DateTime)datum;
            this.Datum = datumm;
            this.SnagaRazmene = (int)snagaR;
            this.Cena = (double)cena;
        }
    }
}
