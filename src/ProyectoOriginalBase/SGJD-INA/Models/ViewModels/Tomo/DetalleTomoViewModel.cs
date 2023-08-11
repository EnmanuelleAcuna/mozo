using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleTomoViewModel {
        //Constructores
        public DetalleTomoViewModel(Tomo ModeloTomo, ParametroGeneral ParametroNombreLibro, ParametroGeneral ParametroUnidad) {
            IdTomo = ModeloTomo.Id;
            Asiento = ModeloTomo.Asiento;
            NumeroTomo = ModeloTomo.Numero;
            NombreTomo = ModeloTomo.Nombre;
            NombreLibro = ParametroNombreLibro.Valor;
            UnidadEjecutora = ParametroUnidad.Valor;
            FechaApertura = ModeloTomo.FechaApertura;
            FechaCierre = ModeloTomo.FechaCierre.HasValue ? Convert.ToDateTime(ModeloTomo.FechaCierre) : DateTime.Now;
            ObservacionApertura = ModeloTomo.ObservacionApertura;
            ObservacionCierre = String.IsNullOrEmpty(ModeloTomo.ObservacionCierre) ? " " : ModeloTomo.ObservacionCierre;
            ObservacionesMedianteOficio = ModeloTomo.ObservacionesMedianteOficio;
            UsuarioApertura = ModeloTomo.UsuarioApertura.Nombre;
            UsuarioCierre = ModeloTomo.UsuarioCierre != null ? ModeloTomo.UsuarioCierre.Nombre : " ";
            CodigoEstado = ModeloTomo.Estado.CodigoEstado;
            NombreEstado = ModeloTomo.Estado.Nombre;

            ListaActas = ModeloTomo.Actas.Select(a => new ActaTomoViewModel(a)).ToList();
        }

        //Atributos
        public int IdTomo { get; set; }

        public int Asiento { get; set; }

        public int NumeroTomo { get; set; }

        [Display(Name = "Tomo")]
        public string NombreTomo { get; set; }

        public string NombreLibro { get; set; }

        public string UnidadEjecutora { get; set; }

        public DateTime FechaApertura { get; set; }

        public DateTime FechaCierre { get; set; }

        public string UsuarioApertura { get; set; }

        public string UsuarioCierre { get; set; }

        public string ObservacionApertura { get; set; }

        public string ObservacionCierre { get; set; }

        public string ObservacionesMedianteOficio { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Lista de Actas")]
        public IEnumerable<ActaTomoViewModel> ListaActas { get; set; }
    }
}