using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUnidadLogic {
        /// <summary>
        /// Agregar una Unidad.
        /// </summary>
        Task<bool> AgregarAsync(Unidad ModeloUnidad);

        /// <summary>
        /// Actualizar una Unidad.
        /// </summary>
        Task<bool> ActualizarAsync(Unidad ModeloUnidad);

        /// <summary>
        /// Eliminar una Unidad.
        /// </summary>
        Task<bool> EliminarAsync(int IdUnidad);

        /// <summary>
        /// Obtener todas las Unidades.
        /// </summary>
        IEnumerable<Unidad> ObtenerTodos();
        Task<IEnumerable<Unidad>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener lista de unidades para Select/DropDownList.
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTodosParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync();

        /// <summary>
        /// Obtener una Unidad por id
        /// </summary>
        Task<Unidad> ObtenerPorIdAsync(int IdUnidad);

        /// <summary>
        /// Obtener una Unidad por nombre
        /// </summary>
        Task<Unidad> ObtenerPorNombre(string Nombre);
    }
}