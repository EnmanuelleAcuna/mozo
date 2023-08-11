using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeSeguimientosResumidoViewModel {
        // Constructores
        public InformeSeguimientosResumidoViewModel() { }

        public InformeSeguimientosResumidoViewModel(IEnumerable<InformeSeguimientosResumidoDTO> ModeloInforme, string FechaInicio, string FechaFin) {
            DatosReporte = ModeloInforme.Select(datos => new DatosInformeSeguimientosResumidoViewModel(datos)).ToList();
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }

        // Propiedades
        [Display(Name = "Fecha inicial")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        public string FechaFin { get; set; }

        public IEnumerable<DatosInformeSeguimientosResumidoViewModel> DatosReporte { get; set; }
    }
}