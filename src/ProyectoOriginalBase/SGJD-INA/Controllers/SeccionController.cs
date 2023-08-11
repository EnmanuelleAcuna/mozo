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
    public class SeccionController : Controller {
        // Constructor y dependencias
        private readonly ISeccionLogic Seccion;

        public SeccionController(ISeccionLogic Seccion) {
            this.Seccion = Seccion;
        }

        // GET: /Seccion/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<ActionResult> Inicio() {
            try {
                var ObtenerTodasSecciones = Seccion.ObtenerTodosAsync();
                var ListaSecciones = await ObtenerTodasSecciones;
                IEnumerable<InicioSeccionViewModel> ListaSeccionesModel = ListaSecciones.Select(Seccion => new InicioSeccionViewModel(Seccion)).ToList();
                return View(ListaSeccionesModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos Seccion para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = Seccion.ObtenerTodosAsync();
                var ListaSeccionModel = await TareaObtenerTodos;
                return Json(new { data = ListaSeccionModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seccion/Agregar
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
        /// Agregar un elemento Seccion
        /// </summary>
        // POST: /Seccion/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarSeccionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Seccion ModeloSeccion = Modelo.Entidad();
                var TareaAgregarSeccion = Seccion.AgregarAsync(ModeloSeccion);
                bool Secciongregada = await TareaAgregarSeccion;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Sección");

                return Json(new { success = Secciongregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seccion/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<ActionResult> Editar(string IdSeccion) {
            try {
                if (string.IsNullOrEmpty(IdSeccion) || !Funciones.IsNumber(IdSeccion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSeccion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSeccion = Seccion.ObtenerPorIdAsync(Convert.ToInt32(IdSeccion));
                var SeccionModel = await TareaObtenerSeccion;

                if (SeccionModel == null) {
                    throw new ArgumentNullException(paramName: nameof(SeccionModel), message: Properties.Resources.ModeloNulo);
                }

                EditarSeccionViewModel SeccionViewModel = new EditarSeccionViewModel(SeccionModel);

                return View(SeccionViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento Seccion
        /// </summary>
        // POST: /Seccion/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarSeccionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Seccion ModeloSeccion = Modelo.Entidad();
                var TareaEditarSeccion = Seccion.ActualizarAsync(ModeloSeccion);
                bool SeccionEditada = await TareaEditarSeccion;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Sección");

                return Json(new { success = SeccionEditada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seccion/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> Eliminar(string IdSeccion) {
            try {
                if (string.IsNullOrEmpty(IdSeccion) || !Funciones.IsNumber(IdSeccion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSeccion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSeccion = Seccion.ObtenerPorIdAsync(Convert.ToInt32(IdSeccion));
                Seccion ModeloSeccion = await TareaObtenerSeccion;

                if (ModeloSeccion == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloSeccion), message: Properties.Resources.ModeloNulo);
                }

                EliminarSeccionViewModel Modelo = new EliminarSeccionViewModel(ModeloSeccion);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Sección
        /// </summary>
        // POST: /Sección/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarSeccionViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarSeccion = Seccion.EliminarAsync(Modelo.IdSeccion);
                bool SeccionEliminado = await TareaEliminarSeccion;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Sección");

                return Json(new { success = SeccionEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}