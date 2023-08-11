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
    ///Lógica para realizar operaciones sobre la entidad Estados de objetos
    /// </summary>
    public class EstadoLogic : IEstadoLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public EstadoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }

        // Métodos públicos
        public async Task<bool> AgregarAsync(Estado ModeloEstado) {
            var EstadoBD = ModeloEstado.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_ESTADOS.Add(EstadoBD);
                int Registrosafectados = await ContextoBD.SaveChangesAsync();

                if (Registrosafectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloEstado.ObtenerInformacion(), SeccionSistema: "Estado");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Estado ModeloEstado) {
            var EstadoBD = ModeloEstado.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(EstadoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloEstado.ObtenerInformacion(), SeccionSistema: "Estado");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdEstado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstado = ContextoBD.SGJD_ADM_TAB_ESTADOS.FindAsync(IdEstado);
                var EstadoBD = await TareaObtenerEstado;
                ContextoBD.SGJD_ADM_TAB_ESTADOS.Attach(EstadoBD);
                ContextoBD.SGJD_ADM_TAB_ESTADOS.Remove(EstadoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdEstado.ToString(), ValorNuevo: "", SeccionSistema: "Estado");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Estado> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaEstadosBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.ToList();
                IEnumerable<Estado> ListaEstados = ListaEstadosBD.Select(e => new Estado(e, e.SGJD_ADM_TAB_TIPOS_OBJETO, e.SGJD_ADM_TAB_AVISOS)).ToList();
                return ListaEstados;
            }
        }

        public Task<IEnumerable<Estado>> ObtenerTodosAsync() {
            Task<IEnumerable<Estado>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<Estado> ObtenerPorIdAsync(int IdEstado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstadoBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.FindAsync(IdEstado);
                var EstadoBD = await TareaObtenerEstadoBD;
                Estado ModeloEstado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
                return ModeloEstado;
            }
        }

        public async Task<Estado> ObtenerPorCodigoAsync(string CodigoEstado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstadoBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.Where(c => c.CodigoEstado.Equals(CodigoEstado)).FirstOrDefaultAsync();
                var EstadoBD = await TareaObtenerEstadoBD;
                Estado ModeloEstado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
                return ModeloEstado;
            }
        }

        public async Task<IEnumerable<Estado>> ObtenerTodosPorIdTipoObjetoAsync(int IdTipoObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstadoBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.Where(e => e.LLF_IdTipoObjeto == IdTipoObjeto).ToListAsync();
                var ListaEstadosBD = await TareaObtenerEstadoBD;
                IEnumerable<Estado> ListaEstado = ListaEstadosBD.Select(e => new Estado(e)).ToList();
                return ListaEstado;
            }
        }

        public async Task<IEnumerable<Estado>> ObtenerTodosPorNombreObjetoAsync(string NombreTabla) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstadoBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.Where(e => e.SGJD_ADM_TAB_TIPOS_OBJETO.NombreTabla.Equals(NombreTabla)).ToListAsync();
                var ListaEstadosBD = await TareaObtenerEstadoBD;
                IEnumerable<Estado> ListaEstado = ListaEstadosBD.Select(e => new Estado(e)).ToList();
                return ListaEstado;
            }
        }

        public async Task<IEnumerable<Estado>> ObtenerTodosPorNombreObjetoConAvisoParaAcuerdoAsync(string NombreTabla) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerEstadoBD = ContextoBD.SGJD_ADM_TAB_ESTADOS.Where(e => e.SGJD_ADM_TAB_TIPOS_OBJETO.NombreTabla.Equals(NombreTabla)).ToListAsync();
                var ListaEstadosBD = await TareaObtenerEstadoBD;
                IEnumerable<Estado> ListaEstado = ListaEstadosBD.Select(e => new Estado(e, e.SGJD_ADM_TAB_AVISOS)).ToList();
                return ListaEstado;
            }
        }
    }
}