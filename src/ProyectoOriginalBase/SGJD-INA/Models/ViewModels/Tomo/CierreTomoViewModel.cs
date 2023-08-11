using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class CierreTomoViewModel {
        //Constructores
        public CierreTomoViewModel() { }

        public CierreTomoViewModel(Tomo ModeloTomo, ParametroGeneral ParametroNombreLibro, ParametroGeneral ParametroUnidad) {
            IdTomo = ModeloTomo.Id;
            NumeroTomo = ModeloTomo.Numero;
            NombreTomo = ModeloTomo.Nombre;
            NombreLibro = ParametroNombreLibro.Valor;
            NombreUnidad = ParametroUnidad.Valor;
            FechaApertura = ModeloTomo.FechaApertura.ToString("dd/MM/yyyy");
            UsuarioApertura = ModeloTomo.UsuarioApertura.Nombre;
            NumeroAsiento = ModeloTomo.Asiento;
            FechaCierre = DateTime.Now;
            ListaActas = ModeloTomo.Actas.Select(a => new ActaTomoViewModel(a)).ToList();
        }

        //Atributos
        public int IdTomo { get; set; }

        public int NumeroTomo { get; set; }

        public string NombreTomo { get; set; }

        public string ObservacionApertura { get; set; }

        [Display(Name = "Observación de cierre")]
        [Required(ErrorMessage = "La observación de cierre es requerido")]
        public string ObservacionCierre { get; set; }

        [Display(Name = "Observaciones mediante oficio")]
        public string ObservacionesMedianteOficio { get; set; }

        public string IdUsuarioApertura { get; set; }

        public string UsuarioApertura { get; set; }

        [Display(Name = "id Usuario cierre")]
        public string IdUsuarioCierre { get; set; }

        public string FechaApertura { get; set; }

        [Display(Name = "Fecha cierre")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de cierre debe ser una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        //[Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaCierre { get; set; }

        public int NumeroAsiento { get; set; }

        [Display(Name = "Nombre del Libro")]
        public string NombreLibro { get; set; }

        [Display(Name = "Unidad")]
        public string NombreUnidad { get; set; }

        [Display(Name = "Lista de Actas")]
        public IEnumerable<ActaTomoViewModel> ListaActas { get; set; }

        // Metodos
        public Tomo Entidad() {
            return new Tomo {
                Id = IdTomo,
                Numero = NumeroTomo,
                ObservacionCierre = ObservacionCierre,
                IdUsuarioCierre = IdUsuarioCierre,
                FechaCierre = FechaCierre,
                Nombre = NombreTomo,
                Asiento = NumeroAsiento,
                ObservacionesMedianteOficio = ObservacionesMedianteOficio
            };
        }
    }
}