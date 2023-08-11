using Dapper;
using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class SeguimientosDAO {
        private static string SGJDConnectionString = SGJDDBContext.ConectionString();

        public IEnumerable<InformeSeguimientosDTO> ObtenerInformeSeguimientos(string Estado, DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeSeguimientosDTO> InformeSeguimientos = Conexion.Query<InformeSeguimientosDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS @Estado, @FechaInicio, @fechaFin", new { Estado = Estado, FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return InformeSeguimientos;
            }
        }

        public IEnumerable<InformeSeguimientosResumidoDTO> ObtenerInformeSeguimientosResumido(DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeSeguimientosResumidoDTO> InformeSeguimientosResumido = Conexion.Query<InformeSeguimientosResumidoDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS_RESUMIDO @FechaInicio, @fechaFin", new { FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return InformeSeguimientosResumido;
            }
        }

        public IEnumerable<InformeSeguimientosVencidosDTO> ObtenerInformeSeguimientosVencidos() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeSeguimientosVencidosDTO> InformeSeguimientosVencido = Conexion.Query<InformeSeguimientosVencidosDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS_VENCIDOS").ToList();
                return InformeSeguimientosVencido;
            }
        }

        public IEnumerable<InformeSeguimientosSinFechaVencimientoDTO> ObtenerInformeSeguimientosSinFechaVencimiento() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeSeguimientosSinFechaVencimientoDTO> InformeSeguimientosSinFechaVencimiento = Conexion.Query<InformeSeguimientosSinFechaVencimientoDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_SEGUIMIENTOS_SIN_FECHA_VENCIMIENTO").ToList();
                return InformeSeguimientosSinFechaVencimiento;
            }
        }

        public IEnumerable<UnidadesEjecucionSeguimientoDTO> ObtenerSeguimientosSegunIdUnidad(int IdUnidad) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<UnidadesEjecucionSeguimientoDTO> InformeSeguimientosSinFechaVencimiento = Conexion.Query<UnidadesEjecucionSeguimientoDTO>("dbo.SGJD_ACT_PRO_SEGUIMIENTO_POR_IDUNIDAD @IdUnidad", new { IdUnidad = IdUnidad }).ToList();
                return InformeSeguimientosSinFechaVencimiento;
            }
        }
    }
}
