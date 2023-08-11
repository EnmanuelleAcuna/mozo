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
    public class OrdenDiaNController : Controller {
        // Constructor y dependencias
        private readonly IOrdenDiaLogic OrdenDia;
        private readonly ISesionLogic Sesion;
        private readonly IEstadoLogic Estado;
        private readonly ISeccionLogic Seccion;
        private readonly ITemaLogic Tema;
        private readonly IAvisosLogic Aviso;
        private readonly IUsuarioLogic Usuario;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly IParametrosGeneralesLogic ParametroGeneral;

        public OrdenDiaNController(IEstadoLogic Estado, IOrdenDiaLogic OrdenDia, ISeccionLogic Seccion, ISesionLogic Sesion, ITemaLogic Tema, IAvisosLogic Aviso, IUsuarioLogic Usuario, ITipoObjetoLogic TipoObjeto, IEncabezadoPiePaginaLogic EncabezadoPiePagina, IArchivoAdjuntoLogic ArchivoAdjunto, IParametrosGeneralesLogic ParametroGeneral) {
            this.Estado = Estado;
            this.OrdenDia = OrdenDia;
            this.Seccion = Seccion;
            this.Sesion = Sesion;
            this.Tema = Tema;
            this.Aviso = Aviso;
            this.Usuario = Usuario;
            this.TipoObjeto = TipoObjeto;
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.ParametroGeneral = ParametroGeneral;
        }

        //Acciones
        // GET: /OrdenDiaN/InicioN/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Director, Coordinación Actas, Archivo")]
        public async Task<ActionResult> InicioN() {
            try {
                var TareaObtenerListaOrdenesDia = OrdenDia.ObtenerTodosAsync();
                var ListaOrdenesDia = await TareaObtenerListaOrdenesDia;
                IEnumerable<InicioOrdenDiaViewModel> ListaOrdenesDiaViewModel = ListaOrdenesDia.Select(OrdenDia => new InicioOrdenDiaViewModel(OrdenDia)).ToList();
                return View(ListaOrdenesDiaViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener información específica de la Orden del Dia para pasarlo en formato JSON a la vista DetalleN 
        /// </summary>
        // GET: /OrdenDiaN/DetallesN?IdOrdenDia=2
        [HttpGet]
        public async Task<ActionResult> DetalleN(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia)) {
                    throw new ArgumentNullException(paramName: nameof(IdOrdenDia), message: Properties.Resources.SolicitudIncorrecta);
                }

                var BuscarOrdenDiaConSecciones = OrdenDia.ObtenerPorIdAsync(Convert.ToInt32(IdOrdenDia));
                var ModeloOrdenDia = await BuscarOrdenDiaConSecciones;

                if (ModeloOrdenDia == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloOrdenDia), message: Properties.Resources.ModeloNulo);
                }

                // Obtener encabezado y pie de pagina del orden del día
                var TareaObtenerEncabezadoPiePaginaOrdenDia = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(1211);
                EncabezadoPiePagina EncabezadoPiePaginaOrdenDia = await TareaObtenerEncabezadoPiePaginaOrdenDia;

                // Obtener parametros de institución
                var TareaObtenerParametros = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                ParametroGeneral ParametroUnidad = await TareaObtenerParametros;

                DetallesOrdenDiaViewModel Modelo = new DetallesOrdenDiaViewModel(ModeloOrdenDia, EncabezadoPiePaginaOrdenDia.Encabezado, EncabezadoPiePaginaOrdenDia.PiePagina, ParametroUnidad);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener información específica de la Orden del Dia aprobada para pasarlo en formato JSON a la vista DetalleAprobado
        /// </summary>
        // GET: /OrdenDiaN/DetallesN?IdOrdenDia=2
        [HttpGet]
        public async Task<ActionResult> DetalleAprobado(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia)) {
                    throw new ArgumentNullException(paramName: nameof(IdOrdenDia), message: Properties.Resources.SolicitudIncorrecta);
                }

                var BuscarOrdenDiaConSecciones = OrdenDia.ObtenerAprobadaPorIdAsync(Convert.ToInt32(IdOrdenDia));
                var ModeloOrdenDia = await BuscarOrdenDiaConSecciones;

                if (ModeloOrdenDia.Id <= 0) {
                    return RedirectToAction("Error404", "Error");
                }

                // Obtener encabezado y pie de pagina del orden del día
                var TareaObtenerEncabezadoPiePaginaOrdenDia = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(1211);
                EncabezadoPiePagina EncabezadoPiePaginaOrdenDia = await TareaObtenerEncabezadoPiePaginaOrdenDia;

                // Obtener parametros de institución
                var TareaObtenerParametros = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                ParametroGeneral ParametroUnidad = await TareaObtenerParametros;

                DetallesOrdenDiaViewModel Modelo = new DetallesOrdenDiaViewModel(ModeloOrdenDia, EncabezadoPiePaginaOrdenDia.Encabezado, EncabezadoPiePaginaOrdenDia.PiePagina, ParametroUnidad);
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /OrdenDiaN/EditarN?IdSesion=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Director, Coordinación Actas, Abogado Secretaría Técnica, Auditor, Abogado, Gerente General, Jefe de despacho, Subgerente Administrativo, Archivo")]
        public async Task<ActionResult> EditarN(string IdSesion) {
            try {
                if (string.IsNullOrEmpty(IdSesion)) {
                    throw new ArgumentNullException(paramName: nameof(IdSesion), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var ModeloOrdenDia = await TareaObtenerOrdenDia;

                if (ModeloOrdenDia == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloOrdenDia), message: Properties.Resources.ModeloNulo);
                }

                var TareaObtenerSecciones = Seccion.ObtenerTodosParaSelectAsync(ModeloOrdenDia.Id);
                ViewBag.ListaSecciones = await TareaObtenerSecciones;

                EditarOrdenDiaViewModel Modelo = new EditarOrdenDiaViewModel(ModeloOrdenDia);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un Orden del Día
        /// </summary>
        // POST: /OrdenDiaN/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Director, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarN(EditarOrdenDiaViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                OrdenDia ModeloOrdenDia = Modelo.Entidad();

                var TareaActualizarOD = OrdenDia.ActualizarAsync(ModeloOrdenDia);
                bool ODActualizada = await TareaActualizarOD;

                if (ODActualizada == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Orden del Día");
                    return Json(new { success = ODActualizada, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Orden del Día");
                    return Json(new { success = ODActualizada, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }      

        // Cambiar el estado del orden del día de borrador a visto bueno 
        // POST: /OrdenDiaN/VistoBueno?IdOrdenDia=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VistoBueno(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEnviarVistoBueno = OrdenDia.EnviarVistoBuenoAsync(Convert.ToInt32(IdOrdenDia));
                bool EnviaVistoBueno = await TareaEnviarVistoBueno;

                if (EnviaVistoBueno == true) {
                    return Json(new { success = EnviaVistoBueno, Mensaje = "Orden del Día con visto bueno" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = EnviaVistoBueno, Mensaje = "Error al dar visto bueno al Orden del Día" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Cambiar el estado del orden del día de borrador a visto bueno 
        // POST: /OrdenDiaN/Convocar?IdOrdenDia=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Convocar(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEnviarConvocar = OrdenDia.EnviarConvocarAsync(Convert.ToInt32(IdOrdenDia));
                bool EnviaConvocar = await TareaEnviarConvocar;

                if (EnviaConvocar == true) {
                    return Json(new { success = EnviaConvocar, Mensaje = "Orden del Día convocada" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = EnviaConvocar, Mensaje = "Error al convocar el Orden del Día" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //////////
        //Secciones
        //////////

        /// <summary>
        /// Agregar una seccion a un orden del día
        /// </summary>
        // POST: /OrdenDia/AgregarSeccion?IdOrdenDia=2&IdNuevaSeccion=13
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarSeccion(string IdOrdenDia, string IdNuevaSeccion) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia) || !Funciones.IsNumber(IdOrdenDia) || string.IsNullOrEmpty(IdNuevaSeccion) || !Funciones.IsNumber(IdNuevaSeccion)) {
                    return Json(new { success = false, message = "Error: información inválida." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarSeccionOrdenDia = OrdenDia.AgregarSeccionAsync(Convert.ToInt32(IdOrdenDia), Convert.ToInt32(IdNuevaSeccion));
                bool SeccionAgregada = await TareaAgregarSeccionOrdenDia;

                string Mensaje;

                if (SeccionAgregada == true) {
                    Mensaje = Funciones.ObtenerMensajeExito("A", "Sección de Orden del Día");
                }
                else {
                    Mensaje = Funciones.ObtenerMensajeError(new Exception("Problemas al eliminar seccion"), "A", "Sección de Orden del Día");
                }

                return Json(new { success = SeccionAgregada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un elemento Seccion mediante su id y sus debidos temas.
        /// </summary>
        // POST: /OrdenDia/EliminarSeccion?IdOrdenDia=2&IdSeccion=13
        [HttpPost]
        [ActionName("EliminarSeccion")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarSeccion(string IdOrdenDia, string IdSeccion) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia) || !Funciones.IsNumber(IdOrdenDia) || string.IsNullOrEmpty(IdSeccion) || !Funciones.IsNumber(IdSeccion)) {
                    return Json(new { success = false, message = "Error: id´s inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Eliminar la sección del orden del día
                var TareaEliminarSeccion = OrdenDia.EliminarSeccionAsync(Convert.ToInt32(IdOrdenDia), Convert.ToInt32(IdSeccion));
                bool SeccionEliminada = await TareaEliminarSeccion;

                string Mensaje;

                if (SeccionEliminada == true) {
                    Mensaje = Funciones.ObtenerMensajeExito("E", "Sección de Orden del Día");
                }
                else {
                    Mensaje = Funciones.ObtenerMensajeError(new Exception("Problemas al eliminar seccion"), "A", "Sección de Orden del Día");
                }

                return Json(new { success = SeccionEliminada, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
        //////////
        // Fin de Secciones
        //////////

        //////////
        // Temas
        //////////

        // POST: /OrdenDia/AgregarTema/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarTema(string TemaJSON) {
            try {
                if (string.IsNullOrEmpty(TemaJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en una entidad Tema
                Tema ModeloTema = JsonConvert.DeserializeObject<Tema>(TemaJSON);

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarTema = Tema.AgregarAsync(ModeloTema);
                int IdNuevoTema = await TareaAgregarTema;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Tema");

                return Json(new { success = IdNuevoTema, IdNuevoTema = IdNuevoTema, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /OrdenDia/ActualizarTema/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Profesional Secretaría Técnica, Secretario Técnico, Director, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ActualizarTema(string TemaJSON) {
            try {
                if (string.IsNullOrEmpty(TemaJSON)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Deserializar el objeto JSON en un elemento tema
                Tema ModeloTema = JsonConvert.DeserializeObject<Tema>(TemaJSON);

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaActualizarTema = await Tema.ActualizarAsync(ModeloTema);
                bool TemaActualizado = TareaActualizarTema;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Tema");

                return Json(new { success = TemaActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /OrdenDia/EliminarTema/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarTema(string IdTema) {
            try {
                if (string.IsNullOrEmpty(IdTema) || !Funciones.IsNumber(IdTema)) {
                    return Json(new { success = false, Mensaje = "Error: id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarTema = Tema.EliminarAsync(Convert.ToInt32(IdTema));
                bool TemaEliminado = await TareaEliminarTema;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Tema");

                return Json(new { success = TemaEliminado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista de todos los temas que han son aprovados pero no ejecutados para el OrdenDia  esto
        /// con el fin de poder cargar un segmento de temas resagados en formato JSON
        /// </summary>
        // GET: /OrdenDia/Tema/ObtenerTemasPendientes
        [HttpGet]
        public async Task<JsonResult> ObtenerTemasPendientes(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia) || !Funciones.IsNumber(IdOrdenDia)) {
                    return Json(new { success = false, message = "Error: id es inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerEstadoPendienteTema = Estado.ObtenerPorCodigoAsync("TEM-PEND");
                Estado EstadoPendienteTema = await TareaObtenerEstadoPendienteTema;

                var TareaObtenerTemaOrdenDia = Tema.ObtenerTodosPorIdEstadoIdOrdenDia(EstadoPendienteTema.Id, Convert.ToInt32(IdOrdenDia));
                IEnumerable<Tema> ListaTemaPendiente = await TareaObtenerTemaOrdenDia;

                return Json(new { data = ListaTemaPendiente }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //POST: /OrdenDia/AgregarTemaPendiente/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico")]
        public async Task<JsonResult> AgregarTemaPendiente(string IdTema, string IdOrdenDia, string IdSeccion) {
            try {
                if (string.IsNullOrEmpty(IdTema) || !Funciones.IsNumber(IdTema) || string.IsNullOrEmpty(IdOrdenDia) || !Funciones.IsNumber(IdOrdenDia) || string.IsNullOrEmpty(IdSeccion) || !Funciones.IsNumber(IdSeccion)) {
                    return Json(new { success = false, message = "Error: id es inválido." }, JsonRequestBehavior.AllowGet);
                }

                string Mensaje;

                var TareaActualizarTemaPendiente = Tema.ActualizarTemaPendienteAsync(Convert.ToInt32(IdTema), Convert.ToInt32(IdOrdenDia), Convert.ToInt32(IdSeccion));
                bool TemaPendienteActualizado = await TareaActualizarTemaPendiente;

                if (TemaPendienteActualizado == true) {
                    Mensaje = Funciones.ObtenerMensajeExito("A", "Tema");
                }
                else {
                    Mensaje = Funciones.ObtenerMensajeError(new Exception("Error al agregar tema pendiente"), "A", "Tema");
                }

                return Json(new { success = TemaPendienteActualizado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }

        }

        /// <summary>
        /// Actualizar el estado de un tema del Orden del Día
        /// </summary>
        // POST: /OrdenDia/ActualizarEstadoTema/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico")]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> ActualizarEstadoTema(string TemaJSON) {
            try {
                if (string.IsNullOrEmpty(TemaJSON)) {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                // Deserealizar TemaJSON en una instancia de la clase Tema
                Tema ModeloTema = JsonConvert.DeserializeObject<Tema>(TemaJSON);

                var tareaEditarTema = Tema.ActualizarEstadoAsync(ModeloTema);
                bool EstadoActualizado = await tareaEditarTema;

                return Json(new { success = EstadoActualizado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un tema en espesifico de una seccion de un orden dia
        /// </summary>
        // POST: /OrdenDia/Tema/ActualizarPosicionTema?IdTema=2?troIdTema=2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ActualizarPosicionTema(int? idTema, int? otroIdTema) {
            try {
                if (idTema == null || otroIdTema == null) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }
                string Mensaje = "";
                var ActualizarPosicionTema = false;

                // obtenemos el tema a cambiar 
                var TareaObtenerTemaActual = Tema.ObtenerPorIdAsync((int)idTema);
                // obtenemos el segundo tema a cambiar 
                var TareaObtenerTemaCambio = Tema.ObtenerPorIdAsync((int)otroIdTema);

                Tema TemaActualModel = await TareaObtenerTemaActual;
                Tema TemaCambioModel = await TareaObtenerTemaCambio;

                if (TemaActualModel != null && TemaCambioModel != null) {
                    if (TemaActualModel.IdSeccion == TemaCambioModel.IdSeccion) {
                        var TareaActualizarPosicionTema = Tema.ActualizarPosicionTema(TemaActualModel, TemaCambioModel);
                        ActualizarPosicionTema = await TareaActualizarPosicionTema;

                        Mensaje = Funciones.ObtenerMensajeExito("A", "Cambio de Tema");
                    }
                    else {
                        Mensaje = "No puede realizar este cambio";
                    }
                }
                else {
                    ActualizarPosicionTema = false;
                    Mensaje = "No puede realizar este cambio";
                }

                return Json(new { success = ActualizarPosicionTema, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Vista que se utiliza para trabajar sobre los temas resagados
        /// </summary>
        // GET: /OrdenDia/Tema/ActualizarTemaResagado
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        public ActionResult ActualizarTemaResagado() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un tema en espesifico de una seccion de un orden dia
        /// </summary>
        // POST: /OrdenDia/Tema/ActualizarTemaResagado
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas, Secretario Técnico, Director")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ActualizarTemaResagado(string temaJson) {
            try {
                if (string.IsNullOrEmpty(temaJson)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }
                string Mensaje = "";

                // Deserializar el objeto JSON en un elemento tema
                var TemaModelTemporal = JsonConvert.DeserializeObject<Tema>(temaJson);

                //llenamos todo el modelo para ser agregado
                var TareaObtenerTema = Tema.ObtenerPorIdAsync(TemaModelTemporal.Id);
                Tema TemaModel = await TareaObtenerTema;

                TemaModel.IdEstado = 8;
                TemaModel.IdOrdenDia = TemaModelTemporal.IdOrdenDia;
                TemaModel.IdSeccion = TemaModelTemporal.IdSeccion;

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var ObtenerUltimoTemaModel = Tema.ObtenerUltimoPorIdSeccionIdOrdenDia(TemaModelTemporal.IdSeccion, (int)TemaModelTemporal.IdOrdenDia);

                var TareaActualizarTema = Tema.ActualizarAsync(TemaModel);
                var ActualizarTema = await TareaActualizarTema;

                if (ActualizarTema == true) {
                    Mensaje = Funciones.ObtenerMensajeExito("A", "Tema");
                }
                else {
                    Mensaje = Funciones.ObtenerMensajeError(new Exception("Problemas al actualizar tema"), "A", "Tema");
                }

                return Json(new { success = ActualizarTema, message = Mensaje, ObtenerUltimoTemaModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Extraer el aviso de la orden del día.
        /// </summary>
        // GET: /OrdenDia/Avisos/ObtenerAviso?IdOrdenDia=2
        [HttpGet]
        public async Task<JsonResult> ObtenerAviso(int? idOrdenDia) {
            try {
                if (idOrdenDia == null) {
                    return Json(new { success = false, message = "Error: modelo inválido." }, JsonRequestBehavior.AllowGet);
                }
                //Tarea para obtener el orden día del cual se extrae su estado
                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdAsync((int)idOrdenDia);
                var OrdenDiaMoldelo = await TareaObtenerOrdenDia;

                //Tarea para obtener el estado del cual se extrae su aviso
                var TareaObtenerEstado = Estado.ObtenerPorIdAsync(OrdenDiaMoldelo.IdEstado);
                var EstadoMoldelo = await TareaObtenerEstado;

                //Tarea para obtener el avio
                var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(EstadoMoldelo.IdAviso);
                var AvisoModelo = await TareaObtenerAviso;

                return Json(new { avisoModelo = AvisoModelo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Enviar un correo con notificación con estado del acuerdo 
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Profesional Secretaría Técnica, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EnviarCorreo(string IdOrdenDia) {
            try {
                if (string.IsNullOrEmpty(IdOrdenDia) || !Funciones.IsNumber(IdOrdenDia)) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdAsync(Convert.ToInt32(IdOrdenDia));
                OrdenDia ModeloOrdenDia = await TareaObtenerOrdenDia;

                // Obtener el aviso del estado del acuerdo,
                Aviso ModeloAviso = ModeloOrdenDia.Estado.Aviso;

                // Crear enlace
                string Enlace = Url.Action("EditarN", "OrdenDiaN", new { IdSesion = ModeloOrdenDia.IdSesion.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                string TextoEnlace = "Ver el Orden del Día";
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

        // Archivos adjuntos y relacionados de un tema
        // GET: Obtener archivos adjuntos relacionados a un tema
        [HttpGet]
        public async Task<JsonResult> ObtenerArchivosTema(string IdTema) {
            try {
                var TareaObtenerTema = Tema.ObtenerPorIdAsync(Convert.ToInt32(IdTema));
                Tema ModeloTema = await TareaObtenerTema;

                var TareaObtenerArchivosAdjuntosTema = Tema.ObtenerAdjuntosAsync(ModeloTema);
                IEnumerable<ArchivoAdjunto> ListaArchivosRelacionadosTema = await TareaObtenerArchivosAdjuntosTema;

                IEnumerable<InicioArchivoAdjuntoViewModel> ListaArchivos = ListaArchivosRelacionadosTema.Select(a => new InicioArchivoAdjuntoViewModel(a, a.TipoObjeto.NombreTabla)).ToList();

                return Json(new { data = ListaArchivos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Agregar un archivo adjunto a un tema
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Secretario Técnico, Profesional Secretaría Técnica, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarArchivoAdjunto(AgregarArchivoViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Message = "Error, modelo inválido" }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerTemaConOrdenDia = Tema.ObtenerPorIdConOrdenDiaAsync(Modelo.IdObjeto);
                Tema ModeloTemaConOrdenDia = await TareaObtenerTemaConOrdenDia;

                Modelo.NombreObjeto = ModeloTemaConOrdenDia.NombreObjeto;

                ArchivoAdjunto ModeloArchivoAdjunto = Modelo.Entidad();
                ModeloArchivoAdjunto.Tema = ModeloTemaConOrdenDia;

                var TareaAgregarAdjunto = Tema.AgregarArchivoAdjuntoAsync(ModeloArchivoAdjunto, Modelo.Archivo);
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
    }
}
