using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IDetalleSeguimientoLogic {
        /// <summary>
        /// Agregar elemento Detalle a seguimiento, se retorna un entero 
        /// por que en vista se necesita para modificar etiquetas del DOM.
        /// </summary>
        Task<int> AgregarAsync(DetalleSeguimiento ModeloDetalleSeguimiento);

        /// <summary>
        /// Actualizar un detalle/avance de seguimiento.
        /// </summary>
        Task<bool> ActualizarAsync(DetalleSeguimiento ModeloDetalleSeguimiento);

        /// <summary>
        /// Eliminar un avance de seguimiento por su id.
        /// </summary>
        Task<bool> EliminarAsync(int IdDetalle);
    }
}