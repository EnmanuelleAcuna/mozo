using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Controllers {
    public class MiembrosJDController : Controller {
        // Constructor y dependencias
        private IMiembrosJDLogic MiembrosJD;
        private IAsistenteSesionLogic Asistentes;

        public MiembrosJDController(IMiembrosJDLogic MiembrosJD, IAsistenteSesionLogic Asistentes) {
            this.MiembrosJD = MiembrosJD;
            this.Asistentes = Asistentes;
        }

        // Miembros JD
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public ActionResult Inicio() {
            try {
                return View();
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerMiembrosJD() {
            try {
                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                var ListaMiembrosJD = await TareaObtenerMiembrosJD;
                return Json(new { data = ListaMiembrosJD }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerMiembrosAusentes(string IdSesion) {
            try {
                var TareaObtenerMiembrosJD = MiembrosJD.ObtenerTodos();
                var ListaMiembrosJD = await TareaObtenerMiembrosJD;

                var TareaObtenerAsistentes = Asistentes.ObtenerTodosPorIdSesionAsync(Convert.ToInt32(IdSesion));
                var ListaAsistentes = await TareaObtenerAsistentes;

                if (!ListaMiembrosJD.Any() || !ListaAsistentes.Any()) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                IEnumerable<MiembroJD> ListaMiembrosAusentes = MiembrosJD.ObtenerMiembrosAusentes(ListaMiembrosJD, ListaAsistentes);

                return Json(new { data = ListaMiembrosAusentes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Agregar un usuario a la lista de miembros de J.D.
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<JsonResult> AgregarMiembro(string IdUsuario) {
            try {
                if (string.IsNullOrEmpty(IdUsuario)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool AgregarMiembro = false;
                var TareaAgregar = MiembrosJD.Agregar(IdUsuario);
                AgregarMiembro = await TareaAgregar;

                string Mensaje = Funciones.ObtenerMensajeExito("A", "Miembro de Junta Directiva");

                return Json(new { success = AgregarMiembro, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        // Quitar a un usuario a la lista de miembros de J.D.
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Coordinación Actas")]
        public async Task<JsonResult> QuitarMiembro(string IdUsuario) {
            try {
                if (string.IsNullOrEmpty(IdUsuario)) {
                    return Json(new { success = false, message = "Error: Modelo inválido." }, JsonRequestBehavior.AllowGet);
                }

                bool EliminarMiembro = false;
                var TareaEliminar = MiembrosJD.Eliminar(IdUsuario);
                EliminarMiembro = await TareaEliminar;

                string Mensaje = Funciones.ObtenerMensajeExito("E", "Miembro de Junta Directiva");

                return Json(new { success = EliminarMiembro, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}