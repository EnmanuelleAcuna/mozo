using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarTipoObjetoViewModel {
        // Propiedades
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Descripcion { get; set; }

        [Display(Name = "Tabla")]
        [Required(ErrorMessage = "La tabla es requerida.")]
        public string NombreTabla { get; set; }

        [Display(Name = "Parámetro de edición")]
        public bool ParametroEdicion { get; set; }

        [Display(Name = "Encabezado y pie de página")]
        public bool ParametroEncabezadoPie { get; set; }

        [Display(Name = "Consecutivo")]
        [Required(ErrorMessage = "El número de consecutivo es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El consecutivo debe ser mayor a {1}")]
        public int Consecutivo { get; set; } = 0;

        [Display(Name = "Nomenclatura")]
        [Required(ErrorMessage = "La nomenclatura es requerida.")]
        public string Nomenclatura { get; set; }

        // Métodos
        public TipoObjeto Entidad() {
            return new TipoObjeto {
                Descripcion = Descripcion,
                Consecutivo = Consecutivo,
                NombreTabla = NombreTabla,
                Nomenclatura = Nomenclatura,
                ParametroEdicion = ParametroEdicion,
                ParametroEncabezadoPie = ParametroEncabezadoPie
            };
        }

    }
}