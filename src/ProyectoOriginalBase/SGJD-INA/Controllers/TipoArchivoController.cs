using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class TipoArchivoController : Controller {
        // Constructor y dependencias
        private readonly ITipoArchivoLogic TipoArchivo;

        public TipoArchivoController(ITipoArchivoLogic TipoArchivo) {
            this.TipoArchivo = TipoArchivo;
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Archivo")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = TipoArchivo.ObtenerTodosAsync();
                var ListaTipoArchivo = await TareaObtenerTodos;
                return View(ListaTipoArchivo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los lista con todos los elementos tipo Archivo para pasarlo en formato Json.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = TipoArchivo.ObtenerTodosAsync();
                IEnumerable<TipoArchivo> ListaTipoArchivoModel = await TareaObtenerTodos;
                return Json(new { data = ListaTipoArchivoModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoArchivo/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerTiposArchivoSinAgergar = TipoArchivo.ObtenerListaTipoArchivoSinAgregar();
                ViewBag.ListaTipoArchivo = await TareaObtenerTiposArchivoSinAgergar;
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un TipoArchivo
        /// </summary>
        // POST: /TipoArchivo/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarTipoArchivoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoArchivo ModeloTipoArchivo = Modelo.Entidad();
                var tareaAgregarTipoArchivo = TipoArchivo.AgregarAsync(ModeloTipoArchivo);
                bool TipoArchivoAgregado = await tareaAgregarTipoArchivo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Tipo Archivo");

                return Json(new { success = TipoArchivoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoArchivo/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoArchivo = TipoArchivo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                TipoArchivo TipoArchivoModel = await TareaObtenerTipoArchivo;

                if (TipoArchivoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoArchivoModel), message: Properties.Resources.ModeloNulo);
                }

                EditarTipoArchivoViewModel Modelo = new EditarTipoArchivoViewModel(TipoArchivoModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un TipoArchivo
        /// </summary>
        // POST: /TipoArchivo/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarTipoArchivoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoArchivo ModeloTipoArchivo = Modelo.Entidad();
                var tareaEditarTipoArchivo = TipoArchivo.ActualizarAsync(ModeloTipoArchivo);
                bool TipoArchivoEditado = await tareaEditarTipoArchivo;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Tipo Archivo");

                return Json(new { success = TipoArchivoEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoArchivo/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerArchivo = TipoArchivo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var TipoArchivoModel = await TareaObtenerArchivo;

                if (TipoArchivoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoArchivoModel), message: Properties.Resources.ModeloNulo);
                }

                EliminarTipoArchivoViewModel Modelo = new EliminarTipoArchivoViewModel(TipoArchivoModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un TipoArchivo.
        /// </summary>
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarTipoArchivoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarTipoArchivo = TipoArchivo.EliminarAsync(Modelo.Id);
                bool TipoArchivoEliminado = await TareaEliminarTipoArchivo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Tipo Archivo");

                return Json(new { success = TipoArchivoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}