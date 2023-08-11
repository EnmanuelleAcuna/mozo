using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class ArticuloParaNuevoAcuerdoViewModel {
        public ArticuloParaNuevoAcuerdoViewModel() { }

        public ArticuloParaNuevoAcuerdoViewModel(ArticuloSinAcuerdoDTO Articulo) {
            IdCapitulo = Articulo.IdCapitulo;
            TituloCapitulo = Articulo.TituloCapitulo;
            IdArticulo = Articulo.IdArticulo;
            TituloArticulo = Articulo.TituloArticulo;
            Confidencial = Articulo.Confidencial ? "Confidencial" : "No confidencial";
        }

        public int IdCapitulo { get; set; }

        [Display(Name = "Capítulo")]
        public string TituloCapitulo { get; set; }

        public int IdArticulo { get; set; }

        [Display(Name = "Artículo")]
        public string TituloArticulo { get; set; }

        [Display(Name = "Confidencialidad")]
        public string Confidencial { get; set; } = "No confidencial";
    }
}