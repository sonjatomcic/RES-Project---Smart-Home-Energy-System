using SmartHomeEnergySystem.ELEKTRODISTRIBUCIJA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.COMMON
{
    public interface IElektrodistribucija
    {
        double PosaljiCenu();
        void PreuzmiRazliku(double? razlika, Elektrodistribucija e);
    }
}
