using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IPerfilLogic {
        /// <summary>
        /// Obtener lista con todos los roles
        /// </summary>
        IEnumerable<ApplicationRole> ObtenerTodos();
        Task<IEnumerable<ApplicationRole>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        ApplicationRole ObtenerPorId(string IdRol);
        Task<ApplicationRole> ObtenerPorIdAsync(string IdRol);

        /// <summary>
        /// Obtener rol por nombre
        /// </summary>
        ApplicationRole ObtenerPorNombre(string NombreRol);
        Task<ApplicationRole> ObtenerPorNombreAsync(string NombreRol);
    }
}