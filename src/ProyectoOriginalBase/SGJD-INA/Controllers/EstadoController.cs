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
    public class EstadoController : Controller {
        // Contructor y dependencias
        private readonly IEstadoLogic Estado;
        private readonly IAvisosLogic Avisos;

        public EstadoController(IEstadoLogic Estado, IAvisosLogic Avisos) {
            this.Estado = Estado;
            this.Avisos = Avisos;
        }

        // Acciones
        // GET: /Estado/Inicio/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = Estado.ObtenerTodosAsync();
                IEnumerable<Estado> ListaEstadoModel = await TareaObtenerTodos;
                IEnumerable<InicioEstadoViewModel> ListaEstadoViewModel = ListaEstadoModel.Select(Estado => new InicioEstadoViewModel(Estado)).ToList();
                return View(ListaEstadoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los Estados para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = Estado.ObtenerTodosAsync();
                IEnumerable<Estado> ListaEstadoModel = await TareaObtenerTodos;
                return Json(ListaEstadoModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Estado/Editar?IdEstado=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string IdEstado) {
            try {
                if (string.IsNullOrEmpty(IdEstado) || !Funciones.IsNumber(IdEstado)) {
                    throw new ArgumentNullException(paramName: nameof(IdEstado), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerEstado = Estado.ObtenerPorIdAsync(Convert.ToInt32(IdEstado));
                Estado ModeloEstado = await TareaObtenerEstado;

                if (ModeloEstado == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloEstado), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerAviso = Avisos.ObtenerTodosAsync();
                ViewBag.Avisos = await TareaObtenerAviso;

                EditarEstadoViewModel Modelo = new EditarEstadoViewModel(ModeloEstado);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un Estado.
        /// </summary>
        // POST: /Estado/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarEstadoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Estado ModeloEstado = Modelo.Entidad();
                var TareaEditarEstado = Estado.ActualizarAsync(ModeloEstado);
                bool EstadoEditado = await TareaEditarEstado;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Estado");

                return Json(new { success = EstadoEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Estado/Detalles
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Detalles(string IdEstado) {
            try {
                if (string.IsNullOrEmpty(IdEstado) || !Funciones.IsNumber(IdEstado)) {
                    throw new ArgumentNullException(paramName: nameof(IdEstado), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerEstado = Estado.ObtenerPorIdAsync(Convert.ToInt32(IdEstado));
                Estado ModeloEstado = await TareaObtenerEstado;

                if (ModeloEstado == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloEstado), message: Properties.Resources.ModeloNulo);
                }

                DetallesEstadoViewModel Modelo = new DetallesEstadoViewModel(ModeloEstado);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}
