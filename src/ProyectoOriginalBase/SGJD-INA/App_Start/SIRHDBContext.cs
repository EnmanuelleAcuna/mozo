using SGJD_INA.Models.DA.EntityFramework.SIRH;
using System.Data.Entity;

namespace SGJD_INA {
    /// <summary>
    /// Clase encargada de generar un contexto de base de datos para EF Oracle hacia SIRH de INA
    /// </summary>
    public class SIRHDBContext : DbContext {
        public SIRHDBContext() : base("SIRHEntities") { }

        public SIRHEntities Create() {
            return new SIRHEntities();
        }
    }
}