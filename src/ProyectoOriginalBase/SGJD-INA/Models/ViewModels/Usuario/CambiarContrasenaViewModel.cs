using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class CambiarContrasenaViewModel {
        public string IdUsuario { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(maximumLength: 40, MinimumLength = 8, ErrorMessage = "La contraseña debe ser mayor a {2} caracteres.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasena { get; set; }
    }
}