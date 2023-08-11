using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioCorreoUnidadAcuerdoViewModel {
        //Constructores
        public InicioCorreoUnidadAcuerdoViewModel(CorreoUnidadAcuerdo CorreoUnidadAcuerdoModelo) {
            IdCorreoUnidadAcuerdo = CorreoUnidadAcuerdoModelo.Id;
            NombreUnidad = CorreoUnidadAcuerdoModelo.Unidad.Nombre;
            Nombre = CorreoUnidadAcuerdoModelo.Nombre;
            Correo = CorreoUnidadAcuerdoModelo.Correo;
        }

        //Atributos
        public int IdCorreoUnidadAcuerdo { get; set; }

        [Display(Name = "Unidad")]
        public string NombreUnidad { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
    }
}