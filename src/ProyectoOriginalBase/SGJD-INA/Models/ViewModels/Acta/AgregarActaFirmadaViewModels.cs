using SGJD_INA.Models.Entities;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarActaFirmadaViewModels {
        // Propiedades
        public int IdActa { get; set; }

        public HttpPostedFileBase Archivo { get; set; }

        public string NombreObjeto { get; set; }

        // Método
        public Acta Entidad() {
            return new Acta() {
                Id = IdActa,
                NombreObjeto = NombreObjeto
            };
        }
    }
}