using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class UnidadPredeterminadaLogic : IUnidadPredeterminadaLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public UnidadPredeterminadaLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        // Métodos públicos
        public async Task<IEnumerable<UnidadPredeterminada>> ObtenerTodasAsync() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUnidadesPredBD = ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.ToListAsync();
                var ListaUnidadesPredBD = await TareaObtenerUnidadesPredBD;
                IEnumerable<UnidadPredeterminada> ListaUnidadesPred = ListaUnidadesPredBD.Select(u => new UnidadPredeterminada(u, u.SGJD_ADM_TAB_UNIDADES)).ToList();
                return ListaUnidadesPred;
            }
        }

        public async Task<bool> AgregarAsync(int IdUnidad) {
            UnidadPredeterminada UnidadPred = new UnidadPredeterminada(IdUnidad);

            var UnidadPredBD = UnidadPred.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.Add(UnidadPredBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: IdUnidad.ToString(), SeccionSistema: "Unidad predeterminada para información");

            return true;
        }

        public async Task<bool> EliminarAsync(int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUnidadPredeterminadaBD = ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.FindAsync(IdUnidad);
                var UnidadPredeterminadaBD = await TareaObtenerUnidadPredeterminadaBD;
                ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.Attach(UnidadPredeterminadaBD);
                ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.Remove(UnidadPredeterminadaBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "", ValorNuevo: IdUnidad.ToString(), SeccionSistema: "Unidad predeterminada para información");
            return true;
        }
    }
}