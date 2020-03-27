using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.Data;
using CoronaSupportPlatform.UI.Models.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("Reports"), Authorize(Roles = "Administrator")]
    public class ReportsController : Controller
    {
        [Route("Products")]
        public ActionResult Products()
        {
            var model = new ProductReportsViewModel();

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Create the parameters
                var fromDateParameter = new SqlParameter("@startDate", System.Data.SqlDbType.DateTime);
                fromDateParameter.Value = DateTime.UtcNow.AddMonths(-1); ;

                var endDateParameter = new SqlParameter("@endDate", System.Data.SqlDbType.DateTime);
                endDateParameter.Value = DateTime.UtcNow;

                // Call the stored procedur
                model.ProductQuantities = ctx.Database.SqlQuery<TenderProductQuantity>("GetTenderProductQuantities @startDate,@endDate", fromDateParameter, endDateParameter).ToList();
            }

            return View(model);
        }

        [Route("Cities")]
        public ActionResult Cities()
        {
            var model = new CityReportsViewModel();

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Create the parameters
                var fromDateParameter = new SqlParameter("@startDate", System.Data.SqlDbType.DateTime);
                fromDateParameter.Value = DateTime.UtcNow.AddMonths(-1); ;

                var endDateParameter = new SqlParameter("@endDate", System.Data.SqlDbType.DateTime);
                endDateParameter.Value = DateTime.UtcNow;

                // Call the stored procedure
                model.TenderQuantities = ctx.Database.SqlQuery<TenderProductQuantity>("GetTenderCityDistribution @startDate,@endDate", fromDateParameter, endDateParameter).ToList();
            }

            return View(model);
        }

        [Route("Organizations")]
        public ActionResult Organizations()
        {
            var model = new OrganizationReportsViewModel();

            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Load organizaitons
                var organizations = ctx.Organizations.Include("Tenders.User.Roles")
                                                     .Include("Tenders.Organization")
                                                     .Include("Tenders.Items.Product")
                                                     .Include("Properties")
                                                     .Include("Tags")
                                                     .Where(o => o.Tenders.Any())
                                                     .ToList();

                organizations.ToList().ForEach(o =>
                {
                    model.Organizations.Add(new Models.ViewModels.Organizations.OrganizationViewModel().From(o));
                });
            }

            return View(model);
        }
    }
}