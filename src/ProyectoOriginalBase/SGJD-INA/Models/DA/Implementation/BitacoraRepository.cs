using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class BitacoraRepository : IBitacoraRepository {
        public async Task<IEnumerable<Bitacora>> ObtenerTodosAsync() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObeneterListaBitacoraBD = ContextoBD.SGJD_ADM_TAB_BITACORA.OrderByDescending(m => m.FechaHora).Take(20).ToListAsync();
                var ListaBitacoraBD = await TareaObeneterListaBitacoraBD;
                IEnumerable<Bitacora> ListaRegistrosBitacora = ListaBitacoraBD.Select(b => new Bitacora(b, b.SGJD_ADM_TAB_USUARIOS)).ToList();
                return ListaRegistrosBitacora;
            }
        }

        public async Task<IEnumerable<Errores>> ObtenerTodosErroresAsync() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObeneterListaErroresBD = ContextoBD.SGJD_ADM_TAB_ERRORES.OrderByDescending(e => e.Fecha).Take(20).ToListAsync();
                var ListaErroresBD = await TareaObeneterListaErroresBD;
                IEnumerable<Errores> ListaRegistrosErrores = ListaErroresBD.Select(Error => new Errores(Error)).ToList();
                return ListaRegistrosErrores;
            }
        }

        public async Task<Bitacora> ObtenerPorIdAsync(int Id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerBitacoraBD = ContextoBD.SGJD_ADM_TAB_BITACORA.FindAsync(Id);
                var BitacoraBD = await TareaObtenerBitacoraBD;
                Bitacora ModeloBitacora = new Bitacora(BitacoraBD, BitacoraBD.SGJD_ADM_TAB_USUARIOS);
                return ModeloBitacora;
            }
        }

        // Métodos estáticos
        /// <summary>
        /// Guardar un registro de la bitácora en la base de datos
        /// </summary>
        public static async Task GuardarAsync(Bitacora Entidad) {
            if (Entidad is null) throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);

            using (var ContextoBD = SGJDDBContext.Create()) {
                var BitacoraBD = Entidad.BD();
                ContextoBD.SGJD_ADM_TAB_BITACORA.Add(BitacoraBD);
                await ContextoBD.SaveChangesAsync();
            }
        }
    }
}