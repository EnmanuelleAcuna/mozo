namespace SGJD_INA.Models.DTO {
    public class InformeAsistenciaSesionDirectoresDTO {
        public int Anno { get; set; }
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int CantidadSesiones { get; set; }
        public int Presente { get; set; }
        public int NoPresente { get; set; }
    }
}