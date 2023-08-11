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
    public class AvisosController : Controller {
        // Constructor y dependencias
        private readonly IAvisosLogic Aviso;
        private readonly IUnidadLogic Unidad;

        public AvisosController(IAvisosLogic Aviso, IUnidadLogic Unidad) {
            this.Aviso = Aviso;
            this.Unidad = Unidad;
        }

        // Acciones
        // GET: /Avisos/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio(string AvisoGuardado) {
            try {
                // Al cargar la vista, si la carga proviene de la vista [Agregar], esta envía una variable de control para indicar
                // si el aviso fue guardado con exito, para mostrar el mensaje de que se realizó correctamente la acción.
                if (!string.IsNullOrEmpty(AvisoGuardado) && AvisoGuardado.Equals("true")) {
                    ViewBag.AvisoGuardado = AvisoGuardado;
                }

                var TareaObtenerAvisos = Aviso.ObtenerTodosAsync();
                IEnumerable<Aviso> ListaAvisos = await TareaObtenerAvisos;
                IEnumerable<InicioAvisoViewModel> Lista = ListaAvisos.Select(a => new InicioAvisoViewModel(a)).ToList();
                return View(Lista);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos Aviso para pasarlo en formato JSON.
        /// </summary>
        // GET: /Avisos/ObtenerDatos
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerAvisos = Aviso.ObtenerTodosAsync();
                var ListaAvisosModel = await TareaObtenerAvisos;
                return Json(new { ListaAvisosModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Avisos/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var ListaTipoAviso = Aviso.ObtenerTiposDeAvisoParaSelect();
                var ListaTipoDestinatario = Aviso.ObtenerTiposDestinatarioParaSelect();
                var TareaObtenerUnidades = Unidad.ObtenerTodosParaSelectAsync();

                ViewBag.TiposAviso = ListaTipoAviso;
                ViewBag.TiposDestinatario = ListaTipoDestinatario;
                ViewBag.Unidades = await TareaObtenerUnidades;

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un Aviso.
        /// </summary>
        // POST: /Avisos/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(AgregarAvisoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    Aviso AvisoModel = Modelo.Entidad();

                    var TareaAgregarAviso = Aviso.AgregarAsync(AvisoModel);
                    bool AvisoCreado = await TareaAgregarAviso;

                    // Verificar si hubo exito al gurdar el aviso
                    if (AvisoCreado == true) {
                        return RedirectToAction("Inicio", new { AvisoGuardado = "true" });
                    }
                }

                // Si se llega a este punto quiere decir que hubo un error
                ModelState.AddModelError("", "Error al guardar la información");

                var ListaTipoAviso = Aviso.ObtenerTiposDeAvisoParaSelect();
                var ListaTipoDestinatario = Aviso.ObtenerTiposDestinatarioParaSelect();
                var TareaObtenerUnidades = Unidad.ObtenerTodosParaSelectAsync();

                ViewBag.TiposAviso = ListaTipoAviso;
                ViewBag.TiposDestinatario = ListaTipoDestinatario;
                ViewBag.Unidades = await TareaObtenerUnidades;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Avisos/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AvisoModel = await TareaObtenerAviso;

                if (AvisoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AvisoModel), message: Properties.Resources.ModeloNulo);
                }

                EditarAvisoViewModel Modelo = new EditarAvisoViewModel(AvisoModel);

                var ListaTipoAviso = Aviso.ObtenerTiposDeAvisoParaSelect();
                var ListaTipoDestinatario = Aviso.ObtenerTiposDestinatarioParaSelect();
                var TareaObtenerUnidades = Unidad.ObtenerTodosParaSelectAsync();

                ViewBag.TiposAviso = ListaTipoAviso;
                ViewBag.TiposDestinatario = ListaTipoDestinatario;
                ViewBag.Unidades = await TareaObtenerUnidades;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un Aviso
        /// </summary>
        // POST: /Avisos/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarAvisoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    Aviso AvisoModel = Modelo.Entidad();

                    var TareaEditarAviso = Aviso.ActualizarAsync(AvisoModel);
                    bool AvisoEditado = await TareaEditarAviso;

                    // Verificar si hubo exito al gurdar el aviso
                    if (AvisoEditado == true) {
                        ViewBag.AvisoGuardado = "true";

                        // Obtener nuevamente el aviso, este paso se hace nuevamente porque la lista de usuarios que viene en el ViewModel es diferente a la que necesita la vista cuando se carga
                        var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(Convert.ToInt32(Modelo.IdAviso));
                        AvisoModel = await TareaObtenerAviso;

                        Modelo = new EditarAvisoViewModel(AvisoModel);
                    }
                }

                var ListaTipoAviso = Aviso.ObtenerTiposDeAvisoParaSelect();
                var ListaTipoDestinatario = Aviso.ObtenerTiposDestinatarioParaSelect();
                var TareaObtenerUnidades = Unidad.ObtenerTodosParaSelectAsync();

                ViewBag.TiposAviso = ListaTipoAviso;
                ViewBag.TiposDestinatario = ListaTipoDestinatario;
                ViewBag.Unidades = await TareaObtenerUnidades;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Avisos/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AvisoModel = await TareaObtenerAviso;

                if (AvisoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AvisoModel), message: Properties.Resources.ModeloNulo);
                }

                EliminarAvisoViewModel Modelo = new EliminarAvisoViewModel(AvisoModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina un Aviso.
        /// </summary>
        // POST: /Avisos/Eliminar/
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarAvisoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var tareaEliminarAviso = Aviso.EliminarAsync(Modelo.Id);
                bool AvisoEliminado = await tareaEliminarAviso;

                // Verificar si hubo exito al eliminar el Aviso.
                if (AvisoEliminado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("E", "Aviso");
                    return Json(new { success = AvisoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "E", "Aviso");
                    return Json(new { success = AvisoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Avisos/Detalles/2
        [HttpGet]
        public async Task<ActionResult> Detalles(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AvisoModel = await TareaObtenerAviso;

                if (AvisoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AvisoModel), message: Properties.Resources.ModeloNulo);
                }

                DetallesAvisoViewModel Modelo = new DetallesAvisoViewModel(AvisoModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Avisos/UsuariosAviso/2
        [HttpGet]
        public async Task<ActionResult> UsuariosAviso(string Id, string NombreAviso) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AvisoModel = await TareaObtenerAviso;

                if (AvisoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AvisoModel), message: Properties.Resources.ModeloNulo);
                }

                IEnumerable<UsuarioParaSelectViewModel> ListaModelo = AvisoModel.Usuarios.Select(u => new UsuarioParaSelectViewModel(u)).ToList();

                ViewBag.NombreAviso = NombreAviso;

                return View(ListaModelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}