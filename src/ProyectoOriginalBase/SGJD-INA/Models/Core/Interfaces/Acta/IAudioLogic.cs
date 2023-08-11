using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IAudioLogic {

        //Obtener lista de audios por id de un acta
        Task<IEnumerable<Audio>> ObtenerTodosPorIdActaAsync(int IdActa);

        //Obtener audio por id
        Task<Audio> ObtenerPorIdAsync(int IdAudio);

        /// <summary>
        /// Agregar un audio
        /// </summary>
        Task<bool> AgregarAsync(Audio ModeloAudio, HttpPostedFileBase Audio);

        /// <summary>
        /// Actualizar información de un audio
        /// </summary>
        Task<bool> ActualizarInformacionAsync(Audio Entidad);

        //Eliminar un audio
        Task<bool> EliminarAsync(Audio AudioModel);

        /// <summary>
        /// Transcribir un audio
        /// </summary>
        Task<bool> TranscribirAsync(int Id);
    }
}