using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IReportesAcuerdosLogic {
        /// <summary>
        /// Obtiener el informe de acuerdos detallado
        /// </summary>
        Task<IEnumerable<InformeAcuerdosDTO>> ObtenerInformeAcuerdosAsync(string Estado, string TipoSeguimiento, string FechaInicio, string FechaFin);

        /// <summary>
        /// Obtiener el informe de acuerdos resumido
        /// </summary>
        Task<IEnumerable<InformeAcuerdosResumidoDTO>> ObtenerInformeAcuerdosResumidoAsync(string FechaInicio, string FechaFin);

        /// <summary>
        /// Obtiener el informe resumen de ejecucion de acuerdos
        /// </summary>;
        Task<IEnumerable<InformeResumenEjecucionAcuerdosDTO>> ObtenerInformeEjecucionAcuerdosAsync();

        /// <summary>
        /// Obtiener el informe resumen de total de acuerdos
        /// </summary>
        Task<IEnumerable<InformeTotalAcuerdosDTO>> ObtenerInformeTotalAcuerdosAsync();

        /// <summary>
        /// Obtiener el informe de seguimiento detallado
        /// </summary>
        Task<IEnumerable<InformeSeguimientosDTO>> ObtenerInformeSeguimientosAsync(string Estado, string FechaInicio, string FechaFin);

        /// <summary>
        /// Obtiener el informe de seguimiento resumido
        /// </summary>
        Task<IEnumerable<InformeSeguimientosResumidoDTO>> ObtenerInformeSeguimientosResumidoAsync(string FechaInicio, string FechaFin);

        /// <summary>
        /// Obtiener el informe de seguimiento vencidos
        /// </summary>
        Task<IEnumerable<InformeSeguimientosVencidosDTO>> ObtenerInformeSeguimientosVencidosAsync();

        /// <summary>
        /// Obtiener el informe de seguimiento sin fecha de vencimiento
        /// </summary>
        Task<IEnumerable<InformeSeguimientosSinFechaVencimientoDTO>> ObtenerInformeSeguimientosSinFechaVencimientoAsync();

        /// <summary>
        /// Obtiener el informe de acuerdos para grafico en inicio
        /// </summary>
        Task<InformeGraficoAcuerdosDTO> ObtenerInformeGraficoAcuerdosAsync();
    }
}
