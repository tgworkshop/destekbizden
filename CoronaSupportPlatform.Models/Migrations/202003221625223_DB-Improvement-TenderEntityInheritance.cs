namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementTenderEntityInheritance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tenders", "Updated", c => c.DateTime());
            AddColumn("dbo.Tenders", "Status", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenders", "Status");
            DropColumn("dbo.Tenders", "Updated");
            DropColumn("dbo.Tenders", "Created");
        }
    }
}
