namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovementLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Path = c.String(),
                        Name = c.String(),
                        Fullname = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            AddColumn("dbo.Users", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Location");
            DropTable("dbo.Locations");
        }
    }
}
