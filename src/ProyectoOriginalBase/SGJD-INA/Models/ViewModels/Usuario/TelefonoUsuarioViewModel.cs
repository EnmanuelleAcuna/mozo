using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class TelefonoUsuarioViewModel {
        #region Constructores
        public TelefonoUsuarioViewModel(UsuarioTelefono ModeloTelefono) {
            IdTelefono = ModeloTelefono.Id;
            Numero = ModeloTelefono.Numero;
            Extension = ModeloTelefono.Extension;
        }
        #endregion

        #region Atributos
        public int IdTelefono { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Extensión")]
        public string Extension { get; set; }
        #endregion
    }
}