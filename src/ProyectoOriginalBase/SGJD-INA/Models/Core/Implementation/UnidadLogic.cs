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
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad Unidad
    /// </summary>
    public class UnidadLogic : IUnidadLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public UnidadLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(Unidad ModeloUnidad) {
            var UnidadBD = ModeloUnidad.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_UNIDADES.Add(UnidadBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloUnidad.ObtenerInformacion(), SeccionSistema: "Unidad");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Unidad ModeloUnidad) {
            var UnidadBD = ModeloUnidad.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(UnidadBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloUnidad.ObtenerInformacion(), SeccionSistema: "Unidad");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUnidadBD = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(IdUnidad);
                var UnidadBD = await TareaObtenerUnidadBD;
                ContextoBD.SGJD_ADM_TAB_UNIDADES.Attach(UnidadBD);
                ContextoBD.SGJD_ADM_TAB_UNIDADES.Remove(UnidadBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "Id unidad: " + IdUnidad, ValorNuevo: "", SeccionSistema: "Unidad");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Unidad> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUnidadesBD = ContextoBD.SGJD_ADM_TAB_UNIDADES.ToList();
                IEnumerable<Unidad> ListaUnidades = ListaUnidadesBD.Select(UnidadBD => new Unidad(UnidadBD, UnidadBD.SGJD_ADM_TAB_USUARIOS)).ToList();
                return ListaUnidades;
            }
        }

        public Task<IEnumerable<Unidad>> ObtenerTodosAsync() {
            Task<IEnumerable<Unidad>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<SelectListItem> ObtenerTodosParaSelect() {
            IEnumerable<Unidad> ListaUnidades = ObtenerTodos();
            List<SelectListItem> ListaUnidadesParaSelect = ListaUnidades.Select(u => new SelectListItem { Text = u.Nombre, Value = u.Id.ToString() }).ToList();
            return ListaUnidadesParaSelect;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTodosParaSelect());
            return Tarea;
        }

        public async Task<Unidad> ObtenerPorIdAsync(int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUnidadBD = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(IdUnidad);
                var UnidadBD = await TareaObtenerUnidadBD;
                Unidad ModeloUnidad = new Unidad(UnidadBD, UnidadBD.SGJD_ADM_TAB_USUARIOS);
                return ModeloUnidad;
            }
        }

        public async Task<Unidad> ObtenerPorNombre(string Nombre) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerUnidadBD = ContextoBD.SGJD_ADM_TAB_UNIDADES.Where(n => n.Nombre.Equals(Nombre)).FirstOrDefaultAsync();
                var UnidadBD = await TareaObtenerUnidadBD;
                Unidad ModeloUnidad = new Unidad(UnidadBD);
                return ModeloUnidad;
            }
        }
        #endregion
    }
}