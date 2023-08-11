using Newtonsoft.Json;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    [Authorize]
    public class UsuarioTelefonoController : Controller {
        // Constructor y dependencias
        private readonly IUsuarioTelefonoLogic UsuarioTelefono;

        public UsuarioTelefonoController(IUsuarioTelefonoLogic UsuarioTelefono) {
            this.UsuarioTelefono = UsuarioTelefono;
        }

        /// <summary>
        /// Agregar un teléfono a un Usuario.
        /// </summary>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(string ModeloTelefonoJSON) {
            try {
                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                //Deserializar el objeto JSON en una entidad UsuarioCorreo
                UsuarioTelefono ModeloTelefono = JsonConvert.DeserializeObject<UsuarioTelefono>(ModeloTelefonoJSON);

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarTelefono = UsuarioTelefono.AgregarAsync(ModeloTelefono);
                bool TelefonoAgregado = await TareaAgregarTelefono;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Teléfono");

                return Json(new { success = TelefonoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un Telefono a un Usuario.
        /// </summary>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(string IdTelefono) {
            try {
                if (string.IsNullOrEmpty(IdTelefono) || !Funciones.IsNumber(IdTelefono)) {
                    throw new ArgumentNullException(paramName: nameof(IdTelefono), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaEliminarTelefono = UsuarioTelefono.EliminarAsync(Convert.ToInt32(IdTelefono));
                bool TelefonoEliminado = await TareaEliminarTelefono;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Teléfono");

                return Json(new { success = TelefonoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}