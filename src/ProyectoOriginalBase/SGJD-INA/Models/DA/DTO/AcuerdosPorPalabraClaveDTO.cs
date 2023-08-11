using System;

namespace SGJD_INA.Models.DTO {
    public class AcuerdosPorPalabraClaveDTO {
        public int IdAcuerdo { get; set; }
        public int NumeroAcuerdo { get; set; }
        public int NumeroVersion { get; set; }
        public string Titulo { get; set; }
        public string Asunto { get; set; }
        public bool Firme { get; set; }
        public DateTime? FechaFirmeza { get; set; }
        public string CodigoEstado { get; set; }
        public string NombreEstado { get; set; }
    }
}