using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class ArticuloViewModel {
        public ArticuloViewModel() { }

        public ArticuloViewModel(Articulo ModeloArticulo) {
            IdArticulo = ModeloArticulo.Id;
            IdCapitulo = ModeloArticulo.IdCapitulo;
            Titulo = ModeloArticulo.Titulo;
            ResumenTema = ModeloArticulo.IdTema != null ? ModeloArticulo.Tema.Resumen : "Artículo sin tema en Orden del Día";
            Contenido = ModeloArticulo.Contenido;
            Observacion = ModeloArticulo.Observacion;
            Confidencialidad = ModeloArticulo.Confidencial;
            UsuariosArticulo = ModeloArticulo.UsuariosArticulo.Select(ua => new UsuarioArticuloViewModel(ua)).ToList();

            AcuerdoArticulo = new AcuerdoArticuloViewModel(ModeloArticulo.Acuerdo);
        }

        // Propiedades
        public int IdArticulo { get; set; }

        public int IdCapitulo { get; set; }

        [Display(Name = "Artículo")]
        public string Titulo { get; set; }

        [Display(Name = "Resumen de tema")]
        public string ResumenTema { get; set; }

        [Display(Name = "Transcripción")]
        public string Contenido { get; set; }

        [Display(Name = "Confidencial")]
        public bool Confidencialidad { get; set; }

        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }

        public IEnumerable<UsuarioArticuloViewModel> UsuariosArticulo { get; set; }

        public AcuerdoArticuloViewModel AcuerdoArticulo { get; set; }
    }
}