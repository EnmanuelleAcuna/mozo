using Microsoft.AspNet.Identity;
using SGJD_INA.Models.Core.Implementation;
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
    public class PerfilController : Controller {
        // Constructor y dependencias
        private readonly ApplicationRoleManager RoleManager;
        private readonly IPerfilLogic Rol;
        private readonly IUsuarioLogic Usuario;
        private readonly IBitacoraLogic Bitacora;

        public PerfilController(ApplicationRoleManager RoleManager, IPerfilLogic Rol, IUsuarioLogic Usuario, IBitacoraLogic Bitacora) {
            this.RoleManager = RoleManager;
            this.Rol = Rol;
            this.Usuario = Usuario;
            this.Bitacora = Bitacora;
        }

        // GET: /Usuario/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodosRoles = Rol.ObtenerTodosAsync();
                IEnumerable<ApplicationRole> ListaRoles = await TareaObtenerTodosRoles;
                IEnumerable<InicioRolViewModel> Lista = ListaRoles.Select(u => new InicioRolViewModel(u)).ToList();
                return View(Lista);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener todos los roles para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = Rol.ObtenerTodosAsync();
                IEnumerable<ApplicationRole> ListaRoles = await TareaObtenerTodos;
                return Json(new { data = ListaRoles }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener todos los usuarios de un perfil
        /// </summary>
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> UsuariosPerfil(string Id, string NombrePerfil) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuariosRol = Usuario.ObtenerTodosPorRolAsync(Id);
                IEnumerable<ApplicationUser> ListaUsuariosRol = await TareaObtenerUsuariosRol;

                ViewBag.NombrePerfil = NombrePerfil;

                return View(ListaUsuariosRol);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Perfil/Agregar
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
        /// Agregar un rol
        /// </summary>
        // POST: /Perfil/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarRolViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                ApplicationRole ModeloRol = Modelo.Entidad();

                // Agregar rol a la base de datos
                var TareaAgregarPerfil = RoleManager.CreateAsync(ModeloRol);
                IdentityResult Result = await TareaAgregarPerfil;

                if (Result.Succeeded) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloRol.ObtenerInformacion(), SeccionSistema: "Perfil");
                    string Mensaje = Funciones.ObtenerMensajeExito("C", "Perfil");
                    return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "C", "Perfil");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Perfil/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerRol = Rol.ObtenerPorIdAsync(Id);
                var ModeloRol = await TareaObtenerRol;

                if (ModeloRol == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloRol), message: Properties.Resources.ModeloNulo);
                }

                EditarRolViewModel Modelo = new EditarRolViewModel(ModeloRol);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento Perfil.
        /// </summary>
        ///       
        // POST: /Usuario/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarRolViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerRol = RoleManager.FindByIdAsync(Modelo.IdRol);
                ApplicationRole ModeloRol = await TareaObtenerRol;

                // Actualizar campos necesarios
                ModeloRol.Name = Modelo.Nombre;

                var TareaActualizarRol = RoleManager.UpdateAsync(ModeloRol);
                IdentityResult Result = await TareaActualizarRol;

                if (Result.Succeeded) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloRol.ObtenerInformacion(), SeccionSistema: "Perfil");
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Perfil");
                    return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Perfil");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}