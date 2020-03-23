using CoronaSupportPlatform.Common;
using CoronaSupportPlatform.Models;
using CoronaSupportPlatform.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoronaSupportPlatform.UI.Controllers
{
    public class BaseController : Controller
    {
        protected static NLog.Logger LogService = NLog.LogManager.GetCurrentClassLogger();
        private CSPSignInManager _signInManager;
        private CSPUserManager _userManager;

        public CSPSignInManager SignInManager
        {
            get
            {

                return _signInManager ?? HttpContext.GetOwinContext().Get<CSPSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public CSPUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<CSPUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #region [ User and session properties ]

        public CSPUser CurrentUser { get; set; }

        public List<CSPUserRole> UserRoles { get; set; } = new List<CSPUserRole>();

        public string UserToken { get; set; }

        public int UserId
        {
            get
            {
                return User.Identity.IsAuthenticated ? User.Identity.GetUserId<int>() : 0;
            }
        }

        public string SessionId
        {
            get { return Session.SessionID; }
        }

        #endregion

        #region [ Organization ]

        public Organization CurrentOrganization { get; set; }

        #endregion

        #region [ Culture ]

        public string CurrentCulture { get; private set; }

        #endregion

        static BaseController() { }

        public BaseController()
        {
            // Set the current culture
            CurrentCulture = "tr";
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            #region [ User ]

            // Try to get user information from the session
            var user = Session["User"] as CSPUser;

            if (user == null || user.Status != EntityStatus.Active)
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (var ctx = new CoronaSupportPlatformDbContext())
                    {
                        // Get the user id
                        var userId = User.Identity.GetUserId<int>();

                        // Get the user
                        user = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
                    }
                }

                // Store contact data in the session
                Session["User"] = user;
            }

            // Set the user
            CurrentUser = user;

            #endregion

            #region [ User roles ]

            if (CurrentUser != null)
            {
                // Try to get user information from the session
                var userRoles = CurrentUser.Roles;

                if (userRoles == null || userRoles.Count == 0)
                {
                    // Get the role information from the related service
                    using (var ctx = new CoronaSupportPlatformDbContext())
                    {
                        userRoles = ctx.UserRoles.Include("Organization").Where(ur => ur.UserId == user.Id).ToList();
                    }

                    // Set the roles in to the current user
                    foreach (var userRole in userRoles)
                    {
                        CurrentUser.Roles.Add(userRole);
                    }

                    // Re-Store user data in the session
                    Session["User"] = CurrentUser;

                    // Set the current organization
                    CurrentOrganization = CurrentUser.Roles.FirstOrDefault().Organization;
                }

                // Set user roles
                UserRoles = CurrentUser.Roles.ToList();
            }

            #endregion

            #region [ User token ]

            // Get the user token
            var userToken = string.Empty;

            // Try to get the user token
            var tokenCookie = requestContext.HttpContext.Request.Cookies["csp.token"];

            if (tokenCookie == null)
            {
                // Get the token from the identity server
                userToken = HttpContext.GetOwinContext().GetUserManager<CSPUserManager>().GetUserGuid();

                // Set the cookie
                tokenCookie = new HttpCookie("csp.token", userToken);
                tokenCookie.Expires = DateTime.MaxValue;
                requestContext.HttpContext.Response.Cookies.Add(tokenCookie);
            }
            else
            {
                // Get the token from the 
                userToken = tokenCookie.Value;
            }

            // Set the token
            UserToken = userToken;
            ViewBag.UserToken = userToken;

            #endregion

            #region [ User Id ]

            // Set the user id
            ViewBag.UserId = UserId;

            #endregion

            #region [ Session Id ]

            // Set the session id
            ViewBag.SessionId = SessionId;

            #endregion
        }

        #region [ Override JsonResult ]

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion

        #region [ Helper methods ]

        protected string ViewToString<T>(string controllerName, string viewPath, T model, bool useCache = false)
        {
            try
            {
                using (var writer = new StringWriter())
                {
                    // Create a new route data
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);

                    // Create fake controller context
                    var fakeControllerContext = new ControllerContext(
                                                    new HttpContextWrapper(
                                                        new HttpContext(
                                                            new HttpRequest(null, "http://google.com", null),
                                                            new HttpResponse(null))), routeData, new FakeController());

                    // Create the razor engine
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewPath, "", false);

                    // Create view context
                    var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(model), new TempDataDictionary(), writer);

                    // Render view
                    razorViewResult.View.Render(viewContext, writer);

                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected string EncryptMessage(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                var hashMessageString = Convert.ToBase64String(hashmessage);
                return hashMessageString.TrimEnd(new char[] { '=' }).Replace('+', '-').Replace('/', '_');
            }
        }

        #endregion
    }

    #region [ Json serialization corrections ]

    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            // Check for a valid contxt
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            // Get the response object
            HttpResponseBase response = context.HttpContext.Response;

            // Check for a redirect request
            if (!String.IsNullOrEmpty(response.RedirectLocation)) return;

            // Set the content type
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            // Set the encoding
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            // Set the serializer and writer
            var scriptSerializer = JsonSerializer.Create(this.Settings);
            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }

    #endregion

    public class FakeController : ControllerBase
    {
        protected override void ExecuteCore() { }
    }

}