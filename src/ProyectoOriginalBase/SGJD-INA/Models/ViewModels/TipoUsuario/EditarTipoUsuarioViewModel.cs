using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarTipoUsuarioViewModel {
        #region Constructores
        public EditarTipoUsuarioViewModel() { }

        public EditarTipoUsuarioViewModel(TipoUsuario ModeloTipoUsuario) {
            IdTipoUsuario = ModeloTipoUsuario.Id;
            Nombre = ModeloTipoUsuario.Nombre;
        }
        #endregion

        #region Propiedades
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Tipo de usuario")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre del tipo de usuario no puede ser mayor a {0} caracteres.")]
        public string Nombre { get; set; }
        #endregion

        #region Métodos
        public TipoUsuario Entidad() {
            return new TipoUsuario {
                Id = IdTipoUsuario,
                Nombre = Nombre
            };
        }
        #endregion
    }
}