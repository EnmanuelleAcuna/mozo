using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InformeGraficoSesionesViewModel {
        // Constructores
        public InformeGraficoSesionesViewModel(InformeGraficoSesionesDTO Modelo) {
            this.Ordinarias = Modelo.Ordinarias;
            this.Extraordinarias = Modelo.Extraordinarias;
        }

        // Propiedades
        [Display(Name = "Ordinarias")]
        public int Ordinarias { get; set; }

        [Display(Name = "Extraordinarias")]
        public int Extraordinarias { get; set; }
    }
}