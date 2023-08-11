using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class ReportesActasController : Controller {
        // Atributos y constructor 
        private readonly IReportesActasLogic Reporte;
        private readonly IActaLogic Acta;
        private readonly IEstadoLogic Estado;
        private readonly IMiembrosJDLogic MiembrosJD;
        private readonly ISesionLogic Sesion;

        public ReportesActasController(IReportesActasLogic Reporte, IActaLogic Acta, IEstadoLogic Estado, IMiembrosJDLogic MiembrosJD, ISesionLogic Sesion) {
            this.Reporte = Reporte;
            this.Acta = Acta;
            this.Estado = Estado;
            this.MiembrosJD = MiembrosJD;
            this.Sesion = Sesion;
        }

        [HttpGet]
        public async Task<JsonResult> InformeGraficoActas() {
            try {
                var TareaObtenerInformeGraficoActas = Reporte.ObtenerInformeGraficoActasAsync();
                IEnumerable<InformeGraficoActasDTO> DatosInformeGraficoActas = await TareaObtenerInformeGraficoActas;

                IEnumerable<InformeGraficoActasViewModel> Modelo = DatosInformeGraficoActas.Select(d => new InformeGraficoActasViewModel(d)).ToList();

                return Json(new { Modelo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> InformeGestion() {
            try {
                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                ViewBag.ListaMiembros = await TareaObtenerMiembrosJD;

                var TareaObtenerAnnoAcuerdos = Reporte.ObtenerAnnoParaSelectAsync();
                ViewBag.ListaAnnoAcuerdos = await TareaObtenerAnnoAcuerdos;

                var TareaObtenerInformeAsistencia = Reporte.ObtenerInformeAsistenciaSesionDirectoresAsync("", "");
                IEnumerable<InformeAsistenciaSesionDirectoresDTO> ListaInformeGestionAsistencia = await TareaObtenerInformeAsistencia;

                var TareaObtenerInformeVotaciones = Reporte.ObtenerInformeVotacioAcuerdosDirectoresAsync("", "");
                IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> ListaInformeGestionVotaciones = await TareaObtenerInformeVotaciones;

                InformeGestionViewModel Informe = new InformeGestionViewModel(ListaInformeGestionAsistencia, ListaInformeGestionVotaciones);

                return View(Informe);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> InformeGestion(string SelectMiembrosJD, string SelectAnno) {
            try {
                var TareaObtenerInformeAsistencia = Reporte.ObtenerInformeAsistenciaSesionDirectoresAsync(SelectMiembrosJD, SelectAnno);
                IEnumerable<InformeAsistenciaSesionDirectoresDTO> ListaInformeGestionAsistencia = await TareaObtenerInformeAsistencia;

                var TareaObtenerInformeVotaciones = Reporte.ObtenerInformeVotacioAcuerdosDirectoresAsync(SelectMiembrosJD, SelectAnno);
                IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> ListaInformeGestionVotaciones = await TareaObtenerInformeVotaciones;

                InformeGestionViewModel Modelo = new InformeGestionViewModel(ListaInformeGestionAsistencia, ListaInformeGestionVotaciones);
                //IEnumerable<InformeGestionViewModel> Modelo = ListaInformeGestion.Select(IG => new InformeGestionViewModel(IG)).ToList();

                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                ViewBag.ListaMiembros = await TareaObtenerMiembrosJD;

                var TareaObtenerAnnoAcuerdos = Reporte.ObtenerAnnoParaSelectAsync();
                ViewBag.ListaAnnoAcuerdos = await TareaObtenerAnnoAcuerdos;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> InformeGestionDetallado() {
            try {
                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                ViewBag.ListaMiembros = await TareaObtenerMiembrosJD;

                var TareaObtenerAnnoAcuerdos = Reporte.ObtenerAnnoParaSelectAsync();
                ViewBag.ListaAnnoAcuerdos = await TareaObtenerAnnoAcuerdos;

                InformeGestionDetalladoViewModel Informe = new InformeGestionDetalladoViewModel(new List<InformeAsistenciaSesionDirectoresDetalladoDTO>(), new List<InformeVotacionesAcuerdosDirectoresDetalladoDTO>());

                return View(Informe);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> InformeGestionDetallado(string SelectMiembrosJD, string SelectAnno) {
            try {
                var TareaObtenerInformeAsistenciaDetallado = Reporte.ObtenerInformeAsistenciaSesionDirectoresDetalladoAsync(SelectMiembrosJD, SelectAnno);
                IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> ListaInformeGestionAsistencia = await TareaObtenerInformeAsistenciaDetallado;

                var TareaObtenerInformeVotacionesDetallado = Reporte.ObtenerInformeVotacioAcuerdosDirectoresDetalladoAsync(SelectMiembrosJD, SelectAnno);
                IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> ListaInformeGestionVotaciones = await TareaObtenerInformeVotacionesDetallado;

                InformeGestionDetalladoViewModel Modelo = new InformeGestionDetalladoViewModel(ListaInformeGestionAsistencia, ListaInformeGestionVotaciones);

                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                ViewBag.ListaMiembros = await TareaObtenerMiembrosJD;

                var TareaObtenerAnnoAcuerdos = Reporte.ObtenerAnnoParaSelectAsync();
                ViewBag.ListaAnnoAcuerdos = await TareaObtenerAnnoAcuerdos;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

    }
}