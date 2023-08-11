using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class ActasPorPalabraClaveViewModel {
        // Constructores
        public ActasPorPalabraClaveViewModel() { }

        public ActasPorPalabraClaveViewModel(ActasPorPalabraClaveDTO ModeloActa) {
            IdActa = ModeloActa.IdActa;
            IdSesion = ModeloActa.IdSesion;
            IdTomo = ModeloActa.IdTomo;
            NumeroSesion = ModeloActa.NumeroSesion;
            FechaSesion = ModeloActa.FechaSesion.ToString("dd/MM/yyyy");
            AnnoSesion = ModeloActa.FechaSesion.Year;
            TipoSesion = ModeloActa.TipoSesion;
            NombreTomo = ModeloActa.NombreTomo;
            CodigoEstado = ModeloActa.CodigoEstado;
            NombreEstado = ModeloActa.NombreEstado;
        }

        // Propiedades
        public int IdActa { get; set; }

        public int IdSesion { get; set; }

        public int IdTomo { get; set; }

        [Display(Name = "Número de Sesión")]
        public int NumeroSesion { get; set; }

        [Display(Name = "Fecha de Sesión")]
        public string FechaSesion { get; set; }

        public int AnnoSesion { get; set; }

        [Display(Name = "Tipo de Sesión")]
        public string TipoSesion { get; set; }

        [Display(Name = "Tomo")]
        public string NombreTomo { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }
    }
}