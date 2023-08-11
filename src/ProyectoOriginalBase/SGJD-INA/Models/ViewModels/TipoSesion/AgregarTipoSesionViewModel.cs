using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarTipoSesionViewModel {
        #region Propiedades
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }
        #endregion

        #region Métodos
        public TipoSesion Entidad() {
            return new TipoSesion {
                Descripcion = Nombre
            };
        }
        #endregion
    }
}