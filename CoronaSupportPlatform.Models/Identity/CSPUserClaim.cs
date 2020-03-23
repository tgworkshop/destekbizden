using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoronaSupportPlatform.Models.Identity
{
    public class CSPUserClaim : IdentityUserClaim<int> { }
}