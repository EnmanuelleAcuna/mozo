using SGJD_INA.Models.Entities;
using System;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleCierreTomoViewModel {
        //Constructores
        public DetalleCierreTomoViewModel(Tomo TomoModelo, ParametroGeneral ParametroNombreLibro, ParametroGeneral ParametroUnidad, EncabezadoPiePagina EncabezadoPiePaginaTomo) {
            IdTomo = TomoModelo.Id;
            Asiento = TomoModelo.Asiento;
            NumeroTomo = TomoModelo.Numero;
            NombreTomo = TomoModelo.Nombre;
            NombreLibro = ParametroNombreLibro.Valor;
            UnidadEjecutora = ParametroUnidad.Valor;
            FechaCierre = TomoModelo.FechaCierre.HasValue ? Convert.ToDateTime(TomoModelo.FechaCierre) : DateTime.Now;
            UsuarioCierre = TomoModelo.UsuarioCierre.Nombre;
            ObservacionesCierre = TomoModelo.ObservacionCierre;
            UrlOficioCierre = TomoModelo.UrlOficioCierre;
            ObservacionesMedianteOficio = TomoModelo.ObservacionesMedianteOficio;
            Encabezado = EncabezadoPiePaginaTomo.Encabezado;
            PiePagina = EncabezadoPiePaginaTomo.PiePagina;
        }

        //Atributos
        public int IdTomo { get; set; }

        public int Asiento { get; set; }

        public int NumeroTomo { get; set; }

        public string NombreTomo { get; set; }

        public string NombreLibro { get; set; }

        public string UnidadEjecutora { get; set; }

        public DateTime FechaCierre { get; set; }

        public string UsuarioCierre { get; set; }

        public string ObservacionesCierre { get; set; }

        public string UrlOficioCierre { get; set; }

        public string ObservacionesMedianteOficio { get; set; }

        public string Encabezado { get; set; }

        public string PiePagina { get; set; }
    }
}