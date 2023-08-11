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
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class AcuerdoController : Controller {
        // Constructor y dependencias
        private readonly IAcuerdoLogic Acuerdo;
        private readonly IVotoLogic Voto;
        private readonly IArticuloLogic Articulo;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IEstadoLogic Estado;
        private readonly IAvisosLogic Aviso;
        private readonly IUsuarioLogic Usuario;
        private readonly IUnidadLogic Unidad;
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly ICorreoAdicionalLogic CorreoAdicional;
        private readonly IUnidadPredeterminadaLogic UnidadPredeterminada;

        public AcuerdoController(IAcuerdoLogic Acuerdo, IVotoLogic Voto, IArticuloLogic Articulo, ITipoObjetoLogic TipoObjeto, IEstadoLogic Estado, IAvisosLogic Aviso, IUsuarioLogic Usuario, IUnidadLogic Unidad, IEncabezadoPiePaginaLogic EncabezadoPiePagina, ICorreoAdicionalLogic CorreoAdicional, IUnidadPredeterminadaLogic UnidadPredeterminada) {
            this.Acuerdo = Acuerdo;
            this.Voto = Voto;
            this.Articulo = Articulo;
            this.TipoObjeto = TipoObjeto;
            this.Estado = Estado;
            this.Aviso = Aviso;
            this.Usuario = Usuario;
            this.Unidad = Unidad;
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.CorreoAdicional = CorreoAdicional;
            this.UnidadPredeterminada = UnidadPredeterminada;
        }

        // Acciones
        // GET: /Acuerdo/Inicio
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Profesional de apoyo Secretaría Técnica, Director, Abogado, Gerente General, Jefe de despacho, Subgerente Administrativo, Profesional de apoyo, Profesional Secretaría Técnica")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerListaAcuerdos = Acuerdo.ObtenerTodosAsync();
                IEnumerable<Acuerdo> ListaAcuerdos = await TareaObtenerListaAcuerdos;
                IEnumerable<InicioAcuerdoViewModel> ListaAcuerdosViewModel = ListaAcuerdos.Select(Acuerdo => new InicioAcuerdoViewModel(Acuerdo)).ToList();
                return View(ListaAcuerdosViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los acuerdos para pasarlo en formato JSON.
        /// </summary>
        // GET: /Acuerdo/ObtenerDatos/
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = Acuerdo.ObtenerTodosAsync();
                var ListaAcuerdoModel = await TareaObtenerTodos;
                return Json(new { ListaAcuerdoModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de acuerdos buscando palabras en contenido para pasarlo en formato JSON.
        /// </summary>
        // GET: /Acuerdo/ObtenerTodosPorPalabraClave?Palabra=Lorem
        [HttpPost]
        public async Task<JsonResult> ObtenerTodosPorPalabraClave(string Palabra) {
            try {
                var TareaObtenerAcuerdos = Acuerdo.ObtenerTodosPorPalabraClaveAsync(Palabra);
                IEnumerable<AcuerdosPorPalabraClaveDTO> ListaAcuerdoModel = await TareaObtenerAcuerdos;

                IEnumerable<AcuerdosPorPalabraClaveViewModel> Lista = ListaAcuerdoModel.Select(a => new AcuerdosPorPalabraClaveViewModel(a)).ToList();

                return Json(new { Lista = Lista, Registros = Lista.Count() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Acuerdo/Agregar/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        public ActionResult Agregar() {
            try {
                IEnumerable<SesionConArticulosParaNuevoAcuerdoDTO> ListaSesionesConArticulosSinAcuerdo = Acuerdo.ObtenerSesionesConArticulosParaNuevoAcuerdo();
                IEnumerable<AgregarAcuerdoViewModel> Modelo = ListaSesionesConArticulosSinAcuerdo.Select(x => new AgregarAcuerdoViewModel(x));
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar un acuerdo
        /// </summary>
        // POST: Acuerdo/Agregar?IdArticulo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(string IdArticulo) {
            try {
                if (string.IsNullOrEmpty(IdArticulo)) {
                    throw new ArgumentNullException(paramName: nameof(IdArticulo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerArticulo = Articulo.ObtenerPorIdAsync(Convert.ToInt32(IdArticulo));
                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACU_TAB_ACUERDOS");
                var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("ACU-BORR");

                Articulo ArticuloModel = await TareaObtenerArticulo;
                TipoObjeto TipoObjetoAcuerdoModel = await TareaObtenerTipoObjeto;
                Estado EstadoModel = await TareaObtenerEstado;

                Acuerdo AcuerdoModel = new Acuerdo(ArticuloModel, EstadoModel, TipoObjetoAcuerdoModel);

                var TareaAgregarAcuerdo = Acuerdo.AgregarAsync(AcuerdoModel);
                int IdAcuerdoNuevo = await TareaAgregarAcuerdo;

                if (IdAcuerdoNuevo >= 0) {
                    return RedirectToAction("Editar", routeValues: new { Id = IdAcuerdoNuevo });
                }
                else {
                    IEnumerable<SesionConArticulosParaNuevoAcuerdoDTO> ListaSesionesConArticulosSinAcuerdo = Acuerdo.ObtenerSesionesConArticulosParaNuevoAcuerdo();
                    IEnumerable<AgregarAcuerdoViewModel> Modelo = ListaSesionesConArticulosSinAcuerdo.Select(x => new AgregarAcuerdoViewModel(x));
                    return View(Modelo);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Acuerdo/Editar/21
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Director, Abogado, Gerente General, Subgerente Administrativo, Jefe de despacho, Profesional de apoyo, Archivo")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id) || Id.Equals("0")) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                // Obtener lista de votantes 
                var TareaObtenerVotacionesAcuerdo = Voto.ObtenerTodosPorIdAcuerdoAsync(Convert.ToInt32(Id));
                IEnumerable<Votacion> Votaciones = await TareaObtenerVotacionesAcuerdo;

                if (ModeloAcuerdo == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAcuerdo), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerTipoFirmeza = Acuerdo.ObtenerTiposFirmezaParaSelectAsync();
                var TareaObtenerTiposRevision = Acuerdo.ObtenerTiposRevisiónParaSelectAsync();
                var TareaObtenerTipoAprobacion = Acuerdo.ObtenerTipoAprobacionParaSelectAsync();
                var TareaObtenerTiposSeguimiento = Acuerdo.ObtenerTiposSeguimientoParaSelectAsync();
                var TareaObtenerTipoVotacion = Voto.ObtenerTipoVotacionParaSelectAsync();
                var TareaObtenerUnidadesParaSelect = Unidad.ObtenerTodosParaSelectAsync();
                var TareaObtenerListaCorreoAdicional = CorreoAdicional.ObtenerTodosParaSelectAsync();

                ViewBag.ListaTipoFirmeza = await TareaObtenerTipoFirmeza;
                ViewBag.ListaTipoRevision = await TareaObtenerTiposRevision;
                ViewBag.ListaTipoAprobacion = await TareaObtenerTipoAprobacion;
                ViewBag.ListaTiposSeguimiento = await TareaObtenerTiposSeguimiento;
                ViewBag.ListaTipoVotacion = await TareaObtenerTipoVotacion;
                ViewBag.Unidades = await TareaObtenerUnidadesParaSelect;
                ViewBag.CorreosAdicionales = await TareaObtenerListaCorreoAdicional;

                EditarAcuerdoViewModel EditarAcuerdoViewModel = new EditarAcuerdoViewModel(ModeloAcuerdo, Votaciones);

                return View(EditarAcuerdoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un acuerdo
        /// </summary>
          // POST: /Acuerdo/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarAcuerdoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Acuerdo ModeloAcuerdo = Modelo.Entidad();

                var TareaActualizarAcuerdo = Acuerdo.ActualizarAsync(ModeloAcuerdo);
                bool AcuerdoActualizado = await TareaActualizarAcuerdo;

                if (AcuerdoActualizado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Acuerdo");
                    return Json(new { success = AcuerdoActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Acuerdo");
                    return Json(new { success = AcuerdoActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Acuerdo/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Abogado Secretaría Técnica")]
        public async Task<ActionResult> Eliminar(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo)) {
                    throw new ArgumentNullException(paramName: nameof(IdAcuerdo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                if (ModeloAcuerdo == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAcuerdo), message: Properties.Resources.ModeloNulo);
                }

                EliminarAcuerdoViewModel Modelo = new EliminarAcuerdoViewModel(ModeloAcuerdo);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Acuerdo
        /// </summary>
        // POST: /Acuerdo/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Abogado Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarAcuerdoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarAcuerdo = Acuerdo.EliminarAsync(Modelo.IdAcuerdo);
                bool AcuerdoEliminado = await TareaEliminarAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Acuerdo");

                return Json(new { success = AcuerdoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Cambiar el estado del acuerdo de borrador a visto bueno 
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VistoBueno(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEnviarVistoBueno = Acuerdo.EnviarVistoBuenoAsync(Convert.ToInt32(IdAcuerdo));
                bool EnviaVistoBueno = await TareaEnviarVistoBueno;

                if (EnviaVistoBueno == true) {
                    string Mensaje = "Acuerdo con visto bueno";
                    return Json(new { success = EnviaVistoBueno, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = "Error al dar visto bueno al Acuero";
                    return Json(new { success = EnviaVistoBueno, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Cambiar el estado del acuerdo de firmado a notificar 
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Notificar(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Crear enlace
                string Enlace = Url.Action("Detalles", "Acuerdo", new { Id = IdAcuerdo.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Acuerdo";
                string MensajeDetalle = "";

                var TareaEnviarNotificar = Acuerdo.NotificarAsync(Convert.ToInt32(IdAcuerdo), Enlace, TextoEnlace, MensajeDetalle);
                bool AcuerdoNotificado = await TareaEnviarNotificar;

                if (AcuerdoNotificado == true) {
                    //Enviar notificación por correo a los destinatarios configurados en el aviso correspondiente al estado del acuerdo
                    await EnviarCorreo(IdAcuerdo);

                    return Json(new { success = AcuerdoNotificado, Mensaje = "Acuerdo notificado" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = AcuerdoNotificado, Mensaje = "Error al notificar el Acuerdo" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/AgregarNuevaVersion?IdAcuerdo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarNuevaVersion(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarNuevaVersionAcuerdo = Acuerdo.AgregarNuevaVersionAsync(Convert.ToInt32(IdAcuerdo));
                int IdNuevaVersionAcuerdo = await TareaAgregarNuevaVersionAcuerdo;

                if (IdNuevaVersionAcuerdo != 0) {
                    return Json(new { success = true, IdNuevoAcuerdo = IdNuevaVersionAcuerdo }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = true, Mensaje = "Error al generar nueva versión del acuerdo" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Acuerdo ejecutado.
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AcuerdoEjecutado(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAcuerdoEjecutado = Acuerdo.AcuerdoEjecutadoAsync(Convert.ToInt32(IdAcuerdo));
                string AcuerdoEjecutado = await TareaAcuerdoEjecutado;

                if (AcuerdoEjecutado.Equals("Ok")) {
                    return Json(new { success = true, Mensaje = "Acuerdo establecido como ejecutado" }, JsonRequestBehavior.AllowGet);
                }

                if (AcuerdoEjecutado.Equals("Error"))
                    return Json(new { success = false, Mensaje = "Error al actualizar el Acuerdo" }, JsonRequestBehavior.AllowGet);
                else {
                    return Json(new { success = false, Mensaje = AcuerdoEjecutado }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/RevocarAcuerdo?IdAcuerdo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RevocarAcuerdo(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaRevocarAcuerdo = Acuerdo.RevocarAcuerdoAsync(Convert.ToInt32(IdAcuerdo));
                bool RevocarAcuerdo = await TareaRevocarAcuerdo;

                if (RevocarAcuerdo == true) {
                    return Json(new { success = true, Mensaje = "Acuerdo establecido como revocado" }, JsonRequestBehavior.AllowGet);
                }

                if (RevocarAcuerdo == false)
                    return Json(new { success = false, Mensaje = "Error al actualizar el Acuerdo" }, JsonRequestBehavior.AllowGet);
                else {
                    return Json(new { success = false, Mensaje = RevocarAcuerdo }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/ActualizarVoto/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Archivo")]
        public async Task<JsonResult> ActualizarVoto(string VotacionJSON) {
            try {
                if (string.IsNullOrEmpty(VotacionJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                Votacion ModeloVoto = JsonConvert.DeserializeObject<Votacion>(VotacionJSON);

                var TareaActualizarVoto = Voto.ActualizarVotoAsync(ModeloVoto);
                bool VotoActualizado = await TareaActualizarVoto;

                if (VotoActualizado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Voto");
                    return Json(new { success = VotoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Voto");
                    return Json(new { success = VotoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/EstablecerAprobacionPorUnanimidad?IdAcuerdo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas, Archivo")]
        public async Task<JsonResult> EstablecerAprobacionPorUnanimidad(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var EstablecerAprobacion = Voto.EstablecerAprobacionPorUnanimidad(Convert.ToInt32(IdAcuerdo));
                bool AprovacionPorUnanimidad = await EstablecerAprobacion;

                if (AprovacionPorUnanimidad == true) {
                    return Json(new { success = AprovacionPorUnanimidad }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = AprovacionPorUnanimidad }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        #region Unidades del acuerdo
        // Agregar una unidad a un acuerdo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarUnidad(string IdAcuerdo, string IdUnidad, string Tipo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo) || string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaUnidadAcuerdo = Acuerdo.AgregarUnidadAsync(Convert.ToInt32(IdAcuerdo), Convert.ToInt32(IdUnidad), Tipo);
                bool UnidadAgregada = await TareaUnidadAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Unidad Acuerdo");

                return Json(new { success = UnidadAgregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Eliminar una unidad de un acuerdo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarUnidad(string IdAcuerdo, string IdUnidad, string Tipo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo) || string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarUnidadAcuerdo = Acuerdo.EliminarUnidadAsync(Convert.ToInt32(IdAcuerdo), Convert.ToInt32(IdUnidad), Tipo);
                bool UnidadAcuerdoEliminada = await TareaEliminarUnidadAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Unidad acuerdo");

                return Json(new { success = UnidadAcuerdoEliminada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Correos adicionales para información del Acuerdo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarCorreoAdicional(string IdAcuerdo, string IdCorreoAdicional) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo) || string.IsNullOrEmpty(IdCorreoAdicional) || !Funciones.IsNumber(IdCorreoAdicional)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarCorreo = Acuerdo.AgregarCorreoAdicionalAsync(Convert.ToInt32(IdAcuerdo), Convert.ToInt32(IdCorreoAdicional));
                bool CorreoAgregado = await TareaAgregarCorreo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Correo adicional");

                return Json(new { success = CorreoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Eliminar una unidad de un acuerdo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> QUitarCorreoAdicional(string IdAcuerdo, string IdCorreoAdicional) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo) || string.IsNullOrEmpty(IdCorreoAdicional) || !Funciones.IsNumber(IdCorreoAdicional)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaQuitarCorreo = Acuerdo.QuitarCorreoAdicionalAsync(Convert.ToInt32(IdAcuerdo), Convert.ToInt32(IdCorreoAdicional));
                bool CorreoQuitado = await TareaQuitarCorreo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Correo adicional");

                return Json(new { success = CorreoQuitado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Archivos adjuntos y relacionados del acuerdo
        // GET: /Acuerdo/ObtenerArchivosAcuerdo?IdAcuerdo=2
        // Esta función devuelve tambien los archivos adjuntos del articulo y el tema de los cuales esta relacionado el acuerdo
        [HttpGet]
        public async Task<JsonResult> ObtenerArchivosAcuerdo(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArchivosAdjuntosAcuerdo = Acuerdo.ObtenerArchivosRelacionadosAsync(Convert.ToInt32(IdAcuerdo));
                IEnumerable<ArchivoAdjunto> ListaArchivosRelacionadosAcuerdo = await TareaObtenerArchivosAdjuntosAcuerdo;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivos = ListaArchivosRelacionadosAcuerdo.Select(a => new InicioArchivoAdjuntoViewModel(a, a.TipoObjeto.NombreTabla)).ToList();

                return Json(new { data = ListaArchivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/AgregarArchivoAdjunto/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarArchivoAdjunto(AgregarArchivoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Modelo.IdObjeto);
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                Modelo.NombreObjeto = ModeloAcuerdo.NombreObjeto;

                ArchivoAdjunto ModeloArchivoAdjunto = Modelo.Entidad();
                ModeloArchivoAdjunto.Acuerdo = ModeloAcuerdo;

                var TareaAgregarAdjunto = Acuerdo.AgregarArchivoAdjuntoAsync(ModeloArchivoAdjunto, Modelo.Archivo);
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

        // POST: Subir archivo con acuerdo firmado
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas, Abogado Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarAcuerdoFirmado(AgregarAcuerdoFirmadoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Modelo.IdAcuerdo);
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                var TareaAgregarAdjunto = Acuerdo.AgregarAcuerdoFirmadoAsync(ModeloAcuerdo, Modelo.Archivo);
                bool AgregarArchivoAdjunto = await TareaAgregarAdjunto;

                if (AgregarArchivoAdjunto) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Acuerdo firmado");
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

        [HttpPost, ActionName("EliminarFirmado")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarAcuerdoFirmado(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAcuerdoFirmado = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
                var ModeloAcuerdo = await TareaObtenerAcuerdoFirmado;

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarAcuerdoFirmado = Acuerdo.EliminarAcuerdoFirmadoAsync(ModeloAcuerdo);
                bool EliminarAcuerdoFirmado = await TareaEliminarAcuerdoFirmado;

                string mensaje = "El acuerdo firmado ha sido eliminado";

                return Json(new { success = EliminarAcuerdoFirmado, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        // GET: /Acuerdo/Detalles/2
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Detalles(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                if (ModeloAcuerdo == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAcuerdo), message: Properties.Resources.ModeloNulo);
                }

                // Obtener encabezado y pie de pagina del acuerdo
                var TareaObtenerEncabezadoPiePaginaAcuerdo = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(73);
                EncabezadoPiePagina EncabezadoPiePaginaAcuerdo = await TareaObtenerEncabezadoPiePaginaAcuerdo;

                // Obtener lista de votantes 
                var TareaObtenerVotacionesAcuerdo = Voto.ObtenerTodosPorIdAcuerdoAsync(Convert.ToInt32(Id));
                IEnumerable<Votacion> Votaciones = await TareaObtenerVotacionesAcuerdo;

                ModeloAcuerdo.UnidadesEjecucion = Acuerdo.ObtenerUnidadesEjecucion(Convert.ToInt32(Id));
                ModeloAcuerdo.UnidadesInformacion = Acuerdo.ObtenerUnidadesInformacion(Convert.ToInt32(Id));

                DetalleAcuerdoViewModel Modelo = new DetalleAcuerdoViewModel(ModeloAcuerdo, EncabezadoPiePaginaAcuerdo.Encabezado, EncabezadoPiePaginaAcuerdo.PiePagina, Votaciones);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Obtener vista para generar acuerdo en PDF
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetallesPDF(string Id) {
            try {
                if (string.IsNullOrEmpty(Id)) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                if (ModeloAcuerdo == null) {
                    return HttpNotFound();
                }

                // Obtener encabezado y pie de pagina del acuerdo
                var TareaObtenerEncabezadoPiePaginaAcuerdo = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(73);
                EncabezadoPiePagina EncabezadoPiePaginaAcuerdo = await TareaObtenerEncabezadoPiePaginaAcuerdo;

                // Obtener lista de votantes 
                var TareaObtenerVotacionesAcuerdo = Voto.ObtenerTodosPorIdAcuerdoAsync(Convert.ToInt32(Id));
                IEnumerable<Votacion> Votaciones = await TareaObtenerVotacionesAcuerdo;

                ModeloAcuerdo.UnidadesEjecucion = Acuerdo.ObtenerUnidadesEjecucion(Convert.ToInt32(Id));
                ModeloAcuerdo.UnidadesInformacion = Acuerdo.ObtenerUnidadesInformacion(Convert.ToInt32(Id));

                DetalleAcuerdoViewModel Modelo = new DetalleAcuerdoViewModel(ModeloAcuerdo, EncabezadoPiePaginaAcuerdo.Encabezado, EncabezadoPiePaginaAcuerdo.PiePagina, Votaciones);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Generar el pdf del Acuerdo para firmar
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GenerarPDF(string Id) {
            try {
                ActionAsPdf AR = new ActionAsPdf("DetallesPDF", new { Id = Id });
                AR.PageSize = Rotativa.Options.Size.Legal;
                AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                AR.PageMargins = new Rotativa.Options.Margins() { Left = 5, Right = 5 };

                string Params = "--print-media-type --disable-smart-shrinking";
                string Footer = string.Format("--footer-line --footer-right [page] --footer-font-size 12");

                AR.CustomSwitches = string.Format("{0} {1}", Params, Footer);

                byte[] PDFData = AR.BuildFile(ControllerContext);

                return File(PDFData, "application/pdf", "Acuerdo.pdf");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Acuerdo/EnviarCorreo?IdAcuerdo=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EnviarCorreo(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
                Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

                // Obtener el aviso del estado del acuerdo,
                Aviso ModeloAviso = ModeloAcuerdo.Estado.Aviso;

                // Crear enlace
                string Enlace = Url.Action("Editar", "Acuerdo", new { Id = IdAcuerdo.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Acuerdo";
                string MensajeDetalle = "";

                var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle);
                bool CorreoEnviado = await TareaEnviarAviso;

                return Json(new { success = CorreoEnviado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // TODO
        #region Filtros sobre acuerdos
        /// Obtener los lista con todos los elementos, filtrado según un rango de fechas para pasarlo en formato Json.
        [HttpPost]
        public async Task<JsonResult> ObtenerActasFecha(DateTime FechaInicio, DateTime FechaFin) {
            try {
                if (FechaInicio == null || FechaFin == null) {
                    return Json(new { data = string.Empty, message = "Error: Datos inválidos." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerListaAcuerdos = Acuerdo.ObtenerTodosPorRangoFechaAsync(FechaInicio, FechaFin);
                var ListaAcuerdos = await TareaObtenerListaAcuerdos;
                var Cantidad = 0;
                foreach (var acta in ListaAcuerdos) {
                    Cantidad += 1;
                }
                string Mensaje = Funciones.ObtenerMensajeExito("B", Convert.ToString(Cantidad + " registros de Acuerdos"));

                return Json(new { data = ListaAcuerdos, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion

        #region Unidades predeterminadas
        // GET: /Acuerdo/UnidadesPredeterminadas/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public ActionResult UnidadesPredeterminadas() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerUnidadesPredeterminadas() {
            try {
                var TareaObtenerUnidadesPredeterminadas = UnidadPredeterminada.ObtenerTodasAsync();
                IEnumerable<UnidadPredeterminada> ListaUnidadesPred = await TareaObtenerUnidadesPredeterminadas;
                return Json(new { data = ListaUnidadesPred }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<JsonResult> AgregarUnidadPred(string IdUnidad) {
            try {
                if (string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregar = UnidadPredeterminada.AgregarAsync(Convert.ToInt32(IdUnidad));
                bool AgregarUnidadPred = await TareaAgregar;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Unidad Pred. para Información");

                return Json(new { success = AgregarUnidadPred, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<JsonResult> QuitarUnidadPred(string IdUnidad) {
            try {
                if (string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminar = UnidadPredeterminada.EliminarAsync(Convert.ToInt32(IdUnidad));
                bool EliminarUnidadPred = await TareaEliminar;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Unidad Pred. para Información");

                return Json(new { success = EliminarUnidadPred, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        #endregion
    }
}