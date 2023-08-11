using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ISesionLogic {
        /// <summary>
        /// Agregar una sesion
        /// </summary>
        Task<bool> AgregarAsync(Sesion ModeloSesion);

        /// <summary>
        /// Actualiza una sesion
        /// </summary>
        Task<bool> ActualizarAsync(Sesion ModeloSesion);

        /// <summary>
        /// Eliminar una sesion
        /// </summary>
        Task<bool> EliminarAsync(int IdSesion);

        /// <summary>
        /// Actualiza una sesion y establecerla cómo realizada
        /// </summary>
        Task<bool> RealizadaAsync(int IdSesion);

        /// <summary>
        /// Obtener lista con todos las sesiones
        /// </summary>
        IEnumerable<Sesion> ObtenerTodos();
        Task<IEnumerable<Sesion>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener una Sesion mediante su id
        /// </summary>
        Task<Sesion> ObtenerPorIdAsync(int IdSesion);

        /// <summary>
        /// Obtener lista de sesiones posteriores
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerSesionPosteriorParaSelectAsync(int IdSesion);

        /// <summary>
        /// Verifica que la fecha de la sesion no sea menor a las otras sesiones
        /// </summary>
        Task<bool> VerificarFechaAsync(DateTime Fecha);

        /// <summary>
        /// Obtener lista con todos las sesiones para motrarse en la grafica
        /// </summary>
        IEnumerable<Sesion> ObtenerSesionesGrafico();
    }
}