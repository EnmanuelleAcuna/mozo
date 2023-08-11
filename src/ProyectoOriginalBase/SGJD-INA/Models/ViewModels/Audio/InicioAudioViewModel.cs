using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAudioViewModel {
        //Constructores
        public InicioAudioViewModel() { }

        public InicioAudioViewModel(Audio Entidad, string NombreObjeto) {
            IdAudio = Entidad.Id;
            IdActa = Entidad.IdActa;
            Nombre = Entidad.Nombre;
            UrlAudio = Entidad.UrlAudio;
            ExtractoTranscripcion = (string.IsNullOrEmpty(Entidad.Transcripcion)) ? string.Empty : Entidad.Transcripcion.Substring(0, 10);
            this.NombreObjeto = NombreObjeto;
        }

        public InicioAudioViewModel(Audio Entidad) {
            IdAudio = Entidad.Id;
            IdActa = Entidad.IdActa;
            Nombre = Entidad.Nombre;
            UrlAudio = Entidad.UrlAudio;
            ExtractoTranscripcion = (string.IsNullOrEmpty(Entidad.Transcripcion)) ? string.Empty : Entidad.Transcripcion.Substring(0, 10);
        }
        //Atributos
        public int IdAudio { get; set; }

        public int IdActa { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        public string UrlAudio { get; set; }

        public string ExtractoTranscripcion { get; set; }

        public string NombreObjeto { get; set; }
    }
}