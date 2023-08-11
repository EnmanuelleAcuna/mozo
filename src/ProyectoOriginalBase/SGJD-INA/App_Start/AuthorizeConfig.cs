using System.Web.Mvc;

namespace SGJD_INA {
    public class AuthorizeConfig : AuthorizeAttribute {
        // Controlar una solicitud que falló en su autorización
        protected override void HandleUnauthorizedRequest(AuthorizationContext FilterContext) {
            if (FilterContext.HttpContext.User.Identity.IsAuthenticated) {
                // El usuario está autenticado, redirigir a la pagina de acceso denegado
                FilterContext.Result = new RedirectResult("~/Inicio/E403");
            }
            else {
                // Si no está autenticado, dejar que se maneje la solicitud con acceso no autorizado, por defecto lo redirigirá a la página de inicio de sesión.
                base.HandleUnauthorizedRequest(FilterContext);
            }
        }
    }
}