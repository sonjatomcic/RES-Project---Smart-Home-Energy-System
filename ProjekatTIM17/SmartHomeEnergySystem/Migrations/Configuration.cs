namespace SmartHomeEnergySystem.Migrations
{
    using SmartHomeEnergySystem.SOLARNI_PANEL;
    using SmartHomeEnergySystem.BATERIJA;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SmartHomeEnergySystem.POTROSAC;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartHomeEnergySystem.BAZA.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartHomeEnergySystem.BAZA.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

           

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PanelIzmereneVrednostis]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [IzmereneVrednostiBaterijes]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Potrosacs]");
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ElektrodistribucijaPodacis]");
           context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PotrosaciStanjes]");


          

            context.SolarniPaneli.AddOrUpdate(
                new SolarniPanel() { Ime="Panel_1", MaxSnaga=350},
                new SolarniPanel() { Ime="Panel_2", MaxSnaga=500},
                new SolarniPanel() { Ime="Panel_3", MaxSnaga=400},
                new SolarniPanel() { Ime="Panel_4", MaxSnaga=550},
                new SolarniPanel() { Ime = "Panel_5", MaxSnaga = 500 },
                new SolarniPanel() { Ime = "Panel_6", MaxSnaga = 0 },
                new SolarniPanel() { Ime = "Panel_7", MaxSnaga = 0 }
                );
            context.SaveChanges();

           
           

            context.Baterije.AddOrUpdate(
               new Baterija() { Ime = "Baterija_1", MaxSnaga = 40, Kapacitet = 3 },
               new Baterija() { Ime = "Baterija_2", MaxSnaga = 50, Kapacitet = 4 },
               new Baterija() { Ime = "Baterija_3", MaxSnaga = 30, Kapacitet = 3 },
               new Baterija() { Ime = "Baterija_4", MaxSnaga = 30, Kapacitet = 2 },
               new Baterija() { Ime = "Baterija_5", MaxSnaga = 35, Kapacitet = 3 },
               new Baterija() { Ime = "Baterija_6", MaxSnaga = 50, Kapacitet = 3 }
               );
            context.SaveChanges();

            context.Potrosaci.AddOrUpdate(
              new Potrosac() { Ime = "Pegla", Potrosnja = 1.0, Upaljeno = true },
              new Potrosac() { Ime = "Sporet", Potrosnja = 0.8, Upaljeno = false },
              new Potrosac() { Ime = "VesMasina",Potrosnja=1, Upaljeno=false }
              );
            context.SaveChanges();
        }
    }
}
