using CoronaSupportPlatform.Models.Configuration;
using CoronaSupportPlatform.Models.Identity;
using CoronaSupportPlatform.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models
{
    public class CoronaSupportPlatformDbContext : DbContext
    {
        public CoronaSupportPlatformDbContext() : base("name=MAIN")
        {

            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static CoronaSupportPlatformDbContext Create()
        {
            return new CoronaSupportPlatformDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region [ Identity ]

            modelBuilder.Entity<CSPUserLogin>().Map(c =>
            {
                c.ToTable("UserLogins");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            // Mapping for ApiRole
            modelBuilder.Entity<CSPRole>().Map(c =>
            {
                c.ToTable("Roles");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<CSPRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<CSPUser>().Map(c =>
            {
                c.ToTable("Users");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.MobileNumber,
                    p.MobileNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName,
                    p.Firstname,
                    p.Lastname,
                    p.Gender,
                    p.Birthdate,
                    p.RegistrationNumber,
                    p.Location,
                    p.Status,
                    p.Created
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<CSPUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<CSPUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<CSPUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CSPUserRole>().Map(c =>
            {
                c.ToTable("UserRoles");
                c.Properties(p => new
                {
                    p.UserRoleId,
                    p.UserId,
                    p.RoleId,
                    p.OrganizationId,
                    p.Data
                });
            })
            .HasKey(c => new { c.UserRoleId });

            modelBuilder.Entity<CSPUserClaim>().Map(c =>
            {
                c.ToTable("UserClaims");
                c.Property(p => p.Id).HasColumnName("UserClaimId");
                c.Properties(p => new
                {
                    p.UserId,
                    p.ClaimValue,
                    p.ClaimType
                });
            }).HasKey(c => c.Id);

            #endregion            
        }

        // IDENTITY
        public DbSet<CSPUser> Users { get; set; }

        public DbSet<CSPRole> Roles { get; set; }

        public DbSet<CSPUserRole> UserRoles { get; set; }
        
        // ORGANIZATIONS
        public DbSet<Organization> Organizations { get; set; }

        // PRODUCTS
        public DbSet<Product> Products { get; set; }

        // TENDERS
        public DbSet<Tender> Tenders { get; set; }

        // LOCATIONS
        public DbSet<Location> Locations { get; set; }

        // CONFIG
        public DbSet<ConfigRecord> SystemConfiguration { get; set; }
    }
}
