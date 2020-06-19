namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class potrosaciMigracija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Potrosacs",
                c => new
                    {
                        Ime = c.String(nullable: false, maxLength: 128),
                        Potrosnja = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Ime);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Potrosacs");
        }
    }
}
