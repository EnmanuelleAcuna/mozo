using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ICorreoAdicionalLogic {
        /// <summary>
        /// Obtener todos los correos adicionales para notificacion de acuerdos
        /// </summary>
        IEnumerable<CorreoAdicional> ObtenerTodos();
        Task<IEnumerable<CorreoAdicional>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los correos adicionales para usar en control SelectList
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTodosParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync();

        /// <summary>
        /// Obtener correo adicional para notificacion de acuerdos por su id
        /// </summary>
        Task<CorreoAdicional> ObtenerPorIdAsync(int IdCorreoAdicional);

        /// <summary>
        /// Agregar un correo adicional para notificacion de acuerdos
        /// </summary>
        Task<bool> AgregarAsync(CorreoAdicional ModeloCorreoAdicional);

        /// <summary>
        /// Eliminar un correo adicional
        /// </summary>
        Task<bool> EliminarAsync(int IdCorreoAdicional);
    }
}