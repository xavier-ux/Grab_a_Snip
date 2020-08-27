namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BootstrapHubSetUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bootstrap",
                c => new
                    {
                        BootstrapId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.BootstrapId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bootstrap");
        }
    }
}
