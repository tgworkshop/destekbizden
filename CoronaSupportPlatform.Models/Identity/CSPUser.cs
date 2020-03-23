using CoronaSupportPlatform.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPUser : IdentityUser<int, CSPUserLogin, CSPUserRole, CSPUserClaim>
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public string MobileNumber { get; set; }

        public bool? MobileNumberConfirmed { get; set; }

        public string RegistrationNumber { get; set; }

        public string Location { get; set; }

        public EntityStatus Status { get; set; }

        public DateTime Created { get; set; }

        #region [ Navigation properties ]

        public virtual List<UserProperty> Properties { get; set; } = new List<UserProperty>();

        public virtual List<UserTag> Tags { get; set; } = new List<UserTag>();

        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();

        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CSPUser, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}
