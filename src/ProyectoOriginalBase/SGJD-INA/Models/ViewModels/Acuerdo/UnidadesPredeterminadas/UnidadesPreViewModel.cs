using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class UnidadesPreViewModel {
        // Constructor
        public UnidadesPreViewModel() { }

        public UnidadesPreViewModel(UnidadPredeterminada ModeloUnidadesPre) {
            IdUnidadesPre = ModeloUnidadesPre.IdUnidad;
            Nombre = ModeloUnidadesPre.Unidad.Nombre;
        }

        // Propiedades
        public int IdUnidadesPre { get; set; }

        public string Nombre { get; set; }
    }
}