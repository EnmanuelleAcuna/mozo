using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarSeguimientoViewModel {
        public AgregarSeguimientoViewModel() { }

        public AgregarSeguimientoViewModel(AcuerdoParaSeguimientoDTO ModeloDTO) {
            IdAcuerdo = ModeloDTO.IdAcuerdo;
            Titulo = ModeloDTO.Titulo;
            Asunto = ModeloDTO.Asunto;
            FechaFirmeza = ModeloDTO.FechaFirmeza;
            EstadoAcuerdo = ModeloDTO.Estado;
        }

        [Required(ErrorMessage = "El acuerdo es requerido.")]
        public int IdAcuerdo { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public string Asunto { get; set; }

        [Display(Name = "Firmeza del Acuerdo")]
        public string FechaFirmeza { get; set; }

        [Display(Name = "Estado del Acuerdo")]
        public string EstadoAcuerdo { get; set; }
    }
}