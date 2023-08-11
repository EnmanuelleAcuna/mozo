using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class TipoSesionController : Controller {
        // Constructor y dependencias
        private readonly ITipoSesionLogic TipoSesion;

        public TipoSesionController(ITipoSesionLogic TipoSesion) {
            this.TipoSesion = TipoSesion;
        }

        // GET: /TipoSesion/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Archivo")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = TipoSesion.ObtenerTodosAsync();
                var ListaTipoSesionModel = await TareaObtenerTodos;
                IEnumerable<InicioTipoSesionViewModel> ListaTipoSesionViewModel = ListaTipoSesionModel.Select(TipoSesion => new InicioTipoSesionViewModel(TipoSesion)).ToList();
                return View(ListaTipoSesionViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos TipoSesion para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTipoSesion = TipoSesion.ObtenerTodosAsync();
                var ListaTipoSesion = await TareaObtenerTipoSesion;
                return Json(new { data = ListaTipoSesion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoSesion/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public ActionResult Agregar() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar TipoSesion
        /// </summary>
        // POST: /TipoSesion/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarTipoSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoSesion ModeloTipoSesion = Modelo.Entidad();
                var TareaAgregarTipoSesion = TipoSesion.AgregarAsync(ModeloTipoSesion);
                bool TipoSesionAgregado = await TareaAgregarTipoSesion;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Tipo de Sesión");

                return Json(new { success = TipoSesionAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoSesion/Editar?IdTipoSesion=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<ActionResult> Editar(string IdTipoSesion) {
            try {
                if (string.IsNullOrEmpty(IdTipoSesion) || !Funciones.IsNumber(IdTipoSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdTipoSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoSesion = TipoSesion.ObtenerPorIdAsync(Convert.ToInt32(IdTipoSesion));
                TipoSesion TipoSesionModel = await TareaObtenerTipoSesion;

                if (TipoSesionModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoSesionModel), message: Properties.Resources.ModeloNulo);
                }

                EditarTipoSesionViewModel Modelo = new EditarTipoSesionViewModel(TipoSesionModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar TipoSesion.
        /// </summary
        // POST: /TipoSesion/Editar/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<JsonResult> Editar(EditarTipoSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoSesion ModeloTipoSesion = Modelo.Entidad();
                var TareaActualizarTipoSesion = TipoSesion.ActualizarAsync(ModeloTipoSesion);
                bool TipoSesionEditado = await TareaActualizarTipoSesion;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Tipo de Sesión");

                return Json(new { success = TipoSesionEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoSesion/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> Eliminar(string IdTipoSesion) {
            try {
                if (string.IsNullOrEmpty(IdTipoSesion) || !Funciones.IsNumber(IdTipoSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdTipoSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoSesion = TipoSesion.ObtenerPorIdAsync(Convert.ToInt32(IdTipoSesion));
                var TipoSesionModel = await TareaObtenerTipoSesion;

                if (TipoSesionModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoSesionModel), message: Properties.Resources.ModeloNulo);
                }

                EliminarTipoSesionViewModel Modelo = new EliminarTipoSesionViewModel(TipoSesionModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un TipoSesion.
        /// </summary>
        // POST: /TipoObjeto/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarTipoSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarTipoSesion = TipoSesion.EliminarAsync(Modelo.Id);
                bool TipoSesionEliminado = await TareaEliminarTipoSesion;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Tipo de Sesión");

                return Json(new { success = TipoSesionEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}