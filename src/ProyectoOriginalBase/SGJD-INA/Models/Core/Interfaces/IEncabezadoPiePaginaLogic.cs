using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IEncabezadoPiePaginaLogic {
        /// <summary>
        /// Agregar un EncabezadoPiePagina
        /// </summary>
        Task<bool> AgregarAsync(EncabezadoPiePagina EncabezadoPiePaginaModel);

        /// <summary>
        /// Actualizar un EncabezadoPiePagina
        /// </summary>
        Task<bool> ActualizarAsync(EncabezadoPiePagina EncabezadoPiePaginaModel);

        /// <summary>
        /// Obtener todos los EncabezadoPiePagina.
        /// </summary>
        IEnumerable<EncabezadoPiePagina> ObtenerTodos();
        Task<IEnumerable<EncabezadoPiePagina>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un EncabezadoPiePagina por id
        /// </summary>
        Task<EncabezadoPiePagina> ObtenerPorIdAsync(int IdEncabezado);

        /// <summary>
        /// Obtener un elemento EncabezadoPiePagina por tipoObjeto
        /// </summary>
        Task<EncabezadoPiePagina> ObtenerPorIdTipoObjetoAsync(int IdTipoObjeto);
    }
}