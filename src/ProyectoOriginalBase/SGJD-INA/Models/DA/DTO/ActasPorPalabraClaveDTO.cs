using System;

namespace SGJD_INA.Models.DTO {
    public class ActasPorPalabraClaveDTO {
        public int IdActa { get; set; }
        public int IdSesion { get; set; }
        public int NumeroSesion { get; set; }
        public DateTime FechaSesion { get; set; }
        public string TipoSesion { get; set; }
        public string CodigoEstado { get; set; }
        public string NombreEstado { get; set; }
        public int IdTomo { get; set; }
        public string NombreTomo { get; set; }
    }
}