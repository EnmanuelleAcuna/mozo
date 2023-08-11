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
    /// Clase para realizar operaciones relacionadas a la clase EmcabezadoPiePagina
    /// </summary>
    public class EncabezadoPiePaginaLogic : IEncabezadoPiePaginaLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public EncabezadoPiePaginaLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(EncabezadoPiePagina EncabezadoPiePaginaModel) {
            var EncabezadoPiePaginaBD = EncabezadoPiePaginaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA.Add(EncabezadoPiePaginaBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: EncabezadoPiePaginaModel.ObtenerInformacion(), SeccionSistema: "EncabezadoPiePagina");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(EncabezadoPiePagina EncabezadoPiePaginaModel) {
            var EncabezadoPiePaginaBD = EncabezadoPiePaginaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(EncabezadoPiePaginaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: EncabezadoPiePaginaModel.ObtenerInformacion(), SeccionSistema: "EncabezadoPiePagina");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<EncabezadoPiePagina> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaEncabezadoPiePaginaBD = ContextoBD.SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA.ToList();
                IEnumerable<EncabezadoPiePagina> ListaEncabezadoPiePagina = ListaEncabezadoPiePaginaBD.Select(epp => new EncabezadoPiePagina(epp, epp.SGJD_ADM_TAB_TIPOS_OBJETO)).ToList();
                return ListaEncabezadoPiePagina;
            }
        }

        public Task<IEnumerable<EncabezadoPiePagina>> ObtenerTodosAsync() {
            Task<IEnumerable<EncabezadoPiePagina>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<EncabezadoPiePagina> ObtenerPorIdAsync(int IdEncabezado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEncabezadoPiePaginaBD = ContextoBD.SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA.FindAsync(IdEncabezado);
                var EncabezadoPiePaginaBD = await TareaObtenerEncabezadoPiePaginaBD;
                EncabezadoPiePagina EncabezadoPiePaginaModel = new EncabezadoPiePagina(EncabezadoPiePaginaBD, EncabezadoPiePaginaBD.SGJD_ADM_TAB_TIPOS_OBJETO);
                return EncabezadoPiePaginaModel;
            }
        }

        public async Task<EncabezadoPiePagina> ObtenerPorIdTipoObjetoAsync(int IdTipoObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEncabezadoPiePaginaBD = ContextoBD.SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA.Where(E => E.LLF_IdTipoObjeto == IdTipoObjeto).FirstOrDefaultAsync();
                var EncabezadoPiePaginaBD = await TareaObtenerEncabezadoPiePaginaBD;

                if (EncabezadoPiePaginaBD == null) {
                    return new EncabezadoPiePagina();
                }
                else {
                    EncabezadoPiePagina EncabezadoPiePaginaModel = new EncabezadoPiePagina(EncabezadoPiePaginaBD, EncabezadoPiePaginaBD.SGJD_ADM_TAB_TIPOS_OBJETO);
                    return EncabezadoPiePaginaModel;
                }
            }
        }
        #endregion
    }
}