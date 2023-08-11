using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosInformeAcuerdosResumidoViewModel {
        // Constructores
        public DatosInformeAcuerdosResumidoViewModel() { }

        public DatosInformeAcuerdosResumidoViewModel(InformeAcuerdosResumidoDTO ModeloInforme) {
            TotalAcuerdo = ModeloInforme.TotalAcuerdo;
            Seguimiento = ModeloInforme.Seguimiento;
            AcuEjecucion = ModeloInforme.AcuEjecucion;
            AcuEjecutado = ModeloInforme.AcuEjecutado;
        }

        // Propiedades
        public int TotalAcuerdo { get; set; }

        [Display(Name = "Acuerdos con seguimiento")]
        public int Seguimiento { get; set; }

        [Display(Name = "Acuerdos en ejecución")]
        public int AcuEjecucion { get; set; }

        [Display(Name = "Acuerdos ejecutados")]
        public int AcuEjecutado { get; set; }
    }
}