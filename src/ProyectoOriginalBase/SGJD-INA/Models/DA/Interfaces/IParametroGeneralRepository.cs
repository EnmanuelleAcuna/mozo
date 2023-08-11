using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IParametroGeneralRepository {
        /// <summary>
        /// Actualizar información de un parámetro general en la base/fuente de datos
        /// </summary>
        Task<bool> ActualizarAsync(ParametroGeneral Entidad);

        /// <summary>
        /// Obtener todos los parametros generales de la fuente/base de datos
        /// </summary>
        IEnumerable<ParametroGeneral> ObtenerTodos();

        /// <summary>
        /// Obtener un parametro general por id de la fuente/base de datos
        /// </summary>
        Task<ParametroGeneral> ObtenerPorIdAsync(int IdParametro);

        /// <summary>
        /// Obtener un parametro general por nombre de la fuente/base de datos
        /// </summary>
        Task<ParametroGeneral> ObtenerPorNombreAsync(string Nombre);
    }
}