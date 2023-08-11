using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioUsuarioAvisoAcuerdoViewModel {
        //Constructores
        public InicioUsuarioAvisoAcuerdoViewModel() { }

        public InicioUsuarioAvisoAcuerdoViewModel(Estado ModeloEstado) {
            CodigoEstado = ModeloEstado.CodigoEstado;
            NombreEstado = ModeloEstado.Nombre;
            NombreAviso = ModeloEstado.Aviso.Nombre;
            IdUsuario = ModeloEstado.Aviso.UsuarioAvisoAcuerdo.Id;
            IdAviso = ModeloEstado.Aviso.Id;
        }

        //Atributos
        public int IdAviso { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Estado del Acuerdo")]
        public string NombreEstado { get; set; }

        [Display(Name = "Aviso ")]
        public string NombreAviso { get; set; }

        [Display(Name = "Usuario destinatario")]
        public string IdUsuario { get; set; }
    }
}