using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class UsuarioSIRHLogic : IUsuarioSIRHLogic {
        private readonly IUsuarioSIRHRepository RepositorioUsuarioSIRH;

        public UsuarioSIRHLogic(IUsuarioSIRHRepository RepositorioUsuarioSIRH) {
            this.RepositorioUsuarioSIRH = RepositorioUsuarioSIRH;
        }

        public async Task<IEnumerable<UsuarioSIRH>> ObtenerTodosAsync() {
            var TareaObtenerUsuariosSIRH = RepositorioUsuarioSIRH.ObtenerTodosAsync();
            IEnumerable<UsuarioSIRH> UsuariosSIRH = await TareaObtenerUsuariosSIRH;
            return UsuariosSIRH;
        }

        public async Task<IEnumerable<UsuarioSIRH>> ObtenerTodosPorNombreUsuarioAsync(string NombreUsuario) {
            var TareaObtenerUsuariosSIRH = RepositorioUsuarioSIRH.ObtenerTodosPorNombreUsuarioAsync(NombreUsuario);
            IEnumerable<UsuarioSIRH> UsuariosSIRH = await TareaObtenerUsuariosSIRH;
            return UsuariosSIRH;
        }

        public async Task<bool> VerificarSiExisteDuplicadoAsync(string NombreUsuario) {
            var TareaObtenerUsuariosSIRH = ObtenerTodosPorNombreUsuarioAsync(NombreUsuario);
            var ListaUsuariosSIRH = await TareaObtenerUsuariosSIRH;

            // A partir de la lista obtenida, validar el conteo de elementos, si es 1 quiere decir que el usuario existe
            // correctamente una sola vez, si por el contrario existe dos o más veces, devolver true
            return ListaUsuariosSIRH.Count() >= 2 ? true : false;
        }
    }
}