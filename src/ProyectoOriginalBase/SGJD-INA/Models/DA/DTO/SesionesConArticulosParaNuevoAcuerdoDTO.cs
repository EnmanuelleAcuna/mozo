using System.Collections.Generic;

namespace SGJD_INA.Models.DTO {
    public class SesionConArticulosParaNuevoAcuerdoDTO {
        public SesionConArticulosSinAcuerdoDTO Sesion { get; set; }
        public IEnumerable<ArticuloSinAcuerdoDTO> Articulos { get; set; }
    }
}