using System;

namespace SGJD_INA.Models.DTO {
    public class InformeSeguimientosSinFechaVencimientoDTO {
        public int IdSeguimiento { get; set; }
        public int IdAcuerdo { get; set; }
        public string Titulo { get; set; }
        public int NumeroVersion { get; set; }
        public string UrlAcuerdoFirmado { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int PlazoDias { get; set; }
        public int PorcentajeAvance { get; set; }
    }
}