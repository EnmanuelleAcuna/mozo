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
    public class CorreoAdicionalController : Controller {
        //Constructor y dependecias
        private readonly ICorreoAdicionalLogic CorreoAdicional;

        public CorreoAdicionalController(ICorreoAdicionalLogic CorreoAdicional) {
            this.CorreoAdicional = CorreoAdicional;
        }

        //Acciones
        //GET: /CorreoAdicional/Inicio
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Inicio() {
            try {
                var TareaObtenerListaCorreoAdicional = CorreoAdicional.ObtenerTodosAsync();
                IEnumerable<CorreoAdicional> ListaCorreoAdicionalModelo = await TareaObtenerListaCorreoAdicional;
                IEnumerable<InicioCorreoAdicionalViewModel> CorreoAdicionalViewModel = ListaCorreoAdicionalModelo.Select(ca => new InicioCorreoAdicionalViewModel(ca)).ToList();
                return View(CorreoAdicionalViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        //GET: /CorreoAdicional/Agregar
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public ActionResult Agregar() {
            try {
                AgregarCorreoAdicionalViewModel Modelo = new AgregarCorreoAdicionalViewModel();
                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar CorreoAdicional
        /// </summary>
        // POST: /CorreoAdicional/Agregar
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(AgregarCorreoAdicionalViewModel Modelo) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                CorreoAdicional ModeloCorreoAdicional = Modelo.Entidad();
                var TareaAgregarCorreoAdicional = CorreoAdicional.AgregarAsync(ModeloCorreoAdicional);
                bool CorreoAdicionalAgregado = await TareaAgregarCorreoAdicional;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "CorreoAdicional");

                return Json(new { success = CorreoAdicionalAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // GET: /CorreoAdicional/Eliminar/2
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> Eliminar(string IdCorreoAdicional) {
            try {
                if (string.IsNullOrEmpty(IdCorreoAdicional) || !Funciones.IsNumber(IdCorreoAdicional)) {
                    throw new ArgumentNullException(paramName: nameof(IdCorreoAdicional), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaObtenerCorreoAdicional = CorreoAdicional.ObtenerPorIdAsync(Convert.ToInt32(IdCorreoAdicional));
                CorreoAdicional ModeloCorreoAdicional = await TareaObtenerCorreoAdicional;

                if (ModeloCorreoAdicional == null) {
                    throw new ArgumentNullException(paramName: nameof(ModeloCorreoAdicional), message: Properties.Resources.ModeloNulo);
                }

                EliminarCorreoAdicionalViewModel Modelo = new EliminarCorreoAdicionalViewModel(ModeloCorreoAdicional);

                return View(Modelo);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un CorreoAdicional
        /// </summary>
        // POST: /CorreoAdicional/Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EliminarConfirmado(string IdCorreoAdicional) {
            try {
                if (string.IsNullOrEmpty(IdCorreoAdicional) || !Funciones.IsNumber(IdCorreoAdicional)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaEliminarCorreoAdicional = CorreoAdicional.EliminarAsync(Convert.ToInt32(IdCorreoAdicional));
                bool CorreoAdicionalEliminado = await TareaEliminarCorreoAdicional;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "CorreoAdicional");

                return Json(new { success = CorreoAdicionalEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

    }
}