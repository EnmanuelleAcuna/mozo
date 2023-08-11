using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class UsuarioSIRHController : Controller {
        // Constructor y dependencias
        private readonly IUsuarioSIRHLogic UsuarioSIRH;

        public UsuarioSIRHController(IUsuarioSIRHLogic UsuarioSIRH) {
            this.UsuarioSIRH = UsuarioSIRH;
        }

        [HttpGet]
        public ActionResult Inicio() {
            return View();
        }

        /// <summary>
        /// Obtener todos los usuarios de SIRH
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> ObtenerTodos() {
            try {
                var TareaObtenerUsuariosSIRH = UsuarioSIRH.ObtenerTodosAsync();
                IEnumerable<UsuarioSIRH> UsuariosSIRH = await TareaObtenerUsuariosSIRH;
                return Json(new { data = UsuariosSIRH }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtener todos los usuarios de SIRH con un nombre de usuario específico
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> VerificarSiNombreUsuarioEstaDuplicado(string NombreUsuario) {
            try {
                var TareaVerificarSiEstaDuplicado = UsuarioSIRH.VerificarSiExisteDuplicadoAsync(NombreUsuario);
                bool ExisteDuplicado = await TareaVerificarSiEstaDuplicado;
                return Json(new { Duplicado = ExisteDuplicado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}