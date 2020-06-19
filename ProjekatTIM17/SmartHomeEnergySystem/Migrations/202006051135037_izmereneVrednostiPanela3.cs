namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmereneVrednostiPanela3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PanelIzmereneVrednostis", "Panel_Ime", c => c.String(maxLength: 128));
            CreateIndex("dbo.PanelIzmereneVrednostis", "Panel_Ime");
            AddForeignKey("dbo.PanelIzmereneVrednostis", "Panel_Ime", "dbo.SolarniPanels", "Ime");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PanelIzmereneVrednostis", "Panel_Ime", "dbo.SolarniPanels");
            DropIndex("dbo.PanelIzmereneVrednostis", new[] { "Panel_Ime" });
            DropColumn("dbo.PanelIzmereneVrednostis", "Panel_Ime");
        }
    }
}
