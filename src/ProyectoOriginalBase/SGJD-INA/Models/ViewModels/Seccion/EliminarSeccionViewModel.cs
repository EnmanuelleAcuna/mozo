using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarSeccionViewModel {
        // Constructores
        public EliminarSeccionViewModel() { }

        public EliminarSeccionViewModel(Seccion ModeloSeccion) {
            IdSeccion = ModeloSeccion.Id;
            Descripcion = ModeloSeccion.Descripcion;
        }

        // Propiedades
        public int IdSeccion { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}