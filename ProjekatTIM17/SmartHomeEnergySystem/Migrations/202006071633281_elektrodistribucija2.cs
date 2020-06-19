namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elektrodistribucija2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElektrodistribucijaPodacis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        SnagaRazmene = c.Int(nullable: false),
                        Cena = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ElektrodistribucijaPodacis");
        }
    }
}
