using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarAcuerdoViewModel {
        // Constructores
        public EliminarAcuerdoViewModel() { }

        public EliminarAcuerdoViewModel(Acuerdo ModeloAcuerdo) {
            IdAcuerdo = ModeloAcuerdo.Id;
            Titulo = string.Format("{0}", ModeloAcuerdo.Titulo);            
        }

        // Propiedades
        public int IdAcuerdo { get; set; }

        [Display(Name = "Acuerdo")]
        public string Titulo { get; set; }
    }
}