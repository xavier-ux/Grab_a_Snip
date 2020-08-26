namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameChangeMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Codehub", newName: "Hubs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Hubs", newName: "Codehub");
        }
    }
}
