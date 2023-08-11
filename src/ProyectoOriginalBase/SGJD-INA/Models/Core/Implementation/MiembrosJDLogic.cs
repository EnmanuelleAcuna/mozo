using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class MiembrosJDLogic : IMiembrosJDLogic {
        #region Atributos & constructor
        private IBitacoraLogic Bitacora;

        public MiembrosJDLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Operaciones en base de datos
        /// <summary>
        /// Agregar un usuario a la lista de miembros de JD
        /// </summary>
        public async Task<bool> Agregar(string IdUsuario) {
            MiembroJD MiembroJD = new MiembroJD(IdUsuario);

            var MiembroJDBD = MiembroJD.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA.Add(MiembroJDBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: IdUsuario, SeccionSistema: "MiembrosJD");

            return true;
        }

        /// <summary>
        /// Eliminar a un usuario de la lista de miembros de JD
        /// </summary>
        public async Task<bool> Eliminar(string IdUsuario) {
            MiembroJD MiembroJD = new MiembroJD(IdUsuario);

            var MiembroJDBD = MiembroJD.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA.Attach(MiembroJDBD);
                ContextoBD.SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA.Remove(MiembroJDBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "", ValorNuevo: IdUsuario, SeccionSistema: "MiembrosJD");

            return true;
        }

        /// <summary>
        /// Obtener listado de todos los miembros de la JD
        /// </summary>
        public async Task<IEnumerable<MiembroJD>> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerMiembrosJDBD = ContextoBD.SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA.ToListAsync();
                var ListaMiembrosJDBD = await TareaObtenerMiembrosJDBD;
                IEnumerable<MiembroJD> ListaMiembrosJD = ListaMiembrosJDBD.Select(m => new MiembroJD(m)).ToList();
                return ListaMiembrosJD;
            }
        }

        /// <summary>
        /// Obtener lista de los miembros ausentes a una sesion 
        /// </summary>
        public IEnumerable<MiembroJD> ObtenerMiembrosAusentes(IEnumerable<MiembroJD> ListaMiembrosJDModel, IEnumerable<AsistenteSesion> ListaUsuarioAsistentesModel) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                List<MiembroJD> ListaMiembrosJDAusentes = ListaMiembrosJDModel.ToList();
                foreach (AsistenteSesion AsistenteSesion in ListaUsuarioAsistentesModel) {
                    foreach (MiembroJD MiembroJD in ListaMiembrosJDAusentes.ToList()) {
                        if (MiembroJD.IdUsuario == AsistenteSesion.IdUsuario) {
                            ListaMiembrosJDAusentes.Remove(MiembroJD);
                            break;
                        }
                    }
                }
                return ListaMiembrosJDAusentes;
            }
        }
        #endregion
    }
}