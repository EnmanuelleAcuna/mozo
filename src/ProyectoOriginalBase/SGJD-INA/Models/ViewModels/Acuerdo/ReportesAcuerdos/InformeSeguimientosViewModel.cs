using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeSeguimientosViewModel {
        // Constructores
        public InformeSeguimientosViewModel() { }

        public InformeSeguimientosViewModel(IEnumerable<InformeSeguimientosDTO> ModeloInforme, string FechaInicio, string FechaFin) {
            DatosReporte = ModeloInforme.Select(datos => new DatosInformeSeguimientosViewModel(datos)).ToList();
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }

        // Propiedades
        [Display(Name = "Fecha inicial")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        public string FechaFin { get; set; }

        public IEnumerable<DatosInformeSeguimientosViewModel> DatosReporte { get; set; }
    }
}