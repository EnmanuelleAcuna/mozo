using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SGJD_INA.Models.Core.Implementation;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Implementation;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System;
using System.Web;
using Unity;
using Unity.Injection;

namespace SGJD_INA {
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig {
        #region Unity Container
        private static Lazy<IUnityContainer> LazyContainer =
          new Lazy<IUnityContainer>(() => {
              var Container = new UnityContainer();
              RegisterTypes(Container);
              return Container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => LazyContainer.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="Container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer Container) {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            // Necesario para Identity
            // Necesario para ApplicationUserManager
            Container.RegisterType<IUserStore<ApplicationUser, string>, UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>>(new InjectionConstructor(typeof(IdentityDBContext)));

            // Necesario para ApplicationRoleManager
            Container.RegisterType<IRoleStore<ApplicationRole, string>, RoleStore<ApplicationRole>>(new InjectionConstructor(typeof(IdentityDBContext)));

            // Necesario para ApplicationSignInManager
            Container.RegisterFactory<IAuthenticationManager>(o => HttpContext.Current.GetOwinContext().Authentication);

            Container.RegisterType<IAvisosLogic, AvisosLogic>();
            Container.RegisterType<IActaLogic, ActaLogic>();
            Container.RegisterType<ITranscripcionActaCore, TranscripcionActaCore>();
            Container.RegisterType<ITomoLogic, TomoLogic>();
            Container.RegisterType<IAsistenteSesionLogic, AsistenteSesionLogic>();
            Container.RegisterType<IBitacoraLogic, BitacoraLogic>();
            Container.RegisterType<IEncabezadoPiePaginaLogic, EncabezadoPiePaginaLogic>();
            Container.RegisterType<IEstadoLogic, EstadoLogic>();
            Container.RegisterType<IMiembrosJDLogic, MiembrosJDLogic>();
            Container.RegisterType<IParametrosGeneralesLogic, ParametrosGeneralesLogic>();
            Container.RegisterType<IPerfilLogic, PerfilLogic>();
            Container.RegisterType<IRepositorioLogic, RepositorioLogic>();
            Container.RegisterType<IRespaldoLogic, RespaldoLogic>();
            Container.RegisterType<ITipoArchivoLogic, TipoArchivoLogic>();
            Container.RegisterType<ITipoObjetoLogic, TipoObjetoLogic>();
            Container.RegisterType<ITipoSesionLogic, TipoSesionLogic>();
            Container.RegisterType<ITipoUsuarioLogic, TipoUsuarioLogic>();
            Container.RegisterType<IUnidadLogic, UnidadLogic>();
            Container.RegisterType<IUsuarioCorreoLogic, UsuarioCorreoLogic>();
            Container.RegisterType<IUsuarioTelefonoLogic, UsuarioTelefonoLogic>();
            Container.RegisterType<IUsuarioLogic, UsuarioLogic>();
            Container.RegisterType<IUsuarioSIRHLogic, UsuarioSIRHLogic>();
            Container.RegisterType<IVistaLogic, VistaLogic>();
            Container.RegisterType<IVistaPerfilLogic, VistaPerfilLogic>();
            Container.RegisterType<ISeccionLogic, SeccionLogic>();
            Container.RegisterType<ISesionLogic, SesionLogic>();
            Container.RegisterType<IOrdenDiaLogic, OrdenDiaLogic>();
            Container.RegisterType<ITamanoLetraLogic, TamanoLetraLogic>();
            Container.RegisterType<ITemaLogic, TemaLogic>();
            Container.RegisterType<IArchivoAdjuntoLogic, ArchivoAdjuntoLogic>();
            Container.RegisterType<IArticuloLogic, ArticuloLogic>();
            Container.RegisterType<IUsuarioArticuloLogic, UsuarioArticuloLogic>();
            Container.RegisterType<IAcuerdoLogic, AcuerdoLogic>();
            Container.RegisterType<IUnidadPredeterminadaLogic, UnidadPredeterminadaLogic>();
            Container.RegisterType<ISeguimientoLogic, SeguimientoLogic>();
            Container.RegisterType<IDetalleSeguimientoLogic, DetalleSeguimientoLogic>();
            Container.RegisterType<IVotoLogic, VotoLogic>();
            Container.RegisterType<ICorreoAdicionalLogic, CorreoAdicionalLogic>();
            Container.RegisterType<ICorreoUnidadAcuerdoLogic, CorreoUnidadAcuerdoLogic>();
            Container.RegisterType<IReportesActasLogic, ReportesActasLogic>();
            Container.RegisterType<IReportesSesionesLogic, ReportesSesionesLogic>();
            Container.RegisterType<IReportesAcuerdosLogic, ReportesAcuerdosLogic>();
            Container.RegisterType<IAudioLogic, AudioLogic>();
            Container.RegisterType<IElementosRelacionadosLogic, ElementosRelacionadosLogic>();
            Container.RegisterType<IAyudaUsuarioLogic, AyudaUsuarioLogic>();

            // Repositorios
            Container.RegisterType<ISeccionRepository, SeccionRepository>();
            Container.RegisterType<ITipoSesionRepository, TipoSesionRepository>();
            Container.RegisterType<ITomoRepository, TomoRepository>();
            Container.RegisterType<IVistaPerfilRepository, VistaPerfilRepository>();
            Container.RegisterType<IUsuarioSIRHRepository, UsuarioSIRHRepository>();
            Container.RegisterType<IAudioRepository, AudioRepository>();
            Container.RegisterType<IParametroGeneralRepository, ParametroGeneralRepository>();
            Container.RegisterType<IBitacoraRepository, BitacoraRepository>();
            Container.RegisterType<IAyudaUsuarioRepository, AyudaUsuarioRepository>();
        }
    }
}