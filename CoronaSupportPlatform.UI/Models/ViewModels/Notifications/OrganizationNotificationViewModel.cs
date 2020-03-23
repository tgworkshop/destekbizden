using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Notifications
{
    public class OrganizationNotificationViewModel : BaseNotificationViewModel
    {
        public CSPUser User { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}