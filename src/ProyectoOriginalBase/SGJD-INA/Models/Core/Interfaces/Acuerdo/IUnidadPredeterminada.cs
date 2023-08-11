using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUnidadPredeterminadaLogic {
        /// <summary>
        /// Obtener todas las unidades predeterminadas para informacion
        /// </summary>
        Task<IEnumerable<UnidadPredeterminada>> ObtenerTodasAsync();

        /// <summary>
        /// Agregar una unidad predeterminada para informacion
        /// </summary>
        Task<bool> AgregarAsync(int IdUnidad);

        /// <summary>
        /// Eliminar una unidad predeterminada para informacion
        /// </summary>
        Task<bool> EliminarAsync(int IdUnidad);
    }
}