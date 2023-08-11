using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;
using Transcripcion;

namespace SGJD_INA.Models.Core.Implementation {
    public class TranscripcionActaCore : ITranscripcionActaCore {
        // Constructor y dependencias
        public TranscripcionActaCore() { }

        // Métodos públicos
        public async Task<string> TranscribirAsync(byte[] Data) {
            var Tarea = Transcriptor.Transcribir(Data);
            string Transcripcion = await Tarea;
            return Transcripcion;
        }
    }
}