using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class ReportesSesionesController : Controller {
        // Atributos y constructor 
        private readonly IReportesSesionesLogic Reporte;
        private readonly ISesionLogic Sesion;
        private readonly IEstadoLogic Estado;

        public ReportesSesionesController(IReportesSesionesLogic Reporte, ISesionLogic Sesion, IEstadoLogic Estado, ISeguimientoLogic Seguimiento) {
            this.Reporte = Reporte;
            this.Sesion = Sesion;
            this.Estado = Estado;
        }

        [HttpGet]
        public async Task<JsonResult> InformeGraficoSesiones() {
            try {
                var TareaObtenerInformeGraficoSesiones = Reporte.ObtenerInformeGraficoSesionesAsync();
                InformeGraficoSesionesDTO DatosInformeGraficoSesiones = await TareaObtenerInformeGraficoSesiones;

                InformeGraficoSesionesViewModel Modelo = new InformeGraficoSesionesViewModel(DatosInformeGraficoSesiones);

                return Json(new { Modelo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}