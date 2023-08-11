using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarTipoUsuarioViewModel {
        // Constructores
        public EliminarTipoUsuarioViewModel() { }

        public EliminarTipoUsuarioViewModel(TipoUsuario ModeloTipoUsuario) {
            Id = ModeloTipoUsuario.Id;
            Nombre = ModeloTipoUsuario.Nombre;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}