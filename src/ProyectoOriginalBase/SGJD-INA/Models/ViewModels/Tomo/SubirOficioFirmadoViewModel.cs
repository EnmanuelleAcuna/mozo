using SGJD_INA.Models.Entities;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class SubirOficioFirmadoViewModel {
        // Propiedades
        public int IdTomo { get; set; }

        public HttpPostedFileBase Archivo { get; set; }

        public string NombreObjeto { get; set; }

        // Método
        public Tomo Entidad() {
            return new Tomo() {
                Id = IdTomo,
                NombreObjeto = NombreObjeto
            };
        }
    }
}