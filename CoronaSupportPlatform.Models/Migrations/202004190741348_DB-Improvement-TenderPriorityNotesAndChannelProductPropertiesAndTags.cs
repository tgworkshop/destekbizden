namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementTenderPriorityNotesAndChannelProductPropertiesAndTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Extra = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Type = c.Short(nullable: false),
                        Value = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.TenderNotes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);
            
            AddColumn("dbo.Organizations", "ContactPerson", c => c.String());
            AddColumn("dbo.Organizations", "ContactPhone", c => c.String());
            AddColumn("dbo.Tenders", "Channel", c => c.Int(nullable: false));
            AddColumn("dbo.Tenders", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "SortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TenderNotes", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.ProductTags", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products");
            DropIndex("dbo.TenderNotes", new[] { "TenderId" });
            DropIndex("dbo.ProductTags", new[] { "ProductId" });
            DropIndex("dbo.ProductProperties", new[] { "ProductId" });
            DropColumn("dbo.Products", "SortOrder");
            DropColumn("dbo.Tenders", "Priority");
            DropColumn("dbo.Tenders", "Channel");
            DropColumn("dbo.Organizations", "ContactPhone");
            DropColumn("dbo.Organizations", "ContactPerson");
            DropTable("dbo.TenderNotes");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductProperties");
        }
    }
}
