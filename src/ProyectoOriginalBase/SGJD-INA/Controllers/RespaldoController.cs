using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    [Authorize]
    public class RespaldoController : Controller {
        // Constructor y dependencias
        private IRespaldoLogic Respaldo;

        public RespaldoController(IRespaldoLogic Respaldo) {
            this.Respaldo = Respaldo;
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public ActionResult Inicio() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerRegistrosRespaldos = Respaldo.ObtenerTodosAsync();
                IEnumerable<Respaldo> ListaRespaldo = await TareaObtenerRegistrosRespaldos;
                IEnumerable<InicioRespaldoViewModel> ListaRespaldoViewModel = ListaRespaldo.Select(r => new InicioRespaldoViewModel(r)).ToList();
                return Json(new { data = ListaRespaldoViewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los lista con todos los elementos Respaldos, filtrado según un rango de fechas para pasarlo en formato Json.
        /// </summary>
        public async Task<JsonResult> ObtenerRespaldosFecha(DateTime fechaInicio, DateTime fechaFin) {
            try {
                if (fechaInicio == null || fechaFin == null) {
                    return Json(new { data = string.Empty, message = "Error: Datos inválidos." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerRespaldos = Respaldo.ObtenerPorRangoFechaAsync(fechaInicio, fechaFin);
                IEnumerable<RespaldoPorFechaDTO> ListaRespaldoPorRangoFecha = await TareaObtenerRespaldos;

                IEnumerable<InicioRespaldoViewModel> Modelo = ListaRespaldoPorRangoFecha.Select(r => new InicioRespaldoViewModel(r)).ToList();

                string Mensaje = Funciones.ObtenerMensajeExito("B", Convert.ToString(ListaRespaldoPorRangoFecha.Count()) + " registros en el respaldo");

                return Json(new { data = Modelo, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un Respaldo.
        /// </summary>
        public async Task<JsonResult> Agregar() {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool crearRespaldo = false;
                var tareaAgregarRepaldo = Respaldo.Agregar();
                crearRespaldo = await tareaAgregarRepaldo;

                string mensaje = Funciones.ObtenerMensajeExito("E", "Respaldo");

                return Json(new { success = crearRespaldo, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Descargar(string IdRespaldo) {
            try {
                if (string.IsNullOrEmpty(IdRespaldo) || !Funciones.IsNumber(IdRespaldo)) {
                    return RedirectToAction("Error404", "Error");
                }

                var TareaObtenerRespaldo = Respaldo.ObtenerPorIdAsync(Convert.ToInt32(IdRespaldo));
                var ModeloRespaldo = await TareaObtenerRespaldo;

                var TareaDescargarRespaldo = Respaldo.ObtenerZip(ModeloRespaldo.NombreRespaldo);
                byte[] TomoZip = await TareaDescargarRespaldo;

                if (TomoZip != null && TomoZip.Length > 0) {
                    return File(TomoZip, System.Net.Mime.MediaTypeNames.Application.Octet, ModeloRespaldo.NombreRespaldo);
                }
                else {
                    return RedirectToAction("Error404", "Error");
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}