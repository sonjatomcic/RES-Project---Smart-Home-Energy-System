namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class potrosaciStanjeMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PotrosaciStanjes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Snaga = c.Double(nullable: false),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PotrosaciStanjes");
        }
    }
}
