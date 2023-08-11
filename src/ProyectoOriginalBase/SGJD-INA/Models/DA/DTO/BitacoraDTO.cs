using System;

namespace SGJD_INA.Models.DTO {
    public class BitacoraPorFechaDTO {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; }
        public string SeccionSistema { get; set; }
    }
}