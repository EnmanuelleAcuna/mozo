using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarHoraViewModel {
        #region Constructores
        public EditarHoraViewModel() { }

        public EditarHoraViewModel(AsistenteSesion ModeloAsistente) {
            IdAsistente = ModeloAsistente.Id;
            NombreUsuario = ModeloAsistente.Usuario.Nombre;
            HoraInicio = ModeloAsistente.HoraInicio;
            HoraFin = ModeloAsistente.HoraFin;
        }
        #endregion

        #region Atributos
        [Required(ErrorMessage = "El id del asistente es requerido.")]
        public int IdAsistente { get; set; }

        public string NombreUsuario { get; set; }

        [Display(Name = "Hora de ingreso")]
        [Required(ErrorMessage = "La hora de ingreso es requerida")]
        public string HoraInicio { get; set; }

        [Display(Name = "Hora de salida")]
        public string HoraFin { get; set; }
        #endregion

        #region Métodos
        public AsistenteSesion Entidad() {
            return new AsistenteSesion {
                Id = IdAsistente,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin
            };
        }
        #endregion
    }
}