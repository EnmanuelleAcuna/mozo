using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosInformeSeguimientosResumidoViewModel {
        // Constructores
        public DatosInformeSeguimientosResumidoViewModel() { }

        public DatosInformeSeguimientosResumidoViewModel(InformeSeguimientosResumidoDTO ModeloInforme) {
            TotalSeguimiento = ModeloInforme.TotalSeguimiento;
            SeguimientoNoEjecutado = ModeloInforme.SeguimientoNoEjecutado;
            SeguimientoEnEjecucion = ModeloInforme.SeguimientoEnEjecucion;
            SeguimientoEjecutado = ModeloInforme.SeguimientoEjecutado;
            SeguimientoVencido = ModeloInforme.SeguimientoVencido;
        }


        // Propiedades
        [Display(Name = "Seguimientos")]
        public int TotalSeguimiento { get; set; }

        [Display(Name = "No ejecutados")]
        public int SeguimientoNoEjecutado { get; set; }

        [Display(Name = "En ejecución")]
        public int SeguimientoEnEjecucion { get; set; }

        [Display(Name = "Ejecutados")]
        public int SeguimientoEjecutado { get; set; }

        [Display(Name = "Vencidos")]
        public int SeguimientoVencido { get; set; }
    }
}