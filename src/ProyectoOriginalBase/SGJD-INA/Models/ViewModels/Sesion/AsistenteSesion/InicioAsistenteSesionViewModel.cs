using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAsistenteSesionViewModel {
        //Constructor
        public InicioAsistenteSesionViewModel(AsistenteSesion AsistenteSesionModel) {
            IdAsistenteSesion = AsistenteSesionModel.Id;
            IdSesion = AsistenteSesionModel.IdSesion;
            IdUsuario = AsistenteSesionModel.IdUsuario;
            Usuario = AsistenteSesionModel.Usuario.Nombre;
            TipoAsistencia = AsistenteSesionModel.TipoAsistencia;
            HoraInicio = AsistenteSesionModel.HoraInicio;
            HoraFin = AsistenteSesionModel.HoraFin;
        }

        //Propiedades       
        public int IdAsistenteSesion { get; set; }

        public int IdSesion { get; set; }

        public string IdUsuario { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Tipo de asistencia")]
        public string TipoAsistencia { get; set; }

        [Display(Name = "Hora entrada")]
        public string HoraInicio { get; set; }

        [Display(Name = "Hora salida")]
        public string HoraFin { get; set; }

    }
}