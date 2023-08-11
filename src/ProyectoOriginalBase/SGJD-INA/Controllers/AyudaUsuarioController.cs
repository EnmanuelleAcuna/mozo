using Newtonsoft.Json;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class AyudaUsuarioController : Controller {
        // Contructor y dependencias
        private readonly IAyudaUsuarioLogic AyudaUsuario;

        public AyudaUsuarioController(IAyudaUsuarioLogic AyudaUsuario) {
            this.AyudaUsuario = AyudaUsuario;
        }

        // Acciones
        // GET: /AyudaUsuarios/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerTodos = AyudaUsuario.ObtenerTodosAsync();
                IEnumerable<AyudaUsuario> ListaAyudaUsuarioModel = await TareaObtenerTodos;
                IEnumerable<InicioAyudaUsuarioViewModel> ListaAyudaUsuarioViewModel = ListaAyudaUsuarioModel.Select(AyudaUsuario => new InicioAyudaUsuarioViewModel(AyudaUsuario)).ToList();
                return View(ListaAyudaUsuarioViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: AyudaUsuario/Editar/21
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string IdAyudaUsuario) {
            try {
                if (string.IsNullOrEmpty(IdAyudaUsuario) || !Funciones.IsNumber(IdAyudaUsuario)) {
                    throw new ArgumentNullException(paramName: nameof(IdAyudaUsuario), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerAyudaUsuario = AyudaUsuario.ObtenerPorIdAsync(Convert.ToInt32(IdAyudaUsuario));
                AyudaUsuario AyudaUsuarioModel = await TareaObtenerAyudaUsuario;

                if (AyudaUsuarioModel == null) {
                    throw new ArgumentNullException(paramName: nameof(AyudaUsuarioModel), message: Properties.Resources.ModeloNulo);
                }

                EditarAyudaUsuarioViewModel Modelo = new EditarAyudaUsuarioViewModel(AyudaUsuarioModel);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualizar un ayuda usuario
        /// </summary>
        // POST: AyudaUsuario/Editar/21
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarAyudaUsuarioViewModel Modelo) {
            try {
                // Validar modelo obtenido
                if (!ModelState.IsValid) {
                    return Json(new { success = false, Mensaje = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                AyudaUsuario ModeloAyudaUsuario = Modelo.Entidad();

                var TareaActualizarAyudaUsuario = AyudaUsuario.ActualizarAsync(ModeloAyudaUsuario);
                bool AyudaUsuarioEditado = await TareaActualizarAyudaUsuario;

                if (AyudaUsuarioEditado == true) {
                    string Mensaje = Funciones.ObtenerMensajeExito("M", "Ayuda a Usuario");
                    return Json(new { success = AyudaUsuarioEditado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
                else {
                    string Mensaje = Funciones.ObtenerMensajeError(new Exception(), "M", "Ayuda a Usuario");
                    return Json(new { success = AyudaUsuarioEditado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }   

        // GET: /AyudaUsuario/Detalles
        [HttpGet]
        public async Task<ActionResult> Detalle(string Abreviatura) {
            try {
                if (string.IsNullOrEmpty(Abreviatura)) {
                    return Json(new { success = false, message = "Error: Id inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaObtenerAyudaUsuario = AyudaUsuario.ObtenerPorAbreviaturaAsync(Abreviatura);
                AyudaUsuario ModeloAyudaUsuario = await TareaObtenerAyudaUsuario;

                if (ModeloAyudaUsuario is null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloAyudaUsuario), message: Properties.Resources.ModeloNulo);
                }

                DetalleAyudaUsuarioViewModel Modelo = new DetalleAyudaUsuarioViewModel(ModeloAyudaUsuario);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}

