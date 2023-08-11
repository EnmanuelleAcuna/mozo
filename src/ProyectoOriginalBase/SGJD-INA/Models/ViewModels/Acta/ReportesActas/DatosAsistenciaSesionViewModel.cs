using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosAsistenciaSesionViewModel {
        // Constructor
        public DatosAsistenciaSesionViewModel() { }

        public DatosAsistenciaSesionViewModel(InformeAsistenciaSesionDirectoresDTO ModeloInforme) {
            Anno = ModeloInforme.Anno;
            IdUsuario = ModeloInforme.IdUsuario;
            NombreUsuario = ModeloInforme.NombreUsuario;
            CantidadSesiones = ModeloInforme.CantidadSesiones;
            Presente = ModeloInforme.Presente;
            NoPresente = ModeloInforme.NoPresente;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Anno { get; set; }

        [Display(Name = "Id Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Total de sesiones")]
        public int CantidadSesiones { get; set; }

        [Display(Name = "Presente")]
        public int Presente { get; set; }

        [Display(Name = "No presente")]
        public int NoPresente { get; set; }
    }
}