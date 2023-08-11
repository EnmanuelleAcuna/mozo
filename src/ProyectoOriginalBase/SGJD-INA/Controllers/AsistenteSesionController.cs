using Newtonsoft.Json;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class AsistenteSesionController : Controller {
        // Constructor y dependencias
        private readonly IAsistenteSesionLogic AsistenteSesion;
        private readonly IMiembrosJDLogic MiembrosJD;
        private readonly ISesionLogic Sesion;
        private readonly ITipoUsuarioLogic TipoUsuario;

        public AsistenteSesionController(IAsistenteSesionLogic AsistenteSesion, IMiembrosJDLogic MiembrosJD, ISesionLogic Sesion, ITipoUsuarioLogic TipoUsuario) {
            this.AsistenteSesion = AsistenteSesion;
            this.MiembrosJD = MiembrosJD;
            this.Sesion = Sesion;
            this.TipoUsuario = TipoUsuario;
        }

        // Acciones
        //Asistentes sesión de miembros de junta directiva 
        // GET: /AsistenteSesion/Inicio?IdSesion=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Inicio(int IdSesion) {
            try {
                var TareaObtenerSesion = Sesion.ObtenerPorIdAsync(IdSesion); // Obtener la sesión
                var TareaObtenerAsistentesSesion = AsistenteSesion.ObtenerTodosPorIdSesionAsync(IdSesion);// Obtener lista de los asistentes a sesión
                var TareaObtenerOtroAsistenteSesion = AsistenteSesion.ObtenerTodosOtroAsistentePorIdSesionAsync(IdSesion); // Obtener lista de otros asistentes a sesión

                Sesion ModeloSesion = await TareaObtenerSesion;
                IEnumerable<AsistenteSesion> ListaAsistenteSesion = await TareaObtenerAsistentesSesion;
                IEnumerable<OtroAsistenteSesion> ListaOtroAsistenteSesion = await TareaObtenerOtroAsistenteSesion;

                // Obtener lista de tipos de asistencia 
                var TareaObtenertipoAsistencia = AsistenteSesion.ObtenerTiposAsistenciaParaSelectAsync();
                ViewBag.ListaTipoAsistencia = await TareaObtenertipoAsistencia;

                InicioAsistenciaViewModel ListaAsistencia = new InicioAsistenciaViewModel(ModeloSesion, ListaAsistenteSesion, ListaOtroAsistenteSesion);

                return View(ListaAsistencia);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /AsistenteSesion/ObtenerAsistentesPorIdSesion?IdSesion=2
        [HttpGet]
        public async Task<JsonResult> ObtenerAsistentesPorIdSesion(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAsistensias = AsistenteSesion.ObtenerTodosPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var ListaAsistenteSesion = await TareaObtenerAsistensias;
                return Json(new { data = ListaAsistenteSesion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /AsistenteSesion/AgregarUsuarioAsistente?IdUsuario=2?IdSesion=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<JsonResult> AgregarUsuarioAsistente(string IdUsuario, string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdUsuario) || string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                AsistenteSesion ModeloAsistente = new AsistenteSesion(Convert.ToInt32(IdSesion), IdUsuario);

                var TareaAgregarAsistente = AsistenteSesion.AgregarAsistenteAsync(ModeloAsistente);
                ModeloAsistente = await TareaAgregarAsistente;

                if (ModeloAsistente.Id != 0) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Asistente");
                    return Json(new { success = true, message = Mensaje, IdAsistente = ModeloAsistente.Id }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "A", "Asistente");
                    return Json(new { success = false, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /AsistentesSesion/ActualizarTipoAsistencia/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<JsonResult> ActualizarAsistencia(string AsistenciaJSON) {
            try {
                if (string.IsNullOrEmpty(AsistenciaJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                AsistenteSesion ModeloAsistente = JsonConvert.DeserializeObject<AsistenteSesion>(AsistenciaJSON);

                var TareaActualizarTipo = AsistenteSesion.ActualizarAsistenteAsync(ModeloAsistente);
                bool TipoActualizado = await TareaActualizarTipo;

                if (TipoActualizado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "AsistenteSesion");
                    return Json(new { success = TipoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "AsistenteSesion");
                    return Json(new { success = TipoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /AsistenteSesion/Editar Hora?IdAsistente=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> EditarHora(string IdAsistente) {
            try {
                if (string.IsNullOrEmpty(IdAsistente) || !Funciones.IsNumber(IdAsistente)) {
                    throw new ArgumentNullException(paramName: nameof(IdAsistente), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAsistencia = AsistenteSesion.ObtenerPorIdAsync(Convert.ToInt32(IdAsistente));
                var ModeloAsistente = await TareaObtenerAsistencia;

                if (ModeloAsistente == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAsistente), message: Properties.Resources.ModeloNulo);
                }

                EditarHoraViewModel Modelo = new EditarHoraViewModel(ModeloAsistente);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /AsistenteSesion/Editar Hora
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarHora(EditarHoraViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                AsistenteSesion ModeloAsistente = Modelo.Entidad();
                var TareaEditarAsistenteSesion = AsistenteSesion.ActualizarHoraAsync(ModeloAsistente);
                bool AsistenteEditado = await TareaEditarAsistenteSesion;

                string mensaje = Funciones.ObtenerMensajeExito("M", "Asistencia");

                return Json(new { success = AsistenteEditado, Mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //Otro Asistente Seion
        // GET: /AsistenteSesion/ObtenerOtroAsistensiasPorIdSesion?IdSesion=2
        [HttpGet]
        public async Task<JsonResult> ObtenerOtroAsistensiasPorIdSesion(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }
                var TareaObtenerOtroAsistente = AsistenteSesion.ObtenerTodosOtroAsistentePorIdSesionAsync(Convert.ToInt32(IdSesion)); //
                var ListaOtroAsistenteSesion = await TareaObtenerOtroAsistente;
                return Json(new { data = ListaOtroAsistenteSesion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /OtroAsistenteSesion/AgregarOtroAsistente?IdSesion=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> AgregarOtroAsistente(string IdSesion) {
            try {
                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var ListaTipoUsuario = await TareaObtenerTipoUsuario;
                ViewBag.TipoUsuario = ListaTipoUsuario;

                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /OtroAsistentesSesion/AgregarOtroAsistente/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarOtroAsistente(OtroAsistenteSesion otroAsistentesesionModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool AgregarOtroAsistenteSesion = false;
                var TareaAgregarOtroAsistenteSesion = AsistenteSesion.AgregarOtroAsistenteAsync(otroAsistentesesionModel);
                AgregarOtroAsistenteSesion = await TareaAgregarOtroAsistenteSesion;

                string mensaje = Funciones.ObtenerMensajeExito("A", "Otro asistente a sesión");

                return Json(new { success = AgregarOtroAsistenteSesion, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /OtroAsistenteSesion/EditarOtroAsistente/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> EditarOtroAsistente(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerOtroAsistenteSesion = AsistenteSesion.ObtenerOtroAsistentePorIdAsync(Convert.ToInt32(Id));
                var OtroAsistenteModel = await TareaObtenerOtroAsistenteSesion;

                if (OtroAsistenteModel == null) {
                    throw new ArgumentNullException(paramName: nameof(OtroAsistenteModel), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerTipoUsuario = TipoUsuario.ObtenerTodosAsync();
                var ListaTipoUsuario = await TareaObtenerTipoUsuario;
                ViewBag.TipoUsuario = ListaTipoUsuario;

                EditarOtroAsistenteViewModel Modelo = new EditarOtroAsistenteViewModel(OtroAsistenteModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /OtroAsistentesSesion/EditarOtroAsistente/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarOtroAsistente(OtroAsistenteSesion OtroAsistenteSesionModel) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EditarOtroAsistenteSesion = false;
                var TareaEditarOtroAsistente = AsistenteSesion.ActualizarOtroAsistenteAsync(OtroAsistenteSesionModel);
                EditarOtroAsistenteSesion = await TareaEditarOtroAsistente;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Otro asistente a sesión");

                return Json(new { success = EditarOtroAsistenteSesion, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /OtroAsistenteSesion/EliminarOtroAsistente/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> EliminarOtroAsistente(string id) {
            try {
                if (string.IsNullOrEmpty(id) || !Funciones.IsNumber(id)) {
                    throw new ArgumentNullException(paramName: nameof(id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerOtroAsistenteSesion = AsistenteSesion.ObtenerOtroAsistentePorIdAsync(Convert.ToInt32(id));
                OtroAsistenteSesion ModeloOtroAsistente = await TareaObtenerOtroAsistenteSesion;

                if (ModeloOtroAsistente == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloOtroAsistente), message: Properties.Resources.ModeloNulo);
                }
                EliminarOtroAsistenteViewModel Modelo = new EliminarOtroAsistenteViewModel(ModeloOtroAsistente);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /OtroAsistentesSesion/EliminarOtroAsistente/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarOtroAsistente(EliminarOtroAsistenteViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarOtroAsistente = AsistenteSesion.EliminarOtroAsistenteAsync(Modelo.Id);
                bool OtroAsistenteEliminado = await TareaEliminarOtroAsistente;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Otro asistente a sesión");

                return Json(new { success = OtroAsistenteEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

    }
}