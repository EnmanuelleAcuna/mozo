using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InicioSeguimientoViewModel {
        #region Constructores
        public InicioSeguimientoViewModel() { }

        public InicioSeguimientoViewModel(IEnumerable<SeguimientoAcuerdo> ListaSeguimientosNoEjecutados, IEnumerable<SeguimientoAcuerdo> ListaSeguimientosEnEjecucion, IEnumerable<SeguimientoAcuerdo> ListaSeguimientosEjecutados, IEnumerable<UnidadesEjecucionSeguimientoDTO> ModeloUnidadSeguimiento) {
            SeguimientosNoEjecutados = ListaSeguimientosNoEjecutados.Select(s => new SeguimientoViewModel(s)).ToList();
            SeguimientosEnEjecucion = ListaSeguimientosEnEjecucion.Select(s => new SeguimientoViewModel(s)).ToList();
            SeguimientosEjecutados = ListaSeguimientosEjecutados.Select(s => new SeguimientoViewModel(s)).ToList();
            UnidadesEjecutorasSeguimiento = ModeloUnidadSeguimiento.Select(datos => new InformeSeguimientoPorUnidadViewModel(datos)).ToList();
        }
        #endregion

        #region Atributos
        [Display(Name = "Seguimientos no ejecutados")]
        public IEnumerable<SeguimientoViewModel> SeguimientosNoEjecutados { get; set; }

        [Display(Name = "Seguimientos en ejecución")]
        public IEnumerable<SeguimientoViewModel> SeguimientosEnEjecucion { get; set; }

        [Display(Name = "Seguimientos ejecutados")]
        public IEnumerable<SeguimientoViewModel> SeguimientosEjecutados { get; set; }
       
        public IEnumerable<InformeSeguimientoPorUnidadViewModel> UnidadesEjecutorasSeguimiento { get; set; }
        #endregion
    }
}