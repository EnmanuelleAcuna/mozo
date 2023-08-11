using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class UsuarioTelefonoLogic : IUsuarioTelefonoLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public UsuarioTelefonoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(UsuarioTelefono ModeloTelefonoUsuario) {
            var TelefonoBD = ModeloTelefonoUsuario.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_TELEFONOS_USUARIO.Add(TelefonoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTelefonoUsuario.ObtenerInformacion(), SeccionSistema: "TeléfonoUsuario");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdTelefono) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTelefonoBD = ContextoBD.SGJD_ADM_TAB_TELEFONOS_USUARIO.FindAsync(IdTelefono);
                var TelefonoBD = await TareaObtenerTelefonoBD;
                ContextoBD.SGJD_ADM_TAB_TELEFONOS_USUARIO.Attach(TelefonoBD);
                ContextoBD.SGJD_ADM_TAB_TELEFONOS_USUARIO.Remove(TelefonoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdTelefono.ToString(), ValorNuevo: "", SeccionSistema: "TeléfonoUsuario");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<UsuarioTelefono> ObtenerTodos(string IdUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTelefonoUsuarioBD = ContextoBD.SGJD_ADM_TAB_TELEFONOS_USUARIO.Where(T => T.LLF_IdUsuario == IdUsuario).ToList();
                IEnumerable<UsuarioTelefono> ListaTelefonosUsuario = ListaTelefonoUsuarioBD.Select(tu => new UsuarioTelefono(tu)).ToList();
                return ListaTelefonosUsuario;
            }
        }

        public Task<IEnumerable<UsuarioTelefono>> ObtenerTodosAsync(string IdUsuario) {
            Task<IEnumerable<UsuarioTelefono>> Tarea = Task.Run(() => ObtenerTodos(IdUsuario));
            return Tarea;
        }
        #endregion
    }
}