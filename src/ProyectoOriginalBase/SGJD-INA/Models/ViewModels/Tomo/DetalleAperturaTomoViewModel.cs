using SGJD_INA.Models.Entities;
using System;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleAperturaTomoViewModel {
        //Constructores
        public DetalleAperturaTomoViewModel(Tomo TomoModelo, ParametroGeneral ParametroNombreLibro, ParametroGeneral ParametroUnidad, EncabezadoPiePagina EncabezadoPiePaginaTomo) {
            IdTomo = TomoModelo.Id;
            Asiento = TomoModelo.Asiento;
            NumeroTomo = TomoModelo.Numero;
            NombreTomo = TomoModelo.Nombre;
            NombreLibro = ParametroNombreLibro.Valor;
            UnidadEjecutora = ParametroUnidad.Valor;
            FechaApertura = TomoModelo.FechaApertura;
            UsuarioApertura = TomoModelo.UsuarioApertura.Nombre;
            ObservacionApertura = TomoModelo.ObservacionApertura;
            UrlOficioApertura = TomoModelo.UrlOficioApertura;
            NombreObjeto = TomoModelo.NombreObjeto;
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

        public DateTime FechaApertura { get; set; }

        public string UsuarioApertura { get; set; }

        public string ObservacionApertura { get; set; }

        public string UrlOficioApertura { get; set; }

        public string Encabezado { get; set; }

        public string PiePagina { get; set; }

        public string NombreObjeto { get; set; }
    }
}