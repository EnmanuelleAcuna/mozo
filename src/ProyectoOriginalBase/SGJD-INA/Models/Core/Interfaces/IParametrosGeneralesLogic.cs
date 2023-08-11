using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IParametrosGeneralesLogic {
        /// <summary>
        /// Actualizar el valor de un parametro general
        /// </summary>
        Task<bool> ActualizarAsync(ParametroGeneral Entidad);

        /// <summary>
        /// Obtener todos los parametros generales del sistema
        /// </summary>
        IEnumerable<ParametroGeneral> ObtenerTodos();
        Task<IEnumerable<ParametroGeneral>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los parametros generales de tipo correo
        /// </summary>
        IEnumerable<ParametroGeneral> ObtenerParametrosCorreo();
        Task<IEnumerable<ParametroGeneral>> ObtenerParametrosCorreoAsync();

        /// <summary>
        /// Obtener todos los parametros generales de tipo institución
        /// </summary>
        IEnumerable<ParametroGeneral> ObtenerParametrosInstitucion();
        Task<IEnumerable<ParametroGeneral>> ObtenerParametrosInstitucionAsync();

        /// <summary>
        /// Obtener un parametro general por id
        /// </summary>
        Task<ParametroGeneral> ObtenerPorIdAsync(int IdParametro);

        /// <summary>
        /// Obtener un parametro general por nombre
        /// </summary>
        Task<ParametroGeneral> ObtenerPorNombreAsync(string Nombre);
    }
}