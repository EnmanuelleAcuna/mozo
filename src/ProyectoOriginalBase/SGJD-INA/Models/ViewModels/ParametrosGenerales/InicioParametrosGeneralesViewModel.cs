using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InicioParametrosGeneralesViewModel {
        //Constructor
        public InicioParametrosGeneralesViewModel(IEnumerable<ParametroGeneral> ListaParametrosInstitucion, IEnumerable<ParametroGeneral> ListaParametrosCorreo) {
            ListaParametrosTipoCorreo = ListaParametrosCorreo.Select(ParametrosGenerales => new ParametroGeneralViewModel(ParametrosGenerales)).ToList();
            ListaParametrosTipoInstitucion = ListaParametrosInstitucion.Select(ParametrosGenerales => new ParametroGeneralViewModel(ParametrosGenerales)).ToList();
        }

        //Propiedades
        public IEnumerable<ParametroGeneralViewModel> ListaParametrosTipoCorreo { get; set; }
        public IEnumerable<ParametroGeneralViewModel> ListaParametrosTipoInstitucion { get; set; }
    }
}