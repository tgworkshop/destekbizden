using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPRoleStore : RoleStore<CSPRole, int, CSPUserRole>
    {
        public CSPRoleStore(CoronaSupportPlatformDbContext context) : base(context) { }
    }
}
