using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class ActaTranscritaViewModel {
        // Constructores
        public ActaTranscritaViewModel() { }

        public ActaTranscritaViewModel(string Transcripcion) {
            this.Transcripcion = Transcripcion;
        }

        // Propiedades
        [Display(Name = "Audio transcrito")]
        public string Transcripcion { get; set; } = string.Empty;

        [Display(Name = "Audio de la Sesión")]
        [Required(ErrorMessage = "El audio es requerido.")]
        public HttpPostedFileBase Audio { get; set; }
    }
}