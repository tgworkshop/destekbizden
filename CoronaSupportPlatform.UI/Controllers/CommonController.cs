using CoronaSupportPlatform.UI.Models.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    public class CommonController : BaseController
    {
        public ActionResult Header()
        {
            var model = new HeaderViewModel()
            {
                CurrentCulture = CurrentCulture,
                CurrentUser = CurrentUser,
                CurrentOrganization = CurrentOrganization
            };

            // Set the breadcrumb
            var breadcrumb = TempData["Breadcrumb"] as BreadcrumbViewModel;
            model.Breadcrumb = breadcrumb;

            return PartialView(model);
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }

        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}