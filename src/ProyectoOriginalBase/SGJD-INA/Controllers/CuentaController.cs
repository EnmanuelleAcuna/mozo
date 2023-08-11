using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
//using LoginAD_INA;

namespace SGJD_INA.Controllers {
    public class CuentaController : Controller {
        // Constructor y dependencias
        private readonly ApplicationSignInManager SignInManager;
        private readonly ApplicationUserManager UserManager;
        private readonly IAuthenticationManager AuthenticationManager;
        private readonly IVistaPerfilLogic VistaPerfil;

        public CuentaController(ApplicationSignInManager SignInManager, ApplicationUserManager UserManager, IAuthenticationManager AuthenticationManager, IVistaPerfilLogic VistaPerfil) {
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
            this.AuthenticationManager = AuthenticationManager;
            this.VistaPerfil = VistaPerfil;
        }

        #region Iniciar y cerrar sesión
        // GET: /Cuenta/IniciarSesion
        [HttpGet]
        [AllowAnonymous]
        public ActionResult IniciarSesion(string ReturnUrl) {
            try {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Cuenta/IniciarSesion
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IniciarSesion(IniciarSesionViewModel Modelo, string ReturnUrl) {
            try {
                ApplicationUser ModeloUsuario;
                SignInStatus Result = SignInStatus.Failure;

                // Inicio de sesion por firma digital
                if (!string.IsNullOrEmpty(Modelo.Certificado)) {
                    string CedulaCertificado = Funciones.ObtenerCedulaCertificado(Modelo.Certificado);
                    var TareaObtenerUsuario = UserManager.Users.Where(u => u.Cedula.Equals(CedulaCertificado)).FirstOrDefaultAsync();
                    ModeloUsuario = await TareaObtenerUsuario;

                    if (ModeloUsuario != null) {
                        if (ModeloUsuario.Activo) {
                            await SignInManager.SignInAsync(ModeloUsuario, false, false);
                            Result = SignInStatus.Success;
                        }
                        else {
                            ModelState.AddModelError("", "El usuario está inactivo");
                        }
                    }
                }
                //Inicio de sesion por email
                else if (Funciones.IsValidEmail(Modelo.Usuario)) {
                    var TareaObtenerUsuario = UserManager.FindByEmailAsync(Modelo.Usuario);
                    ModeloUsuario = await TareaObtenerUsuario;

                    if (ModeloUsuario != null) {
                        if (ModeloUsuario.Activo) {
                            Result = await SignInManager.PasswordSignInAsync(ModeloUsuario.UserName, Modelo.Contrasena, isPersistent: false, shouldLockout: false);
                        }
                        else {
                            ModelState.AddModelError("", "El usuario está inactivo");
                        }
                    }
                }
                //Inicio de sesion por nombre de usuario en AD
                else {
                    var TareaObtenerUsuario = UserManager.FindByNameAsync(Modelo.Usuario);
                    ModeloUsuario = await TareaObtenerUsuario;

                    if (ModeloUsuario != null) {
                        if (ModeloUsuario.Activo) {
                            //var loginAD = new Login_AD();
                            //var usuarioAD = loginAD.LDAP_Login(Modelo.Usuario, Modelo.Contrasena);
                            //if (usuarioAD.Count is 3) {
                            //    await SignInManager.SignInAsync(ModeloUsuario, false, false);
                            //    Result = SignInStatus.Success;
                            //}
                        }
                        else {
                            ModelState.AddModelError("", "El usuario está inactivo");
                        }
                    }
                }

                switch (Result) {
                    case SignInStatus.Success:
                        return RedirectToLocal(ReturnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new {
                            ReturnUrl = ReturnUrl
                        });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Usuario o contraseña incorrecta");
                        break;
                }

                // Si se llega a esta línea quiere decir que hubo un error
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarSesion() {
            try {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie); // Cerrar la sesión
                return RedirectToAction("IniciarSesion", "Cuenta");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Olvidó contraseña y restablecer contraseña
        // GET: /Cuenta/OlvidoContrasena
        [HttpGet]
        [AllowAnonymous]
        public ActionResult OlvidoContrasena() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Cuenta/OlvidoContrasena
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> OlvidoContrasena(OlvidoContrasenaViewModel Modelo) {
            try {
                if (ModelState.IsValid) {
                    var TareaObtenerUsuario = UserManager.FindByEmailAsync(Modelo.CorreoElectronico);
                    ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                    if (ModeloUsuario == null) {
                        // No revelar que el usuario no existe, redirigir a la confirmación
                        return View("OlvidoContrasenaConfirmacion");
                    }

                    if (ModeloUsuario.Activo) {
                        // Generar un token de restablecimiento de contraseña
                        var TareaGenerarToken = UserManager.GeneratePasswordResetTokenAsync(ModeloUsuario.Id);
                        string Token = await TareaGenerarToken;

                        // Crear enlace
                        string UrlRestablecerContrasena = Url.Action("RestablecerContrasena", "Cuenta", new { userId = ModeloUsuario.Id, code = Token }, protocol: Request.Url.Scheme);

                        // Configurar correo
                        string MensajeDeCorreo = ApplicationEmailService.ConfigurarMensajeCorreo(NombreDeUsuario: ModeloUsuario.Nombre, Mensaje: "Usted ha enviado una solicitud para restablecer su contraseña.", TextoBoton: "Para restablecer su contraseña haga clic aquí", URL: UrlRestablecerContrasena, MensajeDetalle: "");

                        // Enviar correo
                        await UserManager.SendEmailAsync(ModeloUsuario.Id, "Restablecer contraseña", MensajeDeCorreo);

                        return View("OlvidoContrasenaConfirmacion");
                    }
                    else {
                        ModelState.AddModelError("", "El usuario está inactivo");
                    }
                }

                // Si se llega a esta línea, algo falló, mostrar vista nuevamente
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Cuenta/OlvidoContrasenaConfirmacion
        [HttpGet]
        [AllowAnonymous]
        public ActionResult OlvidoContrasenaConfirmacion() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Cuenta/RestablecerContrasena
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RestablecerContrasena(string code) {
            try {
                return code == null ? View("Error") : View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Cuenta/RestablecerContrasena
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RestablecerContrasena(RestablecerContrasenaViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return View(Modelo);
                }

                var TareaObtenerUsuario = UserManager.FindByEmailAsync(Modelo.CorreoElectronico);
                ApplicationUser ModeloUsuario = await TareaObtenerUsuario;

                if (ModeloUsuario == null) {
                    // No revelar que el usuario no existe, redirigir a la confirmación
                    return View("RestablecerContrasenaConfirmacion");
                }

                var TareaEstablecerNuevaContrasena = UserManager.ResetPasswordAsync(ModeloUsuario.Id, Modelo.Code, Modelo.Contrasena);
                IdentityResult Result = await TareaEstablecerNuevaContrasena;

                if (Result.Succeeded) {
                    return View("RestablecerContrasenaConfirmacion");
                }

                AddErrors(Result);

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Cuenta/RestablecerContrasenaConfirmacion
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RestablecerContrasenaConfirmacion() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string Returnurl) {
            try {
                if (Url.IsLocalUrl(Returnurl)) {
                    return Redirect(Returnurl);
                }
                return RedirectToAction("Inicio", "Inicio");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

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