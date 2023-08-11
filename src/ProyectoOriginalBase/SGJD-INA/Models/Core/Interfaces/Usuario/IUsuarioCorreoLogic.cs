using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUsuarioCorreoLogic {
        /// <summary>
        /// Agregar un correo a un usuario
        /// </summary>
        Task<bool> AgregarAsync(UsuarioCorreo ModeloCorreoUsuario);

        /// <summary>
        /// Eliminar un correo de un usuario
        /// </summary>
        Task<bool> EliminarAsync(int IdCorreo);

        /// <summary>
        /// Obtener todos los correos de un usuario
        /// </summary>
        IEnumerable<UsuarioCorreo> ObtenerTodos(string IdUsuario);
        Task<IEnumerable<UsuarioCorreo>> ObtenerTodosAsync(string IdUsuario);
    }
}