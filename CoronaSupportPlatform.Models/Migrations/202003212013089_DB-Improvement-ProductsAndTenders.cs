namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementProductsAndTenders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenders",
                c => new
                    {
                        TenderId = c.Int(nullable: false, identity: true),
                        RefNumber = c.String(),
                        UserId = c.Int(nullable: false),
                        OrganizationId = c.Int(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TenderId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.TenderItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);
            
            CreateTable(
                "dbo.TenderItemProperties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Extra = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.TenderItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.TenderItemTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Type = c.Short(nullable: false),
                        Value = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.TenderItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.TenderProperties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Extra = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);
            
            CreateTable(
                "dbo.TenderTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        Type = c.Short(nullable: false),
                        Value = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.SystemConfiguration",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        AppKey = c.String(),
                        Class = c.String(),
                        Property = c.String(),
                        Environment = c.String(),
                        Value = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenders", "UserId", "dbo.Users");
            DropForeignKey("dbo.TenderTags", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderProperties", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Tenders", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.TenderItems", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderItemTags", "ItemId", "dbo.TenderItems");
            DropForeignKey("dbo.TenderItemProperties", "ItemId", "dbo.TenderItems");
            DropIndex("dbo.TenderTags", new[] { "TenderId" });
            DropIndex("dbo.TenderProperties", new[] { "TenderId" });
            DropIndex("dbo.TenderItemTags", new[] { "ItemId" });
            DropIndex("dbo.TenderItemProperties", new[] { "ItemId" });
            DropIndex("dbo.TenderItems", new[] { "TenderId" });
            DropIndex("dbo.Tenders", new[] { "OrganizationId" });
            DropIndex("dbo.Tenders", new[] { "UserId" });
            DropTable("dbo.SystemConfiguration");
            DropTable("dbo.Products");
            DropTable("dbo.TenderTags");
            DropTable("dbo.TenderProperties");
            DropTable("dbo.TenderItemTags");
            DropTable("dbo.TenderItemProperties");
            DropTable("dbo.TenderItems");
            DropTable("dbo.Tenders");
        }
    }
}
