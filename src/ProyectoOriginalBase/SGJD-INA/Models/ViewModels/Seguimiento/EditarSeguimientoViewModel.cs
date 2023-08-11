using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class EditarSeguimientoViewModel {
        // Constructores
        public EditarSeguimientoViewModel() { }

        /// <summary>
        /// Constructor encargado de convertir una entidad de sistema a view model para vista
        /// </summary>
        public EditarSeguimientoViewModel(SeguimientoAcuerdo ModeloSeguimientoAcuerdo, IEnumerable<Unidad> UnidadesEjecutorasSeguimiento, IEnumerable<Unidad> UnidadesEjecutorasAcuerdo) {
            IdSeguimiento = ModeloSeguimientoAcuerdo.Id;
            TituloSeguimiento = string.Format("{0}", ModeloSeguimientoAcuerdo.Acuerdo.Titulo);
            IdEstadoSeguimiento = ModeloSeguimientoAcuerdo.IdEstado;
            NombreEstadoSeguimiento = ModeloSeguimientoAcuerdo.Estado.Nombre;
            CodigoEstado = ModeloSeguimientoAcuerdo.Estado.CodigoEstado;
            Observaciones = ModeloSeguimientoAcuerdo.Observaciones;
            FechaNotificacion = ModeloSeguimientoAcuerdo.FechaNotificacion;
            FechaVencimiento = ModeloSeguimientoAcuerdo.FechaVencimiento;
            PlazoDias = (int)ModeloSeguimientoAcuerdo.PlazoDias;
            PorcentajeAvance = ModeloSeguimientoAcuerdo.PorcentajeAvance;
            NombreObjeto = ModeloSeguimientoAcuerdo.NombreObjeto;
            IdAcuerdo = ModeloSeguimientoAcuerdo.IdAcuerdo;

            this.UnidadesEjecutorasSeguimiento = UnidadesEjecutorasSeguimiento.Select(u => new UnidadViewModel(u)).ToList();
            this.Detalles = ModeloSeguimientoAcuerdo.Detalles.Select(d => new DetalleSeguimientoViewModel(d)).ToList();

            this.UnidadesEjecutorasAcuerdo = UnidadesEjecutorasAcuerdo.Select(u => new UnidadViewModel(u)).ToList();
        }
       

        // Propiedades
        [Required(ErrorMessage = "El id del Seguimiento es requerido.")]
        public int IdSeguimiento { get; set; }

        [Required(ErrorMessage = "El Acuerdo es requerido.")]
        public int IdAcuerdo { get; set; }

        public string TituloSeguimiento { get; set; }

        public int IdEstadoSeguimiento { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstadoSeguimiento { get; set; }

        [Display(Name = "Codigo Estado")]
        public string CodigoEstado { get; set; }

        [Display(Name = "Asunto del Seguimiento")]
        [Required(ErrorMessage = "La observación es requerida")]
        public string Observaciones { get; set; }

        [Display(Name = "Fecha de notificación")]
        [Required(ErrorMessage = "La fecha de notificación es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de notificación debe ser una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        public DateTime? FechaNotificacion { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        [Required(ErrorMessage = "La fecha de vencimiento es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de vencimiento debe ser una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        public DateTime? FechaVencimiento { get; set; }

        [Display(Name = "Plazo de ejecución (días)")]
        [Required(ErrorMessage = "El plazo de ejecucion es requerido.")]
        public int PlazoDias { get; set; }

        [Display(Name = "Porcentaje de avance")]
        [Required(ErrorMessage = "El porcentaje de avance es requerido.")]
        public int PorcentajeAvance { get; set; }

        [Required(ErrorMessage = "El nombre del objeto es requerido.")]
        public string NombreObjeto { get; set; }

        public IEnumerable<UnidadViewModel> UnidadesEjecutorasAcuerdo { get; set; }

        public IEnumerable<UnidadViewModel> UnidadesEjecutorasSeguimiento { get; set; }

        public IEnumerable<DetalleSeguimientoViewModel> Detalles { get; set; }

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public SeguimientoAcuerdo Entidad() {
            return new SeguimientoAcuerdo {
                Id = IdSeguimiento,
                IdEstado = IdEstadoSeguimiento,
                IdAcuerdo = IdAcuerdo,
                Observaciones = Observaciones,
                FechaNotificacion = FechaNotificacion,
                FechaVencimiento = FechaVencimiento,
                PlazoDias = PlazoDias,
                PorcentajeAvance = PorcentajeAvance
            };
        }
        #endregion
    }
}