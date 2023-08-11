using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarAudioViewModel {
        //Atributos
        public int IdActa { get; set; }

        [Display(Name = "Nombre del audio")]
        public string Nombre { get; set; }

        public HttpPostedFileBase Audio { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public Audio Entidad() {
            return new Audio() {
                IdActa = IdActa,
                Nombre = Nombre,
                NombreObjeto = NombreObjeto
            };
        }
    }
}