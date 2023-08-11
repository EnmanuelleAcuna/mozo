using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class ResumenEjecucionAcuerdosViewModel {
        // Constructores
        public ResumenEjecucionAcuerdosViewModel() { }

        public ResumenEjecucionAcuerdosViewModel(InformeResumenEjecucionAcuerdosDTO Modelo) {
            Ano = Modelo.Ano;
            Total = Modelo.Total;
            ConSeguimiento = Modelo.ConSeguimiento;
            SeguimientoPermanente = Modelo.SeguimientoPermanente;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Ano { get; set; }

        [Display(Name = "Acuerdos en ejecución")]
        public int Total { get; set; }

        [Display(Name = "Con seguimiento")]
        public int ConSeguimiento { get; set; }

        [Display(Name = "Seguimiento permanente")]
        public int SeguimientoPermanente { get; set; }
    }
}