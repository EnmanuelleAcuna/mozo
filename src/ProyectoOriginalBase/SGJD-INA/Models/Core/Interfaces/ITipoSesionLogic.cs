using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITipoSesionLogic {
        /// <summary>
        /// Agregar un Tipo de Sesión
        /// </summary>
        Task<bool> AgregarAsync(TipoSesion ModeloTipoSesion);

        /// <summary>
        /// Actualizar un Tipo de Sesión
        /// </summary>
        Task<bool> ActualizarAsync(TipoSesion ModeloTipoSesion);

        /// <Eliminar un Tipo de Sesión
        /// </summary>
        Task<bool> EliminarAsync(int IdTipoSesion);

        /// <summary>
        /// Obtener todos los Tipos de Sesión
        /// </summary>
        IEnumerable<TipoSesion> ObtenerTodos();
        Task<IEnumerable<TipoSesion>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un Tipo de Sesión por su id
        /// </summary>
        Task<TipoSesion> ObtenerPorIdAsync(int IdTipoSesion);
    }
}