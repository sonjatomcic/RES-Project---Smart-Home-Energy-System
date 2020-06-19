namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmereneVrednostiPanela2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PanelIzmereneVrednostis", "NazivPanela", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PanelIzmereneVrednostis", "NazivPanela");
        }
    }
}
