using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IAsistenteSesionLogic {
        // Asistencia de miembros de junta directiva 
        /// <summary>
        /// Agregar asistencia segun los miembros de la junta directiva
        /// </summary>
        Task<AsistenteSesion> AgregarAsistenteAsync(AsistenteSesion ModeloAsistente);

        /// <summary>
        /// Actualizar asistencia segun los miembros de la junta directiva
        /// </summary>
        Task<bool> ActualizarAsistenteAsync(AsistenteSesion ModeloAsistente);

        /// <summary>
        /// Actualiza una hora de la sesion de los miembros de junta directiva
        /// </summary>
        Task<bool> ActualizarHoraAsync(AsistenteSesion ModeloAsistente);

        /// <summary>
        /// Obtener asistente por id de asistente
        /// </summary>
        Task<AsistenteSesion> ObtenerPorIdAsync(int IdAsistente);

        /// <summary>
        /// Obtener asistencia por id de usuario y id de sesión
        /// </summary>
        Task<AsistenteSesion> ObtenerPorIdUsuarioIdSesionAsync(int IdSesion, string IdUsuario);

        /// <summary>
        /// Obtener listado de todos los Asistentes a una sesion
        /// </summary>
        Task<IEnumerable<AsistenteSesion>> ObtenerTodosPorIdSesionAsync(int IdSesion);

        /// <summary>
        /// Obtener listado de todos los Asistentes presentes a una sesion
        /// </summary>
        Task<IEnumerable<AsistenteSesion>> ObtenerTodosPresentesPorIdSesionAsync(int IdSesion);

        /// <summary>
        /// Obtener lista de los miembros de junta directiva para asistencia
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTiposAsistenciaParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTiposAsistenciaParaSelectAsync();

        // Otros asistentes a sesión 
        /// <summary>
        /// Agregar otro asistente a sesión
        /// </summary>
        Task<bool> AgregarOtroAsistenteAsync(OtroAsistenteSesion otroAsistenteSesion);

        /// <summary>
        /// Actualizar otro asistente a sesión
        /// </summary>
        Task<bool> ActualizarOtroAsistenteAsync(OtroAsistenteSesion ModeloOtroAsistente);

        /// <summary>
        /// Eliminar otro asistente a sesión
        /// </summary>
        Task<bool> EliminarOtroAsistenteAsync(int IdOtroAsistente);

        /// <summary>
        /// Obtener listado de todos los otros asistentes a una sesion
        /// </summary>
        Task<IEnumerable<OtroAsistenteSesion>> ObtenerTodosOtroAsistentePorIdSesionAsync(int IdSesion);

        /// <summary>
        /// Obtener otro asistente a sesion por id
        /// </summary>
        Task<OtroAsistenteSesion> ObtenerOtroAsistentePorIdAsync(int idOtroAsistente);

        /// <summary>
        /// Obtener asistentes de sesión para select (Incluyendo directores asistentes y otros asistentes)
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerTodosPorIdSesionParaSelectAsync(int IdSesion);

        /// <summary>
        /// Obtener asistentes de sesión para select (Incluyendo directores asistentes y otros asistentes)
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerTodosPresentesPorIdSesionParaSelectAsync(int IdSesion);
    }
}