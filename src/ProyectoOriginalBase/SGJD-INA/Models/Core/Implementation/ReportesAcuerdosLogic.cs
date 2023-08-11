using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class ReportesAcuerdosLogic : IReportesAcuerdosLogic {

        // Métodos públicos
        public IEnumerable<InformeAcuerdosDTO> ObtenerInformeAcuerdos(string Estado, string TipoSeguimiento, string FechaInicio, string FechaFinal) {
            DateTime _FechaInicio = Funciones.StringToDateTime(FechaInicio);
            DateTime _FechaFinal = Funciones.StringToDateTime(FechaFinal);
            IEnumerable<InformeAcuerdosDTO> InformeAcuerdo = new AcuerdoDAO().ObtenerInformeAcuerdos(Estado, TipoSeguimiento, _FechaInicio, _FechaFinal);
            return InformeAcuerdo;
        }

        public Task<IEnumerable<InformeAcuerdosDTO>> ObtenerInformeAcuerdosAsync(string Estado, string TipoSeguimiento, string FechaInicio, string FechaFin) {
            Task<IEnumerable<InformeAcuerdosDTO>> Tarea = Task.Run(() => ObtenerInformeAcuerdos(Estado, TipoSeguimiento, FechaInicio, FechaFin));
            return Tarea;
        }

        public IEnumerable<InformeAcuerdosResumidoDTO> ObtenerInformeAcuerdosResumido(string FechaInicio, string FechaFinal) {
            DateTime _FechaInicio = Funciones.StringToDateTime(FechaInicio);
            DateTime _FechaFinal = Funciones.StringToDateTime(FechaFinal);
            IEnumerable<InformeAcuerdosResumidoDTO> InformeAcuerdosResumido = new AcuerdoDAO().ObtenerInformeAcuerdosResumido(_FechaInicio, _FechaFinal);
            return InformeAcuerdosResumido;
        }

        public Task<IEnumerable<InformeAcuerdosResumidoDTO>> ObtenerInformeAcuerdosResumidoAsync(string FechaInicio, string FechaFin) {
            Task<IEnumerable<InformeAcuerdosResumidoDTO>> Tarea = Task.Run(() => ObtenerInformeAcuerdosResumido(FechaInicio, FechaFin));
            return Tarea;
        }

        public IEnumerable<InformeResumenEjecucionAcuerdosDTO> ObtenerInformeEjecucionAcuerdos() {
            IEnumerable<InformeResumenEjecucionAcuerdosDTO> InformeEjecucionAcuerdo = new AcuerdoDAO().ObtenerInformeEjecucionAcuerdos();
            return InformeEjecucionAcuerdo;
        }

        public Task<IEnumerable<InformeResumenEjecucionAcuerdosDTO>> ObtenerInformeEjecucionAcuerdosAsync() {
            Task<IEnumerable<InformeResumenEjecucionAcuerdosDTO>> Tarea = Task.Run(() => ObtenerInformeEjecucionAcuerdos());
            return Tarea;
        }

        public IEnumerable<InformeTotalAcuerdosDTO> ObtenerInformeTotalAcuerdos() {
            IEnumerable<InformeTotalAcuerdosDTO> InformeTotalAcuerdo = new AcuerdoDAO().ObtenerInformeTotalAcuerdos();
            return InformeTotalAcuerdo;
        }

        public Task<IEnumerable<InformeTotalAcuerdosDTO>> ObtenerInformeTotalAcuerdosAsync() {
            Task<IEnumerable<InformeTotalAcuerdosDTO>> Tarea = Task.Run(() => ObtenerInformeTotalAcuerdos());
            return Tarea;
        }

        public IEnumerable<InformeSeguimientosDTO> ObtenerInformeSeguimientos(string Estado, string FechaInicio, string FechaFinal) {
            DateTime _FechaInicio = Funciones.StringToDateTime(FechaInicio);
            DateTime _FechaFinal = Funciones.StringToDateTime(FechaFinal);
            IEnumerable<InformeSeguimientosDTO> InformeSeguimiento = new SeguimientosDAO().ObtenerInformeSeguimientos(Estado, _FechaInicio, _FechaFinal);
            return InformeSeguimiento;
        }

        public Task<IEnumerable<InformeSeguimientosDTO>> ObtenerInformeSeguimientosAsync(string Estado, string FechaInicio, string FechaFin) {
            Task<IEnumerable<InformeSeguimientosDTO>> Tarea = Task.Run(() => ObtenerInformeSeguimientos(Estado, FechaInicio, FechaFin));
            return Tarea;
        }

        public IEnumerable<InformeSeguimientosResumidoDTO> ObtenerInformeSeguimientosResumido(string FechaInicio, string FechaFinal) {
            DateTime _FechaInicio = Funciones.StringToDateTime(FechaInicio);
            DateTime _FechaFinal = Funciones.StringToDateTime(FechaFinal);
            IEnumerable<InformeSeguimientosResumidoDTO> InformeSeguimientoResumido = new SeguimientosDAO().ObtenerInformeSeguimientosResumido(_FechaInicio, _FechaFinal);
            return InformeSeguimientoResumido;
        }

        public Task<IEnumerable<InformeSeguimientosResumidoDTO>> ObtenerInformeSeguimientosResumidoAsync(string FechaInicio, string FechaFin) {
            Task<IEnumerable<InformeSeguimientosResumidoDTO>> Tarea = Task.Run(() => ObtenerInformeSeguimientosResumido(FechaInicio, FechaFin));
            return Tarea;
        }

        public IEnumerable<InformeSeguimientosVencidosDTO> ObtenerInformeSeguimientosVencidos() {
            IEnumerable<InformeSeguimientosVencidosDTO> InformeSeguimientoVencidos = new SeguimientosDAO().ObtenerInformeSeguimientosVencidos();
            return InformeSeguimientoVencidos;
        }

        public Task<IEnumerable<InformeSeguimientosVencidosDTO>> ObtenerInformeSeguimientosVencidosAsync() {
            Task<IEnumerable<InformeSeguimientosVencidosDTO>> Tarea = Task.Run(() => ObtenerInformeSeguimientosVencidos());
            return Tarea;
        }

        public IEnumerable<InformeSeguimientosSinFechaVencimientoDTO> ObtenerInformeSeguimientosSinFechaVencimiento() {
            IEnumerable<InformeSeguimientosSinFechaVencimientoDTO> InformeSeguimientoSinFecha = new SeguimientosDAO().ObtenerInformeSeguimientosSinFechaVencimiento();
            return InformeSeguimientoSinFecha;
        }

        public Task<IEnumerable<InformeSeguimientosSinFechaVencimientoDTO>> ObtenerInformeSeguimientosSinFechaVencimientoAsync() {
            Task<IEnumerable<InformeSeguimientosSinFechaVencimientoDTO>> Tarea = Task.Run(() => ObtenerInformeSeguimientosSinFechaVencimiento());
            return Tarea;
        }

        public InformeGraficoAcuerdosDTO ObtenerInformeGraficoAcuerdos() {
            InformeGraficoAcuerdosDTO Informe = new AcuerdoDAO().ObtenerInformeGraficoAcuerdos();
            return Informe;
        }

        public Task<InformeGraficoAcuerdosDTO> ObtenerInformeGraficoAcuerdosAsync() {
            Task<InformeGraficoAcuerdosDTO> Tarea = Task.Run(() => ObtenerInformeGraficoAcuerdos());
            return Tarea;
        }
    }
}