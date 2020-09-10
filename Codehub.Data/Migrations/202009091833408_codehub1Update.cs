namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codehub1Update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CodeHub", newName: "CodeHub1");
            RenameTable(name: "dbo.CodeHubBootstrapCode", newName: "CodeHub1BootstrapCode");
            RenameTable(name: "dbo.CssCodeCodeHub", newName: "CssCodeCodeHub1");
            RenameColumn(table: "dbo.CodeHub1BootstrapCode", name: "CodeHub_CodehubId", newName: "CodeHub1_CodehubId");
            RenameColumn(table: "dbo.CssCodeCodeHub1", name: "CodeHub_CodehubId", newName: "CodeHub1_CodehubId");
            RenameIndex(table: "dbo.CodeHub1BootstrapCode", name: "IX_CodeHub_CodehubId", newName: "IX_CodeHub1_CodehubId");
            RenameIndex(table: "dbo.CssCodeCodeHub1", name: "IX_CodeHub_CodehubId", newName: "IX_CodeHub1_CodehubId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CssCodeCodeHub1", name: "IX_CodeHub1_CodehubId", newName: "IX_CodeHub_CodehubId");
            RenameIndex(table: "dbo.CodeHub1BootstrapCode", name: "IX_CodeHub1_CodehubId", newName: "IX_CodeHub_CodehubId");
            RenameColumn(table: "dbo.CssCodeCodeHub1", name: "CodeHub1_CodehubId", newName: "CodeHub_CodehubId");
            RenameColumn(table: "dbo.CodeHub1BootstrapCode", name: "CodeHub1_CodehubId", newName: "CodeHub_CodehubId");
            RenameTable(name: "dbo.CssCodeCodeHub1", newName: "CssCodeCodeHub");
            RenameTable(name: "dbo.CodeHub1BootstrapCode", newName: "CodeHubBootstrapCode");
            RenameTable(name: "dbo.CodeHub1", newName: "CodeHub");
        }
    }
}
