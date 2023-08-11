using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IOrdenDiaLogic {
        /// <summary>
        /// Agregar un Orden del Día
        /// </summary>
        Task<bool> AgregarAsync(OrdenDia ModeloOrdenDia);

        /// <summary>
        /// Obtener lista de todas las ordenes del día
        /// </summary>
        IEnumerable<OrdenDia> ObtenerTodos();
        Task<IEnumerable<OrdenDia>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un orden de día por id
        /// </summary>
        Task<OrdenDia> ObtenerPorIdAsync(int IdOrdenDia);

        /// <summary>
        /// Obtener un OrdenDia por id de sesion
        /// </summary>
        Task<OrdenDia> ObtenerPorIdSesionAsync(int IdSesion);

        /// <summary>
        /// Obtener un orden de día por id, aprobada con los temas aprobados
        /// </summary>
        Task<OrdenDia> ObtenerAprobadaPorIdAsync(int IdOrdenDia);

        /// <summary>
        /// Obtener un orden de día por id sesion, aprobada con los temas aprobados
        /// </summary>
        Task<OrdenDia> ObtenerAprobadaPorIdSesionAsync(int IdSesion);

        /// <summary>
        /// Actualiza el orden del día
        /// </summary>
        Task<bool> ActualizarAsync(OrdenDia ModeloOrdenDia);

        /// <summary>
        /// Agregar seccion al orden del día
        /// </summary>
        Task<bool> AgregarSeccionAsync(int IdOrdenDia, int IdSeccion);

        /// <summary>
        /// Eliminar una seccion de un orden del día
        /// </summary>
        Task<bool> EliminarSeccionAsync(int OrdenDia, int IdSeccion);

        /// <summary>
        /// Enviar convocatoria de sesión
        /// </summary>
        Task<bool> EnviarConvocatoriaDeSesion(int IdOrdenDia, int IdAviso);

        /// <summary>
        /// Cambiar el estado del orden del día de borrador a visto bueno
        /// </summary>
        Task<bool> EnviarVistoBuenoAsync(int IdOrdenDia);

        /// <summary>
        /// Cambiar el estado del orden del día de visto bueno a convocado
        /// </summary>
        Task<bool> EnviarConvocarAsync(int IdOrdenDia);

        /// <summary>
        /// Cambiar el estado del orden del día a aprobada
        /// </summary>
        Task<bool> AprobarAsync(int IdOrdenDia);
    }
}