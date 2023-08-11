using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class AyudaUsuarioRepository : IAyudaUsuarioRepository {
        public async Task<bool> ActualizarAsync(AyudaUsuario Modelo) {
            if (Modelo is null) {
                throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);
            }

            using (var ContextoBD = SGJDDBContext.Create()) {
                var AyudaUsuarioBD = Modelo.BD();

                ContextoBD.Entry(AyudaUsuarioBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Si la cantidad de registros afectado es 1 o mayor, devolver true, de lo
                // contrario devolver false, lo que quiere decir que no se guardó nada en bd
                return (RegistrosAfectados >= 1) ? true : false;
            }
        }

        public async Task<AyudaUsuario> ObtenerPorIdAsync(int IdAyudaUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAyudaUsuarioBD = ContextoBD.SGJD_ADM_TAB_AYUDA_USUARIO.FindAsync(IdAyudaUsuario);
                var AyudaUsuarioBD = await TareaObtenerAyudaUsuarioBD;
                AyudaUsuario AyudaUsuarioModel = new AyudaUsuario(AyudaUsuarioBD);
                return AyudaUsuarioModel;
            }
        }

        public async Task<AyudaUsuario> ObtenerPorAbreviaturaAsync(string Abreviatura) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAyudaUsuarioBD = ContextoBD.SGJD_ADM_TAB_AYUDA_USUARIO.Where(a => a.Abreviatura.Equals(Abreviatura)).FirstOrDefaultAsync();
                var AyudaUsuarioBD = await TareaObtenerAyudaUsuarioBD;
                AyudaUsuario AyudaUsuarioModel = new AyudaUsuario(AyudaUsuarioBD);
                return AyudaUsuarioModel;
            }
        }
    }
}