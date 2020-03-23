using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPRole : IdentityRole<int, CSPUserRole>
    {
        public CSPRole() { }

        public CSPRole(string name) { Name = name; }
    }
}