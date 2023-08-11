using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad Estado
    /// </summary>
    public class PerfilLogic : IPerfilLogic {
        #region Constructor y dependencias
        private IBitacoraLogic Bitacora;

        public PerfilLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        // Los métodos para agregar, actualizar, inhabilitar y habilitar se manejan con UserManager de tipo ApplicationUserManager que se encuentra en el controlador [UsuarioController], resuelto con Unity
        public IEnumerable<ApplicationRole> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaRolesBD = ContextoBD.SGJD_ADM_TAB_ROLES.ToList();
                IEnumerable<ApplicationRole> ListaRoles = ListaRolesBD.Select(r => new ApplicationRole(r)).ToList();
                return ListaRoles;
            }
        }

        public Task<IEnumerable<ApplicationRole>> ObtenerTodosAsync() {
            Task<IEnumerable<ApplicationRole>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public ApplicationRole ObtenerPorId(string IdRol) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var RolBD = ContextoBD.SGJD_ADM_TAB_ROLES.Find(IdRol);
                ApplicationRole ModeloRol = new ApplicationRole(RolBD);
                return ModeloRol;
            }
        }

        public Task<ApplicationRole> ObtenerPorIdAsync(string IdRol) {
            Task<ApplicationRole> Tarea = Task.Run(() => ObtenerPorId(IdRol));
            return Tarea;
        }

        public ApplicationRole ObtenerPorNombre(string NombreRol) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var RolBD = ContextoBD.SGJD_ADM_TAB_ROLES.Where(r => r.Name.Equals(NombreRol)).FirstOrDefault();
                ApplicationRole ModeloRol = new ApplicationRole(RolBD);
                return ModeloRol;
            }
        }

        public Task<ApplicationRole> ObtenerPorNombreAsync(string NombreRol) {
            Task<ApplicationRole> Tarea = Task.Run(() => ObtenerPorNombre(NombreRol));
            return Tarea;
        }
        #endregion
    }
}