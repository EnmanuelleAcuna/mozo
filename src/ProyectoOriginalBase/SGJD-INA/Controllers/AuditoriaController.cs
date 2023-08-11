using Ionic.Zip;
using Microsoft.AspNet.Identity;
using Rotativa;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class AuditoriaController : Controller {
        // Constructor y dependencias
        private readonly ITomoLogic Tomo;
        private readonly IActaLogic Acta;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IParametrosGeneralesLogic ParametroGeneral;
        private readonly IUsuarioLogic Usuario;
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly IRepositorioLogic Repositorio;

        public AuditoriaController(ITomoLogic Tomo, IActaLogic Acta, ITipoObjetoLogic TipoObjeto, IParametrosGeneralesLogic ParametroGeneral, IUsuarioLogic Usuario, IEncabezadoPiePaginaLogic EncabezadoPiePagina, IRepositorioLogic Repositorio) {
            this.Tomo = Tomo;
            this.Acta = Acta;
            this.TipoObjeto = TipoObjeto;
            this.ParametroGeneral = ParametroGeneral;
            this.Usuario = Usuario;
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.Repositorio = Repositorio;
        }

        // Acciones
        // GET: /Auditoria/Tomos/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Secretario Técnico, Archivo")]
        public async Task<ActionResult> Tomos() {
            try {
                var TareaObtenerTomos = Tomo.ObtenerTodosConActasAsync();
                IEnumerable<Tomo> ListaTomos = await TareaObtenerTomos;
                IEnumerable<InicioTomoViewModel> ListaTomosViewModel = ListaTomos.Select(t => new InicioTomoViewModel(t)).ToList();
                return View(ListaTomosViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/DetalleTomo/5
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Secretario Técnico, Archivo")]
        public async Task<ActionResult> DetalleTomo(int Id) {
            try {
                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTomo = Tomo.ObtenerPorIdConActasAsync(Id);

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                Tomo ModeloTomo = await TareaObtenerTomo;

                DetalleTomoViewModel TomoViewModel = new DetalleTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad);

                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/AperturaTomo/
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        public async Task<ActionResult> AperturaTomo() {
            try {
                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTipoObjetoTomo = TipoObjeto.ObtenerPorNombreAsync("Tomo");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                TipoObjeto TipoObjetoTomo = await TareaObtenerTipoObjetoTomo;

                AgregarTomoViewModel TomoViewModel = new AgregarTomoViewModel(ParametroNombreLibro, ParametroUnidad, TipoObjetoTomo);

                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar AperturaLibro
        /// </summary>
        // POST: /Auditoria/AperturaTomo/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AperturaTomo(AgregarTomoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    ApplicationUser Usuario = new ApplicationUser { Id = User.Identity.GetUserId(), Nombre = User.NombreCompleto() };

                    Tomo ModeloTomo = new Tomo(Modelo.ObservacionApertura, Modelo.FechaApertura, Usuario, Modelo.Asiento);

                    var TareaAgregarTomo = Tomo.AgregarAsync(ModeloTomo);
                    ModeloTomo = await TareaAgregarTomo;

                    if (ModeloTomo != null && ModeloTomo.Id > 0) {
                        // Generar el pdf con oficio de apertura de tomo recien abierto
                        ActionAsPdf AR = new ActionAsPdf("DetalleAperturaTomoPDF", new { Id = ModeloTomo.Id });
                        AR.PageSize = Rotativa.Options.Size.Legal;
                        AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                        AR.PageMargins = new Rotativa.Options.Margins() { Left = 1, Right = 1 };

                        string Params = "--print-media-type --disable-smart-shrinking";
                        string Header = string.Format("--page-offset 0 --allow {0} --header-html {0}", Url.Action("Encabezado", "Actas", null, "https"));

                        AR.CustomSwitches = string.Format("{0} {1}", Params, Header);

                        byte[] PDFData = AR.BuildFile(ControllerContext);

                        // Guardar el oficio pdf de apertura en el repositorio
                        var TareaGuardarOficioApertura = Repositorio.GuardarOficioPDFRepositorioTomo(ModeloTomo, PDFData, "Oficio de Apertura del " + ModeloTomo.Nombre);
                        string OficioAperturaGuardado = await TareaGuardarOficioApertura;

                        // Actualizar el campo UrlOficioApertura en la tabla de tomos
                        var TareaActualizarUrlOficioApertura = Tomo.ActualizarUrlOficioAperturaAsync(ModeloTomo.Id, OficioAperturaGuardado);
                        bool UrlOficioAperturaActualizado = await TareaActualizarUrlOficioApertura;

                        // Enviar notificación si el proceso de actualizar url se realizó con exito
                        if (UrlOficioAperturaActualizado == true) {
                            // Proceder a confeccionar el aviso para enviar por correo la notificacion de que se aperturó el tomo
                            string Enlace = Url.Action("DetalleTomo", "Auditoria", new { Id = ModeloTomo.Id.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                            string TextoEnlace = "Ver Tomo";
                            string NombreAviso = "Tomo Abierto";
                            string MensajeDetalle = ModeloTomo.Nombre + ", " + "asiento  " + ModeloTomo.Asiento + ", " + "por " + ModeloTomo.UsuarioApertura.Nombre;

                            var TareaEnviarNotificacion = Tomo.NotificarAsync(NombreAviso, Enlace, TextoEnlace, MensajeDetalle);
                            bool TomoNotificado = await TareaEnviarNotificacion;

                            if (TomoNotificado == true) {
                                return RedirectToAction("DetalleAperturaTomo", new { Id = ModeloTomo.Id, TomoGuardado = "true" });
                            }
                            else {
                                // Error al notificar apertura
                                ModelState.AddModelError("", "Error al notificar la apertura");
                            }
                        }
                        else {
                            // Error al generar pdf con oficio de apertura
                            ModelState.AddModelError("", "Error al procesar el oficio de apertura del tomo");
                        }
                    }
                    else {
                        // Error al guardar el tomo en la abse de datos
                        ModelState.AddModelError("", "Error al procesar la apertura del tomo");
                    }
                }
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/DetalleAperturaTomo/5
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Secretario Técnico, Archivo")]
        public async Task<ActionResult> DetalleAperturaTomo(int Id, string TomoGuardado) {
            try {
                // Al cargar la vista, si la carga proviene de la vista [Agregar], esta envía una variable de control para indicar
                // si el tomo fue guardado con exito, para mostrar el mensaje de que se realizó correctamente la acción.
                if (!string.IsNullOrEmpty(TomoGuardado) && TomoGuardado.Equals("true")) {
                    ViewBag.TomoGuardado = TomoGuardado;
                }

                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Id);
                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TOMOS");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                Tomo ModeloTomo = await TareaObtenerTomo;
                TipoObjeto TipoObjetoTomo = await TareaObtenerTipoObjeto;

                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(TipoObjetoTomo.Id);
                EncabezadoPiePagina EncabezadoPiePaginaTomo = await TareaObtenerEncabezadoPiePaginaActa;

                DetalleAperturaTomoViewModel TomoViewModel = new DetalleAperturaTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad, EncabezadoPiePaginaTomo);
                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/DetalleAperturaTomo/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetalleAperturaTomoPDF(string Id) {
            try {
                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Convert.ToInt32(Id));
                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TOMOS");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                Tomo ModeloTomo = await TareaObtenerTomo;
                TipoObjeto TipoObjetoTomo = await TareaObtenerTipoObjeto;

                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(TipoObjetoTomo.Id);
                EncabezadoPiePagina EncabezadoPiePaginaTomo = await TareaObtenerEncabezadoPiePaginaActa;

                DetalleAperturaTomoViewModel TomoViewModel = new DetalleAperturaTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad, EncabezadoPiePaginaTomo);

                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GenerarOficioAperturaPDF(string Id) {
            try {
                //ViewAsPdf VW = new ViewAsPdf("NombreVista", modelo);
                ActionAsPdf AR = new ActionAsPdf("DetalleAperturaTomoPDF", new { Id = Id });
                AR.PageSize = Rotativa.Options.Size.Legal;
                AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                AR.PageMargins = new Rotativa.Options.Margins() { Left = 1, Right = 1 };

                string Params = "--print-media-type --disable-smart-shrinking";
                string Header = string.Format("--page-offset 0 --allow {0} --header-html {0}", Url.Action("Encabezado", "Actas", null, "https"));

                AR.CustomSwitches = string.Format("{0} {1}", Params, Header);
                //AR.FileName = "ActaPDF.pdf";

                byte[] PDFData = AR.BuildFile(ControllerContext);

                return File(PDFData, "application/pdf", "AperturaTomo.pdf");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/Edit/5
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        public async Task<ActionResult> CierreTomo(int Id) {
            try {
                var TareaObtenerTomo = Tomo.ObtenerPorIdConActasAsync(Id);
                Tomo ModeloTomo = await TareaObtenerTomo;

                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;

                CierreTomoViewModel TomoViewModel = new CierreTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad);

                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CierreTomo(CierreTomoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                    return View(Modelo);
                }

                ApplicationUser Usuario = new ApplicationUser { Id = User.Identity.GetUserId(), Nombre = User.NombreCompleto() };

                Tomo ModeloTomo = Modelo.Entidad();

                ModeloTomo.IdUsuarioCierre = Usuario.Id;

                var TareaCierreTomo = Tomo.CerrarAsync(ModeloTomo);
                ModeloTomo = await TareaCierreTomo;

                if (ModeloTomo != null && ModeloTomo.Id > 0) {
                    // Generar el pdf con oficio de apertura de tomo recien abierto
                    ActionAsPdf AR = new ActionAsPdf("DetalleCierreTomoPDF", new { Id = Modelo.IdTomo });
                    AR.PageSize = Rotativa.Options.Size.Legal;
                    AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                    AR.PageMargins = new Rotativa.Options.Margins() { Left = 1, Right = 1 };

                    // Obtener consecutivo del foliado para oficio de cierre
                    var TareaObtenerPaginasTomo = Tomo.ObtenerConsecutivoPaginaPorIdAsync(Modelo.IdTomo);
                    int CantidadPaginasTomo = await TareaObtenerPaginasTomo;

                    string Params = "--print-media-type --disable-smart-shrinking";
                    string Header = string.Format("--page-offset {1} --allow {0} --header-html {0}", Url.Action("Encabezado", "Actas", null, "https"), CantidadPaginasTomo);

                    AR.CustomSwitches = string.Format("{0} {1}", Params, Header);

                    byte[] PDFData = AR.BuildFile(ControllerContext);

                    // Guardar el oficio pdf de cierre en el repositorio
                    var TareaGuardarOficioCierre = Repositorio.GuardarOficioPDFRepositorioTomo(ModeloTomo, PDFData, "Oficio de Cierre del " + ModeloTomo.Nombre);
                    string OficioCierreTomoGuardado = await TareaGuardarOficioCierre;

                    // Actualizar el campo UrlOficioCierre en la tabla de tomos
                    var TareaActualizarUrlOficioCierre = Tomo.ActualizarUrlOficioCierreAsync(ModeloTomo.Id, OficioCierreTomoGuardado);
                    bool UrlOficioCierreActualizado = await TareaActualizarUrlOficioCierre;

                    // Enviar notificación si proceso de actualizar url se realizó con exito
                    if (UrlOficioCierreActualizado == true) {
                        // Proceder a confeccionar el aviso para enviar por correo la notificacion de que se cerro el tomo
                        string Enlace = Url.Action("DetalleTomo", "Auditoria", new { Id = Modelo.IdTomo.ToString() }, protocol: HttpContext.Request.Url.Scheme);
                        string TextoEnlace = "Ver Tomo";
                        string NombreAviso = "Tomo Cerrado";
                        string MensajeDetalle = ModeloTomo.Nombre + ", " + "asiento " + ModeloTomo.Asiento + ", " + "por " + Usuario.Nombre;

                        var TareaEnviarNotificacion = Tomo.NotificarAsync(NombreAviso, Enlace, TextoEnlace, MensajeDetalle);
                        bool TomoNotificado = await TareaEnviarNotificacion;

                        // Si la url se actualizó correctamente redirecciona a la pagina de detalle cierre
                        if (TomoNotificado == true) {
                            return RedirectToAction("DetalleCierreTomo", new { Id = Modelo.IdTomo, TomoGuardado = "true" });
                        }
                        else {
                            // Error al guardar pdf con oficio de cierre
                            ModelState.AddModelError("", "Error al notificar el cierre");
                        }
                    }
                    else {
                        // Error al generar pdf con oficio de apertura
                        ModelState.AddModelError("", "Error al procesar el oficio de apertura del tomo");
                    }
                }
                else {
                    // Error a cerrar tomo y guardar en base de datos
                    ModelState.AddModelError("", "Error al guardar la información");
                }
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/DetalleCierreTomo/5
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Secretario Técnico, Archivo")]
        public async Task<ActionResult> DetalleCierreTomo(string Id) {
            try {
                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Convert.ToInt32(Id));
                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TOMOS");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                Tomo ModeloTomo = await TareaObtenerTomo;
                TipoObjeto TipoObjetoTomo = await TareaObtenerTipoObjeto;

                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(TipoObjetoTomo.Id);
                EncabezadoPiePagina EncabezadoPiePaginaTomo = await TareaObtenerEncabezadoPiePaginaActa;

                DetalleCierreTomoViewModel TomoViewModel = new DetalleCierreTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad, EncabezadoPiePaginaTomo);
                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: Auditoria/DetalleCierreTomoPDF/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DetalleCierreTomoPDF(string Id) {
            try {
                var TarearObtenerNombreLibro = ParametroGeneral.ObtenerPorNombreAsync("NombreLibroActas");
                var TarearObtenerNombreUnidad = ParametroGeneral.ObtenerPorNombreAsync("Unidad");
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Convert.ToInt32(Id));
                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TOMOS");

                ParametroGeneral ParametroNombreLibro = await TarearObtenerNombreLibro;
                ParametroGeneral ParametroUnidad = await TarearObtenerNombreUnidad;
                Tomo ModeloTomo = await TareaObtenerTomo;
                TipoObjeto TipoObjetoTomo = await TareaObtenerTipoObjeto;

                var TareaObtenerEncabezadoPiePaginaActa = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(TipoObjetoTomo.Id);
                EncabezadoPiePagina EncabezadoPiePaginaTomo = await TareaObtenerEncabezadoPiePaginaActa;

                DetalleCierreTomoViewModel TomoViewModel = new DetalleCierreTomoViewModel(ModeloTomo, ParametroNombreLibro, ParametroUnidad, EncabezadoPiePaginaTomo);

                return View(TomoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GenerarOficioCierrePDF(string Id) {
            try {
                ActionAsPdf AR = new ActionAsPdf("DetalleCierreTomoPDF", new { Id = Id });
                AR.PageSize = Rotativa.Options.Size.Legal;
                AR.PageOrientation = Rotativa.Options.Orientation.Portrait;
                AR.PageMargins = new Rotativa.Options.Margins() { Left = 1, Right = 1 };

                // Obtener consecutivo del foliado para oficio de cierre
                var TareaObtenerPaginasTomo = Tomo.ObtenerConsecutivoPaginaPorIdAsync(Convert.ToInt32(Id));
                int CantidadPaginasTomo = await TareaObtenerPaginasTomo;

                string Params = "--print-media-type --disable-smart-shrinking";
                string Header = string.Format("--page-offset {1} --allow {0} --header-html {0}", Url.Action("Encabezado", "Actas", null, "https"), CantidadPaginasTomo);

                AR.CustomSwitches = string.Format("{0} {1}", Params, Header);
                //AR.FileName = "ActaPDF.pdf";

                byte[] PDFData = AR.BuildFile(ControllerContext);

                return File(PDFData, "application/pdf", "CierreTomo.pdf");
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Auditor, Sub. Auditor, Usuario De Consulta Auditoría, Coordinación Actas, Secretario Técnico, Archivo")]
        public async Task<ActionResult> Descargar(string IdTomo) {
            try {
                if (string.IsNullOrEmpty(IdTomo) || !Funciones.IsNumber(IdTomo)) {
                    return RedirectToAction("Error404", "Error");
                }

                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Convert.ToInt32(IdTomo));
                var ModeloTomo = await TareaObtenerTomo;

                var TareaDescargarTomo = Tomo.ObtenerZipAsync(ModeloTomo.Nombre);
                ZipFile TomoZip = await TareaDescargarTomo;

                if (TomoZip.Entries.Any()) {
                    MemoryStream MemoryStream = new MemoryStream();
                    TomoZip.Save(MemoryStream);
                    return File(MemoryStream.ToArray(), "application/zip", ModeloTomo.Nombre + ".zip");
                }
                else {
                    return RedirectToAction("Error404", "Error");
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: Subir oficio de apertura firmado
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarAperturaFirmado(SubirOficioFirmadoViewModel Modelo) {
            try {
                // Valida el modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { seccess = false, Message = "Error, modelo invalido" }, JsonRequestBehavior.AllowGet);
                }

                // Obtener el modelo tomo por [IdTomo]
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Modelo.IdTomo);
                Tomo ModeloTomo = await TareaObtenerTomo;

                MemoryStream MS = new MemoryStream();
                Modelo.Archivo.InputStream.CopyTo(MS);
                byte[] PDFData = MS.ToArray();

                // Guardar el oficio apertura firmado en el repositorio
                var TareaGuardarOficioAperturaFirmado = Repositorio.GuardarOficioPDFRepositorioTomo(ModeloTomo, PDFData, "Oficio de Apertura del " + ModeloTomo.Nombre);
                string OficioAperturaFirmadoGuardado = await TareaGuardarOficioAperturaFirmado;

                // Actualizar el campo oficio apertura firmado en la tabla de tomos
                var TareaActualizarUrlOficioAperturaFirmado = Tomo.ActualizarUrlOficioAperturaAsync(ModeloTomo.Id, OficioAperturaFirmadoGuardado);
                bool ActualizarAperturaFirmado = await TareaActualizarUrlOficioAperturaFirmado;

                if (ActualizarAperturaFirmado) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Oficio apertura firmado");
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

        // POST: Subir oficio de cierre firmado
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Auditor, Sub. Auditor")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AgregarCierreFirmado(SubirOficioFirmadoViewModel Modelo) {
            try {
                // Valida el modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { seccess = false, Message = "Error, modelo invalido" }, JsonRequestBehavior.AllowGet);
                }

                // Obtener el modelo tomo por [IdTomo]
                var TareaObtenerTomo = Tomo.ObtenerPorIdSinActasAsync(Modelo.IdTomo);
                Tomo ModeloTomo = await TareaObtenerTomo;

                MemoryStream MS = new MemoryStream();
                Modelo.Archivo.InputStream.CopyTo(MS);
                byte[] PDFData = MS.ToArray();

                // Guardar el oficio cierre firmado en el repositorio
                var TareaGuardarOficioCierreFirmado = Repositorio.GuardarOficioPDFRepositorioTomo(ModeloTomo, PDFData, "Oficio de Cierre del " + ModeloTomo.Nombre);
                string OficioCierreFirmadoGuardado = await TareaGuardarOficioCierreFirmado;

                // Actualizar el campo oficio apertura firmado en la tabla de tomos
                var TareaActualizarUrlOficioCierreFirmado = Tomo.ActualizarUrlOficioCierreAsync(ModeloTomo.Id, OficioCierreFirmadoGuardado);
                bool ActualizarCierreFirmado = await TareaActualizarUrlOficioCierreFirmado;

                if (ActualizarCierreFirmado) {
                    string Mensaje = Funciones.ObtenerMensajeExito("A", "Oficio cierre firmado");
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