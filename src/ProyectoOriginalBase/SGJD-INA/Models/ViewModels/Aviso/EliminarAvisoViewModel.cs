using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarAvisoViewModel {
        // Constructores
        public EliminarAvisoViewModel() { }

        public EliminarAvisoViewModel(Aviso ModeloAviso) {
            Id = ModeloAviso.Id;
            Nombre = ModeloAviso.Nombre;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}