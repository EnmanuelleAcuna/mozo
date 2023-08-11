using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class HabilitarInhabilitarUsuarioViewModel {
        public HabilitarInhabilitarUsuarioViewModel() { }

        public HabilitarInhabilitarUsuarioViewModel(ApplicationUser Modelo) {
            Id = Modelo.Id;
            Nombre = Modelo.Nombre;
        }

        public string Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        public string ObtenerInformacion() {
            return "id:" + Id + ", " +
                "nombre" + Nombre;
        }
    }
}