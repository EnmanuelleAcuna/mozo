using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IAudioRepository {
        /// <summary>
        /// Agregar un audio en la base/fuente de datos
        /// </summary>
        Task<Audio> AgregarAsync(Audio Entidad);

        /// <summary>
        /// Actualizar información de un audio en la base/fuente de datos
        /// </summary>
        Task<bool> ActualizarAsync(Audio Entidad);
    }
}