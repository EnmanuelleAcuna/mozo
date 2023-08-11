using Newtonsoft.Json;
using Rotativa;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class ActasController : Controller {
        // Constructor y dependencias
        private readonly IActaLogic Acta;
        private readonly IOrdenDiaLogic OrdenDia;
        private readonly ISesionLogic Sesion;
        private readonly IEstadoLogic Estado;
        private readonly IArticuloLogic Articulo;
        private readonly IAvisosLogic Aviso;
        private readonly IAsistenteSesionLogic AsistenteSesion;
        private readonly IUsuarioArticuloLogic UsuarioArticulo;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly ITomoLogic Tomo;
        private readonly IVotoLogic Voto;

        public ActasController(ITomoLogic Tomo, IOrdenDiaLogic OrdenDia, ISesionLogic Sesion, IEstadoLogic Estado, IActaLogic Acta, IArticuloLogic Articulo, IAvisosLogic Aviso, IAsistenteSesionLogic AsistenteSesion, IUsuarioArticuloLogic UsuarioArticulo, IArchivoAdjuntoLogic ArchivoAdjunto, IEncabezadoPiePaginaLogic EncabezadoPiePagina, IAudioLogic Audio, ITipoObjetoLogic TipoObjeto, IVotoLogic Voto) {
            this.Sesion = Sesion;
            this.Acta = Acta;
            this.OrdenDia = OrdenDia;
            this.Estado = Estado;
            this.Articulo = Articulo;
            this.Aviso = Aviso;
            this.AsistenteSesion = AsistenteSesion;
            this.UsuarioArticulo = UsuarioArticulo;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.TipoObjeto = TipoObjeto;
            this.Tomo = Tomo;
            this.Voto = Voto;
        }

        // GET: /Actas/Inicio
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Abogado, Gerente General, Jefe de despacho, Subgerente Administrativo, Profesional de apoyo")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerListaActa = Acta.ObtenerTodosAsync();
                var ListaActas = await TareaObtenerListaActa;
                IEnumerable<InicioActaViewModel> ListaActasViewModel = ListaActas.Select(Acta => new InicioActaViewModel(Acta)).ToList();
                return View(ListaActasViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Actas/Inicio
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador, Profesional de apoyo, Secretario Tecnico, Abogado Tecnico")]
        public async Task<ActionResult> InicioActasAcuersoft() {
            try {
                var TareaObtenerActas = Acta.ObtenerActasAcuersoftAsync();
                var ListaActasAcuersoft = await TareaObtenerActas;
                IEnumerable<InicioActasAcuersoftViewModel> ListaActasViewModel = ListaActasAcuersoft.Select(ActaAcuersoft => new InicioActasAcuersoftViewModel(ActaAcuersoft)).ToList();
                return View(ListaActasViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de Actas de sesión buscando palabras en contenido para pasarlo en formato JSON.
        /// </summary>
        // GET: /Actas/ObtenerTodosPorPalabraClave?Palabra=Lorem
        [HttpPost]
        public async Task<JsonResult> ObtenerTodosPorPalabraClave(string Palabra) {
            try {
                var TareaObtenerActas = Acta.ObtenerTodosPorPalabraClaveAsync(Palabra);
                IEnumerable<ActasPorPalabraClaveDTO> ListaActasModel = await TareaObtenerActas;
                IEnumerable<ActasPorPalabraClaveViewModel> Lista = ListaActasModel.Select(a => new ActasPorPalabraClaveViewModel(a)).ToList();
                return Json(new { Lista = Lista, Registros = Lista.Count() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }       

        /// <summary>
        /// Obtener lista de Actas de acuersoft buscando palabras en contenido para pasarlo en formato JSON.
        /// </summary>
        // POST: /Actas/ObtenerTodoPorPalabraClave?Palabra=Lorem
        [HttpPost]
        public async Task<JsonResult> ObtenerTodoAcuersoftPorPalabraClave(string Palabra, int Pagina) {
            try {
                // consulta la cantidad de registros que tiene que traer la consulta en total
                var TareaObtenerCantidadRegistros = Acta.ObtenerRegistrosAcuersoftPorPalabraClaveAsync(Palabra);
                IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO> CantidadRegistros = await TareaObtenerCantidadRegistros;
                var Registros = CantidadRegistros.First().Total;
                var Paginas = CantidadRegistros.First().TotalPagina;

                var TareaObtenerActasAcuersoft = Acta.ObtenerTodosAcuersoftPorPalabraClaveAsync(Palabra, Pagina);
                IEnumerable<ActasAcuersoftPorPalablaClaveDTO> ListaActasModel = await TareaObtenerActasAcuersoft;
                IEnumerable<ActaAcuersoftPorPalabraClaveViewModel> Lista = ListaActasModel.Select(a => new ActaAcuersoftPorPalabraClaveViewModel(a)).ToList();
                return Json(new { Lista = Lista, Registros = Registros, Paginas = Paginas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener la vista para cambiar el estado de los temas del orden del día de una sesión
        /// </summary>
        // GET: (Actas/Agregar?IdSesion=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico")]
        public async Task<ActionResult> Agregar(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var ModeloActa = await TareaObtenerActa;

                if (ModeloActa != null) {
                    return RedirectToAction("Editar", routeValues: new { IdSesion = IdSesion });
                }

                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var ModeloOrdenDia = await TareaObtenerOrdenDia;

                if (ModeloOrdenDia == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloOrdenDia), message: Properties.Resources.ModeloNulo);
                }

                // Obtener los tipos de aprobación para los temas
                var TareaObtenerTipoAprobacionTema = Estado.ObtenerTodosPorIdTipoObjetoAsync(2212);
                ViewBag.ListaTiposAprobacion = await TareaObtenerTipoAprobacionTema;

                ClasificarTemasOrdenDiaViewModel Modelo = new ClasificarTemasOrdenDiaViewModel(ModeloOrdenDia);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un elemento acta y obtener su información basica para pasarlo en formato JSON.
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(ClasificarTemasOrdenDiaViewModel Modelo) {
            try {
                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdSesionAsync(Convert.ToInt32(Modelo.IdSesion));
                var ModeloOrdenDia = await TareaObtenerOrdenDia;

                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");

                    // Obtener los tipos de aprobación para los temas
                    var TareaObtenerTipoAprobacionTema = Estado.ObtenerTodosPorIdTipoObjetoAsync(2212);
                    ViewBag.ListaTiposAprobacion = await TareaObtenerTipoAprobacionTema;

                    Modelo = new ClasificarTemasOrdenDiaViewModel(ModeloOrdenDia);

                    return View(Modelo);
                }
                else {
                    var TareaObtenerTodosAsistentesSesion = AsistenteSesion.ObtenerTodosPorIdSesionAsync(Modelo.IdSesion);
                    var TareaObtenerOtrosAsistentes = AsistenteSesion.ObtenerTodosOtroAsistentePorIdSesionAsync(Modelo.IdSesion);

                    IEnumerable<AsistenteSesion> ListaTodosAsistentesSesion = await TareaObtenerTodosAsistentesSesion;
                    IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes = await TareaObtenerOtrosAsistentes;

                    var TareaAgregarActa = Acta.AgregarAsync(Modelo.IdSesion, Modelo.IdOrdenDia, ListaTodosAsistentesSesion, ListaOtrosAsistentes);
                    string ActaCreada = await TareaAgregarActa;

                    if (ActaCreada.Equals("ok")) {
                        return RedirectToAction("Editar", routeValues: new { IdSesion = Modelo.IdSesion });
                    }
                    else {
                        ModelState.AddModelError("", "Error al crear el Acta, " + ActaCreada);

                        // Obtener los tipos de aprobación para los temas
                        var TareaObtenerTipoAprobacionTema = Estado.ObtenerTodosPorIdTipoObjetoAsync(2212);
                        ViewBag.ListaTiposAprobacion = await TareaObtenerTipoAprobacionTema;

                        Modelo = new ClasificarTemasOrdenDiaViewModel(ModeloOrdenDia);

                        return View(Modelo);
                    }
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Actas/Editar?IdSesion=21
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Director, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Subgerente Administrativo, Abogado, Gerente General, Jefe de despacho, Archivo")]
        public async Task<ActionResult> Editar(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion) || !Funciones.IsNumber(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var TareaObtenerTipoObjetoArticulo = TipoObjeto.ObtenerPorNombreAsync("Articulo");

                TipoObjeto TipoObjetoArticulo = await TareaObtenerTipoObjetoArticulo;
                Acta ModeloActa = await TareaObtenerActa;

                //Validar las acciones de los adjuntos del acta, por medio del nombre de la tabla de los artículos
                ViewBag.TipoObjetoArticulo = TipoObjetoArticulo.NombreTabla;

                if (ModeloActa == null) {
                    return RedirectToAction("Agregar", routeValues: new { IdSesion = IdSesion });
                }

                var TareaObtenerAsistentesSesion = AsistenteSesion.ObtenerTodosPresentesPorIdSesionParaSelectAsync(Convert.ToInt32(IdSesion));
                ViewBag.AsistentesSesionSelect = await TareaObtenerAsistentesSesion;

                var TareaObtenerSesion = Sesion.ObtenerSesionPosteriorParaSelectAsync(Convert.ToInt32(IdSesion));
                ViewBag.SesionSelect = await TareaObtenerSesion;

                EditarActaViewModel Modelo = new EditarActaViewModel(ModeloActa);
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Actas/Editar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarActaViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "El encabezado y párrafo de cierre son requeridos." }, JsonRequestBehavior.AllowGet);
                }

                Acta ModeloActa = Modelo.Entidad();

                var TareaActualizarActa = Acta.ActualizarAsync(ModeloActa);
                bool ActaActualizada = await TareaActualizarActa;

                if (ActaActualizada == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Acta");
                    return Json(new { success = ActaActualizada, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Acta");
                    return Json(new { success = ActaActualizada, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Acta/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await TareaObtenerActa;

                if (ModeloActa == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
                }

                EliminarActaViewModel Modelo = new EliminarActaViewModel(ModeloActa);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Acta
        /// </summary>
        // POST: /Acta/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarActaViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarActa = Acta.EliminarAsync(Modelo.IdActa);
                bool ActaEliminado = await TareaEliminarActa;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Acta");

                return Json(new { success = ActaEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Cambiar el estado del acta de Transcripción a visto bueno 
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DarVistoBueno(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaDarVistoBueno = Acta.DarVistoBuenoAsync(Convert.ToInt32(IdActa));
                bool ActaConVistoBueno = await TareaDarVistoBueno;

                if (ActaConVistoBueno == true) {
                    return Json(new { success = ActaConVistoBueno, Mensaje = "Acta con visto bueno" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = ActaConVistoBueno, Mensaje = "Error al dar visto bueno al Acta" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Cambiar el estado del acta de visto bueno a control de calidad
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ControlCalidad(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEnviarControlCalidad = Acta.DarControlCalidadAsync(Convert.ToInt32(IdActa));
                bool ActaConControlCalidad = await TareaEnviarControlCalidad;

                if (ActaConControlCalidad == true) {
                    return Json(new { success = ActaConControlCalidad, Mensaje = "Acta con control de calidad" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = ActaConControlCalidad, Mensaje = "Error al dar control de calidad al Acta" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Cambiar el estado del acta de Control de acalidad a aprobada 
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AprobarActa(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAprobar = Acta.AprobarAsync(Convert.ToInt32(IdActa));
                bool ActaAprobada = await TareaAprobar;

                if (ActaAprobada == true) {
                    return Json(new { success = ActaAprobada, Mensaje = "Acta aprobada" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = ActaAprobada, Mensaje = "Error al aprobar el Acta" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Subir archivo con acta firmada
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarActaFirmada(AgregarActaFirmadaViewModels Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Modelo.IdActa);
                Acta ActaModelo = await TareaObtenerActa;

                var TareaAgregarAdjunto = Acta.AgregarActaFirmadaAsync(ActaModelo, Modelo.Archivo);
                bool AgregarArchivoAdjunto = await TareaAgregarAdjunto;

                if (AgregarArchivoAdjunto) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Acta firmada");
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
        public async Task<JsonResult> EliminarActaFirmada(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa)) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerActaFirmada = Acta.ObtenerPorIdAsync(Convert.ToInt32(IdActa));
                var ModeloActa = await TareaObtenerActaFirmada;

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarActaFirmado = Acta.EliminarActaFirmadaAsync(ModeloActa);
                bool ActaFirmadaEliminada = await TareaEliminarActaFirmado;

                string Mensaje = "El acta firmada ha sido eliminada";

                return Json(new { success = ActaFirmadaEliminada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        #region Artículos
        /// <summary>
        /// Agregar un articulo a un capitulo del acta
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarArticulo(string ArticuloJSON, string IdSesion, string TituloCapitulo) {
            try {
                if (string.IsNullOrEmpty(ArticuloJSON) || string.IsNullOrEmpty(IdSesion) || string.IsNullOrEmpty(TituloCapitulo)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                //Deserializar el objeto JSON en un elemento artículo
                var ArticuloModel = JsonConvert.DeserializeObject<Articulo>(ArticuloJSON);

                //Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarArticulo = Articulo.AgregarAsync(ArticuloModel, Convert.ToInt32(IdSesion), TituloCapitulo);
                int IdNuevoArticulo = await TareaAgregarArticulo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Artículo");

                return Json(new { success = IdNuevoArticulo, IdNuevoArticulo = IdNuevoArticulo, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Axtualizar la transcripción de un artículo
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Coordinación Actas, Abogado Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<JsonResult> ActualizarArticulo(string ArticuloJSON) {
            try {
                if (string.IsNullOrEmpty(ArticuloJSON)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en un elemento articulo
                Articulo ArticuloModel = JsonConvert.DeserializeObject<Articulo>(ArticuloJSON);

                //Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaActualizarArticulo = Articulo.ActualizarAsync(ArticuloModel);
                var ArticuloActualizado = await TareaActualizarArticulo;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Artículo");

                return Json(new { success = ArticuloActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un asistente al artículo
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Tecnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarUsuarioArticulo(string IdArticulo, string IdUsuario) {
            try {
                if (string.IsNullOrEmpty(IdArticulo) || !Funciones.IsNumber(IdArticulo) || string.IsNullOrEmpty(IdUsuario)) {
                    return Json(new { success = false, message = "Error: modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                UsuarioArticulo ModeloUsuarioArticulo = new UsuarioArticulo(Convert.ToInt32(IdArticulo), IdUsuario);

                var TareaAgregarUsuarioArticulo = UsuarioArticulo.AgregarAsync(ModeloUsuarioArticulo);
                int IdNuevoUsuarioArticulo = await TareaAgregarUsuarioArticulo;

                if (IdNuevoUsuarioArticulo > 0) {
                    return Json(new { success = true, IdNuevoUsuarioArticulo = IdNuevoUsuarioArticulo }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina un elemento articulo del capitulo de acta 
        /// </summary>
        [HttpPost, ActionName("EliminarArticulo")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarArticuloConfirmado(int? IdArticulo) {
            try {
                if (IdArticulo == null) {
                    return Json(new { success = false, message = "Error: id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArticulo = Articulo.ObtenerPorIdAsync(Convert.ToInt32(IdArticulo));
                Articulo ModeloArticulo = await TareaObtenerArticulo;

                var TareaEliminarArticulo = Articulo.ExcluirArticulo(ModeloArticulo);
                var ArticuloEliminado = await TareaEliminarArticulo;

                if (ArticuloEliminado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("E", "Artículo");
                    return Json(new { success = true, Message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, Message = "Error al eliminar el artículo" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Entrega a la vista una lista de asistentes de un articulo en formato JSON
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ObtenerUsuarioArticulo(string IdArticulo) {
            try {
                if (string.IsNullOrEmpty(IdArticulo) || !Funciones.IsNumber(IdArticulo)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerTodosArticulo = UsuarioArticulo.ObtenerTodosPorIdArticulo(Convert.ToInt32(IdArticulo));
                var ListaUsuarioArticuloModel = await TareaObtenerTodosArticulo;

                return Json(new { ListaUsuarioArticulo = ListaUsuarioArticuloModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un usuario de un articulo
        /// </summary>
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarUsuarioArticulo(string IdUsuarioArticulo) {
            try {
                if (string.IsNullOrEmpty(IdUsuarioArticulo)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarUsuarioArticulo = UsuarioArticulo.EliminarAsync(Convert.ToInt32(IdUsuarioArticulo));
                bool UsuarioEliminado = await TareaEliminarUsuarioArticulo;

                return Json(new { success = UsuarioEliminado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        // POST: /Acuerdo/EnviarCorreo?IdActa=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas, Abogado Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnviarCorreo(string IdActa) {
            try {
                if (string.IsNullOrEmpty(IdActa) || !Funciones.IsNumber(IdActa)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Tarea para obtener el acta
                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(IdActa));
                Acta ActaModel = await TareaObtenerActa;

                // Obtener el aviso del estado del acta
                Aviso ModeloAviso = ActaModel.Estado.Aviso;

                // Crear enlace
                string Enlace = Url.Action("Editar", "Actas", new { IdSesion = ActaModel.IdSesion.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Acta";
                string MensajeDetalle = "";

                var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle);
                bool CorreoEnviado = await TareaEnviarAviso;

                return Json(new { success = CorreoEnviado, Mensaje = "Correo enviado." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        #region Archivos adjuntos
        public async Task<JsonResult> ObtenerArchivosAdjunto(string IdArticulo) {
            try {
                if (string.IsNullOrEmpty(IdArticulo) || !Funciones.IsNumber(IdArticulo)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArchivosAdjuntos = Articulo.ObtenerArchivosRelacionadosAsync(Convert.ToInt32(IdArticulo));
                IEnumerable<ArchivoAdjunto> ListaArchivosRelacionados = await TareaObtenerArchivosAdjuntos;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivos = ListaArchivosRelacionados.Select(a => new InicioArchivoAdjuntoViewModel(a, a.TipoObjeto.NombreTabla)).ToList();

                return Json(new { data = ListaArchivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Obtener archivos adjuntos relacionados a un articulo
        [HttpGet]
        public async Task<JsonResult> ObtenerArchivosArticulo(string IdArticulo) {
            try {
                var TareaObtenerArticulo = Articulo.ObtenerPorIdAsync(Convert.ToInt32(IdArticulo));
                Articulo ModeloArticulo = await TareaObtenerArticulo;

                var TareaObtenerArchivosAdjuntosArticulo = Articulo.ObtenerArchivosRelacionadosAsync(ModeloArticulo.Id);
                IEnumerable<ArchivoAdjunto> ListaArchivosRelacionadosArticulo = await TareaObtenerArchivosAdjuntosArticulo;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivos = ListaArchivosRelacionadosArticulo.Select(a => new InicioArchivoAdjuntoViewModel(a, a.TipoObjeto.NombreTabla)).ToList();

                return Json(new { data = ListaArchivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Agregar un archivo adjunto a un articulo
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Profesional Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarArchivoAdjunto(AgregarArchivoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArticulo = Articulo.ObtenerPorIdAsync(Modelo.IdObjeto);
                Articulo ModeloArticulo = await TareaObtenerArticulo;

                Modelo.NombreObjeto = ModeloArticulo.NombreObjeto;

                ArchivoAdjunto ModeloArchivoAdjunto = Modelo.Entidad();
                ModeloArchivoAdjunto.Articulo = ModeloArticulo;

                var TareaAgregarAdjunto = Articulo.AgregarArchivoAdjuntoAsync(ModeloArchivoAdjunto, Modelo.Archivo);
                bool AgregarArchivoAdjunto = await TareaAgregarAdjunto;

                if (AgregarArchivoAdjunto) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Archivo adjunto: " + Modelo.Detalle + ", ");
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
        #endregion

        #region Detalle del acta y PDF
        // Obtener vista de detalles del acta sin los articulos confidenciales
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Detalle(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await TareaObtenerActa;

                if (ModeloActa == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerOrdenDiaAprobado = OrdenDia.ObtenerAprobadaPorIdSesionAsync(ModeloActa.IdSesion);
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2214);
                var TareaObtenerVotosDesidentes = Voto.ObtenerVotosDesidentesAsync(Convert.ToInt32(Id));

                OrdenDia OrdenDiaAprobadaModelo = await TareaObtenerOrdenDiaAprobado;
                EncabezadoPiePagina EncabezadoPiePaginaActa = await TareaObtenerEncabezadoPiePaginaActa;
                IEnumerable<VotosDesidentesAcuerdosDTO> ListaVotosDesidentes = await TareaObtenerVotosDesidentes;

                DetalleActaViewModel Modelo = new DetalleActaViewModel(ModeloActa, EncabezadoPiePaginaActa.Encabezado, EncabezadoPiePaginaActa.PiePagina, OrdenDiaAprobadaModelo, ListaVotosDesidentes);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Obtener vista para generar acta en PDF sin los articulos comfidenciales
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetallePDF(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await TareaObtenerActa;

                if (ModeloActa == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerOrdenDiaAprobado = OrdenDia.ObtenerAprobadaPorIdSesionAsync(ModeloActa.IdSesion);
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2214);
                var TareaObtenerVotosDesidentes = Voto.ObtenerVotosDesidentesAsync(Convert.ToInt32(Id));

                OrdenDia OrdenDiaAprobadaModelo = await TareaObtenerOrdenDiaAprobado;
                EncabezadoPiePagina EncabezadoPiePaginaActa = await TareaObtenerEncabezadoPiePaginaActa;
                IEnumerable<VotosDesidentesAcuerdosDTO> ListaVotosDesidentes = await TareaObtenerVotosDesidentes;

                DetalleActaViewModel Modelo = new DetalleActaViewModel(ModeloActa, EncabezadoPiePaginaActa.Encabezado, EncabezadoPiePaginaActa.PiePagina, OrdenDiaAprobadaModelo, ListaVotosDesidentes);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Generar el pdf del acta sin los articulos confidenciales
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GenerarPDF(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                ActionAsPdf AR = new ActionAsPdf("DetallePDF", new { Id = Id });
                AR.PageSize = Rotativa.Options.Size.Legal;
                AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                AR.PageMargins = new Rotativa.Options.Margins() { Left = 5, Right = 5 };
               
                // Obtener el Acta por su Id
                var tareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await tareaObtenerActa;

                // Obtener la sumatoria de las paginas de las actas del tomo
                var TareaObtenerPaginasTomo = Tomo.ObtenerConsecutivoPaginaPorIdAsync(ModeloActa.IdTomo);
                int CantidadPaginasTomo = await TareaObtenerPaginasTomo;

                string Params = "--print-media-type --disable-smart-shrinking";
                string Header = string.Format("--header-font-size 14 --header-center \"INSTITUTO NACIONAL DE APRENDIZAJE\" --page-offset {0} --header-line --header-right \"N° [page]\" ", CantidadPaginasTomo);
                string Footer = string.Format("--footer-font-size 14 --footer-center \"Acta {0} {1} - {2}\" --footer-line --footer-right [sitepage]", ModeloActa.Sesion.TipoSesion.Descripcion, Convert.ToString(ModeloActa.Sesion.Numero), ModeloActa.Sesion.FechaHora.Year.ToString());
                //string Header = string.Format("--page-offset {1} --allow {0} --header-html {0} --header-spacing 10 --header-line", Url.Action("Encabezado", "Actas", null, "https"), CantidadPaginasTomo);
                //string Footer = string.Format("--allow {0} --footer-html {0} --footer-spacing 10 --footer-line", Url.Action("PiePagina", "Actas", new { TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion, NumeroSesion = Convert.ToString(ModeloActa.Sesion.Numero), FechaSesion = ModeloActa.Sesion.FechaHora }, "https"));

                AR.CustomSwitches = string.Format("{0} {1} {2}", Params, Header, Footer);

                byte[] PDFData = AR.BuildFile(ControllerContext);

                return File(PDFData, "application/pdf", "ActaSinConfidencialidad.pdf");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Obtener vista de detalles del acta completa
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        public async Task<ActionResult> DetalleCompleto(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await TareaObtenerActa;

                if (ModeloActa == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerOrdenDiaAprobado = OrdenDia.ObtenerAprobadaPorIdSesionAsync(ModeloActa.IdSesion);
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2214);
                var TareaObtenerVotosDesidentes = Voto.ObtenerVotosDesidentesAsync(Convert.ToInt32(Id));

                OrdenDia OrdenDiaAprobadaModelo = await TareaObtenerOrdenDiaAprobado;
                EncabezadoPiePagina EncabezadoPiePaginaActa = await TareaObtenerEncabezadoPiePaginaActa;
                IEnumerable<VotosDesidentesAcuerdosDTO> ListaVotosDesidentes = await TareaObtenerVotosDesidentes;

                DetalleActaViewModel Modelo = new DetalleActaViewModel(ModeloActa, EncabezadoPiePaginaActa.Encabezado, EncabezadoPiePaginaActa.PiePagina, OrdenDiaAprobadaModelo, ListaVotosDesidentes);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Obtener vista para generar acta en PDF con todos los articulos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetallePDFCompleto(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await TareaObtenerActa;

                if (ModeloActa == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerOrdenDiaAprobado = OrdenDia.ObtenerAprobadaPorIdSesionAsync(ModeloActa.IdSesion);
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2214);
                var TareaObtenerVotosDesidentes = Voto.ObtenerVotosDesidentesAsync(Convert.ToInt32(Id));

                OrdenDia OrdenDiaAprobadaModelo = await TareaObtenerOrdenDiaAprobado;
                EncabezadoPiePagina EncabezadoPiePaginaActa = await TareaObtenerEncabezadoPiePaginaActa;
                IEnumerable<VotosDesidentesAcuerdosDTO> ListaVotosDesidentes = await TareaObtenerVotosDesidentes;

                DetalleActaViewModel Modelo = new DetalleActaViewModel(ModeloActa, EncabezadoPiePaginaActa.Encabezado, EncabezadoPiePaginaActa.PiePagina, OrdenDiaAprobadaModelo, ListaVotosDesidentes);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Generar el pdf del acta completo
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        public async Task<ActionResult> GenerarPDFCompleto(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                ActionAsPdf AR = new ActionAsPdf("DetallePDFCompleto", new { Id = Id });
                AR.PageSize = Rotativa.Options.Size.Legal;
                AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                AR.PageMargins = new Rotativa.Options.Margins() { Left = 5, Right = 5 };

                // Obtener el Acta por su Id
                var tareaObtenerActa = Acta.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acta ModeloActa = await tareaObtenerActa;

                // Obtener la sumatoria de las paginas de las actas del tomo
                var TareaObtenerPaginasTomo = Tomo.ObtenerConsecutivoPaginaPorIdAsync(ModeloActa.IdTomo);
                int CantidadPaginasTomo = await TareaObtenerPaginasTomo;

                string Params = "--print-media-type --disable-smart-shrinking";
                string Header = string.Format("--header-font-size 14 --header-center \"INSTITUTO NACIONAL DE APRENDIZAJE\" --page-offset {0} --header-line --header-right \"N° [page]\" ", CantidadPaginasTomo);
                string Footer = string.Format("--footer-font-size 14 --footer-center \"Acta {0} {1} - {2}\" --footer-line --footer-right [sitepage]", ModeloActa.Sesion.TipoSesion.Descripcion, Convert.ToString(ModeloActa.Sesion.Numero), ModeloActa.Sesion.FechaHora.Year.ToString());
                //string Header = string.Format("--page-offset {1} --allow {0} --header-html {0} --header-spacing 10 --header-line", Url.Action("Encabezado", "Actas", null, "https"), CantidadPaginasTomo);
                //string Footer = string.Format("--allow {0} --footer-html {0} --footer-spacing 10 --footer-line", Url.Action("PiePagina", "Actas", new { TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion, NumeroSesion = Convert.ToString(ModeloActa.Sesion.Numero), FechaSesion = ModeloActa.Sesion.FechaHora }, "https"), CantidadPaginasTomo);

                AR.CustomSwitches = string.Format("{0} {1} {2}", Params, Header, Footer);

                byte[] PDFData = AR.BuildFile(ControllerContext);

                return File(PDFData, "application/pdf", "ActaCompleta.pdf");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw; 
            }
        }

        // Obtener vista de detalles de actas de acuersoft
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetalleActasAcuersoft(string Id) {
            try {
                // Obtener las actas de acuersoft por Id
                var TareaObtenerActas = Acta.ObtenerActaAcuersoftPorIdAsync(Convert.ToInt32(Id));
                ActaAcuersoft ModeloActaAcuersoft = await TareaObtenerActas;

                // Obtener lista de detalles de actas de acuersoft segun el Id
                var TareaObtenerDetalleActas = Acta.ObtenerDetallesActaAcuersoftAsync(Convert.ToInt32(Id));
                IEnumerable<ActasDetalleAcuersoftDTO> ListaDetalleActasAcuersoft = await TareaObtenerDetalleActas;

                ActaAcuersoftViewModel ActasDetalleAcuersoftViewModel = new ActaAcuersoftViewModel(ModeloActaAcuersoft, ListaDetalleActasAcuersoft);

                return View(ActasDetalleAcuersoftViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Encabezado() {
            try {
                // Obtener encabezado del objeto tipo Tomo
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2213);
                EncabezadoPiePagina EncabezadoPie = await TareaObtenerEncabezadoPiePaginaActa;
                return View(EncabezadoPie);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> PiePagina(string TipoSesion, string NumeroSesion, DateTime FechaSesion) {
            try {
                // Obtener encabezado del objeto tipo Acta para utilizar como pie de página en el Tomo
                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(2214);
                EncabezadoPiePagina EncabezadoPiePaginaActa = await TareaObtenerEncabezadoPiePaginaActa;

                PiePaginaActaViewModel Modelo = new PiePaginaActaViewModel(TipoSesion, NumeroSesion, FechaSesion, EncabezadoPiePaginaActa.Encabezado, EncabezadoPiePaginaActa.PiePagina);               

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion
    }
}