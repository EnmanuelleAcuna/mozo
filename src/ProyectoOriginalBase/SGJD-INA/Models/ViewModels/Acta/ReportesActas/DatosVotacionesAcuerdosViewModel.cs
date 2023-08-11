using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosVotacionesAcuerdosViewModel {
        // Constructor
        public DatosVotacionesAcuerdosViewModel() { }

        public DatosVotacionesAcuerdosViewModel(InformeVotacionesAcuerdosDirectoresDTO ModeloInforme) {
            Anno = ModeloInforme.Fecha;
            IdUsuario = ModeloInforme.IdUsuario;
            NombreUsuario = ModeloInforme.NombreUsuario;
            CantidadAcuerdos = ModeloInforme.CantidadAcuerdos;
            AFavor = ModeloInforme.AFavor;
            EnContra = ModeloInforme.EnContra;
            NoVoto = ModeloInforme.NoVoto;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Anno { get; set; }

        [Display(Name = "Id Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Cantidad de Acuerdos")]
        public int CantidadAcuerdos { get; set; }

        [Display(Name = "A favor")]
        public int AFavor { get; set; }

        [Display(Name = "En contra")]
        public int EnContra { get; set; }

        [Display(Name = "No votó")]
        public int NoVoto { get; set; }
    }
}