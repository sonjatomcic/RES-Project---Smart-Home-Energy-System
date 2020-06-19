
using SmartHomeEnergySystem.SOLARNI_PANEL;
using SmartHomeEnergySystem.BATERIJA;
using SmartHomeEnergySystem.DODATNE_TABELE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeEnergySystem.POTROSAC;

namespace SmartHomeEnergySystem.BAZA
{
    public class ApplicationContext : DbContext
    {
        public DbSet<SolarniPanel> SolarniPaneli { get; set; }
        public DbSet<PanelIzmereneVrednosti> PanelIzmereno { get; set; }
        public DbSet<Baterija> Baterije { get; set; }
        public DbSet<IzmereneVrednostiBaterije> IzmenereVrednostiBaterija { get; set; }
        public DbSet<Potrosac> Potrosaci { get; set; }
        public DbSet<ElektrodistribucijaPodaci> ElektroDistribucijaPodaci { get; set; }
        public DbSet<PotrosaciStanje> stanjePotrosaca { get; set; }

        public ApplicationContext() : base("name=TIM17")
        {

        }
    }
}
