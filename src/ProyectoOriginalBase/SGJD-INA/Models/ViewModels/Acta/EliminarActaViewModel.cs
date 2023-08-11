using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarActaViewModel {
        // Constructores
        public EliminarActaViewModel() { }

        public EliminarActaViewModel(Acta ModeloActa) {
            IdActa = ModeloActa.Id;
            IdSesion = ModeloActa.IdSesion;
            NumeroSesion = ModeloActa.Sesion.Numero;
            AnnoSesion = ModeloActa.Sesion.FechaHora.Year.ToString();
        }

        // Propiedades
        public int IdActa { get; set; }

        public int IdSesion { get; set; }

        public int NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }
    }
}