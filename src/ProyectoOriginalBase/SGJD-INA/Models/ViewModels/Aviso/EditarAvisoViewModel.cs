using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class EditarAvisoViewModel {
        #region Constructor
        public EditarAvisoViewModel() { }

        public EditarAvisoViewModel(Aviso ModeloAviso) {
            IdAviso = ModeloAviso.Id;
            Nombre = ModeloAviso.Nombre;
            Mensaje = ModeloAviso.Mensaje;
            Tipo = ModeloAviso.Tipo;
            TipoDestinatario = ModeloAviso.TipoDestinatario;
            IdUnidad = ModeloAviso.IdUnidad;

            // Tomar el id de cada usuario que es destinatario del aviso y colocarlo en una cadena de texto
            if (ModeloAviso.TipoDestinatario is TipoDestinatario.Usuarios) {
                StringBuilder SB = new StringBuilder();
                foreach (var Usuario in ModeloAviso.Usuarios) {
                    SB.Append(Usuario.Id + "." + Usuario.Nombre).Append(',');
                }
                Usuarios = SB.ToString();
            }
        }
        #endregion

        #region Propiedades
        public int IdAviso { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "El estado es requerido")]
        [AllowHtml]
        public string Mensaje { get; set; }

        [Display(Name = "Tipo de aviso")]
        [Required(ErrorMessage = "El tipo de aviso es requerido.")]
        public TipoAviso Tipo { get; set; }

        [Display(Name = "Tipo de destinatario")]
        [Required(ErrorMessage = "El tipo destinatario es requerido.")]
        public TipoDestinatario TipoDestinatario { get; set; }

        [Display(Name = "Unidad")]
        public int? IdUnidad { get; set; }

        public string Usuarios { get; set; }
        #endregion

        #region Métodos
        public Aviso Entidad() {
            return new Aviso {
                Id = IdAviso,
                Nombre = Nombre,
                Mensaje = Mensaje,
                Tipo = Tipo,
                TipoDestinatario = TipoDestinatario,
                IdUnidad = IdUnidad,
                Usuarios = TipoDestinatario is TipoDestinatario.Usuarios ? Usuarios.Split(',').Select(u => new ApplicationUser(u)).ToList() : null
            };
        }
        #endregion
    }
}