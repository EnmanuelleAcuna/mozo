using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAvisoViewModel {
        #region Constructores
        public InicioAvisoViewModel(Aviso ModeloAviso) {
            IdAviso = ModeloAviso.Id;
            Nombre = ModeloAviso.Nombre;
            Tipo = ModeloAviso.Tipo;
            TipoDestinatario = ModeloAviso.TipoDestinatario;
        }
        #endregion

        #region Propiedades
        public int IdAviso { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo de aviso")]
        public TipoAviso Tipo { get; set; }

        [Display(Name = "Tipo de destinatario")]
        public TipoDestinatario TipoDestinatario { get; set; }
        #endregion
    }
}