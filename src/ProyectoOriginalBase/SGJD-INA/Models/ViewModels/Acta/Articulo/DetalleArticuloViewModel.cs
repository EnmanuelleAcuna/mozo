using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleArticuloViewModel {
        //Constructories
        public DetalleArticuloViewModel() { }

        public DetalleArticuloViewModel(Articulo ModeloArticulo) {
            IdArticulo = ModeloArticulo.Id;
            Titulo = ModeloArticulo.Titulo;
            ResumenTema = ModeloArticulo.IdTema != null ? ModeloArticulo.Tema.Resumen : "Artículo sin tema en Orden del Día";
            Contenido = ModeloArticulo.Contenido;
            Confidencial = ModeloArticulo.Confidencial;

            AcuerdoArticulo = new AcuerdoArticuloViewModel(ModeloArticulo.Acuerdo);
        }

        //Propiedades
        public int IdArticulo { get; set; }

        public string Titulo { get; set; }

        public string ResumenTema { get; set; }

        public string Contenido { get; set; }

        public bool Confidencial { get; set; }

        public AcuerdoArticuloViewModel AcuerdoArticulo { get; set; }
    }
}