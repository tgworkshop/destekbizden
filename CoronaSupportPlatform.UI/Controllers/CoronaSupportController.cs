using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoronaSupportPlatform.Common.Response;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.UI.Models.ViewModels.Tenders;

namespace CoronaSupportPlatform.UI.Controllers
{
    [RoutePrefix("cs")]
    public class CoronaSupportController : BaseController
    {
        [Route("gettenders")]
        public JsonResult GetTender(int tenderId)
        {
            using (var ctx = new CoronaSupportPlatformDbContext())
            {
                // Get the tender
                var tender = ctx.Tenders.Include("Properties").Include("Items").Include("Organization").FirstOrDefault(t => t.TenderId == tenderId);

                // Get the products 
                var products = ctx.Products.ToList();

                var model = new TenderViewModel() 
                {
                    Products = products,
                    TenderId = tender.TenderId,
                    OrganizationId = tender.OrganizationId,
                    Items = tender.Items,
                    LongDescription = tender.LongDescription,
                    ShortDescription = tender.ShortDescription,
                    RefNumber = tender.RefNumber,
                    UserId = tender.UserId,
                    Organization = tender.Organization,
                    State = tender.State,
                };

                // Create the partial
                var partial = ViewToString("CS","~/Views/Tenders/Details.cshtml", model);

                return Json(new CSPartialResponse()
                {
                    ReturnCode = 200,
                    Html = partial
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("updatetenderstate")]
        public JsonResult UpdateTenderState(int tenderId,short state)
        {
            try
            {
                using (var ctx = new CoronaSupportPlatformDbContext())
                {
                    // Get the tender
                    var tender = ctx.Tenders.FirstOrDefault(t => t.TenderId == tenderId);

                    tender.State = (TenderState)state;

                    ctx.Entry(tender).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();

                    return Json(new CSControllerResponse()
                    {
                        ReturnCode = 200
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {

                return Json(new CSControllerResponse()
                {
                    ReturnCode = -300
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}