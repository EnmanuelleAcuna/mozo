using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class VistaPerfilLogic : IVistaPerfilLogic {
        // Constructor y dependencias
        private IVistaPerfilRepository RepositorioVistaPerfil;

        public VistaPerfilLogic(IVistaPerfilRepository RepositorioVistaPerfil) {
            this.RepositorioVistaPerfil = RepositorioVistaPerfil;
        }

        #region Métodos públicos
        public async Task<bool> AgregarAsync(VistaPerfil Modelo) {
            if (Modelo is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaAgregarEnBD = RepositorioVistaPerfil.AgregarAsync(Modelo);
            bool VistaPerfilAgregada = await TareaAgregarEnBD;

            return VistaPerfilAgregada;
        }

        public async Task<bool> ActualizarAsync(VistaPerfil Modelo) {
            var TareaActualizarEnBD = RepositorioVistaPerfil.ActualizarAsync(Modelo);
            bool VistaPerfilActualizado = await TareaActualizarEnBD;

            //if (VistaPerfilActualizado) {
            //    Bitacora.RegistrarAccionEnBitacora(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "VistasPerfil");
            //}

            return VistaPerfilActualizado;
        }

        public async Task<bool> EliminarAsync(VistaPerfil Modelo) {
            if (Modelo is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaEliminarEnBD = RepositorioVistaPerfil.EliminarAsync(Modelo);
            bool VistaPerfilAgregada = await TareaEliminarEnBD;

            return VistaPerfilAgregada;
        }

        public IEnumerable<VistaPerfil> ObtenerTodosPorIdPerfil(string IdPerfil) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaVistaPerfilBD = ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Where(m => m.LLF_IdRol.Equals(IdPerfil)).ToList();

                var ListaVistaPerfilModel = new List<VistaPerfil>();
                foreach (var VistaPerfilBD in ListaVistaPerfilBD) {
                    var vistaPerfilModel = ConvertirBD_Modelo(VistaPerfilBD);
                    ListaVistaPerfilModel.Add(vistaPerfilModel);
                }
                return ListaVistaPerfilModel;
            }
        }

        public Task<IEnumerable<VistaPerfil>> ObtenerTodosPorIdPerfilAsync(string IdPerfil) {
            Task<IEnumerable<VistaPerfil>> Tarea = Task.Run(() => ObtenerTodosPorIdPerfil(IdPerfil));
            return Tarea;
        }

        public IEnumerable<VistaPerfil> ObtenerTodosPorIdVista(int IdVista) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaVistaPerfilBD = ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Where(m => m.LLF_IdVista.Equals(IdVista)).ToList();

                var ListaVistaPerfilModel = new List<VistaPerfil>();
                foreach (var VistaPerfilBD in ListaVistaPerfilBD) {
                    var vistaPerfilModel = ConvertirBD_Modelo(VistaPerfilBD);
                    ListaVistaPerfilModel.Add(vistaPerfilModel);
                }
                return ListaVistaPerfilModel;
            }
        }

        public Task<IEnumerable<VistaPerfil>> ObtenerTodosPorIdVistaAsync(int IdVista) {
            Task<IEnumerable<VistaPerfil>> Tarea = Task.Run(() => ObtenerTodosPorIdVista(IdVista));
            return Tarea;
        }

        public VistaPerfil ObtenerPorId(string IdPerfil, int IdVista) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var VistaPerfilBD = ContextoBD.SGJD_ADM_TAB_VISTAS_PERFIL.Where(m => m.LLF_IdRol.Equals(IdPerfil) && m.LLF_IdVista == IdVista).FirstOrDefault();
                var Modelo = ConvertirBD_Modelo(VistaPerfilBD);
                return Modelo;
            }
        }

        public async Task<VistaPerfil> ObtenerPorIdAsync(string idPerfil, int idVista) {
            var TareaObtenerPorId = RepositorioVistaPerfil.ObtenerPorIdAsync(idPerfil, idVista);
            VistaPerfil Modelo = await TareaObtenerPorId;
            return Modelo;
        }

        public IEnumerable<Vista> ObtenerVistasPorIdPerfil(string idPerfil) {
            var ListaVistaPerfil = ObtenerTodosPorIdPerfil(idPerfil);

            // Construir lista sólo con las vistas
            List<Vista> ListaVistasModel = new List<Vista>();
            foreach (var VistaPerfil in ListaVistaPerfil) {
                Vista Modelo = VistaPerfil.Vista;
                ListaVistasModel.Add(Modelo);
            }
            return ListaVistasModel;
        }

        public Task<IEnumerable<Vista>> ObtenerVistasPorIdPerfilAsync(string idPerfil) {
            Task<IEnumerable<Vista>> Tarea = Task.Run(() => ObtenerVistasPorIdPerfil(idPerfil));
            return Tarea;
        }

        public async Task<IEnumerable<ApplicationRole>> ObtenerPerfilesPorIdVista(int IdVista) {
            var TareaObtenerVistasPerfil = ObtenerTodosPorIdVistaAsync(IdVista);
            IEnumerable<VistaPerfil> ListaVistaPerfil = await TareaObtenerVistasPerfil;

            // Construir lista sólo con los perfiles
            List<ApplicationRole> ListaPerfilesModel = new List<ApplicationRole>();
            foreach (var VistaPerfil in ListaVistaPerfil) {
                ApplicationRole Modelo = VistaPerfil.Perfil;
                ListaPerfilesModel.Add(Modelo);
            }
            return ListaPerfilesModel;
        }

        public int ObtenerPermiso(string IdPerfil, int IdVista) {
            VistaPerfil Modelo = ObtenerPorId(IdPerfil, IdVista);
            return (Modelo != null) ? Modelo.Permiso : -1;
        }
        #endregion

        #region Permisos según perfil
        /// <summary>
        /// Establecer el valor de la propiedad permiso, basándose en los valores de las acciones.
        /// </summary>
        public VistaPerfil AsignarPermiso(VistaPerfil vistaPerfilModel, string crear, string editar, string eliminar) {
            int nivelPermiso = 0;
            int pCrear = 1;
            int pEditar = 2;
            int pEliminar = 4;

            //Para evitar errores se verifica si los valores vienen nulos ya que, materialize sólamente los envía como "on" o null.
            crear = string.IsNullOrEmpty(crear) || Funciones.IsNumber(crear) || (crear != "on") ? "off" : crear;
            editar = string.IsNullOrEmpty(editar) || Funciones.IsNumber(editar) || editar != "on" ? "off" : editar;
            eliminar = string.IsNullOrEmpty(eliminar) || Funciones.IsNumber(eliminar) || eliminar != "on" ? "off" : eliminar;

            // Ninguno marcado (Por defecto ver está marcado por lo que no se neecesita validar)
            if (crear.Equals("off") && editar.Equals("off") && eliminar.Equals("off")) {
                nivelPermiso = 0;
            }
            // Sólo crear marcado
            else if (crear.Equals("on") && editar.Equals("off") && eliminar.Equals("off")) {
                nivelPermiso = pCrear;
            }
            // Sólo editar marcado
            else if (crear.Equals("off") && editar.Equals("on") && eliminar.Equals("off")) {
                nivelPermiso = pEditar;
            }
            // Sólo eliminar marcado
            else if (crear.Equals("off") && editar.Equals("off") && eliminar.Equals("on")) {
                nivelPermiso = pEliminar;
            }
            // Sólo crear y editar marcados
            else if (crear.Equals("on") && editar.Equals("on") && eliminar.Equals("off")) {
                nivelPermiso = pCrear + pEditar;
            }
            // Sólo editar y eliminar marcados
            else if (crear.Equals("off") && editar.Equals("on") && eliminar.Equals("on")) {
                nivelPermiso = pEditar + pEliminar;
            }
            // Sólo crear y eliminar
            else if (crear.Equals("on") && editar.Equals("off") && eliminar.Equals("on")) {
                nivelPermiso = pCrear + pEliminar;
            }
            // Todos marcados
            else if (crear.Equals("on") && editar.Equals("on") && eliminar.Equals("on")) {
                nivelPermiso = pCrear + pEditar + pEliminar;
            }

            vistaPerfilModel.Permiso = nivelPermiso;

            return vistaPerfilModel;
        }
        #endregion

        #region Metodos de conversión
        internal SGJD_ADM_TAB_VISTAS_PERFIL ConvertirModelo_BD(VistaPerfil vistaPerfilModel) {
            return new SGJD_ADM_TAB_VISTAS_PERFIL() {
                LLF_IdRol = vistaPerfilModel.IdPerfil,
                LLF_IdVista = vistaPerfilModel.IdVista,
                Permiso = vistaPerfilModel.Permiso
            };
        }

        internal VistaPerfil ConvertirBD_Modelo(SGJD_ADM_TAB_VISTAS_PERFIL vistaPerfilBD) {
            return new VistaPerfil() {
                IdPerfil = vistaPerfilBD.LLF_IdRol,
                IdVista = vistaPerfilBD.LLF_IdVista,
                Permiso = vistaPerfilBD.Permiso,
                NombreObjeto = vistaPerfilBD.GetType().UnderlyingSystemType.BaseType.Name,
                Perfil = new ApplicationRole() {
                    Id = vistaPerfilBD.SGJD_ADM_TAB_ROLES.Id,
                    Name = vistaPerfilBD.SGJD_ADM_TAB_ROLES.Name
                },
                Vista = new Vista() {
                    Id = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.LLP_Id,
                    NombreVista = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.NombreVista,
                    DireccionVista = vistaPerfilBD.SGJD_ADM_TAB_VISTAS.DireccionVista
                }
            };
        }
        #endregion
    }
}