using Dapper;
using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SGJD_INA.Models.DA.DAO {
    public class AcuerdoDAO {
        private static readonly string SGJDConnectionString = SGJDDBContext.ConectionString();

        public IEnumerable<SesionConArticulosSinAcuerdoDTO> ObtenerSesionesConArticulosSinAcuerdo() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<SesionConArticulosSinAcuerdoDTO> Sesiones = Conexion.Query<SesionConArticulosSinAcuerdoDTO>("dbo.SGJD_ACT_PRO_OBTENER_SESIONES_PARA_ACUERDO").ToList();
                return Sesiones;
            }
        }

        public IEnumerable<ArticuloSinAcuerdoDTO> ObtenerArticulosSinAcuerdo(int IdSesion) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<ArticuloSinAcuerdoDTO> Articulos = Conexion.Query<ArticuloSinAcuerdoDTO>("dbo.SGJD_ACU_PRO_ARTICULOS_POR_SESION @IdSesion", new { IdSesion = IdSesion }).ToList();
                return Articulos;
            }
        }

        public IEnumerable<AcuerdoParaSeguimientoDTO> ObtenerAcuerdosParaSeguimientoSinAcuerdo() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<AcuerdoParaSeguimientoDTO> Acuerdos = Conexion.Query<AcuerdoParaSeguimientoDTO>("dbo.SGJD_ACT_PRO_ACUERDOS_PARA_SEGUIMIENTO").ToList();
                return Acuerdos;
            }
        }

        public IEnumerable<MiembrosPresentesPorIdAcuerdoDTO> ObtenerMiembrosPresentesPorIdAcuerdo(int IdAcuerdo) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<MiembrosPresentesPorIdAcuerdoDTO> Acuerdos = Conexion.Query<MiembrosPresentesPorIdAcuerdoDTO>("dbo.SGJD_ACT_PRO_OBTENER_MIEMBROS_PRESENTES_POR_IDACUERDO @IdAcuerdo", new { IdAcuerdo = IdAcuerdo }).ToList();
                return Acuerdos;
            }
        }

        public IEnumerable<InformeAcuerdosDTO> ObtenerInformeAcuerdos(string Estado, string TipoSeguimiento, DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeAcuerdosDTO> InformeAcuerdos = Conexion.Query<InformeAcuerdosDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_ACUERDOS @Estado, @TipoSeguimiento, @FechaInicio, @fechaFin", new { Estado = Estado, TipoSeguimiento = TipoSeguimiento, FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return InformeAcuerdos;
            }
        }

        public IEnumerable<InformeAcuerdosResumidoDTO> ObtenerInformeAcuerdosResumido(DateTime FechaInicio, DateTime FechaFin) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeAcuerdosResumidoDTO> InformeAcuerdosResumido = Conexion.Query<InformeAcuerdosResumidoDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_ACUERDOS_RESUMIDO @FechaInicio, @fechaFin", new { FechaInicio = FechaInicio, FechaFin = FechaFin }).ToList();
                return InformeAcuerdosResumido;
            }
        }

        public IEnumerable<InformeResumenEjecucionAcuerdosDTO> ObtenerInformeEjecucionAcuerdos() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeResumenEjecucionAcuerdosDTO> InformeResumenAcuerdos = Conexion.Query<InformeResumenEjecucionAcuerdosDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_RESUMEN_EJECUCION_ACUERDOS").ToList();

                return InformeResumenAcuerdos;
            }
        }

        public IEnumerable<InformeTotalAcuerdosDTO> ObtenerInformeTotalAcuerdos() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<InformeTotalAcuerdosDTO> InformeTotalAcuerdos = Conexion.Query<InformeTotalAcuerdosDTO>("dbo.SGJD_ACU_PRO_OBTENER_INFORME_TOTAL_ACUERDOS").ToList();

                return InformeTotalAcuerdos;
            }
        }

        public IEnumerable<AcuerdosPorPalabraClaveDTO> ObtenerAcuerdosPorPalabraClave(string PalabraClave) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<AcuerdosPorPalabraClaveDTO> Acuerdos = Conexion.Query<AcuerdosPorPalabraClaveDTO>("dbo.SGJD_ACU_PRO_FILTRO_PALABRA_CLAVE_ACUERDOS @Palabra", new { Palabra = PalabraClave }).ToList();
                return Acuerdos;
            }
        }

        public InformeGraficoAcuerdosDTO ObtenerInformeGraficoAcuerdos() {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                InformeGraficoAcuerdosDTO Acuerdos = Conexion.Query<InformeGraficoAcuerdosDTO>("dbo.SGJD_ACU_PRO_GRAFICO_ACUERDOS").FirstOrDefault();
                return Acuerdos;
            }
        }

        public IEnumerable<VotosDesidentesAcuerdosDTO> ObtenerVotosDesidentes(int IdActa) {
            using (IDbConnection Conexion = new SqlConnection(SGJDConnectionString)) {
                IEnumerable<VotosDesidentesAcuerdosDTO> Votos = Conexion.Query<VotosDesidentesAcuerdosDTO>("dbo.SGJD_ACU_PRO_OBTENER_VOTOS_DESIDENTES @IdActa", new { IdActa = IdActa }).ToList();
                return Votos;
            }
        }
    }
}