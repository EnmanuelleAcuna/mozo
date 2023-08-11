using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Models.ViewModels;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class ParametrosGeneralesController : Controller {
        // Constructor y dependencias
        private readonly IParametrosGeneralesLogic ParametroGeneral;
        private readonly IUsuarioLogic Usuario;

        public ParametrosGeneralesController(IParametrosGeneralesLogic ParametroGeneral, IUsuarioLogic Usuario) {
            this.ParametroGeneral = ParametroGeneral;
            this.Usuario = Usuario;
        }

        // GET: /ParametroGenerales/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerParametrosInstitucion = ParametroGeneral.ObtenerParametrosInstitucionAsync();
                IEnumerable<ParametroGeneral> ListaParametrosInstitucion = await TareaObtenerParametrosInstitucion;

                var TareaObtenerParametrosCorreo = ParametroGeneral.ObtenerParametrosCorreoAsync();
                IEnumerable<ParametroGeneral> ListaParametrosCorreo = await TareaObtenerParametrosCorreo;

                InicioParametrosGeneralesViewModel ParametroGeneralesViewModel = new InicioParametrosGeneralesViewModel(ListaParametrosInstitucion, ListaParametrosCorreo);

                return View(ParametroGeneralesViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /ParametroGenerales/Editar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        public async Task<ActionResult> Editar(string Id) {
            try {
                if (string.IsNullOrEmpty(Id) || !Funciones.IsNumber(Id)) {
                    throw new ArgumentNullException(paramName: nameof(Id), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerParametro = ParametroGeneral.ObtenerPorIdAsync(Convert.ToInt32(Id, new CultureInfo(Resources.CultureInfo)));
                var ParametroModel = await TareaObtenerParametro;

                if (ParametroModel is null) {
                    throw new ArgumentNullException(paramName: nameof(ParametroModel), message: Resources.ModeloNulo);
                }

                EditarParametroGeneralViewModel ParametroViewModel = new EditarParametroGeneralViewModel(ParametroModel);

                var TareaObtenerUsuarios = Usuario.ObtenerTodosActivosAsync();
                IEnumerable<ApplicationUser> ListaUsuarioActivos = await TareaObtenerUsuarios;
                ViewBag.ListaUsuarios = ListaUsuarioActivos;

                return View(ParametroViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // POST: /ParametroGenerales/Editar/
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar(EditarParametroGeneralViewModel Modelo) {
            try {
                if (Modelo is null) throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                ParametroGeneral ModeloParametroGeneral = Modelo.Entidad();
                var TareaActualizarParametro = ParametroGeneral.ActualizarAsync(ModeloParametroGeneral);
                bool ParametroGeneralEditado = await TareaActualizarParametro;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "Parametro");

                return Json(new { success = ParametroGeneralEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}