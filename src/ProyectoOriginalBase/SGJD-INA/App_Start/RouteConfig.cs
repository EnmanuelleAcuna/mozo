using System.Web.Mvc;
using System.Web.Routing;

namespace SGJD_INA {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection Routes) {
            Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cuenta", action = "IniciarSesion", id = UrlParameter.Optional }
            );
        }
    }
}