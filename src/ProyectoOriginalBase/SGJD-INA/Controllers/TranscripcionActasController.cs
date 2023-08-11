using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using SGJD_INA.Properties;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class TranscripcionActasController : Controller {
        // Constructor y dependencias
        private readonly ITranscripcionActaCore TranscripcionActa;

        public TranscripcionActasController(ITranscripcionActaCore TranscripcionActa) {
            this.TranscripcionActa = TranscripcionActa;
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Profesional de apoyo Secretaría Técnica")]
        public ActionResult Inicio() {
            try {
                ActaTranscritaViewModel Modelo = new ActaTranscritaViewModel();
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Actas/Inicio
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Profesional de apoyo Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Inicio(ActaTranscritaViewModel Modelo) {
            try {
                if (Modelo is null) {
                    throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);
                }

                byte[] Data;
                using (Stream InputStream = Modelo.Audio.InputStream) {
                    MemoryStream MemoryStream = InputStream as MemoryStream;
                    if (MemoryStream == null) {
                        MemoryStream = new MemoryStream();
                        InputStream.CopyTo(MemoryStream);
                    }
                    Data = MemoryStream.ToArray();
                }

                var TareaTranscribirActa = TranscripcionActa.TranscribirAsync(Data);
                string Transcripcion = await TareaTranscribirActa;
                ActaTranscritaViewModel ActaTranscrita = new ActaTranscritaViewModel(Transcripcion);
                return View(ActaTranscrita);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}