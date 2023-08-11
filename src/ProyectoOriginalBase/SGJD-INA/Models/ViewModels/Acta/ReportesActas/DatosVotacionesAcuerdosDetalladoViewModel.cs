using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosVotacionesAcuerdosDetalladoViewModel {
        // Constructor
        public DatosVotacionesAcuerdosDetalladoViewModel() { }

        public DatosVotacionesAcuerdosDetalladoViewModel(InformeVotacionesAcuerdosDirectoresDetalladoDTO ModeloInforme) {
            Anno = ModeloInforme.Anno;
            IdUsuario = ModeloInforme.IdUsuario;
            NombreUsuario = ModeloInforme.NombreUsuario;
            IdAcuerdo = ModeloInforme.IdAcuerdo;
            Titulo = ModeloInforme.Titulo;
            NumeroVersion = ModeloInforme.NumeroVersion;
            TipoVotacion = ModeloInforme.TipoVotacion;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Anno { get; set; }

        [Display(Name = "Id Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Id ACuerdo")]
        public int IdAcuerdo { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Numero de version")]
        public int NumeroVersion { get; set; }

        [Display(Name = "Tipo votacion")]
        public string TipoVotacion { get; set; }
    }
}