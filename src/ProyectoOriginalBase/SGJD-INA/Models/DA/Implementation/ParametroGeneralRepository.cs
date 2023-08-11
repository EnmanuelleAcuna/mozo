using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class ParametroGeneralRepository : IParametroGeneralRepository {
        public async Task<bool> ActualizarAsync(ParametroGeneral Entidad) {
            if (Entidad is null) throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);
            var ParametroGeneralBD = Entidad.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(ParametroGeneralBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<ParametroGeneral> ObtenerPorIdAsync(int IdParametro) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerParametroBD = ContextoBD.SGJD_ADM_TAB_PARAMETROS_GENERALES.FindAsync(IdParametro);
                var ParametroGeneralBD = await TareaObtenerParametroBD;
                ParametroGeneral ModeloParametro = new ParametroGeneral(ParametroGeneralBD);
                return ModeloParametro;
            }
        }

        public async Task<ParametroGeneral> ObtenerPorNombreAsync(string Nombre) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerParametroBD = ContextoBD.SGJD_ADM_TAB_PARAMETROS_GENERALES.Where(pa => pa.Nombre.Equals(Nombre, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
                var ParametroGeneralBD = await TareaObtenerParametroBD;
                ParametroGeneral ModeloParametro = new ParametroGeneral(ParametroGeneralBD);
                return ModeloParametro;
            }
        }

        public IEnumerable<ParametroGeneral> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaParametrosGeneralesBD = ContextoBD.SGJD_ADM_TAB_PARAMETROS_GENERALES.ToList();
                IEnumerable<ParametroGeneral> ListaParametros = ListaParametrosGeneralesBD.Select(pg => new ParametroGeneral(pg)).ToList();
                return ListaParametros;
            }
        }

        // Métodos estáticos
        public static IEnumerable<ParametroGeneral> ObtenerTodosStatic() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaParametrosGeneralesBD = ContextoBD.SGJD_ADM_TAB_PARAMETROS_GENERALES.ToList();
                IEnumerable<ParametroGeneral> ListaParametros = ListaParametrosGeneralesBD.Select(pg => new ParametroGeneral(pg)).ToList();
                return ListaParametros;
            }
        }
    }
}