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
    public class ParametrosAcuerdosController : Controller {
        // Constructor y dependencias
        private readonly IEstadoLogic Estado;
        private readonly IUsuarioLogic Usuario;
        private readonly IAvisosLogic Aviso;

        public ParametrosAcuerdosController(IEstadoLogic Estado, IUsuarioLogic Usuario, IAvisosLogic Aviso) {
            this.Estado = Estado;
            this.Usuario = Usuario;
            this.Aviso = Aviso;
        }

        // Acciones
        // GET: /ParametrosAcuerdos/InicioUsuarioAvisoAcuerdo
        [HttpGet]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        public async Task<ActionResult> InicioUsuarioAvisoAcuerdo() {
            try {
                var TareaObtenerDatos = Estado.ObtenerTodosPorNombreObjetoConAvisoParaAcuerdoAsync("SGJD_ACU_TAB_ACUERDOS");
                var TareaObtenerUsuarios = Usuario.ObtenerTodosActivosAsync();

                IEnumerable<Estado> ListaEstadosAcuerdos = await TareaObtenerDatos;
                IEnumerable<ApplicationUser> ListaUsuarioActivos = await TareaObtenerUsuarios;

                ViewBag.ListaUsuarios = ListaUsuarioActivos;

                IEnumerable<InicioUsuarioAvisoAcuerdoViewModel> ViewModel = ListaEstadosAcuerdos.Select(es => new InicioUsuarioAvisoAcuerdoViewModel(es));
                return View(ViewModel);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Agregar UsuarioAvisoAcuerdo
        /// </summary>
        // POST: /ParametrosAcuerdos/EditarUsuarioAvisoAcuerdo
        [HttpPost]
        [AuthorizeConfig(Roles = "Administrador Datasoft, Administrador, Abogado Secretaría Técnica, Coordinación Actas")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarUsuarioAvisoAcuerdo(string IdAviso, string IdUsuario) {
            try {
                if ((string.IsNullOrEmpty(IdAviso) || !Funciones.IsNumber(IdAviso)) && (string.IsNullOrEmpty(IdUsuario) || !Funciones.IsNumber(IdUsuario))) {
                    throw new ArgumentNullException(paramName: nameof(IdAviso), message: Properties.Resources.SolicitudIncorrecta);
                }
                var TareaEditarUsuarioAvisoAcuerdo = Aviso.ActualizarUsuarioAvisoAcuerdoAsync(IdAviso, IdUsuario);
                bool UsuarioAvisoAcuerdoEditado = await TareaEditarUsuarioAvisoAcuerdo;

                string Mensaje = Funciones.ObtenerMensajeExito("M", "UsuarioAvisoAcuerdo");

                return Json(new { success = UsuarioAvisoAcuerdoEditado, message = Mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                Funciones.LogError(ex);
                throw;
            }
        }
    }
}