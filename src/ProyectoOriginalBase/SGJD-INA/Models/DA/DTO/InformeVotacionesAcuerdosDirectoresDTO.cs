namespace SGJD_INA.Models.DTO {
    public class InformeVotacionesAcuerdosDirectoresDTO {
        public int Fecha { get; set; }
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int CantidadAcuerdos { get; set; }
        public int AFavor { get; set; }
        public int EnContra { get; set; }
        public int NoVoto { get; set; }
    }
}