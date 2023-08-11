using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class TomoRepository : ITomoRepository {

        public async Task<Tomo> GuardarAsync(Tomo ModeloTomo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TomoBD = ModeloTomo.BD();
                ContextoBD.SGJD_ACT_TAB_TOMOS.Add(TomoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    ModeloTomo.Id = TomoBD.LLP_Id; // Asignar el id al modelo
                    return ModeloTomo;
                }
                else {
                    return null;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Tomo ModeloTomo) {
            var TomoBD = ModeloTomo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(TomoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistrosAfectados <= 1) ? true : false;
            }
        }

        public async Task<bool> EliminarAsync(int IdTomo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTomo = ContextoBD.SGJD_ACT_TAB_TOMOS.FindAsync(IdTomo);
                var TomoBD = await TareaObtenerTomo;

                ContextoBD.SGJD_ACT_TAB_TOMOS.Attach(TomoBD);
                ContextoBD.SGJD_ACT_TAB_TOMOS.Remove(TomoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                return (RegistrosAfectados <= 1) ? true : false;
            }
        }

        public IEnumerable<Tomo> ObtenerTodosSinActas() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TomosBD = ContextoBD.SGJD_ACT_TAB_TOMOS.ToList();
                IEnumerable<Tomo> ListaTomos = TomosBD.Select(t => new Tomo(t, t.SGJD_ADM_TAB_ESTADOS, t.SGJD_ADM_TAB_USUARIOS, t.SGJD_ADM_TAB_USUARIOS1)).ToList();
                return ListaTomos;
            }
        }

        public IEnumerable<Tomo> ObtenerTodosConActas() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TomosBD = ContextoBD.SGJD_ACT_TAB_TOMOS.ToList();
                IEnumerable<Tomo> ListaTomos = TomosBD.Select(t => new Tomo(t, t.SGJD_ADM_TAB_ESTADOS, t.SGJD_ADM_TAB_USUARIOS, t.SGJD_ADM_TAB_USUARIOS1, t.SGJD_ACT_TAB_ACTAS)).ToList();
                return ListaTomos;
            }
        }

        public Tomo ObtenerPorIdSinActas(int IdTomo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TomoBD = ContextoBD.SGJD_ACT_TAB_TOMOS.Find(IdTomo);
                Tomo ModeloTomo = new Tomo(TomoBD, TomoBD.SGJD_ADM_TAB_ESTADOS, TomoBD.SGJD_ADM_TAB_USUARIOS, TomoBD.SGJD_ADM_TAB_USUARIOS1);
                return ModeloTomo;
            }
        }

        public Tomo ObtenerPorIdConActas(int IdTomo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TomoBD = ContextoBD.SGJD_ACT_TAB_TOMOS.Find(IdTomo);
                Tomo ModeloTomo = new Tomo(TomoBD, TomoBD.SGJD_ADM_TAB_ESTADOS, TomoBD.SGJD_ADM_TAB_USUARIOS, TomoBD.SGJD_ADM_TAB_USUARIOS1, TomoBD.SGJD_ACT_TAB_ACTAS);
                return ModeloTomo;
            }
        }
    }
}