namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodehubBootstrapCode",
                c => new
                    {
                        Codehub_CodehubId = c.Int(nullable: false),
                        BootstrapCode_BootstrapId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Codehub_CodehubId, t.BootstrapCode_BootstrapId })
                .ForeignKey("dbo.Codehub", t => t.Codehub_CodehubId, cascadeDelete: true)
                .ForeignKey("dbo.BootstrapCode", t => t.BootstrapCode_BootstrapId, cascadeDelete: true)
                .Index(t => t.Codehub_CodehubId)
                .Index(t => t.BootstrapCode_BootstrapId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CodehubBootstrapCode", "BootstrapCode_BootstrapId", "dbo.BootstrapCode");
            DropForeignKey("dbo.CodehubBootstrapCode", "Codehub_CodehubId", "dbo.Codehub");
            DropIndex("dbo.CodehubBootstrapCode", new[] { "BootstrapCode_BootstrapId" });
            DropIndex("dbo.CodehubBootstrapCode", new[] { "Codehub_CodehubId" });
            DropTable("dbo.CodehubBootstrapCode");
        }
    }
}
