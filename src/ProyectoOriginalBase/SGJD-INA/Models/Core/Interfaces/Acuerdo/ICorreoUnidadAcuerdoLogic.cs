using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ICorreoUnidadAcuerdoLogic {
        /// <summary>
        /// Obtener todos los correos de unidades para Acuerdos
        /// </summary>
        Task<IEnumerable<CorreoUnidadAcuerdo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un correo de unidad para Acuerdos por id
        /// </summary>
        Task<CorreoUnidadAcuerdo> ObtenerPorIdAsync(int IdCorreoUnidadAcuerdo);

        /// <summary>
        /// Agregar un correo de unidad para Acuerdos
        /// </summary>
        Task<bool> AgregarAsync(CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo);

        /// <summary>
        /// Actualizar un correo de unidad para Acuerdos
        /// </summary>
        Task<bool> ActualizarAsync(CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo);

        /// <summary>
        /// Eliminar un correo de unidad para Acuerdos
        /// </summary>
        Task<bool> EliminarAsync(int IdCorreoUnidadAcuerdo);
    }
}