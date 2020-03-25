using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using CoronaSupportPlatform.UI.Models.ViewModels;
using CoronaSupportPlatform.UI.Models.ViewModels.Notifications;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    public class IdentityController : BaseController
    {
        //private MailgunService _mailgunService = new MailgunService();
        //private SlackService _slackService = new SlackService();

        #region [ Registration ]

        [Route("register")]
        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Add debug log
                LogService.Debug($"Getting the registration page. SessionId:{SessionId}");

                // Create the model
                var model = new RegisterViewModel()
                {
                    CurrentCulture = CurrentCulture,
                };

                // Load the locations
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    model.Organizations = ctx.Organizations.ToList();
                }

                return View(model);
            }
            else
            {
                return RedirectToLocal(null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            // Add debug log
            LogService.Debug($"Getting the registration page (HTTP_POST). SessionId:{SessionId}");

            if (ModelState.IsValid)
            {
                // Create the user object
                var user = new CSPUser
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    UserName = model.Email,
                    Email = model.Email,
                    MobileNumber = model.Mobile,
                    RegistrationNumber = model.RegistrationNumber,
                    Created = DateTime.UtcNow,
                    Status = EntityStatus.Active
                };

                // Set the location and check email
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Check email from db
                    var emailTaken = ctx.Users.Where(et => et.Email == model.Email).Any();

                    if (emailTaken)
                    {
                        // Load the locations
                        model.Organizations = ctx.Organizations.ToList();

                        model.Errors.Add(model.Email + "'a ait bir hesap bulunmakta");
                        return View(model);
                    }
                }

                // Create the user at the user store
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    using (var ctx = new CoronaSupportPlatformDbContext())
                    {
                        // Get the role id
                        var roleId = Convert.ToInt32(model.Occupation);

                        // Get roles
                        var roles = ctx.Roles.ToList();

                        // Assign to role
                        IdentityResult roleAssignmentResponse = UserManager.AddToRole(user.Id, roles.FirstOrDefault(r => r.Id == roleId).Name);

                        // Load the user role
                        var userRole = ctx.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == roleId);
                        userRole.OrganizationId = Convert.ToInt32(model.OrganizationId);
                        userRole.Data = model.Department;
                        ctx.SaveChanges();
                    }

                    // Log in the user
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    #region [ User e-mail validation ]

                    // Get the current domain (it can be development, staging or production)
                    var siteRoot = String.Format("{0}://{1}{2}",
                                            System.Web.HttpContext.Current.Request.Url.Scheme,
                                            System.Web.HttpContext.Current.Request.Url.Host,
                                            System.Web.HttpContext.Current.Request.Url.Port == 80 ? string.Empty : ":" + System.Web.HttpContext.Current.Request.Url.Port);

                    // Calculate the activation code based on the email and user id
                    var activationCode = EncryptMessage(user.Email, user.Id.ToString());

                    //// Sending confirmation email
                    //var activationEmailModel = new RegistrationNotificationViewModel()
                    //{
                    //    ActivationCode = activationCode,
                    //    User = user,
                    //    SiteRoot = siteRoot
                    //};
                    //var activationEmail = ViewToString("~/Views/Templates/Email/RegistrationActivation.cshtml", activationEmailModel);
                    //var activationEmailResponse = _mailgunService.Send(new EmailMessage()
                    //{
                    //    ChannelId = "Mailgun",
                    //    FromName = "FreelanceFrom",
                    //    FromAddress = "no-reply@mailer.freelancefrom.com",
                    //    Subject = "Freelancefrom Bilgilendirme",
                    //    Body = activationEmail,
                    //    IsHtml = true,
                    //    Deliveries = new List<Delivery>()
                    //    {
                    //        new Delivery()
                    //        {
                    //            RecipientType = Common.RecipientType.Primary,
                    //            RecipientName = user.Firstname + " " + user.Lastname,
                    //            RecipientAddress = user.Email
                    //        }
                    //    }
                    //});

                    #endregion

                    #region [ Slack notification ]

                    //try
                    //{
                    //    var slackNotificationResponse = _slackService.SendActivity(new MessageRequest()
                    //    {
                    //        Attachments = new List<SlackAttachment>() {
                    //            new SlackAttachment()
                    //            {
                    //                Color = "#36a64f",
                    //                Title = "Yeni Üye",
                    //                TitleLink = "http://www.freelancefrom.com/users/" + user.Id,
                    //                Text = "\n",
                    //                Fields = new List<SlackField>()
                    //                {
                    //                    new SlackField()
                    //                    {
                    //                        Title = "Ad Soyad",
                    //                        Value = $"{user.Firstname + " " + user.Lastname}\n"
                    //                    },
                    //                    new SlackField()
                    //                    {
                    //                        Title = "E-posta adresi",
                    //                        Value = $"{user.Email}\n"
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    });
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Do nothing
                    //}

                    #endregion

                    // Add debug log
                    LogService.Debug($"User registration complete, now redirecting to home page. SessionId:{SessionId}");

                    return RedirectToAction("Index", "Home");
                }


                AddErrors(result);
            }
            else
            {
                // Add errors
                model.Errors.AddRange(ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage)));
            }

            // Add debug log
            LogService.Debug($"User registration failed, re-opening the registration page. SessionId:{SessionId}");

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                model.Organizations = ctx.Organizations.ToList();
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region [ Login and logout ]

        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Add debug log
                LogService.Debug($"Getting the login page. SessionId:{SessionId}");

                // Create the model
                var model = new LoginViewModel()
                {
                    CurrentCulture = CurrentCulture,
                };

                // Set the return url into the viewbag
                ViewBag.ReturnUrl = returnUrl;

                //LogService.Debug("Login view model created.", "LOGIN");

                return View(model);
            }
            else
            {
                return RedirectToLocal(null);
            }
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // Add debug log
            LogService.Debug($"Getting the login page (HTTP_POST). SessionId:{SessionId}");

            // Get user status from db
            using(var ctx = new CoronaSupportPlatformDbContext())
            {
                var user = ctx.Users.Where(u => u.Email == model.Email && u.Status == EntityStatus.Deleted).Any();

                if (user)
                {
                    model.Errors.Add("Giriş yapmak istediğiniz kullanıcı silinmiştir. Detaylı bilgi için iletişime geçebilirsiniz.");
                    ModelState.AddModelError("", "Deleted credentials!");

                    return View(model);
                }
            }
            

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    // Return success
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.Remember });

                case SignInStatus.Failure:
                default:

                    model.Errors.Add("Lütfen e-posta ve şifrenizi kontrol ediniz!");
                    ModelState.AddModelError("", "Invalid credentials!");

                    return View(model);
            }
        }

        [Route("logoff")]
        public ActionResult LogOff()
        {
            // Add debug log
            LogService.Debug($"Getting the logoff page. SessionId:{SessionId}");

            AuthenticationManager.SignOut();

            // Clear the session values
            Session.Clear();

            return Redirect("/");
        }

        #endregion

        #region [ Password ]

        #region [ Forgot Password ]

        [Route("forgot-password")]
        public ActionResult ForgotPassword()
        {
            // Add debug log
            LogService.Debug($"Getting the forgot password page. SessionId:{SessionId}");

            var model = new ForgotPasswordViewModel();

            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("forgot-password")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // Add debug log
                    LogService.Debug($"Getting the forgot password page (HTTP_POST). SessionId:{SessionId}");

                    // Check the incoming email address
                    var user = UserManager.FindByEmail(model.Email);

                    if (user == null)
                    {
                        model.HasErrors = true;
                        model.Result = "Kayıtlı e-posta bulunamadı!";

                        // Add debug log
                        LogService.Debug($"Could not found the user. Email:{model.Email}; SessionId:{SessionId}");
                    }
                    else
                    {
                        // Add debug log
                        LogService.Debug($"Found the user.UserId:{user.Id}; Email:{user.Email}; SessionId:{SessionId}");

                        // Creat the password reset link
                        var token = UserManager.GeneratePasswordResetToken(user.Id);

                        // Create the reset password notification view model
                        var notificationViewModel = new ResetPasswordNotificationViewModel()
                        {
                            SiteRoot = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "/"),
                            User = user,
                            ResetToken = HttpUtility.UrlEncode(token),
                            SecurityStamp = user.SecurityStamp
                        };

                        #region [ Reset password email ]

                        //var notificationBody = ViewToString("~/Views/Templates/Email/PasswordReset.cshtml", notificationViewModel);
                        //var activationEmailResponse = _mailgunService.Send(new EmailMessage()
                        //{
                        //    ChannelId = "Mailgun",
                        //    FromName = "FreelanceFrom",
                        //    FromAddress = "no-reply@mailer.freelancefrom.com",
                        //    Subject = "Şifrenizi sıfırlayın",
                        //    Body = notificationBody,
                        //    IsHtml = true,
                        //    Deliveries = new List<Delivery>()
                        //{
                        //    new Delivery()
                        //    {
                        //        RecipientType = Common.RecipientType.Primary,
                        //        RecipientName = user.Firstname + " " + user.Lastname,
                        //        RecipientAddress = user.Email
                        //    }
                        //}
                        //});

                        //// Create the notification
                        //var notificationBody = ViewToString("~/Views/Templates/Email/PasswordReset.cshtml", notificationViewModel);

                        //// Send confirmation email
                        //var notificationResponse = notificationServiceClient.SendNotification(new NotificationCommand()
                        //{
                        //    appkey = ConfigurationManager.AppSettings["AppKey"],
                        //    env = ConfigurationManager.AppSettings["Environment"],
                        //    ut = UserToken,
                        //    sid = SessionId,
                        //    uid = UserId,
                        //    t = 1, // Email
                        //    fn = "Zippsi",
                        //    fa = "m@ntf.zippsi.com",
                        //    s = "Şifre güncelleme",
                        //    b = notificationBody,
                        //    rt = 1,
                        //    rcp = new List<RecipientCommand>()
                        //    {
                        //        new RecipientCommand()
                        //        {
                        //            an = user.Firstname + " " + user.Lastname,
                        //            adr = user.Email
                        //        }
                        //    }
                        //});

                        //if (notificationResponse.Type != ServiceResponseTypes.Success)
                        //{
                        //    // Add debug log
                        //    LogService.Debug($"Could not sent password recovery mail sent.UserId:{user.Id}; Email:{user.Email}; SessionId:{SessionId}; Errors:{String.Join(",", notificationResponse.Errors)}");

                        //    model.HasErrors = true;
                        //    model.Result = "";
                        //}

                        //// Add debug log
                        //LogService.Debug($"Password recovery mail sent.UserId:{user.Id}; Email:{user.Email}; SessionId:{SessionId}");

                        #endregion

                        model.Result = "Şifre sıfırlama e-postası gönderildi!";

                    }
                }
            }

            catch (Exception ex)
            {
                LogService.Error("Error to change password", ex.Message, ex.InnerException);
            }
            return View(model);
        }

        #endregion

        #region [ Reset Password ]

        [Route("reset-password")]
        public ActionResult ResetPassword(string rt, string ss)
        {
            // Add debug log
            LogService.Debug($"Getting the reset password page. SessionId:{SessionId}");

            var model = new ResetPasswordViewModel()
            {
                CurrentCulture = CurrentCulture,
                Token = rt,
                SecurityStamp = ss
            };

            if (ss != null)
            {
                // Add debug log
                LogService.Debug($"Getting the user using the security stamp. SessionId:{SessionId}");

                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    var user = ctx.Users.Where(u => u.SecurityStamp == ss).FirstOrDefault();

                    if (user != null)
                    {
                        // Add debug log
                        LogService.Debug($"User found. Email:{user.Email}; SessionId:{SessionId}");
                        // Set the email
                        model.Email = user.Email;
                    }
                }
            }

            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("reset-password")]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            // Add debug log
            LogService.Debug($"Getting the reset password page (HTTP_POST). SessionId:{SessionId}");
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    model.HasErrors = true;
                    model.Result = "Şifreler uyuşmuyor!";

                    return View(model);
                }

                var securityStamp = model.SecurityStamp;
                
                // Correct the token
                var passwordUpdateToken = model.Token;

                // Add debug log
                LogService.Debug($"Password and security stamps parsed. SessionId:{SessionId}");

                if (securityStamp != null)
                {
                    // Add debug log
                    LogService.Debug($"Getting the user using the security stamp. SessionId:{SessionId}");

                    using (var ctx = new CoronaSupportPlatformDbContext())
                    {
                        var user = ctx.Users.Where(u => u.SecurityStamp == securityStamp).FirstOrDefault();

                        if (user != null)
                        {
                            // Add debug log
                            LogService.Debug($"User found. Email:{user.Email}; SessionId:{SessionId}");

                            // Set the context parameters for internal use
                            HttpContext.Items.Add("SessionId", SessionId);
                            HttpContext.Items.Add("UserToken", UserToken);

                            // Change the password
                            var passwordChangeResponse = UserManager.ResetPassword(user.Id, passwordUpdateToken, model.Password);

                            if (passwordChangeResponse.Succeeded)
                            {
                                model.Result = "Şifre başarıyla değiştirildi!";
                                return View(model);
                            }
                        }
                    }
                }
            }


            model.HasErrors = true;
            model.Result = "Şifre değiştirilemedi!";

            return View(model);
        }

        #endregion

        #region [ Password Confirmaiton ]

        [Route("password-confirmation")]
        public ActionResult PasswordConfirmation()
        {
            return View();
        }

        #endregion

        [HttpPost]
        public JsonResult ChangePassword(string password, string newPassword, string newPasswordConfirm)
        {
            // Add debug log
            LogService.Debug($"Getting the change password. SessionId:{SessionId}");

            var ResultList = new List<int>();

            try
            {
                //
                if (newPassword != newPasswordConfirm)
                {
                    ResultList.Add((int)PasswordChange.PasswordsDoNotMatch);
                }
                else
                {
                    string validPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$";

                    var checkMail = new Regex(validPasswordPattern);

                    if (checkMail.Match(newPassword).Success)
                    {
                        var changePassword = UserManager.ChangePassword(CurrentUser.Id, password, newPassword);

                        if (changePassword.Succeeded)
                        {
                            ResultList.Add((int)PasswordChange.Success);
                        }
                        else
                        {
                            ResultList.Add((int)PasswordChange.WrongPassword);
                        }
                    }
                    else
                    {
                        ResultList.Add((int)PasswordChange.FormatNotApplicable);
                    }
                }
                return Json(new { Result = ResultList },
                      JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogService.Error("Error to change password", ex.Message, ex.InnerException);
            }
            ResultList.Add((int)PasswordChange.Error);
            return Json(new { Result = ResultList },
                  JsonRequestBehavior.AllowGet); ;
        }

        #endregion
        
        #region [ Helper methods ]

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Index", "Home"});
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}