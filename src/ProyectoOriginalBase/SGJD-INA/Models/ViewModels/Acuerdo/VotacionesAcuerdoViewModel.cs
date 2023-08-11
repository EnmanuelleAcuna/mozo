using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class VotacionesAcuerdoViewModel {
        public VotacionesAcuerdoViewModel() { }

        public VotacionesAcuerdoViewModel(Votacion Votacion) {
            IdVoto = Votacion.Id;
            IdAsistente = Votacion.IdAsistente;
            TipoVotacion = Votacion.TipoVotacion;
            Nombre = Votacion.Asistente.Usuario.Nombre;
        }

        public int IdVoto { get; set; }

        public int IdAsistente { get; set; }

        [Display(Name = "Tipo de votación")]
        public string TipoVotacion { get; set; }

        [Display(Name = "Asistente")]
        public string Nombre { get; set; }
    }
}