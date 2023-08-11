using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarElementoRelacionadoViewModel {
        //Propiedades
        public int? IdActa { get; set; }

        public int? IdAcuerdo { get; set; }

        public int IdSeguimiento { get; set; }

        public string TipoElemento { get; set; }

        // Métodos
        public ElementoRelacionado Entidad() {
            return new ElementoRelacionado() {
                IdActa = IdActa,
                IdAcuerdo = IdAcuerdo,
                IdSeguimiento = IdSeguimiento,
                TipoElemento = TipoElemento
            };
        }
    }
}