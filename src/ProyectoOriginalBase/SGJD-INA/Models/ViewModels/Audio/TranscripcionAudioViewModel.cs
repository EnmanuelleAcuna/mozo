using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class TranscripcionAudioViewModel {
        //Constructores
        public TranscripcionAudioViewModel() { }

        public TranscripcionAudioViewModel(Audio Entidad) {
            Nombre = Entidad.Nombre;
            Transcripcion = Entidad.Transcripcion;
        }

        //Atributos
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Transcripción")]
        public string Transcripcion { get; set; }
    }
}