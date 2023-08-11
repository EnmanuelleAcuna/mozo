using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGJD_INA.Startup))]

namespace SGJD_INA {
    public partial class Startup {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }

        public void Configuration(IAppBuilder App) {
            ConfigureAuth(App); // Configurar autenticación.
            DataProtectionProvider = App.GetDataProtectionProvider();
        }
    }
}