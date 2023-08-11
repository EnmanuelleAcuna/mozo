using System;

namespace SGJD_INA.Models.DTO {
    public class ActasAcuersoftPorPalablaClaveDTO {
        public int IdActa { get; set; }
        public string NumeroActa { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string Preside { get; set; }
        public string Secretario { get; set; }
        public string Detalle { get; set; }
    }
}