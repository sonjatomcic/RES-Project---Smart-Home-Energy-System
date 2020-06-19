using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.COMMON
{
    public interface IBaterija
    {
        void PreuzmiRezim(int? rezim);
        void RukovanjeKapacitetom();
        void RukovanjeKapacitetom2(ShesMetode shes);
    }
}
