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
    public class ReportesAcuerdosController : Controller {
        // Atributos y constructor 
        private readonly IReportesAcuerdosLogic Reporte;
        private readonly IAcuerdoLogic Acuerdo;
        private readonly ISeguimientoLogic Seguimiento;
        private readonly IEstadoLogic Estado;

        public ReportesAcuerdosController(IReportesAcuerdosLogic Reporte, IAcuerdoLogic Acuerdo, IEstadoLogic Estado, ISeguimientoLogic Seguimiento) {
            this.Reporte = Reporte;
            this.Acuerdo = Acuerdo;
            this.Seguimiento = Seguimiento;
            this.Estado = Estado;
        }

        // Metodos publicos
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeAcuerdos() {
            try {
                var TareaObtenerListaTipoSeguimiento = Acuerdo.ObtenerTiposSeguimientoParaSelectAsync();
                ViewBag.ListaTipoSeguimiento = await TareaObtenerListaTipoSeguimiento;

                var TareaObtenerEstado = Estado.ObtenerTodosPorNombreObjetoAsync("SGJD_ACU_TAB_ACUERDOS");
                ViewBag.ListaEstado = await TareaObtenerEstado;

                InformeAcuerdosViewModel Lista = new InformeAcuerdosViewModel(new List<InformeAcuerdosDTO>(), "", "");

                return View(Lista);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeAcuerdos(string SelectEstado, string SelectTipoSeguimiento, string FechaInicio, string FechaFin) {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeAcuerdosAsync(SelectEstado, SelectTipoSeguimiento, FechaInicio, FechaFin);
                IEnumerable<InformeAcuerdosDTO> ListaInformeAcuerdo = await TareaObtenerInforme;

                InformeAcuerdosViewModel Modelo = new InformeAcuerdosViewModel(ListaInformeAcuerdo, FechaInicio, FechaFin);

                var TareaObtenerListaTipoSeguimiento = Acuerdo.ObtenerTiposSeguimientoParaSelectAsync();
                ViewBag.ListaTipoSeguimiento = await TareaObtenerListaTipoSeguimiento;

                var TareaObtenerEstado = Estado.ObtenerTodosPorNombreObjetoAsync("SGJD_ACU_TAB_ACUERDOS");
                ViewBag.ListaEstado = await TareaObtenerEstado;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeAcuerdosResumido() {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeAcuerdosResumidoAsync("", "");
                IEnumerable<InformeAcuerdosResumidoDTO> ListaInformeAcuerdo = await TareaObtenerInforme;

                InformeAcuerdosResumidoViewModel Modelo = new InformeAcuerdosResumidoViewModel(ListaInformeAcuerdo, "", "");

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeAcuerdosResumido(string FechaInicio, string FechaFin) {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeAcuerdosResumidoAsync(FechaInicio, FechaFin);
                IEnumerable<InformeAcuerdosResumidoDTO> ListaInformeAcuerdo = await TareaObtenerInforme;

                InformeAcuerdosResumidoViewModel Modelo = new InformeAcuerdosResumidoViewModel(ListaInformeAcuerdo, FechaInicio, FechaFin);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> ResumenEjecucionAcuerdos() {
            try {
                var TareaObtenerInformeEjecucionAcuerdos = Reporte.ObtenerInformeEjecucionAcuerdosAsync();
                IEnumerable<InformeResumenEjecucionAcuerdosDTO> ListaInformeEjecucionAcuerdo = await TareaObtenerInformeEjecucionAcuerdos;

                var TareaObtenerInformeTotalAcuerdos = Reporte.ObtenerInformeTotalAcuerdosAsync();
                IEnumerable<InformeTotalAcuerdosDTO> ListaInformeTotalAcuerdo = await TareaObtenerInformeTotalAcuerdos;

                InformeResumenEjecucionAcuerdosViewModel Modelo = new InformeResumenEjecucionAcuerdosViewModel(ListaInformeEjecucionAcuerdo, ListaInformeTotalAcuerdo, "");

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientos() {
            try {
                var TareaObtenerEstado = Estado.ObtenerTodosPorNombreObjetoAsync("SGJD_ACU_TAB_SEGUIMIENTOS");
                ViewBag.ListaEstado = await TareaObtenerEstado;

                InformeSeguimientosViewModel Lista = new InformeSeguimientosViewModel(new List<InformeSeguimientosDTO>(), "", "");

                return View(Lista);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientos(string SelectEstado, string FechaInicio, string FechaFin) {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeSeguimientosAsync(SelectEstado, FechaInicio, FechaFin);
                IEnumerable<InformeSeguimientosDTO> ListaInformeSeguimiento = await TareaObtenerInforme;

                InformeSeguimientosViewModel Modelo = new InformeSeguimientosViewModel(ListaInformeSeguimiento, "", "");

                var TareaObtenerEstado = Estado.ObtenerTodosPorNombreObjetoAsync("SGJD_ACU_TAB_SEGUIMIENTOS");
                ViewBag.ListaEstado = await TareaObtenerEstado;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientosResumido() {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeSeguimientosResumidoAsync("", "");
                IEnumerable<InformeSeguimientosResumidoDTO> ListaInformeSeguimiento = await TareaObtenerInforme;

                InformeSeguimientosResumidoViewModel Modelo = new InformeSeguimientosResumidoViewModel(ListaInformeSeguimiento, "", "");

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientosResumido(string FechaInicio, string FechaFin) {
            try {
                var TareaObtenerInforme = Reporte.ObtenerInformeSeguimientosResumidoAsync(FechaInicio, FechaFin);
                IEnumerable<InformeSeguimientosResumidoDTO> ListaInformeSeguimiento = await TareaObtenerInforme;

                InformeSeguimientosResumidoViewModel Modelo = new InformeSeguimientosResumidoViewModel(ListaInformeSeguimiento, FechaInicio, FechaFin);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientosVencidos() {
            try {
                var TareaObtenerInformeSeguimientosVencidos = Reporte.ObtenerInformeSeguimientosVencidosAsync();
                IEnumerable<InformeSeguimientosVencidosDTO> ListaInformeSeguimientosVencidos = await TareaObtenerInformeSeguimientosVencidos;

                IEnumerable<InformeSeguimientosVencidosViewModel> Modelo = ListaInformeSeguimientosVencidos.Select(SV => new InformeSeguimientosVencidosViewModel(SV)).ToList();

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho")]
        public async Task<ActionResult> InformeSeguimientosSinFechaVencimiento() {
            try {
                var TareaObtenerInformeSeguimientosSinFecha = Reporte.ObtenerInformeSeguimientosSinFechaVencimientoAsync();
                IEnumerable<InformeSeguimientosSinFechaVencimientoDTO> ListaInformeSeguimientosSinFecha = await TareaObtenerInformeSeguimientosSinFecha;

                IEnumerable<InformeSeguimientosSinFechaVencimientoViewModel> Modelo = ListaInformeSeguimientosSinFecha.Select(SSF => new InformeSeguimientosSinFechaVencimientoViewModel(SSF)).ToList();

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> InformeGraficoAcuerdos() {
            try {
                var TareaObtenerInformeGraficoAcuerdos = Reporte.ObtenerInformeGraficoAcuerdosAsync();
                InformeGraficoAcuerdosDTO DatosInformeGraficoAcuerdos = await TareaObtenerInformeGraficoAcuerdos;

                InformeGraficoAcuerdosViewModel Modelo = new InformeGraficoAcuerdosViewModel(DatosInformeGraficoAcuerdos);

                return Json(new { Modelo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}