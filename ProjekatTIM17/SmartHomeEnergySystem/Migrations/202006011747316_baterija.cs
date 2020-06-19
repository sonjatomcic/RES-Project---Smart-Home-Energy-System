namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baterija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baterijas",
                c => new
                    {
                        Ime = c.String(nullable: false, maxLength: 128),
                        MaxSnaga = c.Int(nullable: false),
                        Kapacitet = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Ime);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Baterijas");
        }
    }
}
