namespace SGJD_INA.Models.DTO {
    public class InformeSeguimientosResumidoDTO {
        public int TotalSeguimiento { get; set; }
        public int SeguimientoNoEjecutado { get; set; }
        public int SeguimientoEnEjecucion { get; set; }
        public int SeguimientoEjecutado { get; set; }
        public int SeguimientoVencido { get; set; }
    }
}