using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SGJD_INA.Models.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SGJD_INA {
    /// <summary>
    /// Configurar el administrador de inicio de sesión de la aplicación.
    /// SigInManager está definido en ASP.NET Identity y es usado en la aplicación, en este caso se hace una implementación de dicha interfaz para
    /// extraer únicamente los métodos necesarios para la aplicación.
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {
        // Constructor
        public ApplicationSignInManager(ApplicationUserManager UserManager, IAuthenticationManager AuthenticationManager) : base(UserManager, AuthenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser User) {
            return User.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}