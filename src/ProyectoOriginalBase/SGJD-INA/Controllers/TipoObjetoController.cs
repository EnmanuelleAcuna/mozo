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
    public class TipoObjetoController : Controller {
        // Constructor y dependencias
        private readonly ITipoObjetoLogic TipoObjeto;

        public TipoObjetoController(ITipoObjetoLogic TipoObjeto) {
            this.TipoObjeto = TipoObjeto;
        }

        // Acciones
        // GET: /TipoObjeto/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = TipoObjeto.ObtenerTodosAsync();
                IEnumerable<TipoObjeto> ListaTipoObjetoModel = await TareaObtenerTodos;
                IEnumerable<InicioTipoObjetoViewModel> ListaTipoObjetoViewModel = ListaTipoObjetoModel.Select(TipoObjeto => new InicioTipoObjetoViewModel(TipoObjeto)).ToList();
                return View(ListaTipoObjetoViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos TipoObjeto para pasarlo en formato JSON.
        /// </summary>
        // GET: /TipoObjeto/ObtenerDatos
        [HttpGet]
        public async Task<JsonResult> ObtenerDatos() {
            try {
                var TareaObtenerTodos = TipoObjeto.ObtenerTodosAsync();
                IEnumerable<TipoObjeto> ListaTipoObjetoModel = await TareaObtenerTodos;
                return Json(ListaTipoObjetoModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener un TipoObjeto por su nombre para pasarlo en formato JSON.
        /// </summary>
        // GET: /TipoObjeto/ObtenerPorNombreTabla
        [HttpGet]
        public async Task<JsonResult> ObtenerPorNombreTabla(string NombreTabla) {
            try {
                var TareaObtenerPorNombreTabla = TipoObjeto.ObtenerPorNombreTablaAsync(NombreTabla);
                var ModeloTipoObjeto = await TareaObtenerPorNombreTabla;
                return Json(new { TipoObjeto = ModeloTipoObjeto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoObjeto/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Agregar() {
            try {
                var TareaObtenerTablasBDSinConfigurar = TipoObjeto.ObtenerTablasBDSinConfigurarParaSelectAsync();
                ViewBag.ListaTablasBD = await TareaObtenerTablasBDSinConfigurar;
                AgregarTipoObjetoViewModel Modelo = new AgregarTipoObjetoViewModel();
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar TipoObjeto
        /// </summary>
        // POST: /TipoObjeto/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarTipoObjetoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoObjeto ModeloTipoObjeto = Modelo.Entidad();
                var TareaAgregarTipoObjeto = TipoObjeto.AgregarAsync(ModeloTipoObjeto);
                bool TipoObjetoAgregado = await TareaAgregarTipoObjeto;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "TipoObjeto");

                return Json(new { success = TipoObjetoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoObjeto/Editar?IdTipoObjeto=2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string IdTipoObjeto) {
            try {
                if (string.IsNullOrEmpty(IdTipoObjeto) || !Funciones.IsNumber(IdTipoObjeto)) {
                    throw new ArgumentNullException(paramName: nameof(IdTipoObjeto), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorIdAsync(Convert.ToInt32(IdTipoObjeto));
                TipoObjeto ModeloTipoObjeto = await TareaObtenerTipoObjeto;

                if (ModeloTipoObjeto == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloTipoObjeto), message: Properties.Resources.ModeloNulo);
                }

                EditarTipoObjetoViewModel Modelo = new EditarTipoObjetoViewModel(ModeloTipoObjeto);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar TipoObjeto
        /// </summary>
        // POST: /TipoObjeto/Editar/2
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarTipoObjetoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                TipoObjeto ModeloTipoObjeto = Modelo.Entidad();
                var TareaEditarTipoObjeto = TipoObjeto.ActualizarAsync(ModeloTipoObjeto);
                bool TipoObjetoEditado = await TareaEditarTipoObjeto;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "TipoObjeto");

                return Json(new { success = TipoObjetoEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoObjeto/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Eliminar(string IdTipoObjeto) {
            try {
                if (string.IsNullOrEmpty(IdTipoObjeto) || !Funciones.IsNumber(IdTipoObjeto)) {
                    throw new ArgumentNullException(paramName: nameof(IdTipoObjeto), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorIdAsync(Convert.ToInt32(IdTipoObjeto));
                TipoObjeto ModeloTipoObjeto = await TareaObtenerTipoObjeto;

                if (ModeloTipoObjeto == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloTipoObjeto), message: Properties.Resources.ModeloNulo);
                }

                EliminarTipoObjetoViewModel Modelo = new EliminarTipoObjetoViewModel(ModeloTipoObjeto);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un TipoObjeto
        /// </summary>
        // POST: /TipoObjeto/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(EliminarTipoObjetoViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarTipoObjeto = TipoObjeto.EliminarAsync(Modelo.Id);
                bool TipoObjetoEliminado = await TareaEliminarTipoObjeto;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "TipoObjeto");

                return Json(new { success = TipoObjetoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /TipoObjeto/Detalles
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Detalles(string IdTipoObjeto) {
            try {
                if (string.IsNullOrEmpty(IdTipoObjeto) || !Funciones.IsNumber(IdTipoObjeto)) {
                    throw new ArgumentNullException(paramName: nameof(IdTipoObjeto), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorIdAsync(Convert.ToInt32(IdTipoObjeto));
                TipoObjeto ModeloTipoObjeto = await TareaObtenerTipoObjeto;

                if (ModeloTipoObjeto == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloTipoObjeto), message: Properties.Resources.ModeloNulo);
                }

                DetallesTipoObjetoViewModel Modelo = new DetallesTipoObjetoViewModel(ModeloTipoObjeto);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}