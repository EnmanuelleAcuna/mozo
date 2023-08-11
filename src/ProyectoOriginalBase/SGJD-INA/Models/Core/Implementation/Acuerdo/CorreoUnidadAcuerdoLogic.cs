using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class CorreoUnidadAcuerdoLogic : ICorreoUnidadAcuerdoLogic {
        // Constructor y dependencias
        public CorreoUnidadAcuerdoLogic() { }

        // Métodos públicos
        private IEnumerable<CorreoUnidadAcuerdo> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaCorreoUnidadAcuerdoBD = ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.ToList();
                IEnumerable<CorreoUnidadAcuerdo> ListaCorreoUnidadAcuerdo = ListaCorreoUnidadAcuerdoBD.Select(cua => new CorreoUnidadAcuerdo(cua, cua.SGJD_ADM_TAB_UNIDADES)).ToList();
                return ListaCorreoUnidadAcuerdo;
            }
        }

        public Task<IEnumerable<CorreoUnidadAcuerdo>> ObtenerTodosAsync() {
            Task<IEnumerable<CorreoUnidadAcuerdo>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<CorreoUnidadAcuerdo> ObtenerPorIdAsync(int IdCorreoUnidadAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerCorreoUnidadAcuerdoBD = ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.FindAsync(IdCorreoUnidadAcuerdo);
                var CorreoUnidadAcuerdoBD = await TareaObtenerCorreoUnidadAcuerdoBD;
                CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo = new CorreoUnidadAcuerdo(CorreoUnidadAcuerdoBD, CorreoUnidadAcuerdoBD.SGJD_ADM_TAB_UNIDADES);
                return ModeloCorreoUnidadAcuerdo;
            }
        }

        public async Task<bool> AgregarAsync(CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo) {
            var CorreoUnidadAcuerdoBD = ModeloCorreoUnidadAcuerdo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.Add(CorreoUnidadAcuerdoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloCorreoUnidadAcuerdo.ObtenerInformacion(), SeccionSistema: "CorreoUnidadAcuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(CorreoUnidadAcuerdo ModeloCorreoUnidadAcuerdo) {
            var CorreoUnidadAcuerdoBD = ModeloCorreoUnidadAcuerdo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(CorreoUnidadAcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloCorreoUnidadAcuerdo.ObtenerInformacion(), SeccionSistema: "CorreoUnidadAcuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdCorreoUnidadAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerCorreoUnidadAcuerdo = ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.FindAsync(IdCorreoUnidadAcuerdo);
                var CorreoUnidadAcuerdoBD = await TareaObtenerCorreoUnidadAcuerdo;
                ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.Attach(CorreoUnidadAcuerdoBD);
                ContextoBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO.Remove(CorreoUnidadAcuerdoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdCorreoUnidadAcuerdo.ToString(), ValorNuevo: "", SeccionSistema: "CorreoUnidadAcuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}