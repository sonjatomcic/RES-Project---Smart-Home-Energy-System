namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PotrosacDva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Potrosacs", "Upaljeno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Potrosacs", "Upaljeno");
        }
    }
}
