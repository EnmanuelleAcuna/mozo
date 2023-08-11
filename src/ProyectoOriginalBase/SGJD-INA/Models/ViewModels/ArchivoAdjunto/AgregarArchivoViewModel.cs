using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarArchivoViewModel {
        // Propiedades
        public int IdObjeto { get; set; }

        [Display(Name = "Detalle del archivo")]
        public string Detalle { get; set; }

        public HttpPostedFileBase Archivo { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public ArchivoAdjunto Entidad() {
            return new ArchivoAdjunto() {
                IdObjeto = IdObjeto,
                Observaciones = Detalle,
                NombreObjeto = NombreObjeto
            };
        }
    }
}