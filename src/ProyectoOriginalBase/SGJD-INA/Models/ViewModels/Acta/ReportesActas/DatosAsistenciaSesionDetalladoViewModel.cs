using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosAsistenciaSesionDetalladoViewModel {
        // Constructor
        public DatosAsistenciaSesionDetalladoViewModel() { }

        public DatosAsistenciaSesionDetalladoViewModel(InformeAsistenciaSesionDirectoresDetalladoDTO ModeloInforme) {
            Anno = ModeloInforme.Anno;
            IdUsuario = ModeloInforme.IdUsuario;
            NombreUsuario = ModeloInforme.NombreUsuario;
            Descripcion = ModeloInforme.Descripcion;
            IdSesion = ModeloInforme.IdSesion;
            NumeroSesion = ModeloInforme.NumeroSesion;
            FechaSesion = ModeloInforme.FechaHora.ToString("dd/MM/yyyy hh:mm tt");
            Asistencia = ModeloInforme.Asistencia;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Anno { get; set; }

        [Display(Name = "Id Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "TipoSesion")]
        public string Descripcion { get; set; }

        [Display(Name = "Id Sesion")]
        public int IdSesion { get; set; }

        [Display(Name = "Numero sesion")]
        public int NumeroSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public string FechaSesion { get; set; }

        [Display(Name = "Asistencia")]
        public string Asistencia { get; set; }
    }
}