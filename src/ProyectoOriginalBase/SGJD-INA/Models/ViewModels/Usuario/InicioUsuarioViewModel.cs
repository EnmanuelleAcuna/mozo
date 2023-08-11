using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioUsuarioViewModel {
        #region Constructor
        public InicioUsuarioViewModel(ApplicationUser ModeloUsuario) {
            IdUsuario = ModeloUsuario.Id;
            Nombre = ModeloUsuario.Nombre;
            Correo = ModeloUsuario.Email;
            Estado = ModeloUsuario.Activo;
            Rol = ModeloUsuario.Rol.Name;
        }
        #endregion

        #region Atributos
        public string IdUsuario { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        public string Rol { get; set; }

        public bool Estado { get; set; }
        #endregion
    }
}