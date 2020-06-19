using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA
{
    public class Elektrodistribucija
    {
        public double Cena { get; set; }

        public Elektrodistribucija(double? cena)
        {
            if (cena == null)
                throw new ArgumentNullException("cena ne moze biti null");
            if (cena < 0)
                throw new ArgumentException("cena ne moze biti negativna");

            this.Cena = (double)cena;
        }
    }
}
