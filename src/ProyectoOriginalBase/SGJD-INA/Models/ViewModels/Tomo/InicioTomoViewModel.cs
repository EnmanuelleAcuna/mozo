using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InicioTomoViewModel {
        //Constructor
        public InicioTomoViewModel(Tomo ModeloTomo) {
            IdTomo = ModeloTomo.Id;
            Numero = ModeloTomo.Numero;
            NombreTomo = ModeloTomo.Nombre;
            NombreEstado = ModeloTomo.Estado.Nombre;
            CodigoEstado = ModeloTomo.Estado.CodigoEstado;
            FechaApertura = ModeloTomo.FechaApertura.ToString("dd/MM/yyyy");
            UsuarioApertura = ModeloTomo.UsuarioApertura.Nombre;
            FechaCierre = ModeloTomo.FechaCierre.HasValue ? Convert.ToDateTime(ModeloTomo.FechaCierre).ToString("dd/MM/yyyy") : string.Empty;
            UsuarioCierre = string.IsNullOrEmpty(ModeloTomo.IdUsuarioCierre) ? string.Empty : ModeloTomo.UsuarioCierre.Nombre;
            NumeroActaInicial = ModeloTomo.Actas.Count > 0 ? string.Format("Acta {0}-{1}", ModeloTomo.Actas.FirstOrDefault().Sesion.Numero.ToString(), ModeloTomo.Actas.FirstOrDefault().Sesion.FechaHora.Year) : " ";
            NumeroActaFinal = ModeloTomo.Actas.Count > 0 ? string.Format("Acta {0}-{1}", ModeloTomo.Actas.LastOrDefault().Sesion.Numero.ToString(), ModeloTomo.Actas.LastOrDefault().Sesion.FechaHora.Year) : " ";
            CantidadDeActas = ModeloTomo.Actas.Count;
        }

        //Propiedades
        [Display(Name = "Id del tomo")]
        public int IdTomo { get; set; }

        [Display(Name = "Tomo")]
        public int Numero { get; set; }

        [Display(Name = "Inicial")]
        public string NumeroActaInicial { get; set; }

        [Display(Name = "Final")]
        public string NumeroActaFinal { get; set; }

        [Display(Name = "Actas")]
        public int CantidadDeActas { get; set; }

        [Display(Name = "Tomo")]
        public string NombreTomo { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Fecha")]
        public string FechaApertura { get; set; }

        [Display(Name = "Usuario")]
        public string UsuarioApertura { get; set; }

        [Display(Name = "Fecha")]
        public string FechaCierre { get; set; }

        [Display(Name = "Usuario")]
        public string UsuarioCierre { get; set; }
    }
}