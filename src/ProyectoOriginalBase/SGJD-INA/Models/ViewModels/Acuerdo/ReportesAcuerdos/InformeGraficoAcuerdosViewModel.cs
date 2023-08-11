using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InformeGraficoAcuerdosViewModel {
        // Constructores
        public InformeGraficoAcuerdosViewModel(InformeGraficoAcuerdosDTO Modelo) {
            this.Notificados = Modelo.Notificados;
            this.EnEjecucion = Modelo.EnEjecucion;
            this.Ejecutados = Modelo.Ejecutados;
        }

        // Propiedades
        [Display(Name = "Notificados")]
        public int Notificados { get; set; }

        [Display(Name = "En ejecución")]
        public int EnEjecucion { get; set; }

        [Display(Name = "Ejecutados")]
        public int Ejecutados { get; set; }
    }
}