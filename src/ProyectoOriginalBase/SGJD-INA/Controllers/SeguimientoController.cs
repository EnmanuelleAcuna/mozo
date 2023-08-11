using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
    public class SeguimientoController : Controller {
        // Constructor y dependencias
        private readonly ISeguimientoLogic SeguimientoAcuerdo;
        private readonly IDetalleSeguimientoLogic DetalleSeguimiento;
        private readonly IAcuerdoLogic Acuerdo;
        private readonly IUnidadLogic Unidad;
        private readonly IUsuarioLogic Usuario;
        private readonly IEstadoLogic Estado;
        private readonly IAvisosLogic Aviso;
        private readonly IElementosRelacionadosLogic ElementoRelacionado;

        public SeguimientoController(ISeguimientoLogic SeguimientoAcuerdo, IDetalleSeguimientoLogic DetalleSeguimiento, IAcuerdoLogic Acuerdo, IUnidadLogic Unidad, IUsuarioLogic Usuario, IEstadoLogic Estado, IAvisosLogic Aviso, IElementosRelacionadosLogic ElementoRelacionado) {
            this.SeguimientoAcuerdo = SeguimientoAcuerdo;
            this.DetalleSeguimiento = DetalleSeguimiento;
            this.Acuerdo = Acuerdo;
            this.Unidad = Unidad;
            this.Usuario = Usuario;
            this.Estado = Estado;
            this.Aviso = Aviso;
            this.ElementoRelacionado = ElementoRelacionado;
        }

        // GET: /Seguimiento/Inicio/
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional de apoyo")]
        public async Task<ActionResult> Inicio() {
            try {
                // Obtener lista de seguimientos no ejecutados, en ejecución y ejecutados
                var TareaObtenerSeguimientosNoEjecutados = SeguimientoAcuerdo.ObtenerTodosEstadoNoEjecutado();
                var TareaObtenerSeguimientosEnEjecucion = SeguimientoAcuerdo.ObtenerTodosEstadoEnEjecucion();
                var TareaObtenerSeguimientosEjecutados = SeguimientoAcuerdo.ObtenerTodosEstadoEjecutado();

                IEnumerable<SeguimientoAcuerdo> ListaSeguimientosNoEjecutados = await TareaObtenerSeguimientosNoEjecutados;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientosEnEjecucion = await TareaObtenerSeguimientosEnEjecucion;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientosEjecutados = await TareaObtenerSeguimientosEjecutados;

                // Obtener lista de unidades ejecutoras del seguimmiento, según la unidad del usuario que se encuentra activo en el sistema
                var TareaObtenerUnidadesSeguimiento = SeguimientoAcuerdo.ObtenerSeguimientosSegunIdUnidadAsyng(Convert.ToInt32(User.IdUnidad()));
                IEnumerable<UnidadesEjecucionSeguimientoDTO> ListaUnidadesSeguimiento = await TareaObtenerUnidadesSeguimiento;

                InicioSeguimientoViewModel Modelo = new InicioSeguimientoViewModel(ListaSeguimientosNoEjecutados, ListaSeguimientosEnEjecucion, ListaSeguimientosEjecutados, ListaUnidadesSeguimiento);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar Seguimiento a un Acuerdo
        /// Obtener lista de acuerdos para agregar seguimiento
        /// Los acuerdos que se obtendrán son aquellos que se encuentren en firme, con estado notificado o en ejecución
        /// y que posea el tipo de seguimiento con seguimiento o seguimiento permanente
        /// </summary>
        // GET: /Seguimiento/Agregar/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerAcuerdosParaSeguimiento = Acuerdo.ObtenerAcuerdosParaSeguimientoAsync();
                IEnumerable<AcuerdoParaSeguimientoDTO> ListaAcuerdosParaSeguimiento = await TareaObtenerAcuerdosParaSeguimiento;

                // Construir ViewModel con la información del DTO
                IEnumerable<AgregarSeguimientoViewModel> Modelo = ListaAcuerdosParaSeguimiento.Select(a => new AgregarSeguimientoViewModel(a)).ToList(); ;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/Agregar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(string IdAcuerdo) {
            try {
                if (string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo)) {
                    throw new ArgumentNullException(paramName: nameof(IdAcuerdo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAcuerdo = Acuerdo.ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
                var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("SA-NE");

                Acuerdo AcuerdoModel = await TareaObtenerAcuerdo;
                Estado EstadoModel = await TareaObtenerEstado;

                // Crear una entidad de SeguimientoAcuerdo
                SeguimientoAcuerdo ModeloSeguimientoAcuerdo = new SeguimientoAcuerdo(AcuerdoModel, EstadoModel);

                var TareaAgregarSeguimientoAcuerdo = SeguimientoAcuerdo.AgregarAsync(ModeloSeguimientoAcuerdo);
                int IdNuevoSeguimiento = await TareaAgregarSeguimientoAcuerdo;

                return RedirectToAction("Editar", routeValues: new { Id = IdNuevoSeguimiento });
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seguimiento/Editar/2
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador, Profesional de apoyo")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id) || Id.Equals("0")) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSeguimientoAcuerdo = SeguimientoAcuerdo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                SeguimientoAcuerdo SeguimientoAcuerdoModel = await TareaObtenerSeguimientoAcuerdo;

                if (SeguimientoAcuerdoModel == null) {
                    throw new ArgumentNullException(paramName: nameof(SeguimientoAcuerdoModel), message: Properties.Resources.ModeloNulo);
                }

                // Obtener las unidades ejecutoras del acuerdo (Esto será enviado a la vista para uso informativo)
                var TareaObtenerUnidadesEjecutorasAcuerdo = Acuerdo.ObtenerUnidadesEjecucionAsync(SeguimientoAcuerdoModel.IdAcuerdo);
                var TareaObtenerTiposElementosRelacionados = ElementoRelacionado.ObtenerTiposElementosRelacionadosParaSelectAsync();

                IEnumerable<Unidad> UnidadesParaEjecucionAcuerdo = await TareaObtenerUnidadesEjecutorasAcuerdo;
                IEnumerable<SelectListItem> ListaTiposElementosRelacionados = await TareaObtenerTiposElementosRelacionados;

                EditarSeguimientoViewModel EditarSeguimientoViewModel = new EditarSeguimientoViewModel(SeguimientoAcuerdoModel, SeguimientoAcuerdoModel.Unidades, UnidadesParaEjecucionAcuerdo);

                var TareaObtenerListaActaParaSelect = ElementoRelacionado.ObtenerListaActaParaSelectAsync(Convert.ToInt32(Id));
                var TareaObtenerListaAcuerdoParaSelect = ElementoRelacionado.ObtenerListaAcuerdoParaSelectAsync(Convert.ToInt32(Id));
                var TareaObtenerUnidades = Unidad.ObtenerTodosParaSelectAsync();

                IEnumerable<SelectListItem> ListaActasParaSelect = await TareaObtenerListaActaParaSelect;
                IEnumerable<SelectListItem> ListaAcuerdoParaSelect = await TareaObtenerListaAcuerdoParaSelect;
                IEnumerable<SelectListItem> ListaUnidadesParaSelect = await TareaObtenerUnidades;

                ViewBag.Unidades = ListaUnidadesParaSelect;
                ViewBag.ListaTiposElementosRelacionados = ListaTiposElementosRelacionados;
                ViewBag.ListaActas = ListaActasParaSelect;
                ViewBag.ListaAcuerdos = ListaAcuerdoParaSelect;

                return View(EditarSeguimientoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/Editar/
        [HttpPost]
        //[AuthorizeConfig(Roles = "Administrador, Profesional de apoyo")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarSeguimientoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                SeguimientoAcuerdo ModeloSeguimientoAcuerdo = Modelo.Entidad();

                var TareaEditarSeguimientoAcuerdo = SeguimientoAcuerdo.ActualizarAsync(ModeloSeguimientoAcuerdo);
                bool SeguimientoActualizado = await TareaEditarSeguimientoAcuerdo;

                if (SeguimientoActualizado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Seguimiento de Acuerdo");
                    return Json(new { success = SeguimientoActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Seguimiento Acuerdo");
                    return Json(new { success = SeguimientoActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seguimiento/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Eliminar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerSeguimietno = SeguimientoAcuerdo.ObtenerPorIdAsync(Convert.ToInt32(Id));
                SeguimientoAcuerdo ModeloSeguimiento = await TareaObtenerSeguimietno;

                if (ModeloSeguimiento == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloSeguimiento), message: Properties.Resources.ModeloNulo);
                }

                EliminarSeguimientoViewModel Modelo = new EliminarSeguimientoViewModel(ModeloSeguimiento);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Seguimiento
        /// </summary>
        // POST: /Seguimiento/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarSeguimientoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarSeguimientoAcuerdo = SeguimientoAcuerdo.EliminarAsync(Modelo.IdSeguimiento);
                bool SeguimientoAcuerdoEliminado = await TareaEliminarSeguimientoAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Seguimiento");

                return Json(new { success = SeguimientoAcuerdoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/AgregarUnidad/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarUnidad(string IdUnidad, string IdSeguimientoAcuerdo) {
            try {
                if ((string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) && (string.IsNullOrEmpty(IdSeguimientoAcuerdo) || !Funciones.IsNumber(IdSeguimientoAcuerdo))) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarUnidad = SeguimientoAcuerdo.AgregarUnidadAsync(Convert.ToInt32(IdSeguimientoAcuerdo), Convert.ToInt32(IdUnidad));
                var UnidadEjecucionSeguimientoAgregado = await TareaAgregarUnidad;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Unidad Ejecutora del Seguimiento");

                return Json(new { success = UnidadEjecucionSeguimientoAgregado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/EliminarUnidad/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarUnidad(string IdUnidad, string IdSeguimientoAcuerdo) {
            try {
                if ((string.IsNullOrEmpty(IdUnidad) || !Funciones.IsNumber(IdUnidad)) && (string.IsNullOrEmpty(IdSeguimientoAcuerdo) || !Funciones.IsNumber(IdSeguimientoAcuerdo))) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarUnidad = SeguimientoAcuerdo.EliminarUnidadAsync(Convert.ToInt32(IdSeguimientoAcuerdo), Convert.ToInt32(IdUnidad));
                var UnidadEjecucionSeguimientoEliminado = await TareaEliminarUnidad;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Unidad Ejecutora del Seguimiento");

                return Json(new { success = UnidadEjecucionSeguimientoEliminado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/EnviarCorreoParaSTJD?IdSeguimiento=2
        [HttpPost]
        //[AuthorizeConfig(Roles = "Administrador, Profesional de apoyo")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EnviarCorreoParaSTJD(string IdSeguimiento) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento) || !Funciones.IsNumber(IdSeguimiento)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Obtener el aviso por nombre para el segimiento,
                var TareaObtenerAviso = Aviso.ObtenerPorNombreAsync("Notificación de avance de seguimiento");
                Aviso ModeloAviso = await TareaObtenerAviso;

                // Crear enlace
                string Enlace = Url.Action("Editar", "Seguimiento", new { Id = IdSeguimiento.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Avance";
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

        // POST: /Seguimiento/NotificarUnidadesDeSeguimientoVencido?IdSeguimiento=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NotificarUnidadesDeSeguimientoVencido(string IdSeguimiento, bool EnviarCorreoUsuariosDeUnidades) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento) || !Funciones.IsNumber(IdSeguimiento)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Crear enlace
                string Enlace = Url.Action("Editar", "Seguimiento", new { Id = IdSeguimiento.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Seguimiento Vencido";
                string MensajeDetalle = "";

                var TareaEnviarAviso = SeguimientoAcuerdo.NotificarUnidadesDeSeguimientoVencido(Convert.ToInt32(IdSeguimiento), TextoEnlace, Enlace, MensajeDetalle, EnviarCorreoUsuariosDeUnidades);
                bool CorreoEnviado = await TareaEnviarAviso;

                return Json(new { success = CorreoEnviado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/NotificarUnidadesDeSeguimiento?IdSeguimiento=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NotificarUnidadesDeSeguimiento(string IdSeguimiento, bool EnviarCorreoUsuariosDeUnidades) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento) || !Funciones.IsNumber(IdSeguimiento)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Crear enlace
                string Enlace = Url.Action("Editar", "Seguimiento", new { Id = IdSeguimiento.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver Seguimiento";
                string MensajeDetalle = "";

                var TareaEnviarAviso = SeguimientoAcuerdo.NotificarUnidadesDeSeguimiento(Convert.ToInt32(IdSeguimiento), TextoEnlace, Enlace, MensajeDetalle, EnviarCorreoUsuariosDeUnidades);
                bool CorreoEnviado = await TareaEnviarAviso;

                return Json(new { success = CorreoEnviado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seguimiento/ObtenerArchivosSeguimiento?IdSeguimiento=2
        [HttpGet]
        //[AuthorizeConfig(Roles = "Administrador, Profesional Secretaría Técnica")]
        public async Task<JsonResult> ObtenerArchivosSeguimiento(string IdSeguimiento) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento) || !Funciones.IsNumber(IdSeguimiento)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerArchivosAdjuntos = SeguimientoAcuerdo.ObtenerAdjuntos(Convert.ToInt32(IdSeguimiento));
                IEnumerable<ArchivoAdjunto> ListaArchivosAdjuntos = await TareaObtenerArchivosAdjuntos;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivos = ListaArchivosAdjuntos.Select(a => new InicioArchivoAdjuntoViewModel(a, a.TipoObjeto.NombreTabla)).ToList();

                return Json(new { data = ListaArchivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Agregar un archivo adjunto a un seguimiento
        [HttpPost]
        //[AuthorizeConfig(Roles = "Administrador, Profesional Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarArchivoAdjunto(AgregarArchivoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerSeguimientoAcuerdo = SeguimientoAcuerdo.ObtenerPorIdAsync(Modelo.IdObjeto);
                SeguimientoAcuerdo ModeloSeguimientoAcuerdo = await TareaObtenerSeguimientoAcuerdo;

                Modelo.NombreObjeto = ModeloSeguimientoAcuerdo.NombreObjeto;

                ArchivoAdjunto ModeloArchivoAdjunto = Modelo.Entidad();
                ModeloArchivoAdjunto.Seguimiento = ModeloSeguimientoAcuerdo;

                var TareaAgregarAdjunto = SeguimientoAcuerdo.AgregarArchivoAdjuntoAsync(ModeloArchivoAdjunto, Modelo.Archivo);
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

        // POST: Acuerdo ejecutado.
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SeguimientoEjecutado(string IdSeguimiento) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaSeguimientoEjecutado = SeguimientoAcuerdo.SeguimientoEjecutadoAsync(Convert.ToInt32(IdSeguimiento));
                string SeguimientoEjecutado = await TareaSeguimientoEjecutado;

                if (SeguimientoEjecutado.Equals("Ok")) {
                    return new JsonResult { Data = new { success = true, Mensaje = "Seguimiento establecido como ejecutado" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

                if (SeguimientoEjecutado.Equals("Error"))
                    return new JsonResult { Data = new { success = false, Mensaje = "Error al actualizar el Acuerdo" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                else {
                    return new JsonResult { Data = new { success = false, Mensaje = SeguimientoEjecutado }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //////////
        // Avances del seguimiento
        //////////

        // POST: /Seguimiento/AgregarAvance/
        [HttpPost]
        //[AuthorizeConfig(Roles = "Administrador, Profesional Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarAvance(string AvanceJSON) {
            try {
                if (string.IsNullOrEmpty(AvanceJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en una entidad DetalleSeguimiento
                DetalleSeguimiento ModeloDetalle = JsonConvert.DeserializeObject<DetalleSeguimiento>(AvanceJSON);
                ModeloDetalle.IdUsuario = User.Identity.GetUserId();

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarDetalleSeguimiento = DetalleSeguimiento.AgregarAsync(ModeloDetalle);
                int IdNuevoDetalle = await TareaAgregarDetalleSeguimiento;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Avance");

                return Json(new { success = IdNuevoDetalle, IdNuevoDetalle = IdNuevoDetalle, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/ActualizarAvance/
        [HttpPost]
        //[AuthorizeConfig(Roles = "Administrador, Profesional Secretaría Técnica")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ActualizarAvance(string AvanceJSON) {
            try {
                if (string.IsNullOrEmpty(AvanceJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en una entidad DetalleSeguimiento
                DetalleSeguimiento ModeloDetalle = JsonConvert.DeserializeObject<DetalleSeguimiento>(AvanceJSON);
                ModeloDetalle.IdUsuario = User.Identity.GetUserId();

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEditarDetalleSeguimiento = DetalleSeguimiento.ActualizarAsync(ModeloDetalle);
                bool DetalleActualizado = await TareaEditarDetalleSeguimiento;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Avance");

                return Json(new { success = DetalleActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/EliminarAvance/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarAvance(string IdDetalle) {
            try {
                if (string.IsNullOrEmpty(IdDetalle) || !Funciones.IsNumber(IdDetalle)) {
                    return Json(new { success = false, Mensaje = "Error: id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarAvance = DetalleSeguimiento.EliminarAsync(Convert.ToInt32(IdDetalle));
                bool DetalleEliminado = await TareaEliminarAvance;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Avance");

                return Json(new { success = DetalleEliminado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        //////////
        // Fin de avances del seguimiento
        //////////

        // GET: /Seguimiento/GestorTipoSeguimiento/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> GestorTipoSeguimiento() {
            try {
                var TareaObtenerAcuerdos = Acuerdo.ObtenerTodosAsync();
                IEnumerable<Acuerdo> ListaAcuerdos = await TareaObtenerAcuerdos;

                var TareaObtenerListaTipoSeguimiento = Acuerdo.ObtenerTiposSeguimientoParaSelectAsync();
                ViewBag.ListaTipoSeguimiento = await TareaObtenerListaTipoSeguimiento;

                // Construir ViewModel con la información del DTO
                IEnumerable<InicioGestionTipoSeguimientoViewModel> Modelo = ListaAcuerdos.Select(a => new InicioGestionTipoSeguimientoViewModel(a)).ToList();
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarTipoSeguimiento(string IdAcuerdo, string TipoSeguimiento) {
            try {
                if ((string.IsNullOrEmpty(IdAcuerdo) || !Funciones.IsNumber(IdAcuerdo)) && (string.IsNullOrEmpty(TipoSeguimiento) || !Funciones.IsNumber(TipoSeguimiento))) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEditarAcuerdo = Acuerdo.ActualizarTipoSeguimiento(IdAcuerdo, TipoSeguimiento);
                bool AcuerdoActualizado = await TareaEditarAcuerdo;

                if (AcuerdoActualizado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Acuerdo");
                    return Json(new { success = AcuerdoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Acuerdo");
                    return Json(new { success = AcuerdoActualizado, message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /Seguimiento/ObtenerElementosRelacionados?IdSeguimiento=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico")]
        public async Task<JsonResult> ObtenerElementosRelacionados(string IdSeguimiento) {
            try {
                if (string.IsNullOrEmpty(IdSeguimiento) || !Funciones.IsNumber(IdSeguimiento)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerListaElementosRelacionados = ElementoRelacionado.ObtenerTodosPorIdSeguimientoAsync(Convert.ToInt32(IdSeguimiento));
                IEnumerable<ElementoRelacionado> ListaElementoRelacionado = await TareaObtenerListaElementosRelacionados;

                IEnumerable<InicioElementoRelacionadoViewModel> ElementosRelacionadosViewModel = ListaElementoRelacionado.Select(a => new InicioElementoRelacionadoViewModel(a, a.Acta, a.Acuerdo)).ToList();

                return Json(new { data = ElementosRelacionadosViewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/AgregarElementoRelacionado/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarElementoRelacionado(AgregarElementoRelacionadoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                // Crear una entidad de ElementoRelacionado
                ElementoRelacionado ModeloElementoRelacionado = Modelo.Entidad();

                var TareaAgregarElementoRelacionado = ElementoRelacionado.AgregarAsync(ModeloElementoRelacionado);
                int IdNuevoElementoRelacionado = await TareaAgregarElementoRelacionado;

                if (IdNuevoElementoRelacionado > 0) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Elemento Relacionado");
                    return Json(new { success = true, Message = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, Message = "Error al agregar el elemento relacionado" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /Seguimiento/AgregarElementoRelacionado?IdElementoRelacionado=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarElementoRelacionado(string IdElementoRelacionado) {
            try {
                if (string.IsNullOrEmpty(IdElementoRelacionado) || !Funciones.IsNumber(IdElementoRelacionado)) {
                    return Json(new { success = false, message = "Error: id´s inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Eliminar elemento relacionado del seguimiento
                var TareaEliminarElementoRelacionado = ElementoRelacionado.EliminarAsync(Convert.ToInt32(IdElementoRelacionado));
                bool ElementoRelacionadoEliminado = await TareaEliminarElementoRelacionado;

                string Mensaje;

                if (ElementoRelacionadoEliminado == true) {
                    Mensaje = Funciones.ObtenerMensajeExito("E", "Elemento Relacionado");
                }
                else {
                    Mensaje = Funciones.ObtenerMensajeError(new Exception("Problemas al eliminar el elemento relacionado"), "E", "Elemento Relacionado");
                }
                return Json(new { success = ElementoRelacionadoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}