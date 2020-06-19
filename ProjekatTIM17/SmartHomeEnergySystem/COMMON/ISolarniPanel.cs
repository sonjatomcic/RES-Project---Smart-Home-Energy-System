using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.COMMON
{
    public interface ISolarniPanel
    {
        void UcitajSnaguSunca();
        bool ValidacijaSnageSunca(string snaga);
        void IzmeriSnagePanela();
        int PosaljiTrenutnuSnaguSunca();
        
    }
}
