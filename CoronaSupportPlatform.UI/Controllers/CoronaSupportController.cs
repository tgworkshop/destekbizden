using CoronaSupportPlatform.Common.Response;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("cs")]
    public class CoronaSupportController : BaseController
    {
        private MailService mailService = new MailService();

        [Route("test/emails/tendernotification")]
        public JsonResult SendTenderConfirmationMailTest()
        {
            var notificationModel = new TenderNotificationViewModel();

            // Create the notification email body
            var notificationBody = ViewToString("Tender", "~/Views/Templates/Email/TenderConfirmation.cshtml", notificationModel);

            // Create the recipient list
            var recipients = new Dictionary<string, string>();
            recipients.Add(CurrentUser.Email, CurrentUser.Firstname + " " + CurrentUser.Lastname);

            // Send the email
            mailService.SendEmail("DestekBizden - Talebiniz bize ulaştı!", "mailer@destekbizden.org", "DestekBizden", recipients, notificationBody);

            return Json(new { ResponseCode = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}