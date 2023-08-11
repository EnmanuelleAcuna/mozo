using System;

namespace SGJD_INA.Models.DTO {
    public class InformeAsistenciaSesionDirectoresDetalladoDTO {
        public int Anno { get; set; }
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Descripcion { get; set; }
        public int IdSesion { get; set; }
        public int NumeroSesion { get; set; }
        public DateTime FechaHora { get; set; }
        public string Asistencia { get; set; }
    }
}