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
    public class CorreoUnidadAcuerdoController : Controller {
        // Constructor y dependencias
        private readonly ICorreoUnidadAcuerdoLogic CorreoUnidadAcuerdo;
        private readonly IUnidadLogic Unidad;

        public CorreoUnidadAcuerdoController(ICorreoUnidadAcuerdoLogic CorreoUnidadAcuerdo, IUnidadLogic Unidad) {
            this.CorreoUnidadAcuerdo = CorreoUnidadAcuerdo;
            this.Unidad = Unidad;
        }

        // Acciones
        // GET: /CorreoUnidadAcuerdo/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = CorreoUnidadAcuerdo.ObtenerTodosAsync();
                var ListaCorreoUnidadAcuerdo = await TareaObtenerTodos;
                IEnumerable<InicioCorreoUnidadAcuerdoViewModel> CorreoUnidadAcuerdoViewModel = ListaCorreoUnidadAcuerdo.Select(cua => new InicioCorreoUnidadAcuerdoViewModel(cua)).ToList();
                return View(CorreoUnidadAcuerdoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /CorreoUnidadAcuerdo/Agregar/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerListaUnidades = Unidad.ObtenerTodosParaSelectAsync();
                ViewBag.ListaUnidades = await TareaObtenerListaUnidades;
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar correos a unidad para notificación de Acuerdos
        /// </summary>
        // POST: /CorreoUnidadAcuerdo/Agregar?IdArticulo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarCorreoUnidadAcuerdoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo = Modelo.Entidad();

                var TareaAgregarCorreoUnidadAcuerdo = CorreoUnidadAcuerdo.AgregarAsync(ModeloCorreoUnidadAcuerdo);
                bool CorreoUnidadAcuerdoAgregado = await TareaAgregarCorreoUnidadAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Correo de unidad para Acuerdo");

                return Json(new { success = CorreoUnidadAcuerdoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /CorreoUnidadAcuerdo/Editar?IdCorreoUnidadAcuerdo=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Editar(string IdCorreoUnidadAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdCorreoUnidadAcuerdo) || !Funciones.IsNumber(IdCorreoUnidadAcuerdo)) {
                    throw new ArgumentNullException(paramName: nameof(IdCorreoUnidadAcuerdo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerCorreoUnidadAcuerdo = CorreoUnidadAcuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdCorreoUnidadAcuerdo));
                CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo = await TareaObtenerCorreoUnidadAcuerdo;

                if (ModeloCorreoUnidadAcuerdo == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloCorreoUnidadAcuerdo), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerListaUnidades = Unidad.ObtenerTodosParaSelectAsync();
                ViewBag.ListaUnidades = await TareaObtenerListaUnidades;

                EditarCorreoUnidadAcuerdoViewModel Modelo = new EditarCorreoUnidadAcuerdoViewModel(ModeloCorreoUnidadAcuerdo);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un correo de unidad para notificación de Acuerdos
        /// </summary>
        // POST: /CorreoUnidadAcuerdo/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarCorreoUnidadAcuerdoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo = Modelo.Entidad();

                var TareaEditarCorreoUnidadAcuerdo = CorreoUnidadAcuerdo.ActualizarAsync(ModeloCorreoUnidadAcuerdo);
                bool CorreoUnidadAcuerdoEditado = await TareaEditarCorreoUnidadAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Correo de unidad para Acuerdo");

                return Json(new { success = CorreoUnidadAcuerdoEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /CorreoUnidadAcuerdo/Eliminar?IdCorreoUnidadAcuerdo=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Eliminar(string IdCorreoUnidadAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdCorreoUnidadAcuerdo) || !Funciones.IsNumber(IdCorreoUnidadAcuerdo)) {
                    throw new ArgumentNullException(paramName: nameof(IdCorreoUnidadAcuerdo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerCorreoUnidadAcuerdo = CorreoUnidadAcuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdCorreoUnidadAcuerdo));
                CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo = await TareaObtenerCorreoUnidadAcuerdo;

                if (ModeloCorreoUnidadAcuerdo == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloCorreoUnidadAcuerdo), message: Properties.Resources.ModeloNulo);
                }

                EliminarCorreoUnidadAcuerdoViewModel Modelo = new EliminarCorreoUnidadAcuerdoViewModel(ModeloCorreoUnidadAcuerdo);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un CorreoUnidadAcuerdo
        /// </summary>
        // POST: /TipoObjeto/Eliminar/
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarCorreoUnidadAcuerdoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarCorreoUnidadAcuerdo = CorreoUnidadAcuerdo.EliminarAsync(Modelo.IdCorreoUnidadAcuerdo);
                bool CorreoUnidadAcuerdoEliminado = await TareaEliminarCorreoUnidadAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Correo de unidad para Acuerdo");

                return Json(new { success = CorreoUnidadAcuerdoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}