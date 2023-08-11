using System;

namespace SGJD_INA.Models.DTO {
    public class RespaldoPorFechaDTO {
        public int Id { get; set; }
        public string NombreRespaldo { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public string UrlArchivoRepositorio { get; set; }
    }
}