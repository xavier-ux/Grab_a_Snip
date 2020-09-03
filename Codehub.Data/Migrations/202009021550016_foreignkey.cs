namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hubs", "CssId", c => c.Int(nullable: false));
            AddColumn("dbo.Hubs", "BootstrapId", c => c.Int(nullable: false));
            CreateIndex("dbo.Hubs", "CssId");
            CreateIndex("dbo.Hubs", "BootstrapId");
            AddForeignKey("dbo.Hubs", "BootstrapId", "dbo.Bootstrap", "BootstrapId", cascadeDelete: true);
            AddForeignKey("dbo.Hubs", "CssId", "dbo.Css", "CssId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hubs", "CssId", "dbo.Css");
            DropForeignKey("dbo.Hubs", "BootstrapId", "dbo.Bootstrap");
            DropIndex("dbo.Hubs", new[] { "BootstrapId" });
            DropIndex("dbo.Hubs", new[] { "CssId" });
            DropColumn("dbo.Hubs", "BootstrapId");
            DropColumn("dbo.Hubs", "CssId");
        }
    }
}
