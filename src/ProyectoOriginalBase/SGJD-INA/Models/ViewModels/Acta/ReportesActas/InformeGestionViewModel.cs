using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeGestionViewModel {
        // Constructor
        public InformeGestionViewModel() { }

        public InformeGestionViewModel(IEnumerable<InformeAsistenciaSesionDirectoresDTO> ModeloInformeAsistencia, IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> ModeloInformeVotaciones) {
            // Informe de asistencia de miembros de la junta directiva a las sesiones por año
            DatosReporteAsistencia = ModeloInformeAsistencia.Select(datos => new DatosAsistenciaSesionViewModel(datos)).ToList();

            // Informe de votaciones de acuerdos de los miembros de la junta directiva  por año
            DatosReporteVotaciones = ModeloInformeVotaciones.Select(datos => new DatosVotacionesAcuerdosViewModel(datos)).ToList();
        }

        // Propiedades
        public IEnumerable<DatosAsistenciaSesionViewModel> DatosReporteAsistencia { get; set; }

        public IEnumerable<DatosVotacionesAcuerdosViewModel> DatosReporteVotaciones { get; set; }
    }
}