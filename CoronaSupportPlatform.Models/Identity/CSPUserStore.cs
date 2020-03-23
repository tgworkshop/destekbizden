using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPUserStore : UserStore<CSPUser, CSPRole, int, CSPUserLogin, CSPUserRole, CSPUserClaim>
    {
        public CSPUserStore(CoronaSupportPlatformDbContext context) : base(context) {}
    }
}
