using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class TipoSesionLogic : ITipoSesionLogic {
        #region Constructor y dependencias
        private readonly ITipoSesionRepository RepositorioTipoSesion;

        public TipoSesionLogic(ITipoSesionRepository RepositorioTipoSesion) {
            this.RepositorioTipoSesion = RepositorioTipoSesion;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(TipoSesion ModeloTipoSesion) {
            if (ModeloTipoSesion is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaAgregarTipoSesion = RepositorioTipoSesion.GuardarAsync(ModeloTipoSesion);
            bool TipoSesionAgregado = await TareaAgregarTipoSesion;

            if (TipoSesionAgregado) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTipoSesion.ObtenerInformacion(), SeccionSistema: "TipoSesión");
            }

            return TipoSesionAgregado;
        }

        public async Task<bool> ActualizarAsync(TipoSesion ModeloTipoSesion) {
            if (ModeloTipoSesion is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaActualizarTipoSesion = RepositorioTipoSesion.ActualizarAsync(ModeloTipoSesion);
            bool TipoSesionActualizado = await TareaActualizarTipoSesion;

            if (TipoSesionActualizado) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloTipoSesion.ObtenerInformacion(), SeccionSistema: "TipoSesión");
            }

            return TipoSesionActualizado;
        }

        public async Task<bool> EliminarAsync(int IdTipoSesion) {
            if (IdTipoSesion <= 0) {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            var TareaEliminarEnBD = RepositorioTipoSesion.EliminarAsync(IdTipoSesion);
            bool TipoSesionEliminada = await TareaEliminarEnBD;

            if (TipoSesionEliminada) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "", ValorNuevo: IdTipoSesion.ToString(), SeccionSistema: "TipoSesión");
            }

            return TipoSesionEliminada;
        }

        public IEnumerable<TipoSesion> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTipoSesionBD = ContextoBD.SGJD_ADM_TAB_TIPOS_SESION.ToList();
                IEnumerable<TipoSesion> ListaTipoSesion = ListaTipoSesionBD.Select(ts => new TipoSesion(ts)).ToList();
                return ListaTipoSesion;
            }
        }

        public Task<IEnumerable<TipoSesion>> ObtenerTodosAsync() {
            Task<IEnumerable<TipoSesion>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<TipoSesion> ObtenerPorIdAsync(int Id) {
            var TareaObtenerPorId = RepositorioTipoSesion.ObtenerPorIdAsync(Id);
            TipoSesion TipoSesionObtenida = await TareaObtenerPorId;
            return TipoSesionObtenida;
        }
        #endregion
    }
}