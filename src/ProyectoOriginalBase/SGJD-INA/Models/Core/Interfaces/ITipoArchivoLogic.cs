using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITipoArchivoLogic {
        /// <summary>
        /// Agregar un TipoArchivo
        /// </summary>
        Task<bool> AgregarAsync(TipoArchivo TipoArchivoModel);

        /// <summary>
        /// Actualiza un TipoArchivo
        /// </summary>
        Task<bool> ActualizarAsync(TipoArchivo TipoArchivoModel);

        /// <summary>
        /// Eliminar un TipoArchivo
        /// </summary>
        Task<bool> EliminarAsync(int IdTipoArchivo);

        /// <summary>
        /// Obtener todos los TipoArchivo
        /// </summary>
        IEnumerable<TipoArchivo> ObtenerTodos();
        Task<IEnumerable<TipoArchivo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un TipoArchivo por id
        /// </summary>
        Task<TipoArchivo> ObtenerPorIdAsync(int Id);

        /// <summary>
        /// Obtener un TipoArchivo por extensión
        /// </summary>
        Task<TipoArchivo> ObtenerPorExtensionAsync(string Extension);


        /// <summary>
        /// Obtener las listas de tipo de archivo disponible para agregar a la base de datos
        /// </summary>
        Task<IEnumerable<TipoArchivo>> ObtenerListaTipoArchivoSinAgregar();
    }
}