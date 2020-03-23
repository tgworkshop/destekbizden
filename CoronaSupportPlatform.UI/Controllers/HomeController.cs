using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    [Authorize(Roles = "Administrator,SupplierUser,OrganizationUser")]
    public class HomeController : BaseController
    {
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
                    var tenders = ctx.Tenders.Include("Organization").Include("Items.Product").Include("Properties").Include("Tags").ToList();
                    model.Tenders = tenders.Select(t => new TenderViewModel().From(t)).ToList();
                }
                else
                {
                    // Load only the tenders for the current user
                    var tenders = ctx.Tenders.Include("Organization").Include("Items.Product").Include("Properties").Include("Items").Where(u => u.UserId == CurrentUser.Id).ToList();
                    model.Tenders = tenders.Select(t => new TenderViewModel().From(t)).ToList();
                }
            }

            #endregion

            return View(model);
        }
    }
}