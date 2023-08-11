using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarTipoArchivoViewModel {
        #region Propiedades
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El tipo de archivo es requerido.")]
        [StringLength(10, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(255, ErrorMessage = "La descripción no puede ser mayor a {1} caracteres.")]
        public string Descripcion { get; set; }
        #endregion

        #region Métodos
        public TipoArchivo Entidad() {
            return new TipoArchivo {
                Nombre = Nombre,
                Descripcion = Descripcion
            };
        }
        #endregion
    }
}