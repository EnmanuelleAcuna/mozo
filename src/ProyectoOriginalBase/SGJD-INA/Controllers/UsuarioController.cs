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
    public class UsuarioController : Controller {
        // Constructor y dependencias
        private readonly ApplicationUserManager UserManager;
        private readonly IUsuarioLogic Usuario;
        private readonly ITipoUsuarioLogic TipoUsuario;
        private readonly IUnidadLogic Unidad;
        private readonly IPerfilLogic Rol;
        private readonly IUsuarioSIRHLogic UsuarioSIRH;
        private readonly IUsuarioCorreoLogic UsuarioCorreo;
        private readonly IUsuarioTelefonoLogic UsuarioTelefono;

        public UsuarioController(ApplicationUserManager UserManager, IUsuarioLogic Usuario, ITipoUsuarioLogic TipoUsuario, IUnidadLogic Unidad, IPerfilLogic Rol, IUsuarioSIRHLogic UsuarioSIRH, IUsuarioCorreoLogic UsuarioCorreo, IUsuarioTelefonoLogic UsuarioTelefono) {
            this.UserManager = UserManager;
            this.Usuario = Usuario;
            this.TipoUsuario = TipoUsuario;
            this.Unidad = Unidad;
            this.Rol = Rol;
            this.UsuarioSIRH = UsuarioSIRH;
            this.UsuarioCorreo = UsuarioCorreo;
            this.UsuarioTelefono = UsuarioTelefono;
        }

        //Acciones
        // GET: /Usuario/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio(string UsuarioGuardado) {
            try {
                // Al cargar la vista, si la carga proviene de la vista [Agregar], esta envía una variable de control para indicar
                // si el usuario fue guardado con exito, para mostrar el mensaje de que se realizó correctamente la acción.
                if (!string.IsNullOrEmpty(UsuarioGuardado) && UsuarioGuardado.Equals("true")) {
                    ViewBag.UsuarioGuardado = UsuarioGuardado;
                }

                var TareaObtenerTodosUsuarios = Usuario.ObtenerTodosAsync();
                IEnumerable<ApplicationUser> ListaUsuarios = await TareaObtenerTodosUsuarios;
                IEnumerable<InicioUsuarioViewModel> Lista = ListaUsuarios.Select(u => new InicioUsuarioViewModel(u)).ToList();
                return View(Lista);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener TODOS los usuarios para enviarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerTodos() {
            try {
                var TareaObtenerTodos = Usuario.ObtenerTodosAsync();
                var ListaUsuarios = await TareaObtenerTodos;
                return Json(new { data = ListaUsuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // TODO: Revisar estas dos acciones y lo que devulve
        /// <summary>
        /// Obtener lista de usuarios activos para enviarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerTodosActivos() {
            try {
                var TareaObtenerTodos = Usuario.ObtenerTodosActivosAsync();
                var ListaUsuariosActivos = await TareaObtenerTodos;
                IEnumerable<UsuarioParaSelectViewModel> ListaUsuarios = ListaUsuariosActivos.Select(u => new UsuarioParaSelectViewModel(u)).ToList();
                return Json(new { data = ListaUsuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de usuarios inactivos para enviarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerTodosInactivos() {
            try {
                var TareaObtenerTodos = Usuario.ObtenerTodosInactivosAsync();
                var ListaUsuariosInactivos = await TareaObtenerTodos;
                return Json(new { data = ListaUsuariosInactivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerUnidades = Unidad.ObtenerTodosAsync();
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var ListaPerfiles = Rol.ObtenerTodos();

                ViewBag.Perfil = ListaPerfiles;
                ViewBag.Unidad = await TareaObtenerUnidades;
                ViewBag.TipoUsuario = await TareaObtenerTipoUsuario;

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un usuario
        /// </summary>
        // POST: /Usuario/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(CrearUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    // Verificar el nombre de usuario en SIRH para saber si está registrado más de una vez
                    //bool verificarUsuarioSIRH = false;
                    //var tareaVerificarUsuarioEnSIRH = usuarioSIRH.VerificarSiUsuarioEstaDuplicado(crearUsuarioModel.usuario);
                    //verificarUsuarioSIRH = await tareaVerificarUsuarioEnSIRH;

                    //if (verificarUsuarioSIRH)
                    //{
                    //    string mensajeError = "Error, existe en SIRH dos usuarios con el mismo nombre de usuario, comuníquese con el administrador";
                    //    return Json(new { success = false, message = mensajeError }, JsonRequestBehavior.AllowGet);
                    //}

                    ApplicationUser ModeloUsuario = Modelo.Entidad();

                    // Agregar usuario a la base de datos
                    var TareaAgregarUsuario = UserManager.CreateAsync(ModeloUsuario, Modelo.Contrasena);
                    IdentityResult Result = await TareaAgregarUsuario;

                    // Verificar si UserManager tuvo exito al agregar al usuario
                    if (Result.Succeeded) {
                        // Asignar rol al usuario recien agregado
                        var TareaAgregarUsuarioEnPerfil = UserManager.AddToRoleAsync(ModeloUsuario.Id, Modelo.NombreRol);
                        Result = await TareaAgregarUsuarioEnPerfil;

                        // Verificar si el userManager tuvo exito al asignar rol al usuario
                        if (Result.Succeeded) {
                            return RedirectToAction("Inicio", new { UsuarioGuardado = "true" });
                        }
                    }

                    AddErrors(Result);
                }

                // Si se llega a este punto, ocurrión un error, volver a mostrar el formulario
                var TareaObtenerUnidades = Unidad.ObtenerTodosAsync();
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var ListaPerfiles = Rol.ObtenerTodos();

                ViewBag.Perfil = ListaPerfiles;
                ViewBag.Unidad = await TareaObtenerUnidades;
                ViewBag.TipoUsuario = await TareaObtenerTipoUsuario;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);
                var ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                EditarUsuarioViewModel Modelo = new EditarUsuarioViewModel(ModeloUsuario);

                var TareaObtenerUnidades = Unidad.ObtenerTodosAsync();
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var listaPerfiles = Rol.ObtenerTodos();

                ViewBag.Perfil = listaPerfiles;
                ViewBag.Unidad = await TareaObtenerUnidades;
                ViewBag.TipoUsuario = await TareaObtenerTipoUsuario;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un usuario
        /// </summary>
        // POST: /Usuario/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    var TareaObtenerUsuario = UserManager.FindByIdAsync(Modelo.IdUsuario);
                    ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                    // Actualizar campos necesarios
                    ModeloUsuario.Cedula = Modelo.Cedula;
                    ModeloUsuario.Nombre = Modelo.Nombre;
                    ModeloUsuario.Email = Modelo.CorreoElectronico;
                    ModeloUsuario.UserName = Modelo.Usuario;
                    ModeloUsuario.IdUnidad = Modelo.IdUnidad;
                    ModeloUsuario.IdTipoUsuario = Modelo.IdTipoUsuario;

                    var TareaActualizarUsuario = UserManager.UpdateAsync(ModeloUsuario);
                    IdentityResult Result = await TareaActualizarUsuario;

                    // Verificar si UserManager tuvo exito al actualizar al usuario
                    if (Result.Succeeded) {
                        // Actualizar rol del usuario
                        // Borrar el rol que tiene el usuario, para borrar el rol que el usuario tiene actualmente, se debe obtener con el UserManager, luego eliminar al usuario del perfil que tiene actualmente
                        var TareaObtenerNombreRolesUsuario = UserManager.GetRolesAsync(ModeloUsuario.Id);
                        IList<string> NombreRolesUsuario = await TareaObtenerNombreRolesUsuario;
                        string NombreRolActual = NombreRolesUsuario.FirstOrDefault();
                        var TareaEliminarUsuarioDeRol = UserManager.RemoveFromRoleAsync(ModeloUsuario.Id, NombreRolActual);
                        Result = await TareaEliminarUsuarioDeRol;

                        // Verificar si el userManager tuvo exito al eliminar al usuario del rol
                        if (Result.Succeeded) {
                            // Asignar nuevo rol (Perfil) al usuario.
                            var TareaAgregarUsuarioEnPerfil = UserManager.AddToRoleAsync(ModeloUsuario.Id, Modelo.NombreRol);
                            Result = await TareaAgregarUsuarioEnPerfil;

                            // Verificar si el userManager tuvo exito al asignar rol al usuario.
                            if (Result.Succeeded) {
                                ViewBag.UsuarioGuardado = "true";
                            }
                        }

                        // Añadir errores en caso de haberlos
                        AddErrors(Result);
                    }
                }

                var TareaObtenerUnidades = Unidad.ObtenerTodosAsync();
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var ListaPerfiles = Rol.ObtenerTodos();

                ViewBag.Perfil = ListaPerfiles;
                ViewBag.Unidad = await TareaObtenerUnidades;
                ViewBag.TipoUsuario = await TareaObtenerTipoUsuario;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/Detalles/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Detalles(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);
                ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                DetalleUsuarioViewModel Modelo = new DetalleUsuarioViewModel(ModeloUsuario);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/TelefonosCorreos/2
        // Id corresponde a Id del Usuario
        [HttpGet]
        public async Task<ActionResult> TelefonosCorreos(string Id) {
            try {
                var TareaObtenerCorreosUsuario = UsuarioCorreo.ObtenerTodosAsync(Id);
                var TareaObtenerTelefonosUsuario = UsuarioTelefono.ObtenerTodosAsync(Id);
                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);

                IEnumerable<UsuarioCorreo> ListaCorreosUsuario = await TareaObtenerCorreosUsuario;
                IEnumerable<UsuarioTelefono> ListaTelefonosUsuario = await TareaObtenerTelefonosUsuario;
                ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                InicioTelefonoCorreoViewModel Modelo = new InicioTelefonoCorreoViewModel(ModeloUsuario.Id, ModeloUsuario.Nombre, ListaTelefonosUsuario, ListaCorreosUsuario);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/Habilitar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Habilitar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);
                var ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                HabilitarInhabilitarUsuarioViewModel Modelo = new HabilitarInhabilitarUsuarioViewModel(ModeloUsuario);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Habilitar un usuario.
        /// </summary>
        // POST: /Usuario/Habilitar/2
        [HttpPost]
        [ActionName("Habilitar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> HabilitarConfirmado(HabilitarInhabilitarUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error, modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerUsuario = UserManager.FindByIdAsync(Modelo.Id);
                var ModeloUsuario = await TareaObtenerUsuario;

                // Actualizar el campo necesario [Activo]
                ModeloUsuario.Activo = true;

                var TareaActualizarUsuario = UserManager.UpdateAsync(ModeloUsuario);
                IdentityResult Result = await TareaActualizarUsuario;

                // Verificar si el userManager tuvo exito al actualizar al Usuario.
                if (Result.Succeeded) {
                    string Mensaje = Funciones.ObtenerMensajeExito("H", "Usuario");
                    return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "H", "Usuario");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/Inhabilitar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inhabilitar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);
                var ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                HabilitarInhabilitarUsuarioViewModel Modelo = new HabilitarInhabilitarUsuarioViewModel(ModeloUsuario);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Inhabilitar un usuario.
        /// </summary>
        // POST: /Usuario/Inhabilitar/2
        [HttpPost]
        [ActionName("Inhabilitar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InhabilitarConfirmado(HabilitarInhabilitarUsuarioViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error, modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerUsuario = UserManager.FindByIdAsync(Modelo.Id);
                var ModeloUsuario = await TareaObtenerUsuario;

                // Actualizar el campo necesario [Activo]
                ModeloUsuario.Activo = false;

                var TareaActualizarUsuario = UserManager.UpdateAsync(ModeloUsuario);
                IdentityResult Result = await TareaActualizarUsuario;

                // Verificar si el userManager tuvo exito al actualizar al Usuario.
                if (Result.Succeeded) {
                    string Mensaje = Funciones.ObtenerMensajeExito("I", "Usuario");
                    return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "I", "Usuario");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Usuario/CambiarContrasena/2
        [HttpGet]
        public async Task<ActionResult> CambiarContrasena(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Id);
                ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloUsuario), message: Properties.Resources.ModeloNulo);
                }

                CambiarContrasenaViewModel Modelo = new CambiarContrasenaViewModel { IdUsuario = Id, Nombre = ModeloUsuario.Nombre };

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Cambiar la contraseña de usuario
        /// </summary>
        // POST: /Usuario/CambiarContrasena/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CambiarContrasena(CambiarContrasenaViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaGenerarToken = UserManager.GeneratePasswordResetTokenAsync(Modelo.IdUsuario);
                string Token = await TareaGenerarToken;

                var TareaEstablecerNuevaContrasena = UserManager.ResetPasswordAsync(Modelo.IdUsuario, Token, Modelo.Contrasena);
                IdentityResult Result = await TareaEstablecerNuevaContrasena;

                // Verificar si el userManager tuvo exito al establecer la nueva contraseña.
                if (Result.Succeeded) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Contraseña");
                    return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Contraseña");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        #region Helpers
        private void AddErrors(IdentityResult Result) {
            try {
                foreach (string Error in Result.Errors) {
                    ModelState.AddModelError("", Error);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion
    }
}