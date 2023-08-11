using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Implementation {
    public class UsuarioSIRHRepository : IUsuarioSIRHRepository {
        public async Task<IEnumerable<UsuarioSIRH>> ObtenerTodosAsync() {
            using (var ContextoBD = new SIRHDBContext().Create()) {
                var TareaObtenerUsuariosSIRHBD = ContextoBD.V_SIRH_FUNC_SIRIA.OrderBy(u => u.USUARIO).ToListAsync();
                var ListaUsuariosSIRHBD = await TareaObtenerUsuariosSIRHBD;
                IEnumerable<UsuarioSIRH> ListaUsuariosSIRH = ListaUsuariosSIRHBD.Select(u => new UsuarioSIRH(u)).ToList();
                return ListaUsuariosSIRH;
            }
        }

        public async Task<IEnumerable<UsuarioSIRH>> ObtenerTodosPorNombreUsuarioAsync(string NombreUsuario) {
            using (var ContextoBD = new SIRHDBContext().Create()) {
                var TareaObtenerUsuariosSIRHBD = ContextoBD.V_SIRH_FUNC_SIRIA.Where(u => u.USUARIO.Equals(NombreUsuario)).ToListAsync();
                var ListaUsuariosSIRHBD = await TareaObtenerUsuariosSIRHBD;
                IEnumerable<UsuarioSIRH> ListaUsuariosSIRH = ListaUsuariosSIRHBD.Select(u => new UsuarioSIRH(u)).ToList();
                return ListaUsuariosSIRH;
            }
        }
    }
}