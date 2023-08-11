using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SGJD_INA.Models.ViewModels {
    public class EditarUsuarioViewModel {
        public EditarUsuarioViewModel() { }

        public EditarUsuarioViewModel(ApplicationUser Modelo) {
            if (Modelo is null) throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);

            IdUsuario = Modelo.Id;
            Cedula = Modelo.Cedula;
            Nombre = Modelo.Nombre;
            Usuario = Modelo.UserName;
            CorreoElectronico = Modelo.Email;
            IdUnidad = Modelo.IdUnidad;
            IdTipoUsuario = Modelo.IdTipoUsuario;
            NombreRol = Modelo.Rol.Name;
        }

        public string IdUsuario { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El número de identificación es requerido.")]
        [StringLength(20, ErrorMessage = "La identificación no puede ser mayor a {1} caracteres.")]
        public string Cedula { get; set; }

        [Display(Name = "Nombre")]
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

        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "Debe seleccionar una unidad.")]
        public int IdUnidad { get; set; }

        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de usuario.")]
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe seleccionar un rol para el usuario.")]
        public string NombreRol { get; set; }

        public ApplicationUser Entidad() {
            CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

            return new ApplicationUser() {
                Id = IdUsuario,
                Cedula = Cedula,
                Nombre = Funciones.ToTitleCase(Nombre), // Aplicar formato correcto a los atributos nombre, en caso de que existan mayúsculas y minúsculas donde no se debe.
                Email = CorreoElectronico.ToLower(CultureInfo), // Aplicar formato correcto a los atributos email, en caso de que existan mayúsculas y minúsculas donde no se debe.
                UserName = Usuario.ToLower(CultureInfo), // Aplicar formato correcto a los atributos username, en caso de que existan mayúsculas y minúsculas donde no se debe.
                IdUnidad = IdUnidad,
                IdTipoUsuario = IdTipoUsuario
            };
        }
    }
}