namespace CoronaSupportPlatform.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(),
                        Data = c.String(),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        District = c.String(),
                        City = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        Website = c.String(),
                        Phone = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.OrganizationProperties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Extra = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.OrganizationTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        Type = c.Short(nullable: false),
                        Value = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Gender = c.String(),
                        Birthdate = c.DateTime(),
                        MobileNumber = c.String(),
                        MobileNumberConfirmed = c.Boolean(),
                        RegistrationNumber = c.String(),
                        Status = c.Short(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProperties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                        Extra = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Type = c.Short(nullable: false),
                        Value = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserTags", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProperties", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationTags", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationProperties", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.UserTags", new[] { "UserId" });
            DropIndex("dbo.UserProperties", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.OrganizationTags", new[] { "OrganizationId" });
            DropIndex("dbo.OrganizationProperties", new[] { "OrganizationId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "OrganizationId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropTable("dbo.UserTags");
            DropTable("dbo.UserProperties");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.OrganizationTags");
            DropTable("dbo.OrganizationProperties");
            DropTable("dbo.Organizations");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.UserLogins");
        }
    }
}
