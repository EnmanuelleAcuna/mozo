using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class SeccionRepository : ISeccionRepository {
        public async Task<bool> GuardarAsync(Seccion Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var SeccionBD = Modelo.BD();

                ContextoBD.SGJD_ADM_TAB_SECCIONES.Add(SeccionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> ActualizarAsync(Seccion Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var SeccionBD = Modelo.BD();

                ContextoBD.Entry(SeccionBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> EliminarAsync(int IdSeccion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeccionBD = ContextoBD.SGJD_ADM_TAB_SECCIONES.FindAsync(IdSeccion);
                var SeccionBD = await TareaObtenerSeccionBD;
                ContextoBD.SGJD_ADM_TAB_SECCIONES.Attach(SeccionBD);
                ContextoBD.SGJD_ADM_TAB_SECCIONES.Remove(SeccionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<Seccion> ObtenerPorIdAsync(int IdSeccion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeccionBD = ContextoBD.SGJD_ADM_TAB_SECCIONES.FindAsync(IdSeccion);
                var SeccionBD = await TareaObtenerSeccionBD;
                var seccionModel = new Seccion(SeccionBD);
                return seccionModel;
            }
        }
    }
}