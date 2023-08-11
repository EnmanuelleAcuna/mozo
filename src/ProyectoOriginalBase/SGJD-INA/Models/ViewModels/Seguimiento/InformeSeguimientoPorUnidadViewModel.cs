using SGJD_INA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class InformeSeguimientoPorUnidadViewModel {
        // Constructores 
        public InformeSeguimientoPorUnidadViewModel() { }

        public InformeSeguimientoPorUnidadViewModel(UnidadesEjecucionSeguimientoDTO Modelo) {
            IdUnidad = Modelo.IdUnidad;
            IdSeguimietno = Modelo.IdSeguimiento;           
        }

        // Propiedades
        public int IdUnidad { get; set; }

        public int IdSeguimietno { get; set; }

        public int IdUnidadUsuario { get; set; }
    }
}