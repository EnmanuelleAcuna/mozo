using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IUsuarioSIRHRepository {
        /// <summary>
        /// Obtener todos los usuarios de la base de datos SIRH.
        /// </summary>
        Task<IEnumerable<UsuarioSIRH>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener usuarios de SIRH por nombre de usuario.
        /// </summary>
        Task<IEnumerable<UsuarioSIRH>> ObtenerTodosPorNombreUsuarioAsync(string NombreUsuario);
    }
}