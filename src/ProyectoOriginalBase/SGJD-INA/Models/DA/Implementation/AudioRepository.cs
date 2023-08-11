using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class AudioRepository : IAudioRepository {
        public async Task<Audio> AgregarAsync(Audio ModeloAudio) {
            var AudioBD = ModeloAudio.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                AudioBD = ContextoBD.SGJD_ADM_TAB_AUDIOS.Add(AudioBD);
                await ContextoBD.SaveChangesAsync();

                //Asignar el id al modelo
                ModeloAudio.Id = AudioBD.LLP_Id;
            }

            return ModeloAudio;
        }

        public async Task<bool> ActualizarAsync(Audio Entidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var AudioBD = Entidad.BD();

                ContextoBD.Entry(AudioBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }
    }
}