using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITipoObjetoLogic {
        /// <summary>
        /// Agregar un Tipo de Objeto
        /// </summary>
        Task<bool> AgregarAsync(TipoObjeto ModeloTipoObjeto);

        /// <summary>
        /// Actualizar un Tipo de Objeto
        /// </summary>
        Task<bool> ActualizarAsync(TipoObjeto ModeloTipoObjeto);

        /// <summary>
        /// Eliminar un Tipo de Objeto
        /// </summary>
        Task<bool> EliminarAsync(int IdTipoObjeto);

        /// <summary>
        /// Obtener todos los Tipos de Objeto
        /// </summary>
        IEnumerable<TipoObjeto> ObtenerTodos();
        Task<IEnumerable<TipoObjeto>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los Tipos de Objeto con parámetro de edición = true
        /// </summary>
        Task<IEnumerable<TipoObjeto>> ObtenerTodosConParametroEdicionAsync();

        /// <summary>
        /// Obtener todos los Tipos de Objeto con parámetro de edición = true y que aún no se han configurado en el mantenimiento de Parámetros de Edición
        /// </summary>
        Task<IEnumerable<TipoObjeto>> ObtenerTodosConParametroEdicionSinConfigurarAsync();

        /// <summary>
        /// Obtener todos los Tipos de Objeto con parámetro de encabezado y pie de página = true
        /// </summary>
        Task<IEnumerable<TipoObjeto>> ObtenerTodosConEncabezadoPiePaginaAsync();

        /// <summary>
        /// Obtener todos los Tipos de Objeto con encabezado y pie de página = true y que appun no se han configurado en el mantenimiento de Encabezados y Pie de Página
        /// </summary>
        Task<IEnumerable<TipoObjeto>> ObtenerTodosConEncabezadoPiePaginaSinConfigurarAsync();

        /// <summary>
        /// Obtener un Tipo de Objeto por su id
        /// </summary>
        Task<TipoObjeto> ObtenerPorIdAsync(int IdTipoObjeto);

        /// <summary>
        /// Obtener un tipo de Objeto por su nombre (Campo [Descripción])
        /// </summary>
        Task<TipoObjeto> ObtenerPorNombreAsync(string Nombre);

        /// <summary>
        /// Obtener un Tipo de Objeto por nombre de tabla
        /// </summary>
        Task<TipoObjeto> ObtenerPorNombreTablaAsync(string NombreTabla);

        /// <summary>
        /// Aumentar el número de consecutivo de un Tipo de Objeto
        /// </summary>
        Task<bool> AumentarConsecutivoAsync(string NombreTabla);

        /// <summary>
        /// Bajar el número de consecutivo de un Tipo de Objeto
        /// </summary>
        Task<bool> BajarConsecutivoAsync(string NombreTabla);

        /// <summary>
        /// Obtener las tablas de la base de datos a las que no se les ha asignado un tipo de objeto
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTablasBDSinConfigurarParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTablasBDSinConfigurarParaSelectAsync();
    }
}