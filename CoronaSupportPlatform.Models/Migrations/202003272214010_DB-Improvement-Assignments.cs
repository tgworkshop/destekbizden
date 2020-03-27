namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementAssignments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        TenderItemId = c.Int(),
                        OrganizationId = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .ForeignKey("dbo.TenderItems", t => t.TenderItemId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.TenderItemId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.TenderLog",
                c => new
                    {
                        EntryId = c.Int(nullable: false, identity: true),
                        TenderId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Message = c.String(),
                        Details = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.EntryId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.TenderLog", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Assignments", "TenderItemId", "dbo.TenderItems");
            DropForeignKey("dbo.Assignments", "TenderId", "dbo.Tenders");
            DropIndex("dbo.TenderLog", new[] { "TenderId" });
            DropIndex("dbo.Assignments", new[] { "OrganizationId" });
            DropIndex("dbo.Assignments", new[] { "TenderItemId" });
            DropIndex("dbo.Assignments", new[] { "TenderId" });
            DropTable("dbo.TenderLog");
            DropTable("dbo.Assignments");
        }
    }
}
