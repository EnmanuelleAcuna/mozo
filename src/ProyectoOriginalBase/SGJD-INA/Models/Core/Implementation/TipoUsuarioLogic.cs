using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad TipoUsuario
    /// </summary>
    public class TipoUsuarioLogic : ITipoUsuarioLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public TipoUsuarioLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(TipoUsuario ModeloTipoUsuario) {
            var TipoUsuarioBD = ModeloTipoUsuario.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.Add(TipoUsuarioBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTipoUsuario.ObtenerInformacion(), SeccionSistema: "TipoUsuario");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(TipoUsuario ModeloTipoUsuario) {
            var TipoUsuarioBD = ModeloTipoUsuario.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(TipoUsuarioBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloTipoUsuario.ObtenerInformacion(), SeccionSistema: "TipoUsuario");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdTipoUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoUsuario = ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.FindAsync(IdTipoUsuario);
                var TipoUsuarioBD = await TareaObtenerTipoUsuario;
                ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.Attach(TipoUsuarioBD);
                ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.Remove(TipoUsuarioBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdTipoUsuario.ToString(), ValorNuevo: "", SeccionSistema: "TipoUsuario");
                    return true;

                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<TipoUsuario> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTipoUsuarioBD = ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.ToList();
                IEnumerable<TipoUsuario> ListaTipoUsuario = ListaTipoUsuarioBD.Select(tu => new TipoUsuario(tu)).ToList();
                return ListaTipoUsuario;
            }
        }

        public Task<IEnumerable<TipoUsuario>> ObtenerTodosAsync() {
            Task<IEnumerable<TipoUsuario>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<TipoUsuario> ObtenerPorIdAsync(int Id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoUsuarioBD = ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.FindAsync(Id);
                var TipoUsuarioBD = await TareaObtenerTipoUsuarioBD;
                TipoUsuario ModeloTipoUsuario = new TipoUsuario(TipoUsuarioBD);
                return ModeloTipoUsuario;
            }
        }

        public async Task<TipoUsuario> ObtenerPorNombreAsync(string Nombre) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoUsuarioBD = ContextoBD.SGJD_ADM_TAB_TIPOS_USUARIO.Where(n => n.Nombre.Equals(Nombre)).FirstOrDefaultAsync();
                var TipoUsuarioBD = await TareaObtenerTipoUsuarioBD;
                TipoUsuario ModeloTipoUsuario = new TipoUsuario(TipoUsuarioBD);
                return ModeloTipoUsuario;
            }
        }
        #endregion
    }
}