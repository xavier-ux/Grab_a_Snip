namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thursmigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bootstrap", newName: "BootstrapCode");
            RenameTable(name: "dbo.Css", newName: "CssCode");
            DropForeignKey("dbo.Hubs", "BootstrapId", "dbo.Bootstrap");
            DropForeignKey("dbo.Hubs", "CssId", "dbo.Css");
            DropIndex("dbo.Hubs", new[] { "CssId" });
            DropIndex("dbo.Hubs", new[] { "BootstrapId" });
            CreateTable(
                "dbo.Codehub",
                c => new
                    {
                        CodehubId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CodehubId);
            
            CreateTable(
                "dbo.CssCodeCodehub",
                c => new
                    {
                        CssCode_CssId = c.Int(nullable: false),
                        Codehub_CodehubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CssCode_CssId, t.Codehub_CodehubId })
                .ForeignKey("dbo.CssCode", t => t.CssCode_CssId, cascadeDelete: true)
                .ForeignKey("dbo.Codehub", t => t.Codehub_CodehubId, cascadeDelete: true)
                .Index(t => t.CssCode_CssId)
                .Index(t => t.Codehub_CodehubId);
            
            DropTable("dbo.Hubs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Hubs",
                c => new
                    {
                        CodehubId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                        CssId = c.Int(nullable: false),
                        BootstrapId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodehubId);
            
            DropForeignKey("dbo.CssCodeCodehub", "Codehub_CodehubId", "dbo.Codehub");
            DropForeignKey("dbo.CssCodeCodehub", "CssCode_CssId", "dbo.CssCode");
            DropIndex("dbo.CssCodeCodehub", new[] { "Codehub_CodehubId" });
            DropIndex("dbo.CssCodeCodehub", new[] { "CssCode_CssId" });
            DropTable("dbo.CssCodeCodehub");
            DropTable("dbo.Codehub");
            CreateIndex("dbo.Hubs", "BootstrapId");
            CreateIndex("dbo.Hubs", "CssId");
            AddForeignKey("dbo.Hubs", "CssId", "dbo.Css", "CssId", cascadeDelete: true);
            AddForeignKey("dbo.Hubs", "BootstrapId", "dbo.Bootstrap", "BootstrapId", cascadeDelete: true);
            RenameTable(name: "dbo.CssCode", newName: "Css");
            RenameTable(name: "dbo.BootstrapCode", newName: "Bootstrap");
        }
    }
}
