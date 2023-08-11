using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioTipoSesionViewModel {
        //Constructor
        public InicioTipoSesionViewModel(TipoSesion TipoSesionModel) {
            IdTipoSesion = TipoSesionModel.Id;
            Descripcion = TipoSesionModel.Descripcion;
        }

        //Propiedades
        public int IdTipoSesion { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}