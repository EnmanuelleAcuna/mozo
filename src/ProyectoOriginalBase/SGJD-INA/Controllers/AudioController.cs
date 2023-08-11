using Newtonsoft.Json;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class AudioController : Controller {
        // Atributos y constructor
        private IAudioLogic Audio;

        public AudioController(IAudioLogic Audio) {
            this.Audio = Audio;
        }

        // GET: /Acta/ObtenerAudiosActa?IdActa=2
        [HttpGet]
        public async Task<JsonResult> ObtenerAudiosActa(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa) || !Funciones.IsNumber(IdActa)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAudiosActa = Audio.ObtenerTodosPorIdActaAsync(Convert.ToInt32(IdActa, new CultureInfo("es-CR")));
                IEnumerable<Audio> ListaAudiosRelacionados = await TareaObtenerAudiosActa;

                IEnumerable<InicioAudioViewModel> ListaAudios = ListaAudiosRelacionados.Select(au => new InicioAudioViewModel(au, au.NombreObjeto)).ToList();

                return Json(new { data = ListaAudios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Acta/ObtenerAudiosActa?IdActa=2
        [HttpGet]
        public async Task<JsonResult> ObtenerAudiosActaConTranscripcion(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa) || !Funciones.IsNumber(IdActa)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAudiosActa = Audio.ObtenerTodosPorIdActaAsync(Convert.ToInt32(IdActa, new CultureInfo("es-CR")));
                IEnumerable<Audio> ListaAudiosRelacionados = await TareaObtenerAudiosActa;

                IEnumerable<TranscripcionAudioViewModel> ListaAudios = ListaAudiosRelacionados.Select(au => new TranscripcionAudioViewModel(au)).ToList();

                return Json(ListaAudios, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Detalles(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAudios = Audio.ObtenerPorIdAsync(Convert.ToInt32(Id, new CultureInfo("es-CR")));
                Audio ModeloAudio = await TareaObtenerAudios;

                if (ModeloAudio is null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAudio), message: Resources.ModeloNulo);
                }

                TranscripcionAudioViewModel Modelo = new TranscripcionAudioViewModel(ModeloAudio);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Profesional Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarAudio(AgregarAudioViewModel Modelo) {
            try {
                if (Modelo is null) {
                    throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);
                }

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                Audio ModeloAudio = Modelo.Entidad();

                var TareaAgregarAudio = Audio.AgregarAsync(ModeloAudio, Modelo.Audio);
                bool AgregarAudio = await TareaAgregarAudio;

                if (AgregarAudio) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Audio: " + Modelo.Nombre + ", ");
                    return Json(new { success = true, Message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, Message = "Error al subir el archivo adjunto" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> TranscribirAudio(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaTranscribirAudio = Audio.TranscribirAsync(Convert.ToInt32(Id, new CultureInfo("es-CR")));
                bool AudioTranscrito = await TareaTranscribirAudio;

                if (AudioTranscrito) {
                    return Json(new { success = true, Message = "El audio ha sido transcrito correctamente." }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, Message = "Error al transcribir el audio." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAudio = Audio.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AudioModel = await TareaObtenerAudio;

                if (AudioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AudioModel), message: Properties.Resources.ModeloNulo);
                }
                return View(AudioModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAudio = Audio.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var AudioModel = await TareaObtenerAudio;

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EliminarAudio = false;
                var TareaEliminarAudio = Audio.EliminarAsync(AudioModel);
                EliminarAudio = await TareaEliminarAudio;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Archivo Adjunto: " + AudioModel.Nombre);

                return Json(new { success = EliminarAudio, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}