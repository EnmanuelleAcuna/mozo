using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleActaAcuersoftViewModel {
        //Contructores
        public DetalleActaAcuersoftViewModel(ActasDetalleAcuersoftDTO ModeloDetalle) {
            IdActa = ModeloDetalle.IdActa;
            IdDetalle = ModeloDetalle.IdDetalle;
            Indice = ModeloDetalle.Indice;
            Detalle = ModeloDetalle.Texto;
        }

        // Propiedades
        public int IdActa { get; set; }

        public int IdDetalle { get; set; }

        [Display(Name = "Índice")]
        public int Indice { get; set; }

        [Display(Name = "Detalle")]
        public string Detalle { get; set; }
    }
}