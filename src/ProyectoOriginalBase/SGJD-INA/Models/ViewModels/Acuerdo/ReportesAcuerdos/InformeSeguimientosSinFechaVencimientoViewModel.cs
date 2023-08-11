using SGJD_INA.Models.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InformeSeguimientosSinFechaVencimientoViewModel {
        // Constructores
        public InformeSeguimientosSinFechaVencimientoViewModel() { }

        public InformeSeguimientosSinFechaVencimientoViewModel(InformeSeguimientosSinFechaVencimientoDTO ModeloInforme) {
            IdSeguimiento = ModeloInforme.IdSeguimiento;
            IdAcuerdo = ModeloInforme.IdAcuerdo;
            Titulo = string.Format("{0}", ModeloInforme.Titulo);
            NumeroVersion = ModeloInforme.NumeroVersion;
            UrlAcuerdoFirmado = ModeloInforme.UrlAcuerdoFirmado;
            Observaciones = ModeloInforme.Observaciones;
            FechaNotificacion = ModeloInforme.FechaNotificacion.ToString("dd/MM/yyyy");
            FechaVencimiento = ModeloInforme.FechaVencimiento.HasValue ? Convert.ToDateTime(ModeloInforme.FechaVencimiento).ToString("dd/MM/yyyy") : string.Empty;
            PlazoDias = ModeloInforme.PlazoDias;
            PorcentajeAvance = ModeloInforme.PorcentajeAvance;
        }

        // Propiedades
        public int IdSeguimiento { get; set; }

        public int IdAcuerdo { get; set; }

        [Display(Name = "Seguimiento")]
        public string Titulo { get; set; }

        public int NumeroVersion { get; set; }

        public string UrlAcuerdoFirmado { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Notificación")]
        public string FechaNotificacion { get; set; }

        [Display(Name = "Vencimiento")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")] // Especificar explicitamente formato de texto
        public string FechaVencimiento { get; set; }

        [Display(Name = "Plazo días")]
        public int PlazoDias { get; set; }

        [Display(Name = "% de ejecución")]
        public int PorcentajeAvance { get; set; }
    }
}