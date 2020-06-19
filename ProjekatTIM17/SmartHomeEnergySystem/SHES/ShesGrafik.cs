using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartHomeEnergySystem.SHES
{
    public partial class ShesGrafik : Form
    {
        private double ProizvodnjaSP { get; set; }
        private double EnergijaBaterija { get; set; }
        private double UvozED { get; set; }
        private double PotrosnjaPotrosaca { get; set; }
        private string Datum { get; set; }

        public ShesGrafik(double proizvodnjaSP,double energijaBaterija,double uvozED,double potrosnjaPotrosaca,string datum)
        {

            ProizvodnjaSP = proizvodnjaSP;
            EnergijaBaterija = energijaBaterija;
            UvozED = uvozED;
            PotrosnjaPotrosaca = potrosnjaPotrosaca;
            Datum = datum;
            
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void ShesGrafik_Load(object sender, EventArgs e)
        {
           
            chart1.Series["Proizvodnja solarnih panela"].Points.AddY(ProizvodnjaSP);
          //  chart1.Series["Shes"].Points[0].Color = System.Drawing.Color.DeepPink;

            chart1.Series["Energija baterije"].Points.AddY(EnergijaBaterija);
        //    chart1.Series["Shes2"].Points[1].Color = System.Drawing.Color.HotPink;

           

            chart1.Series["Uvoz iz elektrodistribucije"].Points.AddY(UvozED);
            chart1.Series["Potrosnja potrosaca"].Points.AddY(PotrosnjaPotrosaca);
        }

       
    }
}
