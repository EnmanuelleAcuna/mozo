using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SGJD_INA.Models.Entities;

namespace SGJD_INA {
    /// <summary>
    /// Configurar el administrador de usuarios en la aplicación.
    /// UserManager está definido en ASP.NET Identity y es usado en la aplicación, en este caso se hace una implementación de dicha
    /// interfaz para extraer únicamente los métodos necesarios para la aplicación.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser, string> {
        // Constructor
        public ApplicationUserManager(IUserStore<ApplicationUser, string> UserStore) : base(UserStore) {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            this.EmailService = new ApplicationEmailService();

            // Configure user lockout defaults 
            //Manager.UserLockoutEnabledByDefault = false;
            //Manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //Manager.MaxFailedAccessAttemptsBeforeLockout = 5; No se bloquea por intentos fallidos

            IDataProtectionProvider DataProtectionProvider = Startup.DataProtectionProvider;
            if (DataProtectionProvider != null) {
                IDataProtector DataProtector = DataProtectionProvider.Create("SGJDIdentity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(DataProtector);
            }
        }
    }
}