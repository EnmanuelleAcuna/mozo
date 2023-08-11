using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class UsuarioCorreoLogic : IUsuarioCorreoLogic {
        #region Constructor y dependencias
        private IBitacoraLogic Bitacora;

        public UsuarioCorreoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(UsuarioCorreo ModeloCorreoUsuario) {
            var CorreoBD = ModeloCorreoUsuario.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_CORREOS_USUARIO.Add(CorreoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloCorreoUsuario.ObtenerInformacion(), SeccionSistema: "CorreoUsuario");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdCorreo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerCorreoBD = ContextoBD.SGJD_ADM_TAB_CORREOS_USUARIO.FindAsync(IdCorreo);
                var CorreoBD = await TareaObtenerCorreoBD;
                ContextoBD.SGJD_ADM_TAB_CORREOS_USUARIO.Attach(CorreoBD);
                ContextoBD.SGJD_ADM_TAB_CORREOS_USUARIO.Remove(CorreoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdCorreo.ToString(), ValorNuevo: "", SeccionSistema: "CorreoUsuario"); ;
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<UsuarioCorreo> ObtenerTodos(string IdUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaCorreosUsuarioBD = ContextoBD.SGJD_ADM_TAB_CORREOS_USUARIO.Where(C => C.LLF_IdUsuario == IdUsuario).ToList();
                IEnumerable<UsuarioCorreo> ListaCorreosUsuario = ListaCorreosUsuarioBD.Select(cu => new UsuarioCorreo(cu)).ToList();
                return ListaCorreosUsuario;
            }
        }

        public Task<IEnumerable<UsuarioCorreo>> ObtenerTodosAsync(string IdUsuario) {
            Task<IEnumerable<UsuarioCorreo>> Tarea = Task.Run(() => ObtenerTodos(IdUsuario));
            return Tarea;
        }
        #endregion
    }
}