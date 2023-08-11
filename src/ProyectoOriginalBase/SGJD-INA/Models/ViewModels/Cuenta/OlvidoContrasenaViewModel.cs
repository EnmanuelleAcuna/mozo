using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class OlvidoContrasenaViewModel {
        [EmailAddress(ErrorMessage = "Debe digitar una dirección de correo electrónico válida.")]
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [Display(Name = "Correo electrónico")]
        public string CorreoElectronico { get; set; }
    }
}