using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarAvisoViewModel {
        #region Propiedades
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Mensaje")]
        [StringLength(4000, ErrorMessage = "El mensaje no puede ser mayor a {1} caracteres.")]
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