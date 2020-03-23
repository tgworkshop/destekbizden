using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoronaSupportPlatform.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static NLog.Logger LogService = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create the logger
            var logger = NLog.LogManager.GetCurrentClassLogger();

            logger.Debug("Saglik Cephesine Destek application initialized.");
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            var httpException = exception as HttpException;

            string oldUrl = HttpContext.Current.Request.Url.AbsolutePath;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "NotFound";
                        break;
                    case 400:
                        // bad request
                        action = "BadRequest";
                        break;
                    case 401:
                        // unauthorized
                        action = "Unauthorized";
                        break;
                    default:
                        action = "Default";
                        break;
                }

                LogService.Error("Application error. Http Status Code: " + httpException.GetHttpCode() + ", Error message: " + exception.ToString());

                Response.Redirect("/" + action + "?refUrl=" + oldUrl);
            }
            else
            {
                // redirect any error
                Response.Redirect("/Error" + "?refUrl=" + oldUrl);
            }
        }
    }
}
