using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarCorreoAdicionalViewModel {
        //Constructores
        public EliminarCorreoAdicionalViewModel() { }

        public EliminarCorreoAdicionalViewModel(CorreoAdicional CorreoAdicionalModelo) {
            IdCorreoAdicional = CorreoAdicionalModelo.Id;
            Correo = CorreoAdicionalModelo.Correo;
            Nombre = CorreoAdicionalModelo.Nombre;
        }

        //Atributos
        public int IdCorreoAdicional { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }
    }
}