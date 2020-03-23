using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoronaSupportPlatform.Models
{
    public class Organization : Entity
    {
        [Key]
        public int OrganizationId { get; set; }

        public string Name { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Website { get; set; }

        #region [ Contact information ]

        public string Phone { get; set; }

        #endregion

        #region [ Navigation properties ]

        public virtual List<CSPUserRole> Users { get; set; } = new List<CSPUserRole>();

        public virtual List<OrganizationProperty> Properties { get; set; } = new List<OrganizationProperty>();

        public virtual List<OrganizationTag> Tags { get; set; } = new List<OrganizationTag>();

        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();

        #endregion
    }
}
