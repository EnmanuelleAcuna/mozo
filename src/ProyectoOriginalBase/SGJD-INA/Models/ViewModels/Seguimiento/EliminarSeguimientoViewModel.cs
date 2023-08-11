using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarSeguimientoViewModel {
        // Constructores
        public EliminarSeguimientoViewModel() { }
        
        public EliminarSeguimientoViewModel(SeguimientoAcuerdo ModeloSeguimientoAcuerdo) {
            IdSeguimiento = ModeloSeguimientoAcuerdo.Id;
            TituloSeguimiento = string.Format("{0}", ModeloSeguimientoAcuerdo.Acuerdo.Titulo);
        }

        // Propiedades
        public int IdSeguimiento { get; set; }

        public string TituloSeguimiento { get; set; }

    }
}