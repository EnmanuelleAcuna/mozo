using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SGJD_INA {
    public class MvcApplication : HttpApplication {
        protected void Application_Start(object Sender, EventArgs E) {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            MvcHandler.DisableMvcResponseHeader = true;
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_BeginRequest(object Sender, EventArgs E) {
            if (Sender is HttpApplication App && App.Context != null) {
                App.Context.Response.Headers.Remove("Server");
                App.Context.Response.Headers.Remove("X-Powered-By");
                App.Context.Response.Headers.Remove("X-AspNet-Version");
                App.Context.Response.Headers.Remove("X-AspNetMvc-Version");
                App.Context.Response.Headers.Remove("X-SourceFiles");

                App.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                App.Context.Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                App.Context.Response.AppendHeader("Pragma", "no-cache");
                App.Context.Response.AppendHeader("Expires", "0");
            }

            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("X-SourceFiles");

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            HttpContext.Current.Response.AppendHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AppendHeader("Expires", "0");
        }

        protected void Application_PreSendRequestHeaders(object Sender, EventArgs E) {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-SourceFiles");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");
        }        
    }
}