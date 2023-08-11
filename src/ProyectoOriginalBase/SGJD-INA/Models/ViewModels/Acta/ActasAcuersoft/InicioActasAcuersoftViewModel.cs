using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioActasAcuersoftViewModel {
        // Constructor
        public InicioActasAcuersoftViewModel() { }

        public InicioActasAcuersoftViewModel(ActasAcuersoftDTO ModeloActasAcuersoft) {
            IdActaAcuersoft = ModeloActasAcuersoft.Id;
            NumeroActa = ModeloActasAcuersoft.NumeroActa;
            Fecha = ModeloActasAcuersoft.Fecha.ToString("dd/MM/yyyy");
            Lugar = ModeloActasAcuersoft.Lugar;
        }

        //Propiedades
        public int IdActaAcuersoft { get; set; }

        [Display(Name = "Número de acta")]
        public string NumeroActa { get; set; }

        [Display(Name = "Fecha")]
        public string Fecha { get; set; }

        [Display(Name = "Lugar")]
        public string Lugar { get; set; }
    }
}