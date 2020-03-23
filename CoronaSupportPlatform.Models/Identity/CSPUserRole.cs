using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPUserRole : IdentityUserRole<int> {

        public int UserRoleId { get; set; }

        public int? OrganizationId { get; set; }

        public string Data { get; set; }

        #region [ Navigation properties ]

        [ForeignKey("UserId")]
        public CSPUser User { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        #endregion
    } 
}