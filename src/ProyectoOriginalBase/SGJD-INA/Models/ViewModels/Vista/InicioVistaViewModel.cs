using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioVistaViewModel {
        //Constructor
        public InicioVistaViewModel(Vista VistaModel) {
            IdVista = VistaModel.Id;
            NombreVista = VistaModel.NombreVista;
            DireccionVista = VistaModel.DireccionVista;
        }

        //Propiedades
        public int IdVista { get; set; }

        [Display(Name = "Nombre vista")]
        public string NombreVista { get; set; }

        [Display(Name = "Rutas")]
        public string DireccionVista { get; set; }
    }
}