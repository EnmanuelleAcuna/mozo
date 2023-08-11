using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarTipoObjetoViewModel {
        // Constructores
        public EliminarTipoObjetoViewModel() { }

        public EliminarTipoObjetoViewModel(TipoObjeto ModeloTipoObjeto) {
            Id = ModeloTipoObjeto.Id;
            Nombre = ModeloTipoObjeto.Descripcion;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}