using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class UnidadViewModel {
        #region Constructores
        public UnidadViewModel() { }

        public UnidadViewModel(Unidad ModeloUnidad) {
            IdUnidad = ModeloUnidad.Id;
            Nombre = ModeloUnidad.Nombre;
        }
        #endregion

        #region Propiedades
        public int IdUnidad { get; set; }

        public string Nombre { get; set; }
        #endregion
    }
}