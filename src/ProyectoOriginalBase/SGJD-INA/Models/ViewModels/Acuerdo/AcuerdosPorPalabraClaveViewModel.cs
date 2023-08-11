using SGJD_INA.Models.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AcuerdosPorPalabraClaveViewModel {
        // Constructores
        public AcuerdosPorPalabraClaveViewModel() { }

        public AcuerdosPorPalabraClaveViewModel(AcuerdosPorPalabraClaveDTO ModeloAcuerdo) {
            IdAcuerdo = ModeloAcuerdo.IdAcuerdo;
            NumeroAcuerdo = ModeloAcuerdo.NumeroAcuerdo;
            NumeroVersion = ModeloAcuerdo.NumeroVersion;
            Titulo = ModeloAcuerdo.Titulo;
            Asunto = ModeloAcuerdo.Asunto;
            Firme = ModeloAcuerdo.Firme;
            FechaFirmeza = ModeloAcuerdo.Firme == true ? Convert.ToDateTime(ModeloAcuerdo.FechaFirmeza).ToString("dd/MM/yyyy") : string.Empty;
            CodigoEstado = ModeloAcuerdo.CodigoEstado;
            NombreEstado = ModeloAcuerdo.NombreEstado;
        }

        // Propiedades
        public int IdAcuerdo { get; set; }

        [Display(Name = "Número de Acuerdo")]
        public int NumeroAcuerdo { get; set; }

        [Display(Name = "Versión")]
        public int NumeroVersion { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Firmeza del Acuerdo")]
        public bool Firme { get; set; }

        [Display(Name = "Fecha de firmeza")]
        public string FechaFirmeza { get; set; }
    }
}