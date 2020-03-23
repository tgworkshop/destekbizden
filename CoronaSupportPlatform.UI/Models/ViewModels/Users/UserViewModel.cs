using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Users
{
    public class UserViewModel : BaseViewModel
    {
        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int? OccupationId { get; set; }

        public string Occupation { get; set; }

        public int? OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public EntityStatus Status { get; set; }

        public List<Organization> Organizations { get; set; } = new List<Organization>();

        #region [ Converters ]

        public UserViewModel From(CSPUser user, List<CSPRole> roleList)
        {
            // Set the basic fields
            this.UserId = user.Id;
            this.Firstname = user.Firstname;
            this.Lastname = user.Lastname;
            this.Email = user.Email;
            this.Mobile = user.MobileNumber;
            this.Status = user.Status;

            // Set the organization
            this.OrganizationId = user.Roles?.FirstOrDefault().OrganizationId;
            this.OrganizationName = user.Roles?.FirstOrDefault().Organization?.Name;

            // Set the role
            this.OccupationId = user.Roles?.FirstOrDefault().RoleId;
            this.Occupation = roleList.FirstOrDefault(r => r.Id == user.Roles?.FirstOrDefault().RoleId)?.Name;

            return this;
        }

        #endregion
    }
}