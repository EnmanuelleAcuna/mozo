using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class CrearUsuarioViewModel {
        [Display(Name = "No. de identificación")]
        [Required(ErrorMessage = "El número de identificación es requerido.")]
        [StringLength(20, ErrorMessage = "La identificación no puede ser mayor a {1} caracteres.")]
        public string Cedula { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Usuario { get; set; }

        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Debe digitar una dirección de correo electrónico válida.")]
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [StringLength(100, ErrorMessage = "El correo no puede ser mayor a {1} caracteres.")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(maximumLength: 40, MinimumLength = 6, ErrorMessage = "La contraseña debe ser mayor a {2} caracteres.")]
        public string Contrasena { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasena { get; set; }

        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "Debe seleccionar una unidad.")]
        public int IdUnidad { get; set; }

        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de usuario.")]
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe seleccionar un perfil de usuario.")]
        public string NombreRol { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "cedula" + Cedula + ", " +
                "nombre" + Nombre + ", " +
                "correo" + CorreoElectronico + ", " +
                "username" + Usuario + ", " +
                "idUnidad:" + IdUnidad + ", " +
                "idTipoUsuario" + IdTipoUsuario + ", " +
                "rol" + NombreRol;
        }

        public ApplicationUser Entidad() {
            return new ApplicationUser {
                Cedula = Cedula,
                Nombre = Nombre,
                Email = CorreoElectronico.ToLower(),
                UserName = Usuario.ToLower(),
                IdUnidad = IdUnidad,
                IdTipoUsuario = IdTipoUsuario
            };
        }
    }
}