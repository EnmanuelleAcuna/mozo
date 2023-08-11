using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class CorreoUsuarioViewModel {
        #region Constructores
        public CorreoUsuarioViewModel(UsuarioCorreo ModeloCorreo) {
            IdCorreo = ModeloCorreo.Id;
            Correo = ModeloCorreo.Correo;
        }
        #endregion

        #region Atributos
        public int IdCorreo { get; set; }

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
        #endregion
    }
}