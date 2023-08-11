using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AcuerdoArticuloViewModel {
        //Constructores
        public AcuerdoArticuloViewModel(Acuerdo ModeloAcuerdo) {
            IdAcuerdo = ModeloAcuerdo.Id;
            NumeroVersion = ModeloAcuerdo.NumeroVersion;
            Titulo = ModeloAcuerdo.Titulo;
            DetalleConsiderando = ModeloAcuerdo.DetalleConsiderando;
            DetallePortanto = ModeloAcuerdo.DetallePorTanto;
        }

        //Atributos
        public int IdAcuerdo { get; set; }

        [Display(Name = "Versión")]
        public int NumeroVersion { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Considerando")]
        public string DetalleConsiderando { get; set; }

        [Display(Name = "Por Tanto")]
        public string DetallePortanto { get; set; }
    }
}