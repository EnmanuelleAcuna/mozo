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
    public class ElementosRelacionadosLogic : IElementosRelacionadosLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IActaLogic Acta;
        private readonly IAcuerdoLogic Acuerdo;

        public ElementosRelacionadosLogic(IBitacoraLogic Bitacora, ITipoObjetoLogic TipoObjeto, IActaLogic Acta, IAcuerdoLogic Acuerdo) {
            this.Bitacora = Bitacora;
            this.TipoObjeto = TipoObjeto;
            this.Acta = Acta;
            this.Acuerdo = Acuerdo;
        }
        #endregion

        #region Métodos públicos
        public IEnumerable<ElementoRelacionado> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaElementosRelacionadosBD = ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.ToList();
                IEnumerable<ElementoRelacionado> ListaElementosRelacionadosModel = ListaElementosRelacionadosBD.Select(ElemRelacionado => new ElementoRelacionado(ElemRelacionado)).ToList();
                return ListaElementosRelacionadosModel;
            }
        }

        public Task<IEnumerable<ElementoRelacionado>> ObtenerTodosAsync() {
            Task<IEnumerable<ElementoRelacionado>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<IEnumerable<ElementoRelacionado>> ObtenerTodosPorIdSeguimientoAsync(int IdSeguimiento) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaElementoRelacionadoBD = ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.Where(ER => ER.LLF_IdSeguimiento == IdSeguimiento).ToListAsync();
                var ListaElementoRelacionadoBD = await TareaElementoRelacionadoBD;
                IEnumerable<ElementoRelacionado> ListaElementoRelacionadoModel = ListaElementoRelacionadoBD.Select(ElemRelacionado => new ElementoRelacionado(ElemRelacionado, ElemRelacionado.SGJD_ACU_TAB_SEGUIMIENTOS, ElemRelacionado.SGJD_ACT_TAB_ACTAS, ElemRelacionado.SGJD_ACU_TAB_ACUERDOS)).ToList();
                return ListaElementoRelacionadoModel;
            }
        }

        public async Task<IEnumerable<ElementoRelacionado>> ObtenerTodosPorIdSeguimientoTipoElementoAsync(int IdSeguimiento, string TipoElemento) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaElementoRelacionadoBD = ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.Where(ER => ER.LLF_IdSeguimiento == IdSeguimiento && ER.TipoElemento.Equals(TipoElemento)).ToListAsync();
                var ListaElementoRelacionadoBD = await TareaElementoRelacionadoBD;
                IEnumerable<ElementoRelacionado> ListaElementoRelacionadoModel = ListaElementoRelacionadoBD.Select(ElemRelacionado => new ElementoRelacionado(ElemRelacionado)).ToList();
                return ListaElementoRelacionadoModel;
            }
        }

        public IEnumerable<SelectListItem> ObtenerTiposElementosRelacionadosParaSelect() {
            List<SelectListItem> ListaTiposSeguimiento = new List<SelectListItem>();
            ListaTiposSeguimiento.Add(new SelectListItem { Text = "Acta", Value = "0" });
            ListaTiposSeguimiento.Add(new SelectListItem { Text = "Acuerdo", Value = "0" });
            return ListaTiposSeguimiento;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTiposElementosRelacionadosParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTiposElementosRelacionadosParaSelect());
            return Tarea;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerListaActaParaSelectAsync(int IdSeguimiento) {
            var TareObtenerTodosElementosRelacionadosPorSeguimientoTipoElemento = ObtenerTodosPorIdSeguimientoTipoElementoAsync(IdSeguimiento, "Acta");
            IEnumerable<ElementoRelacionado> ListaElementosRelacionadosPorSeguimiento = await TareObtenerTodosElementosRelacionadosPorSeguimientoTipoElemento;

            var TareaObtenerTodosActas = Acta.ObtenerTodosAsync();
            IEnumerable<Acta> ListaTodosActas = await TareaObtenerTodosActas;

            List<SelectListItem> Lista = new List<SelectListItem>();
            foreach (Acta Acta in ListaTodosActas) {
                ElementoRelacionado ElementoRelacionadoParaSelect = ListaElementosRelacionadosPorSeguimiento.SingleOrDefault(ER => ER.IdActa == Acta.Id);
                if (ElementoRelacionadoParaSelect == null) Lista.Add(new SelectListItem { Text = string.Format("Acta de {0}  {1} - {2}", Acta.Sesion.TipoSesion.Descripcion, Acta.Sesion.Numero, Acta.Sesion.FechaHora.Year), Value = Acta.Id.ToString() });
            }
            return Lista;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerListaAcuerdoParaSelectAsync(int IdSeguimiento) {
            var TareObtenerTodosElementosRelacionadosPorSeguimientoTipoElemento = ObtenerTodosPorIdSeguimientoTipoElementoAsync(IdSeguimiento, "Acuerdo");
            IEnumerable<ElementoRelacionado> ListaElementosRelacionadosPorSeguimientoTipoElemento = await TareObtenerTodosElementosRelacionadosPorSeguimientoTipoElemento;

            var TareaObtenerTodosAcuerdo = Acuerdo.ObtenerTodosAsync();
            IEnumerable<Acuerdo> ListaTodosAcuerdos = await TareaObtenerTodosAcuerdo;

            List<SelectListItem> Lista = new List<SelectListItem>();
            foreach (Acuerdo Acuerdo in ListaTodosAcuerdos) {
                ElementoRelacionado ElementoRelacionadoParaSelect = ListaElementosRelacionadosPorSeguimientoTipoElemento.SingleOrDefault(ER => ER.IdAcuerdo == Acuerdo.Id);
                if (ElementoRelacionadoParaSelect == null) Lista.Add(new SelectListItem { Text = Acuerdo.Titulo, Value = Acuerdo.Id.ToString() });
            }
            return Lista;
        }

        public async Task<int> AgregarAsync(ElementoRelacionado ElementoRelacionadoModel) {
            // Variables de control
            int RegistrosAfectados = 0;

            // Guardar elemento relacionado en la base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ElementoRelacionadoBD = ElementoRelacionadoModel.BD();
                ElementoRelacionadoBD = ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.Add(ElementoRelacionadoBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id establecido en la base de datos al modelo
                ElementoRelacionadoModel.Id = ElementoRelacionadoBD.LLP_Id;
            }

            if (RegistrosAfectados > 0) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ElementoRelacionadoModel.ObtenerInformacion(), SeccionSistema: "Elemento Relacionado");
                return ElementoRelacionadoModel.Id;
            }
            else {
                return 0;
            }
        }

        public async Task<bool> EliminarAsync(int IdElementoRelacionado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerElementoRelacionado = ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.FindAsync(IdElementoRelacionado);
                var ElementoRelacionadoBD = await TareaObtenerElementoRelacionado;
                ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.Attach(ElementoRelacionadoBD);
                ContextoBD.SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS.Remove(ElementoRelacionadoBD);
                await ContextoBD.SaveChangesAsync();
                return true;
            }
        }
        #endregion
    }
}