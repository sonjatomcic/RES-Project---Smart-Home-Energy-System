namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmerenevrednostibaterija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IzmereneVrednostiBaterijes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazivBaterije = c.String(),
                        Kapacitet = c.Double(nullable: false),
                        Rezim = c.Int(nullable: false),
                        Baterija_Ime = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baterijas", t => t.Baterija_Ime)
                .Index(t => t.Baterija_Ime);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IzmereneVrednostiBaterijes", "Baterija_Ime", "dbo.Baterijas");
            DropIndex("dbo.IzmereneVrednostiBaterijes", new[] { "Baterija_Ime" });
            DropTable("dbo.IzmereneVrednostiBaterijes");
        }
    }
}
