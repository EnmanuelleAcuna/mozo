using SGJD_INA.Models.Entities;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarAcuerdoFirmadoViewModel {
        // Propiedades
        public int IdAcuerdo { get; set; }

        public HttpPostedFileBase Archivo { get; set; }

        public string NombreObjeto { get; set; }
    }
}