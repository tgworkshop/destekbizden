using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using System;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Notifications
{
    public class RegistrationNotificationViewModel : BaseNotificationViewModel
    {
        public CSPUser User { get; set; }

        public string ActivationCode { get; set; }
    }
}