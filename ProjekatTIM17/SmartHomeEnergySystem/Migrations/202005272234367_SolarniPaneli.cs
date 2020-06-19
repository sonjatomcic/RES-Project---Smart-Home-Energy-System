namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolarniPaneli : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolarniPanels",
                c => new
                    {
                        Ime = c.String(nullable: false, maxLength: 128),
                        MaxSnaga = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Ime);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SolarniPanels");
        }
    }
}
