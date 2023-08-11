using Dapper;
using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class ActaDAO {
        private static readonly string SGJDConnectionString = SGJDDBContext.ConectionString();

        public IEnumerable<ActasPorPalabraClaveDTO> ObtenerActasPorPalabraClave(string Palabra) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ActasPorPalabraClaveDTO> Actas = Conexion.Query<ActasPorPalabraClaveDTO>("dbo.SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE_ACTAS @Palabra", new { Palabra = Palabra }).ToList();
                return Actas;
            }
        }

        public IEnumerable<InformeGraficoActasDTO> ObtenerInformeGraficoActas() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeGraficoActasDTO> Actas = Conexion.Query<InformeGraficoActasDTO>("dbo.SGJD_ACT_PRO_GRAFICO_ACTAS").ToList();
                return Actas;
            }
        }

        public IEnumerable<ActasAcuersoftDTO> ObtenerActasAcuersoft() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ActasAcuersoftDTO> Actas = Conexion.Query<ActasAcuersoftDTO>("dbo.SGJD_ACT_PRO_ACTAS_ACUERSOFT").ToList();
                return Actas;
            }
        }

        public IEnumerable<ActasDetalleAcuersoftDTO> ObtenerDetalleActasAcuersoft(int IdActa) {
            using (IDbConnection conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ActasDetalleAcuersoftDTO> ActasDetalle = conexion.Query<ActasDetalleAcuersoftDTO>("dbo.SGJD_ACT_PRO_ACTAS_DETALLE_ACUERSOFT @IdActa", new { IdActa = IdActa }).ToList();
                return ActasDetalle;
            }
        }

        public IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO> ObtenerRegistrosActasAcuersoftPorPalabraClave(string Palabra) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO> Actas = Conexion.Query<ContarRegistrosActasAcuersoftPorPalabraDTO>("dbo.SGJD_ACT_PRO_CONTAR_REGISTROS_PALABRA_CLAVE_ACTAS_ACUERSOFT @Palabra", new { Palabra = Palabra }).ToList();
                return Actas;
            }
        }

        public IEnumerable<ActasAcuersoftPorPalablaClaveDTO> ObtenerActasAcuersoftPorPalabraClave(string Palabra, int Pagina) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ActasAcuersoftPorPalablaClaveDTO> Actas = Conexion.Query<ActasAcuersoftPorPalablaClaveDTO>("dbo.SGJD_ACT_PRO_FILTRO_PALABRA_CLAVE_ACTAS_ACUERSOFT @Palabra, @Pagina", new { Palabra = Palabra, Pagina = Pagina }).ToList();
                return Actas;
            }
        }

        public IEnumerable<InformeAsistenciaSesionDirectoresDTO> ObtenerInformeAsistenciaSesionDirectores(string Usuario, string Anno) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeAsistenciaSesionDirectoresDTO> InformeGestion = Conexion.Query<InformeAsistenciaSesionDirectoresDTO>("dbo.SGJD_ACT_PRO_OBTENER_INFORME_ASISTENCIA_SESION_DIRECTORES @Usuario, @Anno", new { Usuario = Usuario, Anno = Anno }).ToList();
                return InformeGestion;
            }
        }

        public IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> ObtenerInformeAsistenciaSesionDirectoresDetallado(string Usuario, string Anno) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeAsistenciaSesionDirectoresDetalladoDTO> InformeGestionDetallado = Conexion.Query<InformeAsistenciaSesionDirectoresDetalladoDTO>("dbo.SGJD_ACT_PRO_OBTENER_INFORME_ASISTENCIA_SESION_DIRECTORES_DETALLADO @Usuario, @Anno", new { Usuario = Usuario, Anno = Anno }).ToList();
                return InformeGestionDetallado;
            }
        }

        public IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> ObtenerInformeVotacionesAcuerdosDirectores(string Usuario, string Anno) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeVotacionesAcuerdosDirectoresDTO> InformeGestion = Conexion.Query<InformeVotacionesAcuerdosDirectoresDTO>("dbo.SGJD_ACT_PRO_OBTENER_INFORME_VOTACIONES_ACUERDO_DIRECTORES @Usuario, @Anno", new { Usuario = Usuario, Anno = Anno }).ToList();
                return InformeGestion;
            }
        }

        public IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> ObtenerInformeVotacionesAcuerdosDirectoresDetallado(string Usuario, string Anno) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeVotacionesAcuerdosDirectoresDetalladoDTO> InformeGestionDetallado = Conexion.Query<InformeVotacionesAcuerdosDirectoresDetalladoDTO>("dbo.SGJD_ACT_PRO_OBTENER_INFORME_VOTACIONES_ACUERDO_DIRECTORES_DETALLADO @Usuario, @Anno", new { Usuario = Usuario, Anno = Anno }).ToList();
                return InformeGestionDetallado;
            }
        }
    }
}