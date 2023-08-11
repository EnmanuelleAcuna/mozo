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
    [Authorize]
    public class VistaController : Controller {
        // Constructor y dependencias
        private readonly IVistaLogic Vista;
        private readonly IVistaPerfilLogic VistaPerfil;

        public VistaController(IVistaLogic Vista, IVistaPerfilLogic VistaPerfil) {
            this.Vista = Vista;
            this.VistaPerfil = VistaPerfil;
        }

        // GET: /Vista/Inicio
        [HttpGet]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = Vista.ObtenerTodos();
                var ListaVistaModel = await TareaObtenerTodos;
                IEnumerable<InicioVistaViewModel> ListaVistaViewModel = ListaVistaModel.Select(vista => new InicioVistaViewModel(vista)).ToList();
                return View(ListaVistaViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos Vista para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerVista = Vista.ObtenerTodos();
                var ListaVistaModel = await TareaObtenerVista;
                return Json(new { data = ListaVistaModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de perfiles que tienen acceso a la vista.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> PerfilesVista(int? Id) {
            try {
                if (Id == null) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerPerfil = VistaPerfil.ObtenerPerfilesPorIdVista(Convert.ToInt32(Id));
                var TareaObtenerVista = Vista.ObtenerPorId(Convert.ToInt32(Id));

                var ListaPerfilModel = await TareaObtenerPerfil;
                var VistaModel = await TareaObtenerVista;

                if (ListaPerfilModel == null) {
                    throw new ArgumentNullException(paramName: nameof(ListaPerfilModel), message: Properties.Resources.ModeloNulo);
                }

                ViewBag.Vista = VistaModel;

                return View(ListaPerfilModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerVistasProyecto = Vista.ObtenerVistasProyectoSinAsignar();
                var ListaVistaProyecto = await TareaObtenerVistasProyecto;
                ViewBag.Vista = ListaVistaProyecto;

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un elemento Vista
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(Vista vistaModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool CrearVista = false;
                var TarearAgregarVista = Vista.Agregar(vistaModel);
                CrearVista = await TarearAgregarVista;

                string Mensaje = Funciones.ObtenerMensajeExito("C", "Vista");

                return Json(new { success = CrearVista, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Editar(string IdVista) {
            try {
                if (string.IsNullOrEmpty(IdVista)) {
                    throw new ArgumentNullException(paramName: nameof(IdVista), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerVista = Vista.ObtenerPorId(Convert.ToInt32(IdVista));
                var VistaModel = await TareaObtenerVista;

                if (VistaModel == null) {
                    throw new ArgumentNullException(paramName: nameof(VistaModel), message: Properties.Resources.ModeloNulo);
                }

                return View(VistaModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento Vista
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(Vista vistaModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool ActualizarVista = false;
                var TareaEditarVista = Vista.Actualizar(vistaModel);
                ActualizarVista = await TareaEditarVista;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Vista");

                return Json(new { success = ActualizarVista, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(string IdVista) {
            try {
                if (string.IsNullOrEmpty(IdVista)) {
                    throw new ArgumentNullException(paramName: nameof(IdVista), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerVista = Vista.ObtenerPorId(Convert.ToInt32(IdVista));
                var VistaModel = await TareaObtenerVista;

                if (VistaModel == null) {
                    throw new ArgumentNullException(paramName: nameof(VistaModel), message: Properties.Resources.ModeloNulo);
                }

                return View(VistaModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        ///  Eliminar un elemento Vista.
        /// </summary>
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(Vista vistaModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EliminarVista = false;
                var TareaEliminarVista = Vista.Eliminar(vistaModel);
                EliminarVista = await TareaEliminarVista;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Vista");

                return Json(new { success = EliminarVista, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}