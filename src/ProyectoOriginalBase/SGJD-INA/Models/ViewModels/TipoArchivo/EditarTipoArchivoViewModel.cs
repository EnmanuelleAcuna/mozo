using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarTipoArchivoViewModel {
        #region Constructores
        public EditarTipoArchivoViewModel() { }

        public EditarTipoArchivoViewModel(TipoArchivo ModeloTipoArchivo) {
            Id = ModeloTipoArchivo.Id;
            Extension = ModeloTipoArchivo.Nombre;
            Descripcion = ModeloTipoArchivo.Descripcion;
        }
        #endregion

        #region Propiedades
        public int Id { get; set; }

        [Display(Name = "Extensión (No editable)")]
        public string Extension { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(255, ErrorMessage = "La descripción no puede ser mayor a {1} caracteres.")]
        public string Descripcion { get; set; }
        #endregion

        #region Métodos
        public TipoArchivo Entidad() {
            return new TipoArchivo {
                Id = Id,
                Nombre = Extension,
                Descripcion = Descripcion
            };
        }
        #endregion
    }
}