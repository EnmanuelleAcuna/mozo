using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeAcuerdosViewModel {
        // Constructores      
        public InformeAcuerdosViewModel() { }

        public InformeAcuerdosViewModel(IEnumerable<InformeAcuerdosDTO> ModeloInformeAcuerdo, string FechaInicio, string FechaFin) {
            DatosReporte = ModeloInformeAcuerdo.Select(datos => new DatosInformeAcuerdosViewModel(datos)).ToList();
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }

        // Propiedades
        [Display(Name = "Fecha inicial")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        public string FechaFin { get; set; }

        public IEnumerable<DatosInformeAcuerdosViewModel> DatosReporte { get; set; }
    }
}