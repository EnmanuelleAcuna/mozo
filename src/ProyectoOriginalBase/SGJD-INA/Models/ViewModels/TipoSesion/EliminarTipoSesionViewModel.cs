using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarTipoSesionViewModel {
        // Constructores
        public EliminarTipoSesionViewModel() { }

        public EliminarTipoSesionViewModel(TipoSesion ModeloTipoSesion) {
            Id = ModeloTipoSesion.Id;
            Nombre = ModeloTipoSesion.Descripcion;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}