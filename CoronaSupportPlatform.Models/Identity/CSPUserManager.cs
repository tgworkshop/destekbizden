using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPUserManager : UserManager<CSPUser, int>
    {
        public CSPUserManager(IUserStore<CSPUser, int> store) : base(store) { }

        public static CSPUserManager Create(IdentityFactoryOptions<CSPUserManager> options, IOwinContext context)
        {
            var manager = new CSPUserManager(new CSPUserStore(CoronaSupportPlatformDbContext.Create()));

            // Configure validation logic for usernames 
            manager.UserValidator = new UserValidator<CSPUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords 
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Register two factor authentication providers. This application uses Phone 
            // and Emails as a step of receiving a code for verifying the user 
            // You can write your own provider and plug in here. 
            manager.RegisterTwoFactorProvider("PhoneCode",
                new PhoneNumberTokenProvider<CSPUser, int>
                {
                    MessageFormat = "Your security code is: {0}"
                });
            manager.RegisterTwoFactorProvider("EmailCode",
                new EmailTokenProvider<CSPUser, int>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is: {0}"
                });

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<CSPUser, int>(dataProtectionProvider.Create("CSPIdentity"));
            }
            return manager;
        }
        
        public string GetUserGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }

}
