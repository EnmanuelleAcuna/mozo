using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    [Authorize]
    public class RepositorioController : Controller {
        // Constructor y dependencias
        private IRepositorioLogic Repositorio;

        public RepositorioController(IRepositorioLogic Repositorio) {
            this.Repositorio = Repositorio;
        }

        [HttpGet]
        public ActionResult Inicio() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los lista con todos los elementos Repositorio para pasarlo en formato Json.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var tareaObtenerTodos = Repositorio.ObtenerTodosAsync();
                var listaRepositorioModel = await tareaObtenerTodos;
                return Json(new { data = listaRepositorioModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
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
        /// Agregar un elemento Repositorio.
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> Agregar(Repositorio repositorioModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool crearRepositorio = false;
                var tareaAgregarRepositorio = Repositorio.Agregar(repositorioModel, true, true);
                crearRepositorio = await tareaAgregarRepositorio;

                string mensaje = Funciones.ObtenerMensajeExito("A", "Repositorio");

                return Json(new { success = crearRepositorio, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int? id) {
            try {
                if (id == null) {
                    throw new ArgumentNullException(paramName: nameof(id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var tareaObtenerRepositorio = Repositorio.ObtenerPorIdAsync(Convert.ToInt32(id));
                var repositorioModel = await tareaObtenerRepositorio;

                if (repositorioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(repositorioModel), message: Properties.Resources.ModeloNulo);
                }

                return View(repositorioModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento Repositorio.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(Repositorio repositorioModel, string nuevoNombre) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var editarRepositorio = false;
                var tareaActualizarRepositorio = Repositorio.Actualizar(repositorioModel, nuevoNombre);
                editarRepositorio = await tareaActualizarRepositorio;

                string mensaje = Funciones.ObtenerMensajeExito("M", "Repositorio");

                return Json(new { success = editarRepositorio, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(int? id) {
            try {
                if (id == null) {
                    throw new ArgumentNullException(paramName: nameof(id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var tareaObtenerRepositorio = Repositorio.ObtenerPorIdAsync(Convert.ToInt32(id));
                var repositorioModel = await tareaObtenerRepositorio;

                if (repositorioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(repositorioModel), message: Properties.Resources.ModeloNulo);
                }

                return View(repositorioModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un elemento Repositorio.
        /// </summary>
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(Repositorio repositorioModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var eliminarRepositorio = false;
                var tareaEliminarRepositorio = Repositorio.Eliminar(repositorioModel);
                eliminarRepositorio = await tareaEliminarRepositorio;

                string mensaje = Funciones.ObtenerMensajeExito("E", "Repositorio");

                return Json(new { success = eliminarRepositorio, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}