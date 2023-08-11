using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarOtroAsistenteViewModel {
        // Constructores
        public EliminarOtroAsistenteViewModel() { }

        public EliminarOtroAsistenteViewModel(OtroAsistenteSesion ModeloOtroAsistente) {
            Id = ModeloOtroAsistente.Id;
            Nombre = ModeloOtroAsistente.Nombre;
        }


        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}