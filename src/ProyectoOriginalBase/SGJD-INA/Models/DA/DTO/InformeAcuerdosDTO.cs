using System;

namespace SGJD_INA.Models.DTO {
    public class InformeAcuerdosDTO {
        public int IdAcuerdo { get; set; }
        public string Titulo { get; set; }
        public int NumeroVersion { get; set; }
        public string Asunto { get; set; }
        public string UrlAcuerdoFirmado { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string TipoSeguimiento { get; set; }
        public string Unidades { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}