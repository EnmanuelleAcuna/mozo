using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITranscripcionActaCore {
        /// <summary>
        /// Transcribir audio
        /// </summary>
        Task<string> TranscribirAsync(byte[] Data);
    }
}