namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PanelIzmereneVrednosti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PanelIzmereneVrednostis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        IzmerenaSnaga = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PanelIzmereneVrednostis");
        }
    }
}
