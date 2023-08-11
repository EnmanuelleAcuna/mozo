using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUsuarioArticuloLogic {
        /// <summary>
        /// Agregar un usuario a un articulo
        /// </summary>
        Task<int> AgregarAsync(UsuarioArticulo usuarioArticulo);

        /// <summary>
        /// Eliminar un usuario de un artículo
        /// </summary>
        Task<bool> EliminarAsync(int IdUsuarioArticulo);

        /// <summary>
        /// Eliminar una lista de usuarios de un articulo por id de articulo
        /// </summary>
        Task<bool> EliminarTodosPorIdArticulo(int IdArticulo);

        /// <summary>
        /// Obtener todos los UsuariosArticulo
        /// </summary>
        Task<IEnumerable<UsuarioArticulo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los usuario de un articulo por id de artículo
        /// </summary>
        Task<IEnumerable<UsuarioArticulo>> ObtenerTodosPorIdArticulo(int IdArticulo);

        /// <summary>
        /// Obtener un UsuarioArticulo por id
        /// </summary>
        Task<UsuarioArticulo> ObtenerPorIdAsync(int id);
    }
}