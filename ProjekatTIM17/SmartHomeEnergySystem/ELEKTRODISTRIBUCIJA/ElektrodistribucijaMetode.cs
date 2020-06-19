using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA
{
    public class ElektrodistribucijaMetode : IElektrodistribucija
    {
       private double cena { get; set; }
        private double razlika { get; set; }
        private Object elLock = new object();

        public double PosaljiCenu()
        {
            lock (elLock)
            {
                return Math.Abs(razlika) * cena;    //ukupna cena = razlika * cena
            }
        }

        public void PreuzmiRazliku(double? razlika, Elektrodistribucija e)
        {
            if (razlika == null || e == null)
                throw new ArgumentNullException("parametri ne mogu biti null");

            this.razlika = (double)razlika;
            this.cena = e.Cena;
        }
    }
}
