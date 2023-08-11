using Dapper;
using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class BitacoraDAO {
        private static string SGJDConnectionString = SGJDDBContext.ConectionString();

        public IEnumerable<BitacoraPorFechaDTO> ObtenerBitacoraPorRangoFecha(DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<BitacoraPorFechaDTO> BitacoraPorRangoFecha = Conexion.Query<BitacoraPorFechaDTO>("dbo.SGJD_ADM_PRO_OBTENER_RANGO_FECHA_BITACORA @FechaInicio, @FechaFin", new { FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return BitacoraPorRangoFecha;
            }
        }
    }
}