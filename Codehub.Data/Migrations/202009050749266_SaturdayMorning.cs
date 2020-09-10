namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaturdayMorning : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CodeHubBootstrapCode", new[] { "Codehub_CodehubId" });
            DropIndex("dbo.CssCodeCodeHub", new[] { "Codehub_CodehubId" });
            CreateIndex("dbo.CodeHubBootstrapCode", "CodeHub_CodehubId");
            CreateIndex("dbo.CssCodeCodeHub", "CodeHub_CodehubId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CssCodeCodeHub", new[] { "CodeHub_CodehubId" });
            DropIndex("dbo.CodeHubBootstrapCode", new[] { "CodeHub_CodehubId" });
            CreateIndex("dbo.CssCodeCodeHub", "Codehub_CodehubId");
            CreateIndex("dbo.CodeHubBootstrapCode", "Codehub_CodehubId");
        }
    }
}
