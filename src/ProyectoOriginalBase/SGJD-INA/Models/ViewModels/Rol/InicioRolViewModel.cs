using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class InicioRolViewModel {
        #region Constructor
        public InicioRolViewModel(ApplicationRole ModeloRol) {
            IdRol = ModeloRol.Id;
            Nombre = ModeloRol.Name;
        }
        #endregion

        #region Atributos
        public string IdRol { get; set; }

        public string Nombre { get; set; }
        #endregion
    }
}