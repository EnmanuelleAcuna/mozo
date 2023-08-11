using SGJD_INA.Models.Entities;
using System.Collections.Generic;

namespace SGJD_INA.Models.ViewModels {
    public class InicioUnidadesPredViewModel {
        #region Constructores
        public InicioUnidadesPredViewModel() { }

        public InicioUnidadesPredViewModel(IEnumerable<UnidadPredeterminada> ModeloListaUnidadesPred, IEnumerable<Unidad> ModeloListaUnidades) {
            ListaUnidadesPred = ModeloListaUnidadesPred;
            ListaUnidades = ModeloListaUnidades;
        }
        #endregion
        #region Atributos
        public IEnumerable<UnidadPredeterminada> ListaUnidadesPred { get; set; }
        public IEnumerable<Unidad> ListaUnidades { get; set; }
        #endregion
    }
}