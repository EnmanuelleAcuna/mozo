using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarCorreoAdicionalViewModel {
        //Constructor
        public AgregarCorreoAdicionalViewModel() { }

        public AgregarCorreoAdicionalViewModel(CorreoAdicional CorreoAdicionalModelo) {
            Correo = CorreoAdicionalModelo.Correo;
            Nombre = CorreoAdicionalModelo.Nombre;
        }

        //Atributos
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo es requerido.")]
        public string Correo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string Nombre { get; set; }

        //Métodos
        public CorreoAdicional Entidad() {
            return new CorreoAdicional {
                Correo = Correo,
                Nombre = Nombre
            };
        }
    }
}