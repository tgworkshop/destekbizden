using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Notifications
{
    public class TenderNotificationViewModel : BaseNotificationViewModel
    {
        public CSPUser User { get; set; }

        public Organization Organization { get; set; }

        public Tender Tender { get; set; }
    }
}