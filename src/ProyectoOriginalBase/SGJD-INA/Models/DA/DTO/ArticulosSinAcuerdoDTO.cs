namespace SGJD_INA.Models.DTO {
    public class ArticuloSinAcuerdoDTO {
        public int IdCapitulo { get; set; }
        public string TituloCapitulo { get; set; }
        public int IdArticulo { get; set; }
        public string TituloArticulo { get; set; }
        public bool Confidencial { get; set; }
    }
}