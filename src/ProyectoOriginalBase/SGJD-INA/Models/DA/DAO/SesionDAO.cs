using Dapper;
using SGJD_INA.Models.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class SesionDAO {
        private static readonly string SGJDConnectionString = SGJDDBContext.ConectionString();

        public InformeGraficoSesionesDTO ObtenerInformeGraficoSesiones() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                InformeGraficoSesionesDTO Sesiones = Conexion.Query<InformeGraficoSesionesDTO>("dbo.SGJD_ACT_PRO_GRAFICO_SESIONES").FirstOrDefault();
                return Sesiones;
            }
        }
    }
}