using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Clase para realizar operaciones relacionadas a la ayuda de usuario
    /// </summary>
    public class AyudaUsuarioLogic : IAyudaUsuarioLogic {
        // Constructor y dependencias
        private readonly IAyudaUsuarioRepository RepositorioAyudaUsuario;

        public AyudaUsuarioLogic(IAyudaUsuarioRepository RepositorioAyudaUsuario) {
            this.RepositorioAyudaUsuario = RepositorioAyudaUsuario;
        }

        // Métodos públicos
        public async Task<bool> ActualizarAsync(AyudaUsuario ModeloayudaUsuario) {
            if (ModeloayudaUsuario is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaActualizarAyudaUsuario = RepositorioAyudaUsuario.ActualizarAsync(ModeloayudaUsuario);
            bool AyudaUsuarioActualizado = await TareaActualizarAyudaUsuario;

            if (AyudaUsuarioActualizado) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloayudaUsuario.ObtenerInformacion(), SeccionSistema: "AyudaUsuario");
            }

            return AyudaUsuarioActualizado;
        }

        public IEnumerable<AyudaUsuario> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaAyudaUsuarioBD = ContextoBD.SGJD_ADM_TAB_AYUDA_USUARIO.ToList();
                IEnumerable<AyudaUsuario> ListaAyudaUsuario = ListaAyudaUsuarioBD.Select(au => new AyudaUsuario(au)).ToList();
                return ListaAyudaUsuario;
            }
        }

        public Task<IEnumerable<AyudaUsuario>> ObtenerTodosAsync() {
            Task<IEnumerable<AyudaUsuario>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<AyudaUsuario> ObtenerPorIdAsync(int Id) {
            var TareaObtenerPorId = RepositorioAyudaUsuario.ObtenerPorIdAsync(Id);
            AyudaUsuario AyudaUsuarioObtenida = await TareaObtenerPorId;
            return AyudaUsuarioObtenida;
        }

        public async Task<AyudaUsuario> ObtenerPorAbreviaturaAsync(string Abreviatura) {
            var TareaObtenerPorAbreviatura = RepositorioAyudaUsuario.ObtenerPorAbreviaturaAsync(Abreviatura);
            AyudaUsuario AyudaUsuarioObtenida = await TareaObtenerPorAbreviatura;
            return AyudaUsuarioObtenida;
        }
    }
}