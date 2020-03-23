using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPSignInManager : SignInManager<CSPUser, int>
    {
        public CSPSignInManager(CSPUserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CSPUser user)
        {
            return user.GenerateUserIdentityAsync((CSPUserManager)UserManager);
        }

        public static CSPSignInManager Create(IdentityFactoryOptions<CSPSignInManager> options, IOwinContext context)
        {
            return new CSPSignInManager(context.GetUserManager<CSPUserManager>(), context.Authentication);
        }   
    }
}
