namespace SmartHomeEnergySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmereneVrednostiBatDodatoPolje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IzmereneVrednostiBaterijes", "Datum", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IzmereneVrednostiBaterijes", "Datum");
        }
    }
}
