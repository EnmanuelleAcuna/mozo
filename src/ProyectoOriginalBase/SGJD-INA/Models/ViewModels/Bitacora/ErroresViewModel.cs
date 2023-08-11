using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class ErroresViewModel {
        // Constructores
        public ErroresViewModel () { }

        public ErroresViewModel (Errores ModeloErrores) {
            IdError = ModeloErrores.Id;
            Fecha = ModeloErrores.Fecha.ToString("dd/MM/yyyy hh:mm:ss tt");
            Error = ModeloErrores.Error;
            NombreArchivo = ModeloErrores.NombreArchivo;
            NombreMetodo = ModeloErrores.NombreMetodo;
            CodigoExcepcion = ModeloErrores.CodigoExcepcion;
            LineaError = ModeloErrores.LineaError;
        }

        // Propiedades
        public int IdError { get; set; }

        [Display(Name = "Fecha")]
        public string Fecha { get; set; }

        [Display(Name = "Error")]
        public string Error { get; set; }

        [Display(Name = "Nombre del archivo")]
        public string NombreArchivo { get; set; }

        [Display(Name = "Nombre del metodo")]
        public string NombreMetodo { get; set; }

        [Display(Name = "Exceptión")]
        public string CodigoExcepcion { get; set; }

        [Display(Name = "Linea")]
        public int? LineaError { get; set; }
    }
}