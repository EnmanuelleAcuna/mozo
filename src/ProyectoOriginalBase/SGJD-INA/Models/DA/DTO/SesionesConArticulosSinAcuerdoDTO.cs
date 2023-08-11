using System;

namespace SGJD_INA.Models.DTO {
    public class SesionConArticulosSinAcuerdoDTO {
        public int IdSesion { get; set; }
        public string NombreSesion { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoEstadoActa { get; set; }
    }
}