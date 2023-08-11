using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ISeguimientoLogic {
        /// <summary>
        /// Agregar un Seguimiento a un Acuerdo
        /// </summary>
        Task<int> AgregarAsync(SeguimientoAcuerdo ModeloSeguimientoAcuerdo);

        /// <summary>
        /// Actualizar un Seguimiento de Acuerdo
        /// </summary>
        Task<bool> ActualizarAsync(SeguimientoAcuerdo ModeloSeguimientoAcuerdo);

        /// <summary>
        /// Eliminar un Seguimiento de un Acuerdo
        /// </summary>
        Task<bool> EliminarAsync(int IdSeguimientoAcuerdo);

        /// <summary>
        /// Obtener todos los Seguimientos
        /// </summary>
        IEnumerable<SeguimientoAcuerdo> ObtenerTodos();
        Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener Seguimiento de un Acuerdo por su id
        /// </summary>
        Task<SeguimientoAcuerdo> ObtenerPorIdAsync(int IdSeguimientoAcuerdo);

        /// <summary>
        /// Obtener todos los Seguimientos de un Acuerdo por id de Acuerdo
        /// </summary>
        Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosPorIdAcuerdoAsync(int IdAcuerdo);

        /// <summary>
        /// Obtener un el ultimo seguimiento de un acuerdo mediante el id del acuerdo
        /// </summary>
        Task<SeguimientoAcuerdo> ObtenerUltimoSeguimientoAcuerdoPorAcuerdo(int IdAcuerdo);

        /// <summary>
        /// Obtener todos los seguimientos agrupados con el estado [No ejecutados]
        /// </summary>
        Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoNoEjecutado();

        /// <summary>
        /// Obtener todos los seguimientos agrupados con el estado [En ejecucion]
        /// </summary>
        Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoEnEjecucion();

        /// <summary>
        /// Obtener todos los seguimientos agrupados con el estado [Ejecutados]
        /// </summary>
        Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoEjecutado();

        /// <summary>
        /// Obtener lista las opciones para tipos de seguimiento
        /// </summary>
        IEnumerable<SelectListItem> ObtenerEstadosSeguimientoParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerEstadosSeguimientoParaSelectAsync();

        /// <summary>
        /// Obtener lista de archivos adjuntos de un seguimiento
        /// </summary>
        Task<IEnumerable<ArchivoAdjunto>> ObtenerAdjuntos(int IdSeguimiento);

        /// <summary>
        /// Agregar un archivo adjunto al Seguimiento
        /// </summary>
        Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo);

        /// <summary>
        /// Notifica a las unidades ejecutoras del seguimiento vencido
        /// </summary>
        Task<bool> NotificarUnidadesDeSeguimientoVencido(int IdSeguimiento, string TextoEnlace, string Enlace, string MensajeDetalle, bool EnviarCorreoUsuariosDeUnidades);

        /// <summary>
        /// Notifica a las unidades ejecutoras del seguimiento
        /// </summary>
        Task<bool> NotificarUnidadesDeSeguimiento(int IdSeguimiento, string TextoEnlace, string Enlace, string MensajeDetalle, bool EnviarCorreoUsuariosDeUnidades);

        /// <summary>
        /// Cambia el estado del seguimiento a [Ejecutado y revisa si todos los seguimientos estan ejecutados para marcar el acuerdo como ejecutado]
        /// </summary>
        Task<string> SeguimientoEjecutadoAsync(int IdSeguimiento);

        Task<bool> AgregarUnidadAsync(int IdSeguimiento, int IdUnidad);

        Task<bool> EliminarUnidadAsync(int IdSeguimiento, int IdUnidad);

        /// <summary>
        /// Obtener todas las unidades para ejecución que están asignadas a un acuerdo
        /// </summary>
        IEnumerable<Unidad> ObtenerUnidadesEjecucion(int IdSeguimiento);
        Task<IEnumerable<Unidad>> ObtenerUnidadesEjecucionAsync(int IdSeguimiento);

        /// <summary>
        /// Obtener todas las unidades para ejecución que están asignadas a un seguimiento
        /// </summary>
        Task<IEnumerable<UnidadesEjecucionSeguimientoDTO>> ObtenerSeguimientosSegunIdUnidadAsyng(int IdUnidad);
    }
}