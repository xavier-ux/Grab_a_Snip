namespace Codehub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedstar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Css", "IsStarred");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Css", "IsStarred", c => c.Boolean(nullable: false));
        }
    }
}
