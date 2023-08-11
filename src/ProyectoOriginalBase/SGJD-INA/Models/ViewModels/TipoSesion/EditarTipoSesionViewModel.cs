using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarTipoSesionViewModel {
       // Constructores
        public EditarTipoSesionViewModel() { }

        public EditarTipoSesionViewModel(TipoSesion ModeloTipoSesion) {
            Id = ModeloTipoSesion.Id;
            Nombre = ModeloTipoSesion.Descripcion;
        }      

       // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }        

       // Métodos
        public TipoSesion Entidad() {
            return new TipoSesion {
                Id = Id,
                Descripcion = Nombre
            };
        }        
    }
}