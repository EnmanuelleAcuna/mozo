using Dapper;
using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class RespaldoDAO {
        private static string SGJDConnectionString = SGJDDBContext.ConectionString();

        public IEnumerable<RespaldoPorFechaDTO> ObtenerRespaldoPorRangoFecha(DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<RespaldoPorFechaDTO> RespaldoPorRangoFecha = Conexion.Query<RespaldoPorFechaDTO>("dbo.SGJD_ADM_PRO_OBTENER_RANGO_FECHA_RESPALDO @FechaInicio, @FechaFin", new { FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return RespaldoPorRangoFecha;
            }
        }
    }
}