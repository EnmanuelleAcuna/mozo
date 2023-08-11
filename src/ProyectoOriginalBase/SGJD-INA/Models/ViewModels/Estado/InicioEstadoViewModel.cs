using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioEstadoViewModel {
        //Constructor
        public InicioEstadoViewModel(Estado ModeloEstado) {
            IdEstado = ModeloEstado.Id;
            Nombre = ModeloEstado.Nombre;
            CodigoEstado = ModeloEstado.CodigoEstado;
            TipoObjeto = ModeloEstado.TipoObjeto.Descripcion;
            Aviso = ModeloEstado.Aviso.Nombre;
        }

        //Propiedades
        public int IdEstado { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Código")]
        public string CodigoEstado { get; set; }

        [Display(Name = "Tipo de Objeto")]
        public string TipoObjeto { get; set; }

        [Display(Name = "Aviso")]
        public string Aviso { get; set; }
    }
}