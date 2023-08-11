using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IReportesActasLogic {
        /// <summary>
        /// Obtiener el informe de Actas para gráfico en inicio
        /// </summary>
        Task<IEnumerable<InformeGraficoActasDTO>> ObtenerInformeGraficoActasAsync();

        /// <summary>
        /// Obtiener el informe de asistencia de los miembros de junta directiva
        /// </summary>
        Task<IEnumerable<InformeAsistenciaSesionDirectoresDTO>> ObtenerInformeAsistenciaSesionDirectoresAsync(string Usuario, string Anno);

        /// <summary>
        /// Obtiener el informe de asistencia de los miembros de junta directiva detallado
        /// </summary>
        Task<IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO>> ObtenerInformeAsistenciaSesionDirectoresDetalladoAsync(string Usuario, string Anno);

        /// <summary>
        /// Obtiener el informe de votaciones de acuerdos de miembros de junta directiva 
        /// </summary>
        Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDTO>> ObtenerInformeVotacioAcuerdosDirectoresAsync(string Usuario, string Anno);

        /// <summary>
        /// Obtiener el informe de votaciones de acuerdos de miembros de junta directiva detallado
        /// </summary>
        Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO>> ObtenerInformeVotacioAcuerdosDirectoresDetalladoAsync(string Usuario, string Anno);

        /// <summary>
        /// Obtiener lista de opciones para select de año para informe de gestión                                                         
        /// </summary>
        Task<IEnumerable<SelectListItem>> ObtenerAnnoParaSelectAsync();

    }
}
