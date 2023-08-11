using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IBitacoraRepository {
        /// <summary>
        /// Obtener todos los registros de la bitácora en la base de datos
        /// </summary>
        Task<IEnumerable<Bitacora>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los registros de la errores en la base de datos
        /// </summary>
        Task<IEnumerable<Errores>> ObtenerTodosErroresAsync();

        /// <summary>
        /// Obtener un registro de la bitácora en la base de datos por su id
        /// </summary>
        Task<Bitacora> ObtenerPorIdAsync(int Id);
    }
}