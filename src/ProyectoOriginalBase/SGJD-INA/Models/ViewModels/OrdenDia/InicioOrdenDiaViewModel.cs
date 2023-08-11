using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioOrdenDiaViewModel {
        //Constructor
        public InicioOrdenDiaViewModel(OrdenDia ModeloOrdenDia) {
            IdOrdenDia = ModeloOrdenDia.Id;
            IdSesion = ModeloOrdenDia.IdSesion;
            TipoSesion = ModeloOrdenDia.Sesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloOrdenDia.Sesion.Numero;
            AnnoSesion = ModeloOrdenDia.Sesion.FechaHora.Year.ToString();
            FechaSesion = ModeloOrdenDia.Sesion.FechaHora;
            NombreEstado = ModeloOrdenDia.Estado.Nombre;
        }

        //Propiedades
        [Display(Name = "Id del acuerdo")]
        public int IdOrdenDia { get; set; }

        [Display(Name = "Id de la sesión")]
        public int IdSesion { get; set; }

        public string TipoSesion { get; set; }

        public int NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")] // Especificar explicitamente formato de texto
        public DateTime FechaSesion { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }
    }
}