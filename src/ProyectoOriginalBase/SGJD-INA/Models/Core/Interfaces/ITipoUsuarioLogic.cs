using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITipoUsuarioLogic {
        /// <summary>
        /// Agregar un Tipo de Usuario
        /// </summary>
        Task<bool> AgregarAsync(TipoUsuario ModeloTipoUsuario);

        /// <summary>
        /// Actualizar un Tipo de Usuario
        /// </summary>
        Task<bool> ActualizarAsync(TipoUsuario ModeloTipoUsuario);

        /// <summary>
        /// Eliminar un Tipo de Usuario
        /// </summary>
        Task<bool> EliminarAsync(int IdTipoUsuario);

        /// <summary>
        /// Obtener todos los Tipod de Usuario
        /// </summary>
        IEnumerable<TipoUsuario> ObtenerTodos();
        Task<IEnumerable<TipoUsuario>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un Tipo de Usuario por su id
        /// </summary>
        Task<TipoUsuario> ObtenerPorIdAsync(int Id);

        /// <summary>
        /// Obtener un Tipo de Objeto por su nombre
        /// </summary>
        Task<TipoUsuario> ObtenerPorNombreAsync(string Nombre);
    }
}