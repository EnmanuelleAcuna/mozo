using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class TipoObjetoRepository : ITipoObjetoRepository {
        public async Task<TipoObjeto> ActualizarAsync(TipoObjeto Modelo) {
            if (Modelo is null) {
                throw new ArgumentNullException(paramName: nameof(Modelo), message: "El modelo TipoObjeto es nulo.");
            }

            using (var ContextoBD = SGJDDBContext.Create()) {
                var TipoObjetoBD = Modelo.BD();
                ContextoBD.Entry(TipoObjetoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();
                return (RegistrosAfectados >= 1) ? Modelo : null;
            }
        }
    }
}