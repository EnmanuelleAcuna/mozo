using Microsoft.AspNet.Identity;
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
    public class SesionController : Controller {
        // Constructor y dependencias
        private readonly ISesionLogic Sesion;
        private readonly ITipoSesionLogic TipoSesion;

        public SesionController(ISesionLogic Sesion, ITipoSesionLogic TipoSesion) {
            this.Sesion = Sesion;
            this.TipoSesion = TipoSesion;
        }

        // Acciones
        // GET: /Sesion/Inicio/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Profesional Secretaría Técnica, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Archivo")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerSesiones = Sesion.ObtenerTodosAsync();
                IEnumerable<Sesion> ListaSesiones = await TareaObtenerSesiones;
                IEnumerable<InicioSesionViewModel> ListaSesionViewModel = ListaSesiones.Select(Sesion => new InicioSesionViewModel(Sesion)).ToList();
                return View(ListaSesionViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos Sesion para pasarlo en formato JSON.
        /// </summary>
        // GET: /Sesion/ObtenerDatos
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = Sesion.ObtenerTodosAsync();
                var ListaSesionModel = await TareaObtenerTodos;
                return Json(new { ListaSesionModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Sesion/Agregar/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerTiposSesion = TipoSesion.ObtenerTodosAsync();
                IEnumerable<TipoSesion> ListaTiposSesion = await TareaObtenerTiposSesion;
                ViewBag.ListaTiposSesion = ListaTiposSesion;
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar una Sesión
        /// </summary>
         // POST: /Sesion/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                if (!Funciones.IsDate(Modelo.Fecha)) {
                    return Json(new { success = false, message = "Error: Fecha inválida." }, JsonRequestBehavior.AllowGet);
                }

                Sesion ModeloSesion = new Sesion(Modelo.IdTipoSesion, Funciones.StringToDateTime(Modelo.Fecha), Modelo.Hora, User.Identity.GetUserId());

                var TareaAgregarSesion = Sesion.AgregarAsync(ModeloSesion);
                bool SesionAgregada = await TareaAgregarSesion;

                if (SesionAgregada == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Sesión");
                    return Json(new { success = SesionAgregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "A", "Sesión");
                    return Json(new { success = SesionAgregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Sesion/Editar?IdSesion
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        public async Task<ActionResult> Editar(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSesion = Sesion.ObtenerPorIdAsync(Convert.ToInt32(IdSesion));
                var SesionModel = await TareaObtenerSesion;

                if (SesionModel == null) {
                    throw new ArgumentNullException(paramName: nameof(SesionModel), message: Properties.Resources.ModeloNulo);
                }

                EditarSesionViewModel SesionViewModel = new EditarSesionViewModel(SesionModel);

                var TareaObtenerTipoSesion = TipoSesion.ObtenerTodosAsync();
                ViewBag.ListaTiposSesion = await TareaObtenerTipoSesion;

                return View(SesionViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Editar la información básica de un elemento Sesión
        /// </summary>
        // POST: /Sesion/Editar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                if (!Funciones.IsDate(Convert.ToString(Modelo.Fecha))) {
                    return Json(new { success = false, message = "Error: Fecha inválida." }, JsonRequestBehavior.AllowGet);
                }

                Sesion ModeloSesion = Modelo.Entidad();
                ModeloSesion.IdUsuario = User.Identity.GetUserId(); // Establecer el id del usuario con el usuario actual del sistema

                var TareaEditarSesion = Sesion.ActualizarAsync(ModeloSesion);
                bool SesionEditada = await TareaEditarSesion;

                if (SesionEditada == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Sesión");
                    return Json(new { success = SesionEditada, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Sesión");
                    return Json(new { success = SesionEditada, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener detalles de un elemento Sesión.
        /// </summary>
        // GET: /Sesion/Detalles
        [HttpGet]
        public async Task<ActionResult> Detalles(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSesion = Sesion.ObtenerPorIdAsync(Convert.ToInt32(IdSesion));
                Sesion SesionModel = await TareaObtenerSesion;

                if (SesionModel == null) {
                    throw new ArgumentNullException(paramName: nameof(SesionModel), message: Properties.Resources.ModeloNulo);
                }
                DetalleSesionViewModel Modelo = new DetalleSesionViewModel(SesionModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Verificar fecha de sesion que no exista una fecha mayor
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerificarFecha(string Fecha) { 
            try {
                if (string.IsNullOrEmpty(Fecha)) {
                    throw new ArgumentNullException(paramName: nameof(Fecha), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaVerificarFecha = Sesion.VerificarFechaAsync(Convert.ToDateTime(Fecha));
                bool FechaValida = await TareaVerificarFecha;

                return Json(new { success = FechaValida }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Sesion/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> Eliminar(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSesion = Sesion.ObtenerPorIdAsync(Convert.ToInt32(IdSesion));
                Sesion ModeloSesion = await TareaObtenerSesion;

                if (ModeloSesion == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloSesion), message: Properties.Resources.ModeloNulo);
                }

                EliminarSesionViewModel Modelo = new EliminarSesionViewModel(ModeloSesion);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Sesión
        /// </summary>
        //POST: /Sesion/Eliminar
       [HttpPost]
       [ActionName("Eliminar")]
       [AuthorizeConfig(Roles = "Administrador Datasoft")]
       [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarSesionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarSesion = Sesion.EliminarAsync(Modelo.IdSesion);
                bool SesionEliminado = await TareaEliminarSesion;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Sesión");

                return Json(new { success = SesionEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}