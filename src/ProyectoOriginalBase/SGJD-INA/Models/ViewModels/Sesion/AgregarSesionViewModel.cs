using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarSesionViewModel {
        //Propiedades
        [Display(Name = "Tipo de sesión")]
        [Required(ErrorMessage = "El tipo de sesión es requerido.")]
        public int IdTipoSesion { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        public string Fecha { get; set; }

        [Display(Name = "Hora")]
        [Required(ErrorMessage = "La hora es requerida.")]
        public string Hora { get; set; }
    }
}