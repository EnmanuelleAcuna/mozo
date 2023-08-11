using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetallesAvisoViewModel {
        // Constructores
        public DetallesAvisoViewModel() { }

        public DetallesAvisoViewModel(Aviso ModeloAviso) {
            Nombre = ModeloAviso.Nombre;
            Mensaje = ModeloAviso.Mensaje;
            Tipo = ModeloAviso.Tipo;
            TipoDestinatario = ModeloAviso.TipoDestinatario;
            NombreUnidad = ModeloAviso.IdUnidad != null ? ModeloAviso.Unidad.Nombre : string.Empty;
        }

        // Propiedades
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Mensaje")]
        public string Mensaje { get; set; }

        [Display(Name = "Tipo de aviso")]
        public TipoAviso Tipo { get; set; }

        [Display(Name = "Tipo de destinatario")]
        public TipoDestinatario TipoDestinatario { get; set; }

        [Display(Name = "Unidad")]
        public string NombreUnidad { get; set; }
    }
}