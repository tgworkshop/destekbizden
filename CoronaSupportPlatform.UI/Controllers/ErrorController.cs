using CoronaSupportPlatform.UI.Models.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace CoronaSupportPlatform.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("not-authorized")]
        public ActionResult NotAuthorized()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                HttpStatusCode = (int)HttpStatusCode.Unauthorized
            };
            return View(model);
        }

        [Route("Error")]
        public ActionResult Default()
        {
            return View("~/Views/Error/Index.cshtml", new ErrorViewModel());
        }

        [Route("NotFound")]
        public ActionResult NotFound()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                HttpStatusCode = (int)HttpStatusCode.NotFound
            };
            return View("~/Views/Error/Index.cshtml", model);
        }

        [Route("BadRequest")]
        public ActionResult BadRequest()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                HttpStatusCode = (int)HttpStatusCode.BadRequest
            };
            return View("~/Views/Error/Index.cshtml", model);
        }
    }
}