using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioSeccionViewModel {
        //Constructor
        public InicioSeccionViewModel() { }

        public InicioSeccionViewModel(Seccion SeccionModelo) {
            IdSeccion = SeccionModelo.Id;
            Descripcion = SeccionModelo.Descripcion;
        }


        //Propiedades
        public int IdSeccion;

        [Display(Name = "Descripción")]
        public string Descripcion;

    }
}