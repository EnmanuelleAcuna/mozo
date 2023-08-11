using Dapper;
using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class TipoObjetoDAO {
        private TipoObjetoDAO() { }

        private static string SGJDConnectionString = SGJDDBContext.ConectionString();

        public static List<TablaBDDTO> ObtenerTablasBD() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                List<TablaBDDTO> Tablas = Conexion.Query<TablaBDDTO>("dbo.SGJD_ADM_PRO_OBTENER_TABLAS_BD").ToList();
                return Tablas;
            }
        }
    }
}