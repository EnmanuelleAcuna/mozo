using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarCorreoUnidadAcuerdoViewModel {
        //Constructor
        public EliminarCorreoUnidadAcuerdoViewModel() { }

        public EliminarCorreoUnidadAcuerdoViewModel(CorreoUnidadAcuerdo CorreoUnidadAcuerdoModelo) {
            IdCorreoUnidadAcuerdo = CorreoUnidadAcuerdoModelo.Id;
            NombreUnidad = CorreoUnidadAcuerdoModelo.Unidad.Nombre;
            Nombre = CorreoUnidadAcuerdoModelo.Nombre;
            Correo = CorreoUnidadAcuerdoModelo.Correo;
        }

        //Atributos
        public int IdCorreoUnidadAcuerdo { get; set; }

        public string NombreUnidad { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }
    }
}