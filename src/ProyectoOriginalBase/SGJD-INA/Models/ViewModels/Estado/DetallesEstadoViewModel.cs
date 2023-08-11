using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetallesEstadoViewModel {
        // Constructores
        public DetallesEstadoViewModel() { }

        public DetallesEstadoViewModel(Estado ModeloEstado) {
            Nombre = ModeloEstado.Nombre;
            Descripcion = ModeloEstado.Descripcion;
            CodigoEstado = ModeloEstado.CodigoEstado;
            NombreTipoObjeto = ModeloEstado.TipoObjeto.Descripcion;
            NombreAviso = ModeloEstado.Aviso.Nombre;
        }

        // Propiedades
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Código de estado")]
        public string CodigoEstado { get; set; }

        [Display(Name = "TipoO de Objeto")]
        public string NombreTipoObjeto { get; set; }

        [Display(Name = "Aviso")]
        public string NombreAviso { get; set; }
    }
}