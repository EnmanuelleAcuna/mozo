using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad UsuarioArticulo
    /// </summary>
    public class UsuarioArticuloLogic : IUsuarioArticuloLogic {
        #region Atributos & constructor
        private readonly IBitacoraLogic Bitacora;

        public UsuarioArticuloLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<int> AgregarAsync(UsuarioArticulo UsuarioArticuloModel) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var UsuarioArticuloBD = UsuarioArticuloModel.BD();
                UsuarioArticuloBD = ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.Add(UsuarioArticuloBD);
                await ContextoBD.SaveChangesAsync();

                UsuarioArticuloModel.Id = UsuarioArticuloBD.LLP_Id;
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar usuario a artículo", ValorAntiguo: "", ValorNuevo: UsuarioArticuloModel.ObtenerInformacion(), SeccionSistema: "Artículo");
            return UsuarioArticuloModel.Id;
        }

        public async Task<bool> EliminarAsync(int IdUsuarioArticulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUsuarioArticuloBD = ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.FindAsync(IdUsuarioArticulo);
                var UsuarioArticuloBD = await TareaObtenerUsuarioArticuloBD;
                ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.Attach(UsuarioArticuloBD);
                ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.Remove(UsuarioArticuloBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar usuario de artículo", ValorAntiguo: IdUsuarioArticulo.ToString(), ValorNuevo: "", SeccionSistema: "Artículo");
            return true;
        }

        public async Task<IEnumerable<UsuarioArticulo>> ObtenerTodosAsync() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUsuarioArticuloBD = ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.ToListAsync();
                var ListaUsuarioArticuloBD = await TareaObtenerUsuarioArticuloBD;

                IEnumerable<UsuarioArticulo> ListaUsuariosArticulo = ListaUsuarioArticuloBD.Select(ua => new UsuarioArticulo(ua, ua.SGJD_ACT_TAB_ARTICULOS, ua.SGJD_ADM_TAB_USUARIOS, ua.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION)).ToList();

                return ListaUsuariosArticulo;
            }
        }

        public async Task<IEnumerable<UsuarioArticulo>> ObtenerTodosPorIdArticulo(int IdArticulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerUsuarioArticuloBD = ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.Where(UA => UA.LLF_IdArticulo == IdArticulo).ToListAsync();
                var ListaUsuarioArticuloBD = await tareaObtenerUsuarioArticuloBD;

                IEnumerable<UsuarioArticulo> ListaUsuariosArticulo = ListaUsuarioArticuloBD.Select(ua => new UsuarioArticulo(ua, ua.SGJD_ACT_TAB_ARTICULOS, ua.SGJD_ADM_TAB_USUARIOS, ua.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION)).ToList();

                return ListaUsuariosArticulo;
            }
        }

        public async Task<UsuarioArticulo> ObtenerPorIdAsync(int Id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerUsuarioArticuloBD = ContextoBD.SGJD_ACT_TAB_USUARIOS_ARTICULO.FindAsync(Id);
                var UsuarioArticuloBD = await tareaObtenerUsuarioArticuloBD;
                UsuarioArticulo UsuarioArticuloModel = new UsuarioArticulo(UsuarioArticuloBD, UsuarioArticuloBD.SGJD_ACT_TAB_ARTICULOS, UsuarioArticuloBD.SGJD_ADM_TAB_USUARIOS, UsuarioArticuloBD.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION);
                return UsuarioArticuloModel;
            }
        }

        public async Task<bool> EliminarTodosPorIdArticulo(int IdArticulo) {
            var TareaObtenerTodosUsuarioArticulo = ObtenerTodosPorIdArticulo(IdArticulo);
            IEnumerable<UsuarioArticulo> ListaUsuarioArticulo = await TareaObtenerTodosUsuarioArticulo;

            if (ListaUsuarioArticulo.Any()) {
                foreach (UsuarioArticulo UsuarioArticuloModelo in ListaUsuarioArticulo) {
                    bool UsuarioArticuloEliminado = false;
                    UsuarioArticulo UsuarioArticuloAnterior = UsuarioArticuloModelo;

                    var TareaEliminarUsuarioArticulo = EliminarAsync(UsuarioArticuloModelo.Id);
                    UsuarioArticuloEliminado = await TareaEliminarUsuarioArticulo;

                    if (UsuarioArticuloEliminado == false) {
                        return false;
                    }
                    else {
                        await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: UsuarioArticuloAnterior.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "UsuarioArticulo");
                    }
                }
            }
            return true;
        }
        #endregion       
    }
}