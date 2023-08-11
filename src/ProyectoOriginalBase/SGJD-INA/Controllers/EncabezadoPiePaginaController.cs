using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class EncabezadoPiePaginaController : Controller {
        // Constructor y dependencias
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly ITamanoLetraLogic Fuentes;
        private readonly IParametrosGeneralesLogic ParametrosGenerales;

        public EncabezadoPiePaginaController(IEncabezadoPiePaginaLogic EncabezadoPiePagina, ITipoObjetoLogic TipoObjeto, ITamanoLetraLogic Fuentes, IParametrosGeneralesLogic ParametrosGenerales, IBitacoraLogic Bitacora) {
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.TipoObjeto = TipoObjeto;
            this.Fuentes = Fuentes;
            this.ParametrosGenerales = ParametrosGenerales;
        }

        //Acciones
        // GET: /EncabezadoPiePagina/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio(string EncabezadoPieGuardado) {
            try {
                // Al cargar la vista, si la carga proviene de la vista [Agregar], esta envía una variable de control para indicar
                // si el usuario fue guardado con exito, para mostrar el mensaje de que se realizó correctamente la acción.
                if (!string.IsNullOrEmpty(EncabezadoPieGuardado) && EncabezadoPieGuardado.Equals("true")) {
                    ViewBag.EncabezadoPieGuardado = EncabezadoPieGuardado;
                }

                var TareaObtenerTodos = EncabezadoPiePagina.ObtenerTodosAsync();
                IEnumerable<EncabezadoPiePagina> ListaEncabezadoPiePagina = await TareaObtenerTodos;
                return View(ListaEncabezadoPiePagina);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /EncabezadoPiePagina/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerTiposObjeto = TipoObjeto.ObtenerTodosConEncabezadoPiePaginaSinConfigurarAsync();
                ViewBag.ListaTipoObjeto = await TareaObtenerTiposObjeto;
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar Encabezado y Pie de Pagina
        /// </summary>
        // POST: /EncabezadoPiePagina/Agregar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Agregar(EncabezadoPiePagina Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }
                else {
                    var TareaAgregarEncabezadoPiePagina = EncabezadoPiePagina.AgregarAsync(Modelo);
                    bool EncabezadoPiePaginaAgregado = await TareaAgregarEncabezadoPiePagina;

                    // Verificar si se agregó correctamente
                    if (EncabezadoPiePaginaAgregado == true) {
                        return RedirectToAction("Inicio", new { EncabezadoPieGuardado = "true" });
                    }
                }

                // Si se llega a este punto quiere decir que hubo un error
                ModelState.AddModelError("", "Error al guardar la información");

                var TareaObtenerTiposObjeto = TipoObjeto.ObtenerTodosConEncabezadoPiePaginaSinConfigurarAsync();
                ViewBag.ListaTipoObjeto = await TareaObtenerTiposObjeto;

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /EncabezadoPiePagina/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerEncabezadoPiePagina = EncabezadoPiePagina.ObtenerPorIdAsync(Convert.ToInt32(Id));
                var EncabezadoPiePaginaModel = await TareaObtenerEncabezadoPiePagina;

                if (EncabezadoPiePaginaModel == null) {
                    throw new ArgumentNullException(paramName: nameof(EncabezadoPiePaginaModel), message: Properties.Resources.ModeloNulo);
                }

                return View(EncabezadoPiePaginaModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un elemento EncabezadoPiePagina
        /// El modelo es de tipo JSON
        /// </summary>
        // POST: /EncabezadoPiePagina/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EncabezadoPiePagina Modelo) {
            try {
                if (!ModelState.IsValid) {
                    ModelState.AddModelError("", "Error, modelo inválido.");
                }
                else {
                    var TareaEditarEncabezadoPiePagina = EncabezadoPiePagina.ActualizarAsync(Modelo);
                    bool EncabezadoPiePaginaEditado = await TareaEditarEncabezadoPiePagina;

                    // Verificar si hubo exito al actualizar el encabezado y pie de página
                    if (EncabezadoPiePaginaEditado == true) {
                        ViewBag.EPPGuardado = "true";
                    }
                    else {
                        ModelState.AddModelError("", "Error al actualizar.");
                    }
                }

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener el encabezado y pie de pagina a partir de un tipo objeto orden del día para pasarlo en formato JSON.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerEncabezadoPiePagina(string nombreObjeto) {
            try {
                // Obtener el tipo de objeto correspondiente al orden del día
                var tareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync(nombreObjeto);
                var tipoObjetoOrdenDiaModel = await tareaObtenerTipoObjeto;

                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                // Obtener el encabezado y pie de pagina del orden del día
                var tareaObtenerEncabezadoPiePagina = EncabezadoPiePagina.ObtenerPorIdTipoObjetoAsync(tipoObjetoOrdenDiaModel.Id);
                var encabezadoPiePaginaModelo = await tareaObtenerEncabezadoPiePagina;

                return Json(new { encabezadoPiePagina = encabezadoPiePaginaModelo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}