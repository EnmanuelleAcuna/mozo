using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioOtroAsistenteSesionViewModel {
        //Constructor
        public InicioOtroAsistenteSesionViewModel(OtroAsistenteSesion OtroAsistenteSesionModel) {
            IdOtroAsistenteSesion = OtroAsistenteSesionModel.Id;
            IdSesion = OtroAsistenteSesionModel.IdSesion;
            IdTipoUsuario = OtroAsistenteSesionModel.IdTipoUsuario;
            Nombre = OtroAsistenteSesionModel.Nombre;
            Puesto = OtroAsistenteSesionModel.Puesto;
            HoraInicio = OtroAsistenteSesionModel.HoraInicio;
            HoraFin = OtroAsistenteSesionModel.HoraFin;
        }

        //Propiedades       
        public int IdOtroAsistenteSesion { get; set; }

        public int IdSesion { get; set; }

        public int IdTipoUsuario { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Puesto")]
        public string Puesto { get; set; }

        [Display(Name = "Hora entrada")]
        public string HoraInicio { get; set; }

        [Display(Name = "Hora salida")]
        public string HoraFin { get; set; }
    }
}