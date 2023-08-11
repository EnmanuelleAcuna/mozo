using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IAcuerdoLogic {
        /// <summary>
        /// Agregar un acuerdo
        /// </summary>
        Task<int> AgregarAsync(Acuerdo AcuerdoModel);

        /// <summary>
        /// Actualizar un acuerdo
        /// </summary>
        Task<bool> ActualizarAsync(Acuerdo AcuerdoModel);

        /// <summary>
        /// Eliminar un acuerdo
        /// </summary>
        Task<bool> EliminarAsync(int IdAcuerdo);

        /// <summary>
        /// Actualizar el Tipo de Seguimiento de un acuerdo
        /// </summary>
        Task<bool> ActualizarTipoSeguimiento(string IdAcuerdo, string TipoSeguimiento);

        /// <summary>
        /// Cambiar el estado de un acuerdo a visto bueno
        /// </summary>
        Task<bool> EnviarVistoBuenoAsync(int IdAcuerdo);

        /// <summary>
        /// Agergar archivo con el acuerdo firmado
        /// </summary>
        Task<bool> AgregarAcuerdoFirmadoAsync(Acuerdo ModeloAcuerdo, HttpPostedFileBase Archivo);

        /// <summary>
        /// Cambiar el estado de un acuerdo a notificado
        /// </summary>
        Task<bool> NotificarAsync(int IdAcuerdo, string Enlace, string TextoEnlace, string MensajeDetalle);

        /// <summary>
        /// Generar nueva versión de un acuerdo
        /// </summary>
        Task<int> AgregarNuevaVersionAsync(int IdAcuerdo);

        /// <summary>
        /// Establecer un acuerdo como en ejecución
        /// </summary>
        Task<bool> AcuerdoEnEjecucionAsync(int IdAcuerdo);

        /// <summary>
        /// Establecer un acuerdo como ejecutado
        /// </summary>
        Task<string> AcuerdoEjecutadoAsync(int IdAcuerdo);

        /// <summary>
        /// Revocar un acuerdo
        /// </summary>
        Task<bool> RevocarAcuerdoAsync(int IdAcuerdo);

        /// <summary>
        /// Agregar un archivo adjunto al acuerdo
        /// </summary>
        Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo);

        /// <summary>
        /// Obtener todos los archivos relacionados al acuerdo, incluyendo los del articulo del acuerdo y el tema del articulo del acuerdo
        /// </summary>
        Task<IEnumerable<ArchivoAdjunto>> ObtenerArchivosRelacionadosAsync(int IdAcuerdo);

        /// <summary>
        /// Eliminar archivo con el acuerdo firmado
        /// </summary>
        Task<bool> EliminarAcuerdoFirmadoAsync(Acuerdo ModeloAcuerdo);

        /// <summary>
        /// Obtener lista con todos los acuerdos
        /// </summary>
        IEnumerable<Acuerdo> ObtenerTodos();
        Task<IEnumerable<Acuerdo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los acuerdos que tengan la palabra clave
        /// </summary>
        Task<IEnumerable<AcuerdosPorPalabraClaveDTO>> ObtenerTodosPorPalabraClaveAsync(string Palabra);

        /// <summary>
        /// Obtener un acuerdo por id
        /// </summary>
        Task<Acuerdo> ObtenerPorIdAsync(int IdAcuerdo);        

        /// <summary>
        /// Obtener lo acuerdos disponibles para agregarle un seguimiento
        /// Los acuerdos que se obtendrán son aquellos que se encuentren en firme, con estado notificado o en ejecución
        /// y que posea el tipo de seguimiento con seguimiento o seguimiento permanente
        /// </summary>
        IEnumerable<AcuerdoParaSeguimientoDTO> ObtenerAcuerdosParaSeguimiento();
        Task<IEnumerable<AcuerdoParaSeguimientoDTO>> ObtenerAcuerdosParaSeguimientoAsync();

        /// <summary>
        /// Obtener lista de Sesiones con sus respectivos Articulos que no tienen un Acuerdo creado
        /// </summary>
        IEnumerable<SesionConArticulosParaNuevoAcuerdoDTO> ObtenerSesionesConArticulosParaNuevoAcuerdo();

        /// <summary>
        /// Agregar unidad a un Acuerdo
        /// </summary>
        Task<bool> AgregarUnidadAsync(int IdAcuerdo, int IdUnidad, string TipoUnidad);

        /// <summary>
        /// Quitar unidad de un Acuerdo
        /// </summary>
        Task<bool> EliminarUnidadAsync(int IdAcuerdo, int IdUnidad, string TipoUnidad);

        /// <summary>
        /// Obtener todas las unidades para ejecución que están asignadas a un acuerdo
        /// </summary>
        IEnumerable<Unidad> ObtenerUnidadesEjecucion(int IdAcuerdo);
        Task<IEnumerable<Unidad>> ObtenerUnidadesEjecucionAsync(int IdAcuerdo);

        /// <summary>
        /// Obtener todas las unidades para información que están asignadas a un acuerdo
        /// </summary>
        IEnumerable<Unidad> ObtenerUnidadesInformacion(int IdAcuerdo);
        Task<IEnumerable<Unidad>> ObtenerUnidadesInformacionAsync(int IdAcuerdo);

        /// <summary>
        /// Agregar correo adicional a un Acuerdo
        /// </summary>
        Task<bool> AgregarCorreoAdicionalAsync(int IdAcuerdo, int IdCorreoAdicional);

        /// <summary>
        /// Quitar correo adicional de un Acuerdo
        /// </summary>
        Task<bool> QuitarCorreoAdicionalAsync(int IdAcuerdo, int IdCorreoAdicional);

        /// <summary>
        /// Obtener lista de opciones para tipos de firmeza para DropDownList
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposFirmezaParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTiposFirmezaParaSelectAsync();

        /// <summary>
        /// Obtener lista de opciones para tipos de revisión para DropDownList
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposRevisiónParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTiposRevisiónParaSelectAsync();

        /// <summary>
        /// Obtener lista de opciones para tipos de seguimiento para DropDownList
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposSeguimientoParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTiposSeguimientoParaSelectAsync();

        /// <summary>
        /// Obtener lista de opciones para tipos de aprobacion para DropDownList
        /// </summary>
        IEnumerable<TipoAprobacion> ObtenerTipoAprobacionParaSelect();
        Task<IEnumerable<TipoAprobacion>> ObtenerTipoAprobacionParaSelectAsync();

        /// <summary>
        /// Obtener todos los acuerdos por rango de fechas
        /// </summary>
        Task<IEnumerable<Acuerdo>> ObtenerTodosPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin);
    }
}