using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InformeGestionDetalladoViewModel {
        // Constructor
        public InformeGestionDetalladoViewModel() { }

        public InformeGestionDetalladoViewModel(IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> ModeloInformeAsistencia, IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> ModeloInformeVotaciones) {
            // Informe de asistencia de miembros de la junta directiva a las sesiones por año
            DatosReporteAsistencia = ModeloInformeAsistencia.Select(datos => new DatosAsistenciaSesionDetalladoViewModel(datos)).ToList();

            // Informe de votaciones de acuerdos de los miembros de la junta directiva  por año
            DatosReporteVotaciones = ModeloInformeVotaciones.Select(datos => new DatosVotacionesAcuerdosDetalladoViewModel(datos)).ToList();
        }

        // Propiedades
        public IEnumerable<DatosAsistenciaSesionDetalladoViewModel> DatosReporteAsistencia { get; set; }

        public IEnumerable<DatosVotacionesAcuerdosDetalladoViewModel> DatosReporteVotaciones { get; set; }
    }
}