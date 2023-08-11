using SGJD_INA.Models.DTO;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IReportesSesionesLogic {
        /// <summary>
        /// Obtiener el informe de Sesiones para gráfico en inicio
        /// </summary>
        Task<InformeGraficoSesionesDTO> ObtenerInformeGraficoSesionesAsync();
    }
}
