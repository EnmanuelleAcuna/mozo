using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class UsuarioParaSelectViewModel {
        //Constructor
        public UsuarioParaSelectViewModel(ApplicationUser ModeloUsuario) {
            Id = ModeloUsuario.Id;
            Identificacion = ModeloUsuario.Cedula;
            Nombre = ModeloUsuario.Nombre;
        }

        //Propiedades
        [Display(Name = "Id del usuario")]
        public string Id { get; set; }

        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}