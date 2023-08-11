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
    public class BitacoraController : Controller {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public BitacoraController(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }

        #region Bitacora
        // Acciones
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor")]
        public ActionResult Inicio() {
            try {
                InicioBitacoraViewModel ModeloVacio = new InicioBitacoraViewModel();
                return View(ModeloVacio);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con los registros de Bitácora para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor")]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerRegistrosBitacora = Bitacora.ObtenerTodosAsync();
                IEnumerable<Bitacora> ListaBitacora = await TareaObtenerRegistrosBitacora;
                IEnumerable<InicioBitacoraViewModel> Lista = ListaBitacora.Select(b => new InicioBitacoraViewModel(b)).ToList();
                return Json(new { data = Lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los lista con todos los elementos Bitacora, filtrado según un rango de fechas para pasarlo en formato Json.
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor")]
        public async Task<JsonResult> ObtenerBitacoraPorRangoFecha(DateTime FechaInicio, DateTime FechaFin) {
            try {
                if (FechaInicio == null || FechaFin == null) {
                    return Json(new { data = string.Empty, message = "Error: Datos inválidos." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerBitacorPorRangoFecha = Bitacora.ObtenerPorRangoFechaAsync(FechaInicio, FechaFin);
                IEnumerable<BitacoraPorFechaDTO> ListaBitacoraPorRangoFecha = await TareaObtenerBitacorPorRangoFecha;

                IEnumerable<InicioBitacoraViewModel> Modelo = ListaBitacoraPorRangoFecha.Select(b => new InicioBitacoraViewModel(b)).ToList();

                string Mensaje = Funciones.ObtenerMensajeExito("B", Convert.ToString(ListaBitacoraPorRangoFecha.Count()) + " registros en la bitácora");

                return Json(new { data = Modelo, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener un elemento Bitácora.
        /// </summary>
        // GET: /Bitacora/Detalles/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor")]
        public async Task<ActionResult> Detalles(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerRegistroBitacora = Bitacora.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Bitacora ModeloBitacora = await TareaObtenerRegistroBitacora;

                DetalleBitacoraViewModel Modelo = new DetalleBitacoraViewModel(ModeloBitacora);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Log
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public ActionResult Log() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los datos con los registros de el Log para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public JsonResult ObtenerDatosLog() {
            try {
                var tareaObtenerLog = Bitacora.ObtenerLog();
                var listaLog = tareaObtenerLog;
                return Json(new { data = listaLog }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Log Errores       
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> LogErrores() {
            try {
                var TareaObtenerTodos = Bitacora.ObtenerTodosErroresAsync();
                var ListaErrores = await TareaObtenerTodos;
                IEnumerable<ErroresViewModel> ListaErroresModel = ListaErrores.Select(Error => new ErroresViewModel(Error)).ToList();
                return View(ListaErroresModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion
    }
}