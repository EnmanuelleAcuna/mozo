using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioUnidadViewModel {

        //Constructor
        public InicioUnidadViewModel(Unidad ModeloUnidad) {
            IdUnidad = ModeloUnidad.Id;
            Nombre = ModeloUnidad.Nombre;
            Correo = ModeloUnidad.Correo;
        }

        //Propiedades
        public int IdUnidad { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Correo")]
        public string Correo { get; set; }
    }
}