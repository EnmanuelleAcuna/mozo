using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Configuration;
using System.Data.Entity;

namespace SGJD_INA {
    /// <summary>
    /// Clase encargada de generar un contexto de base de datos para EF
    /// </summary>
    public class SGJDDBContext : DbContext {
        public SGJDDBContext() : base("SGJDEntities") { }

        public static SGJDEntities Create() {
            return new SGJDEntities();
        }

        public static string ConectionString() {
            return ConfigurationManager.ConnectionStrings["SGJDPA"].ConnectionString;
        }
    }
}