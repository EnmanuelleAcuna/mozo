using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class TipoSesionRepository : ITipoSesionRepository {
        public async Task<bool> GuardarAsync(TipoSesion Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TipoSesionBD = Modelo.BD();

                ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.Add(TipoSesionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> ActualizarAsync(TipoSesion Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TipoSesionBD = Modelo.BD();

                ContextoBD.Entry(TipoSesionBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<bool> EliminarAsync(int IdTipoSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoSesionBD = ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.FindAsync(IdTipoSesion);
                var TipoSesionBD = await TareaObtenerTipoSesionBD;

                ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.Attach(TipoSesionBD);
                ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.Remove(TipoSesionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }


        public async Task<TipoSesion> ObtenerPorIdAsync(int IdTipoSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoSesionBD = ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.FindAsync(IdTipoSesion);
                var TipoSesionBD = await TareaObtenerTipoSesionBD;
                TipoSesion TipoSesionModel = new TipoSesion(TipoSesionBD);
                return TipoSesionModel;
            }
        }
    }
}