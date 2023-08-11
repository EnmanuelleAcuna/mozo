using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    [Authorize]
    public class VistaPerfilController : Controller {
        // Constructor y dependencias
        private readonly IPerfilLogic Perfil;
        private readonly IVistaLogic Vista;
        private readonly IVistaPerfilLogic VistaPerfil;

        public VistaPerfilController(IPerfilLogic Perfil, IVistaLogic Vista, IVistaPerfilLogic VistaPerfil) {
            this.Perfil = Perfil;
            this.Vista = Vista;
            this.VistaPerfil = VistaPerfil;
        }

        // GET: /vistaPerfil/Inicio
        [HttpGet]
        // id corresponde al id del Perfil
        public async Task<ActionResult> Inicio(string id) {
            try {
                if (string.IsNullOrEmpty(id)) {
                    throw new ArgumentNullException(paramName: nameof(id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerPerfil = Perfil.ObtenerPorIdAsync(id);
                var PerfilModel = await TareaObtenerPerfil;

                return View(PerfilModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con los elementos VistaPerfil de un Perfil para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerVistasPerfil(string idPerfil) {
            try {
                var TareaObtenerVistasPerfil = VistaPerfil.ObtenerTodosPorIdPerfilAsync(idPerfil);
                var ListaVistasPerfil = await TareaObtenerVistasPerfil;
                return Json(new { data = ListaVistasPerfil }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Agregar(string idPerfil, string nombrePerfil) {
            try {
                var TareaObtenerVistasSinAsignar = Vista.ObtenerVistasSinAsignar(idPerfil);
                var ListaVistasSinAsignar = await TareaObtenerVistasSinAsignar;

                ViewBag.Perfil = new ApplicationRole() { Id = idPerfil, Name = nombrePerfil };
                ViewBag.ListaVistas = ListaVistasSinAsignar;

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un elemento VistaPerfil.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(VistaPerfil vistaPerfilModel) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool AgregarVistaPerfil = false;
                var TareaAgregarVistaPerfil = VistaPerfil.AgregarAsync(vistaPerfilModel);
                AgregarVistaPerfil = await TareaAgregarVistaPerfil;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Vista");

                return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Editar(string idPerfil, int? idVista) {
            try {
                if (string.IsNullOrEmpty(idPerfil) || idVista == null) {
                    throw new ArgumentNullException(paramName: nameof(idPerfil), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerVistaPerfil = VistaPerfil.ObtenerPorIdAsync(idPerfil, Convert.ToInt32(idVista));
                var VistaPerfilModel = await TareaObtenerVistaPerfil;

                if (VistaPerfilModel == null) {
                    throw new ArgumentNullException(paramName: nameof(VistaPerfilModel), message: Properties.Resources.ModeloNulo);
                }

                return View(VistaPerfilModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento VistaPerfil.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(VistaPerfil vistaPerfilModel) {
            try {
                //if (!ModelState.IsValid)
                //{
                //    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                //}

                bool ActualizarVistaPerfil = false;
                var TareaActualizarVistaPerfil = VistaPerfil.ActualizarAsync(vistaPerfilModel);
                ActualizarVistaPerfil = await TareaActualizarVistaPerfil;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "ParametroEdicion");

                return Json(new { success = ActualizarVistaPerfil, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(string idPerfil, int? idVista) {
            try {
                if (string.IsNullOrEmpty(idPerfil) || idVista == null) {
                    throw new ArgumentNullException(paramName: nameof(idPerfil), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerVistaPerfil = VistaPerfil.ObtenerPorIdAsync(idPerfil, Convert.ToInt32(idVista));
                var VistaPerfilModel = await TareaObtenerVistaPerfil;

                if (VistaPerfilModel == null) {
                    throw new ArgumentNullException(paramName: nameof(VistaPerfilModel), message: Properties.Resources.ModeloNulo);
                }

                return View(VistaPerfilModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un elemento VIstaPerfil, es decir, eliminar una vista de un perfil.
        /// </summary>
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(VistaPerfil vistaPerfilModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EliminarVistaPerfil = false;
                var TareaEliminarVistaPerfil = VistaPerfil.EliminarAsync(vistaPerfilModel);
                EliminarVistaPerfil = await TareaEliminarVistaPerfil;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Vista");

                return Json(new { success = EliminarVistaPerfil, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}