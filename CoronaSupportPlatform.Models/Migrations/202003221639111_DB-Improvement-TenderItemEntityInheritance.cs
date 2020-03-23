namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementTenderItemEntityInheritance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TenderItems", "State", c => c.Int(nullable: false));
            AddColumn("dbo.TenderItems", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.TenderItems", "Updated", c => c.DateTime());
            AddColumn("dbo.TenderItems", "Status", c => c.Short(nullable: false));
            CreateIndex("dbo.TenderItems", "ProductId");
            AddForeignKey("dbo.TenderItems", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TenderItems", "ProductId", "dbo.Products");
            DropIndex("dbo.TenderItems", new[] { "ProductId" });
            DropColumn("dbo.TenderItems", "Status");
            DropColumn("dbo.TenderItems", "Updated");
            DropColumn("dbo.TenderItems", "Created");
            DropColumn("dbo.TenderItems", "State");
        }
    }
}
