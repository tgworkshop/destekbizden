using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels.Common;
using CoronaSupportPlatform.UI.Models.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("users"), Authorize(Roles = "Administrator")]
    public class UsersController : BaseController
    {
        [Route("")]
        public ActionResult Index()
        {
            var model = new UserListViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Load the roles
                var roles = ctx.Roles.ToList();

                // Load the users
                var users = ctx.Users.Include("Roles.Organization").ToList();

                // Convert
                model.Roles = roles;
                model.Users = users.Select(u => new UserViewModel().From(u, roles)).ToList();
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Kullanıcı Listesi";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Kullanıcı Listesi", "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            var model = new UserViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Load the roles
                var roles = ctx.Roles.ToList();

                // Load the user
                var user = ctx.Users.Include("Roles.Organization").FirstOrDefault(u => u.Id == id);

                // Convert
                model = model.From(user, roles);
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Kullanıcı Detay";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Kullanıcı Listesi", "/Users");
            breadcrumb.Items.Add(model.Firstname + " " + model.Lastname, "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [Route("{id}/edit")]
        public ActionResult Edit(int id)
        {
            var model = new UserViewModel()
            {
                CurrentOrganization = CurrentOrganization,
                CurrentUser = CurrentUser
            };

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Load the roles
                var roles = ctx.Roles.ToList();

                // Load the user
                var user = ctx.Users.Include("Roles.Organization").FirstOrDefault(u => u.Id == id);

                // Convert
                model = model.From(user, roles);

                // Set organizations
                model.Organizations = ctx.Organizations.ToList();
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Kullanıcı Güncelleme";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Kullanıcı Listesi", "/Users");
            breadcrumb.Items.Add(model.Firstname + " " + model.Lastname, "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }

        [HttpPost, Route("{id}/edit")]
        public ActionResult Edit(int id, UserViewModel model)
        {
            #region [ Custom validation ]

            var customErrors = new List<string>();

            // Check for mobile number
            if (String.IsNullOrEmpty(model.Mobile))
            {
                customErrors.Add("Cep telefonu alanı zorunludur.");
            }

            #endregion

            if (ModelState.IsValid && !customErrors.Any())
            {
                using(var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the user
                    var user = ctx.Users.Include("Roles").FirstOrDefault(u => u.Id == model.UserId);

                    // Update the user
                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Email = model.Email;
                    user.MobileNumber = model.Mobile;
                    user.Status = model.Status;

                    // Update the user role
                    var userRole = user.Roles.FirstOrDefault();

                    switch (model.OccupationId.GetValueOrDefault())
                    {
                        case 1:
                            userRole.OrganizationId = (int?)null;
                            userRole.RoleId = 1;
                            break;
                        default:
                            userRole.OrganizationId = model.OrganizationId;
                            userRole.RoleId = model.OccupationId.GetValueOrDefault();
                            break;
                    }

                    // Set the department
                    userRole.Data = model.Department;

                    ctx.SaveChanges();

                    // Redirect to user list page
                    return Redirect("/Users?a=user-updated");
                }
            }
            else
            {
                // Add model state errors
                model.Errors.AddRange(ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage)));

                // Add custom errors
                model.Errors.AddRange(customErrors);
            }

            #region [ Breadcrumb ]

            var breadcrumb = new BreadcrumbViewModel();
            breadcrumb.PageName = "Kullanıcı Güncelleme";
            breadcrumb.Items.Add("Anasayfa", "/");
            breadcrumb.Items.Add("Kullanıcı Listesi", "/Users");
            breadcrumb.Items.Add(model.Firstname + " " + model.Lastname, "");
            TempData["Breadcrumb"] = breadcrumb;

            #endregion

            return View(model);
        }
    }
}