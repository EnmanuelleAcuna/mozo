using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IElementosRelacionadosLogic {
        /// <summary>
        /// Obtener todos los elementos relacionados
        /// </summary>
        Task<IEnumerable<ElementoRelacionado>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los elementos relacionados por id seguimiento
        /// </summary>
        Task<IEnumerable<ElementoRelacionado>> ObtenerTodosPorIdSeguimientoAsync(int IdSeguimiento);

        /// <summary>
        /// Obtener lista las opciones para tipos de elementos relacionados
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerTiposElementosRelacionadosParaSelectAsync();

        /// <summary>
        /// Obtener lista las actas para el tipo de elemento relacionado acta
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerListaActaParaSelectAsync(int IdSeguimiento);

        /// <summary>
        /// Obtener lista las acuerdos para el tipo de elemento relacionado acuerdo
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerListaAcuerdoParaSelectAsync(int IdSeguimiento);

        /// <summary>
        /// Obtener todos los elementos relacionados por id seguimiento y tipo de elemento relacionado 
        /// </summary>
        Task<IEnumerable<ElementoRelacionado>> ObtenerTodosPorIdSeguimientoTipoElementoAsync(int IdSeguimiento, string TipoElemento);

        /// <summary>
        /// Agregar un elemento relacionado
        /// </summary>
        Task<int> AgregarAsync(ElementoRelacionado ElementoRelacionadoModel);

        /// <summary>
        /// Eliminar un elemento relacionado
        /// </summary>
        Task<bool> EliminarAsync(int IdElementoRelacionado);
    }
}