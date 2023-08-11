using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarTipoArchivoViewModel {
        // Constructores
        public EliminarTipoArchivoViewModel() { }

        public EliminarTipoArchivoViewModel(TipoArchivo ModeloTipoArchivo) {
            Id = ModeloTipoArchivo.Id;
            Nombre = string.Format("{0} ({1})", ModeloTipoArchivo.Descripcion, ModeloTipoArchivo.Nombre);
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}