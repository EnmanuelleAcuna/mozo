using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    public class UsuarioLogic : IUsuarioLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public UsuarioLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        // Métodos públicos
        // Los métodos para agregar, actualizar, inhabilitar y habilitar se manejan con UserManager de tipo ApplicationUserManager que se encuentra en el controlador [UsuarioController], resuelto con Unity
        public IEnumerable<ApplicationUser> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUsuariosBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.ToList();
                IEnumerable<ApplicationUser> ListaUsuarios = ListaUsuariosBD.Select(u => new ApplicationUser(u, u.SGJD_ADM_TAB_UNIDADES, u.SGJD_ADM_TAB_TIPOS_USUARIO, u.SGJD_ADM_TAB_ROLES)).ToList();
                return ListaUsuarios;
            }
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync() {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<ApplicationUser> ObtenerTodosActivos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUsuariosBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(u => u.Activo == true).ToList();
                IEnumerable<ApplicationUser> ListaUsuarios = ListaUsuariosBD.Select(u => new ApplicationUser(u, u.SGJD_ADM_TAB_UNIDADES, u.SGJD_ADM_TAB_TIPOS_USUARIO, u.SGJD_ADM_TAB_ROLES)).ToList();
                return ListaUsuarios;
            }
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosActivosAsync() {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodosActivos());
            return Tarea;
        }

        public IEnumerable<ApplicationUser> ObtenerTodosInactivos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUsuariosBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(u => u.Activo == false).ToList();
                IEnumerable<ApplicationUser> ListaUsuarios = ListaUsuariosBD.Select(u => new ApplicationUser(u, u.SGJD_ADM_TAB_UNIDADES, u.SGJD_ADM_TAB_TIPOS_USUARIO, u.SGJD_ADM_TAB_ROLES)).ToList();
                return ListaUsuarios;
            }
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosInactivosAsync() {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodosInactivos());
            return Tarea;
        }

        public IEnumerable<ApplicationUser> ObtenerTodosPorUnidad(int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUsuariosBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(u => u.LLF_IdUnidad == IdUnidad).ToList();
                IEnumerable<ApplicationUser> ListaUsuarios = ListaUsuariosBD.Select(u => new ApplicationUser(u, u.SGJD_ADM_TAB_UNIDADES, u.SGJD_ADM_TAB_TIPOS_USUARIO, u.SGJD_ADM_TAB_ROLES)).ToList();
                return ListaUsuarios;
            }
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosPorUnidadAsync(int IdUnidad) {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodosPorUnidad(IdUnidad));
            return Tarea;
        }

        public IEnumerable<ApplicationUser> ObtenerTodosPorTipoUsuario(int IdTipoUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUsuariosBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(u => u.LLF_IdTipoUsuario == IdTipoUsuario).ToList();
                IEnumerable<ApplicationUser> ListaUsuarios = ListaUsuariosBD.Select(u => new ApplicationUser(u, u.SGJD_ADM_TAB_UNIDADES, u.SGJD_ADM_TAB_TIPOS_USUARIO, u.SGJD_ADM_TAB_ROLES)).ToList();
                return ListaUsuarios;
            }
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosPorTipoUsuarioAsync(int IdTipoUsuario) {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodosPorTipoUsuario(IdTipoUsuario));
            return Tarea;
        }

        public IEnumerable<ApplicationUser> ObtenerTodosPorRol(string IdRol) {
            IEnumerable<ApplicationUser> ListaUsuariosBD = ObtenerTodos();

            List<ApplicationUser> ListaUsuariosRol = new List<ApplicationUser>();
            foreach (ApplicationUser Usuario in ListaUsuariosBD) {
                if (Usuario.Rol.Id.Equals(IdRol)) {
                    ListaUsuariosRol.Add(Usuario);
                }
            }

            return ListaUsuariosRol;
        }

        public Task<IEnumerable<ApplicationUser>> ObtenerTodosPorRolAsync(string IdRol) {
            Task<IEnumerable<ApplicationUser>> Tarea = Task.Run(() => ObtenerTodosPorRol(IdRol));
            return Tarea;
        }

        public IEnumerable<SelectListItem> ObtenerTodosParaSelect() {
            IEnumerable<ApplicationUser> ListaUsuarios = ObtenerTodos();
            IEnumerable<SelectListItem> ListaUsuariosParaSelect = ListaUsuarios.Select(Usuario => new SelectListItem() { Value = Usuario.Id, Text = Usuario.Nombre });
            return ListaUsuariosParaSelect;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTodosParaSelect());
            return Tarea;
        }

        public ApplicationUser ObtenerPorId(string IdUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var UsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Find(IdUsuario);
                ApplicationUser ModeloUsuario = new ApplicationUser(UsuarioBD, UsuarioBD.SGJD_ADM_TAB_UNIDADES, UsuarioBD.SGJD_ADM_TAB_TIPOS_USUARIO, UsuarioBD.SGJD_ADM_TAB_ROLES);
                return ModeloUsuario;
            }
        }

        public Task<ApplicationUser> ObtenerPorIdAsync(string IdUsuario) {
            Task<ApplicationUser> Tarea = Task.Run(() => ObtenerPorId(IdUsuario));
            return Tarea;
        }

        public ApplicationUser ObtenerPorCorreo(string Correo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var UsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(U => U.Email.Equals(Correo)).FirstOrDefault();
                ApplicationUser ModeloUsuario = new ApplicationUser(UsuarioBD, UsuarioBD.SGJD_ADM_TAB_UNIDADES, UsuarioBD.SGJD_ADM_TAB_TIPOS_USUARIO, UsuarioBD.SGJD_ADM_TAB_ROLES);
                return ModeloUsuario;
            }
        }

        public Task<ApplicationUser> ObtenerPorCorreoAsync(string Correo) {
            Task<ApplicationUser> Tarea = Task.Run(() => ObtenerPorCorreo(Correo));
            return Tarea;
        }

        public ApplicationUser ObtenerPorCedula(string Cedula) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var UsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(U => U.Cedula.Equals(Cedula)).FirstOrDefault();
                ApplicationUser ModeloUsuario = new ApplicationUser(UsuarioBD, UsuarioBD.SGJD_ADM_TAB_UNIDADES, UsuarioBD.SGJD_ADM_TAB_TIPOS_USUARIO, UsuarioBD.SGJD_ADM_TAB_ROLES);
                return ModeloUsuario;
            }
        }

        public Task<ApplicationUser> ObtenerPorCedulaAsync(string Cedula) {
            Task<ApplicationUser> Tarea = Task.Run(() => ObtenerPorCedula(Cedula));
            return Tarea;
        }

        public async Task<ApplicationUser> ObtenerPorNombreUsuarioADAsync(string NombreUsuarioAD) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUsuarioBD = ContextoBD.SGJD_ADM_TAB_USUARIOS.Where(U => U.UserName.Equals(NombreUsuarioAD)).FirstOrDefaultAsync();
                var UsuarioBD = await TareaObtenerUsuarioBD;
                ApplicationUser ModeloUsuario = new ApplicationUser(UsuarioBD, UsuarioBD.SGJD_ADM_TAB_UNIDADES, UsuarioBD.SGJD_ADM_TAB_TIPOS_USUARIO, UsuarioBD.SGJD_ADM_TAB_ROLES);
                return ModeloUsuario;
            }
        }
    }
}