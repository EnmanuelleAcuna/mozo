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
    public class TipoUsuarioController : Controller {
        // Constructor y dependencias
        private readonly ITipoUsuarioLogic TipoUsuario;
        private readonly IUsuarioLogic Usuario;

        public TipoUsuarioController(ITipoUsuarioLogic TipoUsuario, IUsuarioLogic Usuario) {
            this.TipoUsuario = TipoUsuario;
            this.Usuario = Usuario;
        }

        // GET: /TipoUsuario/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                IEnumerable<TipoUsuario> ListaTipoUsuario = await TareaObtenerTipoUsuario;
                IEnumerable<InicioTipoUsuarioViewModel> ListaViewModel = ListaTipoUsuario.Select(tu => new InicioTipoUsuarioViewModel(tu)).ToList();
                return View(ListaViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos TipoUsuario para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                IEnumerable<TipoUsuario> ListaTipoUsuario = await TareaObtenerTipoUsuario;
                return Json(new { ListaTipoUsuario }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los usuarios de un tipo de usuario
        /// </summary>
        // GET: /TipoUsuario/UsuariosTipoUsuario/2
        [HttpGet]
        public async Task<ActionResult> UsuariosTipoUsuario(string Id, string NombreTipo) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuariosTipo = Usuario.ObtenerTodosPorTipoUsuarioAsync(Convert.ToInt32(Id));
                IEnumerable<ApplicationUser> ListaUsuarios = await TareaObtenerUsuariosTipo;
                IEnumerable<UsuarioParaSelectViewModel> ListaUsuariosTipo = ListaUsuarios.Select(u => new UsuarioParaSelectViewModel(u)).ToList();

                ViewBag.NombreTipo = NombreTipo;

                return View(ListaUsuariosTipo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoUsuario/Agregar
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
        /// Agregar TipoUsuario
        /// </summary>
        // POST: /TipoUsuario/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarTipoUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoUsuario ModeloTipoUsuario = Modelo.Entidad();
                var TareaAgregarTipoUsuario = TipoUsuario.AgregarAsync(ModeloTipoUsuario);
                bool TipoUsuarioAgregado = await TareaAgregarTipoUsuario;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Tipo de Usuario");

                return Json(new { success = TipoUsuarioAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoUsuario/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var TipoUsuarioModel = await TareaObtenerTipoUsuario;

                if (TipoUsuarioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoUsuarioModel), message: Properties.Resources.ModeloNulo);
                }

                EditarTipoUsuarioViewModel Modelo = new EditarTipoUsuarioViewModel(TipoUsuarioModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar TipoUsuario
        /// </summary>
        // POST: /TipoUsuario/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarTipoUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoUsuario ModeloTipoUsuario = Modelo.Entidad();
                var TareaActualizarTipoUsuario = TipoUsuario.ActualizarAsync(ModeloTipoUsuario);
                bool TipoUsuarioActualizado = await TareaActualizarTipoUsuario;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "TipoUsuario");

                return Json(new { success = TipoUsuarioActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Tipousuario/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerPorIdAsync(Convert.ToInt32(Id));
                TipoUsuario TipoUsuarioModel = await TareaObtenerTipoUsuario;

                if (TipoUsuarioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(TipoUsuarioModel), message: Properties.Resources.ModeloNulo);
                }

                EliminarTipoUsuarioViewModel Modelo = new EliminarTipoUsuarioViewModel(TipoUsuarioModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un TipoUsuario
        /// </summary>
        // POST: /TipoObjeto/Eliminar/2
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EliminarConfirmado(EliminarTipoUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarTipoUsuario = TipoUsuario.EliminarAsync(Modelo.Id);
                bool TipoUsuarioEliminado = await TareaEliminarTipoUsuario;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "TipoUsuario");

                return Json(new { success = TipoUsuarioEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}