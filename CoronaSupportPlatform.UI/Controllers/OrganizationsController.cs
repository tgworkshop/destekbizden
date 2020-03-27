
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels.Common;
using CoronaSupportPlatform.UI.Models.ViewModels.Organizations;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("organizations"), Authorize(Roles = "Administrator")]
    public class OrganizationsController : BaseController
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
            // Create the model
            var model = new OrganizationListViewModel();

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Load organizaitons
                var organizations = ctx.Organizations.Include("Properties")
                                                     .Include("Tags")
                                                     .ToList();

                model.Organizations = organizations.Select(o => new OrganizationViewModel().From(o)).ToList();
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Hastane Listesi";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Hastane Listesi", "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [Route("new")]
        public ActionResult New()
        {
            // Create the model
            var model = new OrganizationViewModel()
            {
                CurrentUser = CurrentUser,
                CurrentOrganization = CurrentOrganization,
            };

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Yeni Hastane";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Hastane Listesi", "/Organizations");
            breadcrumb.Items.Add("Yeni Hastane", "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [HttpPost, Route("new")]
        public ActionResult New(OrganizationViewModel model)
        {
            try
            {
                #region [ Load the create page data  ]

                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    
                }

                #endregion

                #region [ Breadcrumb ]

                var breadcrumb = new BreadcrumbViewModel();
                breadcrumb.PageName = "Yeni Hastane";
                breadcrumb.Items.Add("Anasayfa", "/");
                breadcrumb.Items.Add("Hastane Listesi", "/Organizations");
                breadcrumb.Items.Add("Yeni Hastane", "");
                TempData["Breadcrumb"] = breadcrumb;

                #endregion
            }
            catch (Exception ex)
            {
                LogService.Debug(ex, $"There is an error while creating tender");

                return View(model);
            }

            return Redirect("/Organizations");
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            var model = new OrganizationViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            try
            {
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the organization
                    var organization = ctx.Organizations.Include("Tenders.User.Roles")
                                                         .Include("Tenders.Organization")
                                                         .Include("Tenders.Items.Product")
                                                         .Include("Properties")
                                                         .Include("Tags")
                                                         .FirstOrDefault(o => o.OrganizationId == id);

                    #region [ Authorization ]

                    if (!User.IsInRole("Administrator"))
                    {
                        // Check the owner
                        if (!organization.Users.Any(u => u.UserId == UserId))
                        {
                            Response.Redirect("/not-authorized");
                        }
                    }

                    #endregion

                    // Load the model
                    model.From(organization);

                    #region [ Breadcrumb ]

                    var breadcrumb = new BreadcrumbViewModel();
                    breadcrumb.PageName = "Organizasyon Detay";
                    breadcrumb.Items.Add("Anasayfa", "/");
                    breadcrumb.Items.Add("Organizasyon Listesi", "/Organizations");
                    breadcrumb.Items.Add(organization.Name, "");
                    TempData["Breadcrumb"] = breadcrumb;

                    #endregion
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(model);

        }

        //[Route("{id}/edit")]
        //public ActionResult Edit(int id)
        //{
        //    var model = new TenderViewModel()
        //    {
        //        CurrentOrganization = CurrentOrganization,
        //        CurrentUser = CurrentUser
        //    };

        //    try
        //    {
        //        using (var ctx = new CoronaSupportPlatformDbContext())
        //        {
        //            // Get the tender
        //            var tender = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").FirstOrDefault(t => t.TenderId == id);

        //            #region [ Authorization ]

        //            if (!User.IsInRole("Administrator"))
        //            {
        //                // Check the owner
        //                if (tender.UserId != UserId)
        //                {
        //                    Response.Redirect("/not-authorized");
        //                }
        //            }

        //            #endregion

        //            // Fill the model
        //            model.From(tender);

        //            #region [ Breadcrumb ]

        //            var breadcrumb = new BreadcrumbViewModel();
        //            breadcrumb.PageName = "İhtiyaç Detay";
        //            breadcrumb.Items.Add("Anasayfa", "/");
        //            breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
        //            breadcrumb.Items.Add(tender.ShortDescription, "");
        //            TempData["Breadcrumb"] = breadcrumb;

        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return View(model);

        //}

        //[HttpPost,Route("{id}/edit")]
        //public ActionResult Edit(int id, TenderViewModel model)
        //{
        //    try
        //    {
        //        using (var ctx = new CoronaSupportPlatformDbContext())
        //        {
        //            // Get the tender
        //            var tender = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").FirstOrDefault(t => t.TenderId == id);

        //            #region [ Authorization ]

        //            if (!User.IsInRole("Administrator"))
        //            {
        //                // Check the owner
        //                if (tender.UserId != UserId)
        //                {
        //                    Response.Redirect("/not-authorized");
        //                }
        //            }

        //            #endregion

        //            // Update the tender
        //            tender.Address = model.Address;
        //            tender.LongDescription = model.LongDescription;

        //            // Update tender items
        //            foreach(var item in tender.Items)
        //            {
        //                // Get the related quantity
        //                var updatedQuantity = Convert.ToInt32(Request.Form[item.ItemId + "_Quantity"]);
        //                item.Quantity = updatedQuantity;
        //            }

        //            // Save changes
        //            ctx.SaveChanges();

        //            // Fill the model
        //            model.From(tender);

        //            // Set the update flag
        //            model.IsUpdated = true;

        //            #region [ Breadcrumb ]

        //            var breadcrumb = new BreadcrumbViewModel();
        //            breadcrumb.PageName = "İhtiyaç Güncelle";
        //            breadcrumb.Items.Add("Anasayfa", "/");
        //            breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
        //            breadcrumb.Items.Add(tender.ShortDescription, "");
        //            TempData["Breadcrumb"] = breadcrumb;

        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return View(model);

        //}
    }
}