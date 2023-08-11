using Newtonsoft.Json;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    [Authorize]
    public class UsuarioCorreoController : Controller {
        // Constructor y dependencias
        private readonly IUsuarioCorreoLogic UsuarioCorreo;

        public UsuarioCorreoController(IUsuarioCorreoLogic UsuarioCorreo) {
            this.UsuarioCorreo = UsuarioCorreo;
        }

        /// <summary>
        /// Agregar un correo a un Usuario
        /// </summary>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Agregar(string ModeloCorreoJSON) {
            try {
                if (string.IsNullOrEmpty(ModeloCorreoJSON)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                //Deserializar el objeto JSON en una entidad UsuarioCorreo
                UsuarioCorreo ModeloCorreo = JsonConvert.DeserializeObject<UsuarioCorreo>(ModeloCorreoJSON);

                if (!ModelState.IsValid) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                var TareaAgregarCorreo = UsuarioCorreo.AgregarAsync(ModeloCorreo);
                bool CorreoAgregado = await TareaAgregarCorreo;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Correo");

                return Json(new { success = CorreoAgregado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un correo a un Usuario.
        /// </summary>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(string IdCorreo) {
            try {
                if (string.IsNullOrEmpty(IdCorreo) || !Funciones.IsNumber(IdCorreo)) {
                    throw new ArgumentNullException(paramName: nameof(IdCorreo), message: Properties.Resources.SolicitudIncorrecta);
                }

                var TareaEliminarCorreo = UsuarioCorreo.EliminarAsync(Convert.ToInt32(IdCorreo));
                bool CorreoEliminado = await TareaEliminarCorreo;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Correo");

                return Json(new { success = CorreoEliminado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}