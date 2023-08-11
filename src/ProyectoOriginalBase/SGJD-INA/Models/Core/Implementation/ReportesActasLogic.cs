using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    public class ReportesActasLogic : IReportesActasLogic {

        // Métodos públicos
        public IEnumerable<InformeGraficoActasDTO> ObtenerInformeGraficoActas() {
            IEnumerable<InformeGraficoActasDTO> Informe = new ActaDAO().ObtenerInformeGraficoActas();
            return Informe;
        }

        public Task<IEnumerable<InformeGraficoActasDTO>> ObtenerInformeGraficoActasAsync() {
            Task<IEnumerable<InformeGraficoActasDTO>> Tarea = Task.Run(() => ObtenerInformeGraficoActas());
            return Tarea;
        }

        private IEnumerable<InformeAsistenciaSesionDirectoresDTO> ObtenerInformeAsistenciaSesionDirectores(string Usuario, string Anno) {
            IEnumerable<InformeAsistenciaSesionDirectoresDTO> InformeGestionAsistencia = new ActaDAO().ObtenerInformeAsistenciaSesionDirectores(Usuario, Anno);
            return InformeGestionAsistencia;
        }

        public Task<IEnumerable<InformeAsistenciaSesionDirectoresDTO>> ObtenerInformeAsistenciaSesionDirectoresAsync(string Usuario, string Anno) {
            Task<IEnumerable<InformeAsistenciaSesionDirectoresDTO>> Tarea = Task.Run(() => ObtenerInformeAsistenciaSesionDirectores(Usuario, Anno));
            return Tarea;
        }

        private IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> ObtenerInformeAsistenciaSesionDirectoresDetallado(string Usuario, string Anno) {
            IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> InformeGestionAsistenciaDetallado = new ActaDAO().ObtenerInformeAsistenciaSesionDirectoresDetallado(Usuario, Anno);
            return InformeGestionAsistenciaDetallado;
        }

        public Task<IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO>> ObtenerInformeAsistenciaSesionDirectoresDetalladoAsync(string Usuario, string Anno) {
            Task<IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO>> Tarea = Task.Run(() => ObtenerInformeAsistenciaSesionDirectoresDetallado(Usuario, Anno));
            return Tarea;
        }

        private IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> ObtenerInformeVotacioAcuerdosDirectores(string Usuario, string Anno) {
            IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> InformeGestionVotaciones = new ActaDAO().ObtenerInformeVotacionesAcuerdosDirectores(Usuario, Anno);
            return InformeGestionVotaciones;
        }

        public Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDTO>> ObtenerInformeVotacioAcuerdosDirectoresAsync(string Usuario, string Anno) {
            Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDTO>> Tarea = Task.Run(() => ObtenerInformeVotacioAcuerdosDirectores(Usuario, Anno));
            return Tarea;
        }

        private IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> ObtenerInformeVotacioAcuerdosDirectoresDetallado(string Usuario, string Anno) {
            IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> InformeGestionVotaciones = new ActaDAO().ObtenerInformeVotacionesAcuerdosDirectoresDetallado(Usuario, Anno);
            return InformeGestionVotaciones;
        }

        public Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO>> ObtenerInformeVotacioAcuerdosDirectoresDetalladoAsync(string Usuario, string Anno) {
            Task<IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO>> Tarea = Task.Run(() => ObtenerInformeVotacioAcuerdosDirectoresDetallado(Usuario, Anno));
            return Tarea;
        }

        private IEnumerable<SelectListItem> ObtenerAnnoParaSelect() {
            List<SelectListItem> ListaAnnoAcuerdo = new List<SelectListItem>();
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "Todos", Value = "" });
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "2019", Value = "2019" });
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "2020", Value = "2020" });
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "2021", Value = "2021" });
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "2022", Value = "2022" });
            ListaAnnoAcuerdo.Add(new SelectListItem { Text = "2023", Value = "2023" });
            return ListaAnnoAcuerdo;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerAnnoParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerAnnoParaSelect());
            return Tarea;
        }
    }
}