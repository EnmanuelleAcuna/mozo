using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ISeccionLogic {
        /// <summary>
        /// Agregar una sección al catálogo de secciones disponibles para el Orden del Día
        /// </summary>
        Task<bool> AgregarAsync(Seccion ModeloSeccion);

        /// <summary>
        /// Actualizar una sección en el ctálogo de secciones disponibles para el Orden del Día
        /// </summary>
        Task<bool> ActualizarAsync(Seccion ModeloSeccion);

        /// <summary>
        /// Eliminar una sección del catálogo de secciones
        /// Si la sección ya ha sido utilizada por uno o mas Órdenes del Día, la sección no podrá ser eliminada
        /// </summary>
        Task<bool> EliminarAsync(int IdSeccion);

        /// <summary>
        /// Obtener todas las secciones del catálogo disponibles para Orden del Día
        /// </summary>
        IEnumerable<Seccion> ObtenerTodos();
        Task<IEnumerable<Seccion>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todas las secciones del catálogo disponibles para Orden del Día
        /// </summary>
        IEnumerable<Seccion> ObtenerTodosPorIdOrdenDia(int IdOrdenDia);
        Task<IEnumerable<Seccion>> ObtenerTodosPorIdOrdenDiaAsync(int IdOrdenDia);

        /// <summary>
        /// Obtener una sección por id
        /// </summary>
        Task<Seccion> ObtenerPorIdAsync(int IdSeccion);

        /// <summary>
        /// Obtener todas las secciones del catálogo para ser utilizado en un ComboBox/DropDownList
        /// Se necesita el id del Orden del Dïa ya que se devolverá la lista de secciones que dicho Orden del Día no tiene asignadas
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync(int IdOrdenDia);

        /// <summary>
        /// Obtener una sección por la descripción
        /// </summary>
        Task<Seccion> ObtenerPorDescripcion(string Descripcion);
    }
}