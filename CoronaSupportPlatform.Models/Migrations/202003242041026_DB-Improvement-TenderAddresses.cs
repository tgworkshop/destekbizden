namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementTenderAddresses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenders", "Address");
        }
    }
}
