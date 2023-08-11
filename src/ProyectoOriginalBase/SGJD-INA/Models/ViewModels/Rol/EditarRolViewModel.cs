using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarRolViewModel {
        public EditarRolViewModel() { }

        public EditarRolViewModel(ApplicationRole Entidad) {
            if (Entidad is null) throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);

            IdRol = Entidad.Id;
            Nombre = Entidad.Name;
        }

        public string IdRol { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }

        public ApplicationUser Entidad() {
            return new ApplicationUser() {
                Id = IdRol,
                Nombre = Funciones.ToTitleCase(Nombre) // Aplicar formato correcto al atributo nombre, en caso de que existan mayúsculas y minúsculas donde no se debe.

            };
        }
    }
}