using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class CapituloViewModel {
        public CapituloViewModel() { }

        public CapituloViewModel(Capitulo ModeloCapitulo) {
            IdCapitulo = ModeloCapitulo.Id;
            Titulo = ModeloCapitulo.Titulo;
            Articulos = ModeloCapitulo.Articulos.Select(Articulo => new ArticuloViewModel(Articulo)).ToList();
        }

        // propiedades
        public int IdCapitulo { get; set; }

        public string Titulo { get; set; }

        public IEnumerable<ArticuloViewModel> Articulos { get; set; }
    }
}