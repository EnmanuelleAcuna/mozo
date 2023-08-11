using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeAcuerdosResumidoViewModel {
        // Constructores
        public InformeAcuerdosResumidoViewModel() { }

        public InformeAcuerdosResumidoViewModel(IEnumerable<InformeAcuerdosResumidoDTO> ModeloInforme, string FechaInicio, string FechaFin) {
            this.DatosReporte = ModeloInforme.Select(datos => new DatosInformeAcuerdosResumidoViewModel(datos)).ToList();
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }

        // Propiedades
        [Display(Name = "Fecha inicial")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        public string FechaFin { get; set; }

        public IEnumerable<DatosInformeAcuerdosResumidoViewModel> DatosReporte { get; set; }
    }
}