
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels.Common;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("tenders"), Authorize(Roles = "Administrator,SupplierUser,OrganizationAdminstrator,OrganizationUser,Doctor")]
    public class TendersController : BaseController
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
            var model = new TenderListViewModel();

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

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "İhtiyaç Listesi";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("İhtiyaç Listesi", "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [Route("new")]
        public ActionResult New()
        {
            // Create the model
            var model = new TenderViewModel()
            {
                CurrentUser = CurrentUser,
                CurrentOrganization = CurrentOrganization,
            };

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Get the products
                var products = ctx.Products.ToList();
                model.Products = products;

                // Get the organization Id
                var organizationId = CurrentUser.Roles.FirstOrDefault().OrganizationId;

                // Get the organization
                var organization = ctx.Organizations.FirstOrDefault(o => o.OrganizationId == organizationId);
                model.Organization = organization;
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Yeni İhtiyaç";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
            breadcrumb.Items.Add("Yeni İhtiyaç", "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [HttpPost, Route("new")]
        public ActionResult New(TenderViewModel model)
        {
            try
            {
                #region [ Basic values ]

                model.CurrentUser = CurrentUser;
                model.CurrentOrganization = CurrentOrganization;

                #endregion

                #region [ Load the create page data  ]

                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the products
                    var products = ctx.Products.ToList();
                    model.Products = products;

                    // Get the organization Id
                    var organizationId = CurrentUser.Roles.FirstOrDefault().OrganizationId;

                    // Get the organization
                    var organization = ctx.Organizations.FirstOrDefault(o => o.OrganizationId == organizationId);
                    model.Organization = organization;
                }

                #endregion

                #region [ Breadcrumb ]

                var breadcrumb = new BreadcrumbViewModel();
                breadcrumb.PageName = "Yeni İhtiyaç";
                breadcrumb.Items.Add("Anasayfa", "/");
                breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
                breadcrumb.Items.Add("Yeni İhtiyaç", "");
                TempData["Breadcrumb"] = breadcrumb;

                #endregion

                if (ModelState.IsValid)
                {
                    // Create the tender items
                    var tenderItems = new List<TenderItem>();

                    for (int i = 0; i < model.Products.Count; i++)
                    {
                        // Get the current product
                        var product = model.Products[i];

                        // Check for quantity
                        var quantity = Convert.ToInt32(Request.Form[product.ProductId + "_Quantity"]);

                        // Check for a positive quantity
                        if (quantity == 0) continue;

                        tenderItems.Add(new TenderItem()
                        {
                            ProductId = product.ProductId,
                            Quantity = quantity,
                            State = TenderItemState.New,
                            Created = DateTime.UtcNow
                        });
                    }

                    // Check for product quantities
                    if (CurrentOrganization.OrganizationId == 1063) // Organization selection for individuals
                    {
                        // Check for limits
                        if (tenderItems.Any(ti => ti.Quantity > 5))
                        {
                            model.Errors.Add("Bireysel başvuru yapıyor olmanıza rağmen bazı ürünleri 5 adetten fazla talep ettiniz!");
                        }
                    }

                    // Check for 0 tender
                    if (tenderItems.Sum(ti => ti.Quantity) == 0)
                    {
                        model.Errors.Add("Hiç bir ürün için adet girmediniz!");
                    }

                    if (!model.Errors.Any())
                    {
                        using (var ctx = new CoronaSupportPlatformDbContext())
                        {
                            // Create new tender object
                            var tender = new Tender()
                            {
                                OrganizationId = model.Organization.OrganizationId,
                                ShortDescription = model.ShortDescription,
                                LongDescription = model.LongDescription,
                                Address = model.Address,
                                UserId = CurrentUser.Id,
                                Created = DateTime.UtcNow,
                                State = TenderState.New,
                                Status = Common.EntityStatus.Active,
                            };

                            // Add the tender items to tender
                            tender.Items = tenderItems;

                            // Add the tender
                            ctx.Tenders.Add(tender);
                            ctx.SaveChanges();
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    // Add model state errors
                    model.Errors.AddRange(ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage)));

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                LogService.Debug(ex, $"There is an error while creating tender");

                return View(model);
            }

            return Redirect("/Tenders");
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            var model = new TenderViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            try
            {
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the tender
                    var tender = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").FirstOrDefault(t => t.TenderId == id);

                    #region [ Authorization ]

                    if (!User.IsInRole("Administrator"))
                    {
                        // Check the owner
                        if (tender.UserId != UserId)
                        {
                            Response.Redirect("/not-authorized");
                        }
                    }

                    #endregion

                    // Load the model
                    model.From(tender);

                    #region [ Breadcrumb ]

                    var breadcrumb = new BreadcrumbViewModel();
                    breadcrumb.PageName = "İhtiyaç Detay";
                    breadcrumb.Items.Add("Anasayfa", "/");
                    breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
                    breadcrumb.Items.Add(tender.ShortDescription, "");
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

        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            var model = new TenderViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            try
            {
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the tender
                    var tender = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").FirstOrDefault(t => t.TenderId == id);

                    #region [ Authorization ]

                    if (!User.IsInRole("Administrator"))
                    {
                        // Check the owner
                        if (tender.UserId != UserId)
                        {
                            Response.Redirect("/not-authorized");
                        }
                    }

                    #endregion

                    // Fill the model
                    model.From(tender);

                    #region [ Breadcrumb ]

                    var breadcrumb = new BreadcrumbViewModel();
                    breadcrumb.PageName = "İhtiyaç Detay";
                    breadcrumb.Items.Add("Anasayfa", "/");
                    breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
                    breadcrumb.Items.Add(tender.ShortDescription, "");
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

        [HttpPost,Route("{id}/edit")]
        public ActionResult Edit(int id, TenderViewModel model)
        {
            try
            {
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the tender
                    var tender = ctx.Tenders.Include("User.Roles").Include("Organization").Include("Items.Product").FirstOrDefault(t => t.TenderId == id);

                    #region [ Authorization ]

                    if (!User.IsInRole("Administrator"))
                    {
                        // Check the owner
                        if (tender.UserId != UserId)
                        {
                            Response.Redirect("/not-authorized");
                        }
                    }

                    #endregion

                    // Update the tender
                    tender.Address = model.Address;
                    tender.LongDescription = model.LongDescription;

                    // Update tender items
                    foreach(var item in tender.Items)
                    {
                        // Get the related quantity
                        var updatedQuantity = Convert.ToInt32(Request.Form[item.ItemId + "_Quantity"]);
                        item.Quantity = updatedQuantity;
                    }

                    // Save changes
                    ctx.SaveChanges();

                    // Fill the model
                    model.From(tender);

                    // Set the update flag
                    model.IsUpdated = true;

                    #region [ Breadcrumb ]

                    var breadcrumb = new BreadcrumbViewModel();
                    breadcrumb.PageName = "İhtiyaç Güncelle";
                    breadcrumb.Items.Add("Anasayfa", "/");
                    breadcrumb.Items.Add("İhtiyaç Listesi", "/Tenders");
                    breadcrumb.Items.Add(tender.ShortDescription, "");
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
    }
}