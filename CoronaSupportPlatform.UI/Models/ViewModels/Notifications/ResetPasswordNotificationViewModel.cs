using CoronaSupportPlatform.Models.Identity;

namespace CoronaSupportPlatform.UI.Models.ViewModels.Notifications
{
    public class ResetPasswordNotificationViewModel : BaseNotificationViewModel
    {
        public CSPUser User { get; set; }

        public string ResetToken { get; set; }

        public string SecurityStamp { get; set; }
    }
}