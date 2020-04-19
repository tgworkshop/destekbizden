using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System.Collections.Generic;
using System.Linq;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Organizations
{
    public class OrganizationViewModel : BaseViewModel
    {
        public int OrganizationId { get; set; }

        public string Name { get; set; }

        public OrganizationType Type { get; set; }
        
        public string District { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Website { get; set; }

        public EntityStatus Status { get; set; }

        #region [ Calculated fields ]

        public string TypeName { 
            get
            {
                switch (Type)
                {
                    case OrganizationType.Hospital:
                        return "Hastane";
                    case OrganizationType.RegionalHealthAdministration:
                        return "Bölge Sağlık İdaresi";
                    case OrganizationType.Supplier:
                        return "Paydaş";
                    case OrganizationType.None:
                    default:
                        return "Girilmemiş";

                }
            }
        }

        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case EntityStatus.Draft:
                        return "Taslak";
                    case EntityStatus.Active:
                        return "Aktif";
                    case EntityStatus.Passive:
                        return "Pasif";
                    case EntityStatus.Unknown:
                    default:
                        return "Girilmemiş";

                }
            }
        }

        #endregion

        #region [ Contact information ]

        public string Phone { get; set; }

        #endregion

        #region [ Navigation properties ]

        public virtual List<CSPUserRole> Users { get; set; } = new List<CSPUserRole>();

        public virtual List<OrganizationProperty> Properties { get; set; } = new List<OrganizationProperty>();

        public virtual List<OrganizationTag> Tags { get; set; } = new List<OrganizationTag>();

        public virtual List<TenderViewModel> Tenders { get; set; } = new List<TenderViewModel>();

        #endregion

        #region [ Converters ]

        public OrganizationViewModel From(Organization organization)
        {
            // Set basic fields
            this.OrganizationId = organization.OrganizationId;
            this.Name = organization.Name;
            this.District = organization.District;
            this.City = organization.City;
            this.ShortDescription = organization.ShortDescription;
            this.LongDescription = organization.LongDescription;
            this.Website = organization.Website;
            this.Phone = organization.Phone;
            this.Type = organization.Type;
            this.Status = organization.Status;

            // Set collections
            this.Users = organization.Users;
            this.Properties = organization.Properties;
            this.Tags = organization.Tags;

            // Set tenders
            this.Tenders = organization.Tenders.Select(t => new TenderViewModel().From(t)).ToList();


            return this;
        }

        #endregion
    }
}