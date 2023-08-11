using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
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
    /// Lógica para realizar operaciones sobre la entidad Seccion
    /// </summary>
    public class SeccionLogic : ISeccionLogic {
        #region Constructor y dependencias
        private readonly ISeccionRepository RepositorioSeccion;

        public SeccionLogic(ISeccionRepository RepositorioSeccion) {
            this.RepositorioSeccion = RepositorioSeccion;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(Seccion ModeloSeccion) {
            if (ModeloSeccion is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaAgregarEnBD = RepositorioSeccion.GuardarAsync(ModeloSeccion);
            bool SeccionAgregada = await TareaAgregarEnBD;

            if (SeccionAgregada) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloSeccion.ObtenerInformacion(), SeccionSistema: "Seccion");
            }

            return SeccionAgregada;

        }

        public async Task<bool> ActualizarAsync(Seccion ModeloSeccion) {
            if (ModeloSeccion is null) {
                throw new NullReferenceException("El modelo está nulo.");
            }

            var TareaActualizarEnBD = RepositorioSeccion.ActualizarAsync(ModeloSeccion);
            bool SeccionActualizada = await TareaActualizarEnBD;

            if (SeccionActualizada) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloSeccion.ObtenerInformacion(), SeccionSistema: "Seccion");
            }

            return SeccionActualizada;
        }

        public async Task<bool> EliminarAsync(int IdSeccion) {
            if (IdSeccion <= 0) {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            var TareaEliminarDeBD = RepositorioSeccion.EliminarAsync(IdSeccion);
            bool SeccionEliminada = await TareaEliminarDeBD;

            if (SeccionEliminada) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "", ValorNuevo: IdSeccion.ToString(), SeccionSistema: "Seccion");
            }

            return SeccionEliminada;

        }

        public IEnumerable<Seccion> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaSeccionBD = ContextoBD.SGJD_ADM_TAB_SECCIONES.ToList();
                IEnumerable<Seccion> ListaSecciones = ListaSeccionBD.Select(SeccionBD => new Seccion(SeccionBD)).ToList();
                return ListaSecciones;
            }
        }

        public Task<IEnumerable<Seccion>> ObtenerTodosAsync() {
            Task<IEnumerable<Seccion>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<Seccion> ObtenerTodosPorIdOrdenDia(int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaSeccionBD = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.Where(OD => OD.LLP_Id == IdOrdenDia).FirstOrDefault().SGJD_ADM_TAB_SECCIONES.ToList();
                IEnumerable<Seccion> ListaSecciones = ListaSeccionBD.Select(SeccionBD => new Seccion(SeccionBD)).ToList();
                return ListaSecciones;
            }
        }

        public Task<IEnumerable<Seccion>> ObtenerTodosPorIdOrdenDiaAsync(int IdOrdenDia) {
            Task<IEnumerable<Seccion>> Tarea = Task.Run(() => ObtenerTodosPorIdOrdenDia(IdOrdenDia));
            return Tarea;
        }

        public async Task<Seccion> ObtenerPorIdAsync(int IdSeccion) {
            var TareaObtenerPorId = RepositorioSeccion.ObtenerPorIdAsync(IdSeccion);
            Seccion SeccionObtenida = await TareaObtenerPorId;
            return SeccionObtenida;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync(int IdOrdenDia) {
            var TareaObtenerSeccionesDeOrdenDia = ObtenerTodosPorIdOrdenDiaAsync(IdOrdenDia);
            IEnumerable<Seccion> ListaSeccionesOrdenDia = await TareaObtenerSeccionesDeOrdenDia;

            var TareaObtenerSecciones = ObtenerTodosAsync();
            IEnumerable<Seccion> ListaSecciones = await TareaObtenerSecciones;

            // Revisar la lista de todos los elementos y remover lo que ya estan asignados al orden del día
            List<SelectListItem> Lista = new List<SelectListItem>();
            foreach (Seccion Seccion in ListaSecciones) {
                Seccion SeccionParaSelect = ListaSeccionesOrdenDia.SingleOrDefault(f => f.Id == Seccion.Id);
                if (SeccionParaSelect == null) Lista.Add(new SelectListItem { Text = Seccion.Descripcion, Value = Seccion.Id.ToString() });
            }

            return Lista;
        }

        public async Task<Seccion> ObtenerPorDescripcion(string Descripcion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeccionBD = ContextoBD.SGJD_ADM_TAB_SECCIONES.Where(S => S.Descripcion.Equals(Descripcion)).FirstOrDefaultAsync();
                var SeccionBD = await TareaObtenerSeccionBD;
                var seccionModel = new Seccion(SeccionBD);
                return seccionModel;
            }
        }
        #endregion
    }
}