using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class VotosDesidentesAcuerdosViewModel {
        // Constructores
        public VotosDesidentesAcuerdosViewModel() { }

        public VotosDesidentesAcuerdosViewModel(VotosDesidentesAcuerdosDTO ModeloVotos) {
            IdActa = ModeloVotos.IdActa;
            NombreUsuario = ModeloVotos.NombreUsuario;
            TipoVotacion = ModeloVotos.TipoVotacion;
        }
        // Propiedades
        public int IdActa { get; set; }

        public string NombreUsuario { get; set; }

        public string TipoVotacion { get; set; }
    }
}