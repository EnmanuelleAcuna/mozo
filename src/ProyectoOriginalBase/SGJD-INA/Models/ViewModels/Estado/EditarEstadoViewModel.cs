using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarEstadoViewModel {
        // Constructores
        public EditarEstadoViewModel() { }

        public EditarEstadoViewModel(Estado ModeloEstado) {
            IdEstado = ModeloEstado.Id;
            Nombre = ModeloEstado.Nombre;
            Descripcion = ModeloEstado.Descripcion;
            CodigoEstado = ModeloEstado.CodigoEstado;
            IdTipoObjeto = ModeloEstado.IdTipoObjeto;
            NombreTipoObjeto = ModeloEstado.TipoObjeto.Descripcion;
            IdAviso = ModeloEstado.IdAviso;
        }

        // Propiedades
        public int IdEstado { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(1000, ErrorMessage = "La descripción del estado no puede ser mayor a {1} caracteres.")]
        public string Descripcion { get; set; }

        [Display(Name = "Código de estado")]
        public string CodigoEstado { get; set; }

        [Display(Name = "Tipo de Objeto")]
        [Required(ErrorMessage = "El tipo de objeto es requerido.")]
        public int IdTipoObjeto { get; set; }

        public string NombreTipoObjeto { get; set; }

        [Display(Name = "Aviso")]
        [Required(ErrorMessage = "El aviso es requerido.")]
        public int IdAviso { get; set; }

        // Métodos
        public Estado Entidad() {
            return new Estado {
                Id = IdEstado,
                Nombre = Nombre,
                Descripcion = Descripcion,
                CodigoEstado = CodigoEstado,
                IdTipoObjeto = IdTipoObjeto,
                IdAviso = IdAviso,
            };
        }

    }
}