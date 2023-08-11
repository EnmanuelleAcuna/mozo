using Newtonsoft.Json;
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
    public class ArchivoAdjuntoController : Controller {
        // Atributos y contructor
        private IArchivoAdjuntoLogic ArchivoAdjunto;
        private IRepositorioLogic Repositorio;
        private ITipoObjetoLogic TipoObjeto;
        private ITipoArchivoLogic TipoArchivo;

        public ArchivoAdjuntoController(IArchivoAdjuntoLogic ArchivoAdjunto, IRepositorioLogic Repositorio, ITipoObjetoLogic TipoObjeto, ITipoArchivoLogic TipoArchivo) {
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.Repositorio = Repositorio;
            this.TipoObjeto = TipoObjeto;
            this.TipoArchivo = TipoArchivo;
        }

        // Archivos adjuntos
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Profesional Secretaría Técnica, Coordinación Actas, Archivo")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = ArchivoAdjunto.ObtenerTodos();
                var ListaArchivoAdjuntoModel = await TareaObtenerTodos;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivosAdjuntos = ListaArchivoAdjuntoModel.Select(ArchivoAdjunto => new InicioArchivoAdjuntoViewModel(ArchivoAdjunto)).ToList();
                return View(ListaArchivosAdjuntos);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener los lista con todos los archivos adjuntos que contiene el sitéma para pasarlo en formato Json.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = ArchivoAdjunto.ObtenerTodos();
                var ListaArchivoAdjuntoModel = await TareaObtenerTodos;

                return Json(new { data = ListaArchivoAdjuntoModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de elementos archivos adjuntos que pertenecen a un tema de OrdenDia para pasarlo en formato JSON. 
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerArchivosAdjuntos(int? idObjeto, string nombreObjeto) {
            try {
                if (idObjeto == null) {
                    return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
                }

                var tareaObtenerArchivosAdjuntos = ArchivoAdjunto.ObtenerTodosPorIdObjetoAsync((int)idObjeto, nombreObjeto);
                var ArchivosAdjuntosModel = await tareaObtenerArchivosAdjuntos;
                return Json(new { data = ArchivosAdjuntosModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener un elemento archivo adjunto que pertenezca a un acta para pasarlo en formato JSON. 
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerArchivoAdjunto(int idObjeto, string nombreObjeto) {
            try {
                var tareaObtenerArchivoAdjunto = ArchivoAdjunto.ObtenerPorIdObjetoAsync(idObjeto, nombreObjeto);
                var archivoAdjuntoModel = await tareaObtenerArchivoAdjunto;
                return Json(new { data = archivoAdjuntoModel }, JsonRequestBehavior.AllowGet);
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

                var TareaObtenerArchivoAdujunto = ArchivoAdjunto.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var ArchivoAdjuntoModel = await TareaObtenerArchivoAdujunto;

                if (ArchivoAdjuntoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(ArchivoAdjuntoModel), message: Properties.Resources.ModeloNulo);
                }
                return View(ArchivoAdjuntoModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un elemento Repositorio, se pasa el idintificador en este caso, debido a que el metodo controlador
        /// se utiliza en la seccion de archivo adjunto en orden del día y/o actas, donde solo se envia el id
        /// </summary>
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(string IdArchivo) {
            try {
                if (string.IsNullOrEmpty(IdArchivo)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArchivoAdujunto = ArchivoAdjunto.ObtenerPorIdAsync(Convert.ToInt32(IdArchivo));
                var ArchivoAdjuntoModel = await TareaObtenerArchivoAdujunto;

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EliminarArchivoAdujunto = false;
                var TareaEliminararchivoAdujunto = ArchivoAdjunto.EliminarAsync(ArchivoAdjuntoModel);
                EliminarArchivoAdujunto = await TareaEliminararchivoAdujunto;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Archivo Adjunto: " + ArchivoAdjuntoModel.Observaciones + " " + ArchivoAdjuntoModel.TipoArchivo.Descripcion);

                return Json(new { success = EliminarArchivoAdujunto, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar varios elemento de Repositorio, en caso de que el articulo lnuevo se cancele y posea imagenes cargadas, debido a que el metodo controlador
        /// se utiliza en la seccion de archivo adjunto en orden del día y/o actas, donde solo se envia el id
        /// </summary>
        [HttpPost, ActionName("EliminarVarios")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarCascada(string jsonArchivo) {
            try {
                if (string.IsNullOrEmpty(jsonArchivo)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en una lista de Destinatarios 
                var listaAdjuntoModel = JsonConvert.DeserializeObject<List<ArchivoAdjunto>>(jsonArchivo);
                var eliminararchivoAdujunto = false;

                // Elimina las imagenes que el corresponden al articulo, ya que previamente las imagenes se carga con el idObjeto
                // igual al del capitulo.
                if (listaAdjuntoModel.Count > 0) {

                    foreach (var imagen in listaAdjuntoModel) {
                        var tareaEliminararchivoAdujunto = ArchivoAdjunto.EliminarAsync(imagen);
                        eliminararchivoAdujunto = await tareaEliminararchivoAdujunto;

                    }
                }

                return Json(new { success = eliminararchivoAdujunto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}