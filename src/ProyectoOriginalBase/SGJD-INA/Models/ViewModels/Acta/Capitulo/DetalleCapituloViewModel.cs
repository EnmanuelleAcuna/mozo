using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleCapituloViewModel {
        //Contructores
        public DetalleCapituloViewModel() { }

        public DetalleCapituloViewModel(Capitulo ModeloCapitulo) {
            Titulo = ModeloCapitulo.Titulo;
            ListaArticulos = ModeloCapitulo.Articulos.Select(Articulo => new DetalleArticuloViewModel(Articulo)).ToList();
        }

        //Propiedades
        public string Titulo { get; set; }

        public IEnumerable<DetalleArticuloViewModel> ListaArticulos { get; set; }
    }
}