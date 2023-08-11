using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class IniciarSesionViewModel {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [Display(Name = "Usuario o correo electrónico")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un certificado.")]
        [Display(Name = "Certificado")]
        public string Certificado { get; set; }
    }
}