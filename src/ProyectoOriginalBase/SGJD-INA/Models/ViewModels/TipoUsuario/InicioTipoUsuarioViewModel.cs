using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class InicioTipoUsuarioViewModel {
        #region Constructor
        public InicioTipoUsuarioViewModel(TipoUsuario ModeloTipoUsuario) {
            IdTipoUsuario = ModeloTipoUsuario.Id;
            Nombre = ModeloTipoUsuario.Nombre;
        }
        #endregion

        #region Propiedades
        public int IdTipoUsuario { get; set; }

        public string Nombre { get; set; }
        #endregion
    }
}