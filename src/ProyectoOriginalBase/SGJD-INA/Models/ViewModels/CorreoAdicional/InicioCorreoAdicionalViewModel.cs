using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioCorreoAdicionalViewModel {
        //Constructor
        public InicioCorreoAdicionalViewModel(CorreoAdicional CorreoAdicionalModelo) {
            IdCorreoAdicional = CorreoAdicionalModelo.Id;
            Correo = CorreoAdicionalModelo.Correo;
            Nombre = CorreoAdicionalModelo.Nombre;
        }

        //Atributos
        public int IdCorreoAdicional { get; set; }

        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}