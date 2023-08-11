using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeResumenEjecucionAcuerdosViewModel {
        // Constructores
        public InformeResumenEjecucionAcuerdosViewModel() { }

        public InformeResumenEjecucionAcuerdosViewModel(IEnumerable<InformeResumenEjecucionAcuerdosDTO> ResumenAcuerdos, IEnumerable<InformeTotalAcuerdosDTO> TotalAcuerdos, string Encabezado) {
            this.ResumenAcuerdos = ResumenAcuerdos.Select(r => new ResumenEjecucionAcuerdosViewModel(r)).ToList();
            this.TotalAcuerdos = TotalAcuerdos.Select(t => new TotalAcuerdosViewModel(t)).ToList();
            this.Encabezado = Encabezado;
        }

        // Propiedades
        public IEnumerable<ResumenEjecucionAcuerdosViewModel> ResumenAcuerdos { get; set; }

        public IEnumerable<TotalAcuerdosViewModel> TotalAcuerdos { get; set; }

        public string Encabezado { get; set; }
    }
}