using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InformeGraficoActasViewModel {
        // Constructores
        public InformeGraficoActasViewModel() { }

        public InformeGraficoActasViewModel(InformeGraficoActasDTO Modelo) {
            this.Anno = Modelo.Anno;
            this.Cantidad = Modelo.Cantidad;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Anno { get; set; }

        [Display(Name = "Cantidad de Actas")]
        public int Cantidad { get; set; }
    }
}