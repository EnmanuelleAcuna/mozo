using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class ReportesSesionesLogic : IReportesSesionesLogic {

        // Métodos públicos
        public InformeGraficoSesionesDTO ObtenerInformeGraficoSesiones() {
            InformeGraficoSesionesDTO Informe = new SesionDAO().ObtenerInformeGraficoSesiones();
            return Informe;
        }

        public Task<InformeGraficoSesionesDTO> ObtenerInformeGraficoSesionesAsync() {
            Task<InformeGraficoSesionesDTO> Tarea = Task.Run(() => ObtenerInformeGraficoSesiones());
            return Tarea;
        }
    }
}