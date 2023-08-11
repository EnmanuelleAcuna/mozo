using Microsoft.AspNet.Identity;
using SGJD_INA.Models.Entities;

namespace SGJD_INA {
    /// <summary>
    /// Configurar el administrador de perfiles en la aplicación.
    /// RoleManager está definido en ASP.NET Identity y es usado en la aplicación, en este caso se hace una implementación de dicha
    /// interfaz para extraer únicamente los métodos necesarios para la aplicación.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string> {
        // Constructor
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> RoleStore) : base(RoleStore) { }
    }
}