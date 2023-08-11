using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarTipoUsuarioViewModel {
        // Propiedades
        [Display(Name = "Tipo de usuario")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre del tipo de usuario no puede ser mayor a {0} caracteres.")]
        public string Nombre { get; set; }

        // Métodos
        public TipoUsuario Entidad() {
            return new TipoUsuario {
                Nombre = Nombre
            };
        }
    }
}