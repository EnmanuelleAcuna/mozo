using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUsuarioTelefonoLogic {
        /// <summary>
        /// Agregar un teléfono a un usuario
        /// </summary>
        Task<bool> AgregarAsync(UsuarioTelefono ModeloTelefonoUsuario);

        /// <summary>
        /// Eliminar un teléfono de un usuario
        /// </summary>
        Task<bool> EliminarAsync(int IdTelefono);

        /// <summary>
        /// Obtener todos los telefonos de un usuario
        /// </summary>
        IEnumerable<UsuarioTelefono> ObtenerTodos(string IdUsuario);
        Task<IEnumerable<UsuarioTelefono>> ObtenerTodosAsync(string IdUsuario);
    }
}