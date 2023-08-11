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
    public class UnidadController : Controller {
        // Constructor y dependencias
        private readonly IUnidadLogic Unidad;
        private readonly IUsuarioLogic Usuario;

        public UnidadController(IUnidadLogic Unidad, IUsuarioLogic Usuario) {
            this.Unidad = Unidad;
            this.Usuario = Usuario;
        }

        // GET: /Unidad/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = Unidad.ObtenerTodosAsync();
                var ListaUnidades = await TareaObtenerTodos;
                IEnumerable<InicioUnidadViewModel> ListaUnidadesModel = ListaUnidades.Select(Unidad => new InicioUnidadViewModel(Unidad)).ToList();
                return View(ListaUnidadesModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos las Unidades para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerListaUnidades = Unidad.ObtenerTodosAsync();
                var ListaUnidades = await TareaObtenerListaUnidades;
                return Json(new { data = ListaUnidades }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los usuarios de una unidad
        /// </summary>
        // GET: /Unidad/UsuariosUnidad/2
        [HttpGet]
        public async Task<ActionResult> UsuariosUnidad(string Id, string NombreUnidad) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuariosTipo = Usuario.ObtenerTodosPorUnidadAsync(Convert.ToInt32(Id));
                IEnumerable<ApplicationUser> ListaUsuarios = await TareaObtenerUsuariosTipo;
                IEnumerable<UsuarioParaSelectViewModel> ListaUsuariosUnidad = ListaUsuarios.Select(u => new UsuarioParaSelectViewModel(u)).ToList();

                ViewBag.NombreUnidad = NombreUnidad;

                return View(ListaUsuariosUnidad);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Unidad/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
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
        /// Agregar elemento Unidad.
        /// </summary>
        // POST: /Unidad/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarUnidadViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Unidad ModeloUnidad = Modelo.Entidad();
                var TareaAgregarUnidad = Unidad.AgregarAsync(ModeloUnidad);
                bool UnidadAgregada = await TareaAgregarUnidad;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Unidad");

                return Json(new { success = UnidadAgregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoUsuario/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> EditarEliminar(string Id, string Accion) {
            try {
                if ((string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) && (string.IsNullOrEmpty(Accion) || (!Accion.Equals("EDITAR") || !Accion.Equals("ELIMINAR")))) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUnidad = Unidad.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var UnidadModel = await TareaObtenerUnidad;

                if (UnidadModel == null) {
                    throw new ArgumentNullException(paramName: nameof(UnidadModel), message: Properties.Resources.ModeloNulo);
                }

                EditarUnidadViewModel UnidadViewModel = new EditarUnidadViewModel(UnidadModel);

                ViewBag.EditarBorrar = Accion;
                return View(UnidadViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento Unidad.
        /// </summary>
        // POST: /Unidad/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarUnidadViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Unidad ModeloUnidad = Modelo.Entidad();
                var TareaEditarUnidad = Unidad.ActualizarAsync(ModeloUnidad);
                bool UnidadEditado = await TareaEditarUnidad;

                string mensaje = Funciones.ObtenerMensajeExito("M", "Unidad");

                return Json(new { success = UnidadEditado, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un elemento Unidad.
        /// </summary>
        // POST: /Unidad/Eliminar/
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EditarUnidadViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarUnidad = Unidad.EliminarAsync(Modelo.IdUnidad);
                bool UnidadEliminada = await TareaEliminarUnidad;

                string mensaje = Funciones.ObtenerMensajeExito("E", "Unidad");

                return Json(new { success = UnidadEliminada, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}