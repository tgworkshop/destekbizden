namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementOrganizationTypeAndRegion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Organizations", "Region", c => c.String());
            AddColumn("dbo.Products", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "Updated", c => c.DateTime());
            AddColumn("dbo.Products", "Status", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Status");
            DropColumn("dbo.Products", "Updated");
            DropColumn("dbo.Products", "Created");
            DropColumn("dbo.Organizations", "Region");
            DropColumn("dbo.Organizations", "Type");
        }
    }
}
