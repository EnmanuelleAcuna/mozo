using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarRolViewModel {
        #region Propiedades
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public ApplicationRole Entidad() {
            return new ApplicationRole {
                Name = Funciones.ToTitleCase(Nombre) // Aplicar formato correcto al atributo nombre, en caso de que existan mayúsculas y minúsculas donde no se debe.
            };
        }
        #endregion
    }
}