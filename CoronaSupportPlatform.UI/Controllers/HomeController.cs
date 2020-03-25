using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoronaSupportPlatform.UI.Controllers
{
    [Authorize(Roles = "Administrator,SupplierUser,OrganizationAdminstrator,OrganizationUser,Doctor")]
    public class HomeController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {            
            base.Initialize(requestContext);

            // Check user status
            if (CurrentUser != null && CurrentUser.Status == Common.EntityStatus.Draft)
            {
                Response.Redirect("/not-authorized");
            }
        }

        [Route("")]
        public ActionResult Index()
        {
            var model = new HomeViewModel()
            {
                CurrentCulture = CurrentCulture,
                CurrentUser = CurrentUser,
                CurrentOrganization = CurrentOrganization
            };

            #region [ Load tenders ]

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                if (User.IsInRole("Administrator"))
                {
                    // Load all tenders if administrator
                    var tenders = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").Include("Properties").Include("Tags").ToList();
                    model.Tenders = tenders.Select(t => new TenderViewModel().From(t)).ToList();
                }
                else
                {
                    // Load only the tenders for the current user
                    var tenders = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").Include("Properties").Include("Items").Where(u => u.UserId == CurrentUser.Id).ToList();
                    model.Tenders = tenders.Select(t => new TenderViewModel().From(t)).ToList();
                }
            }

            #endregion

            return View(model);
        }
    }
}