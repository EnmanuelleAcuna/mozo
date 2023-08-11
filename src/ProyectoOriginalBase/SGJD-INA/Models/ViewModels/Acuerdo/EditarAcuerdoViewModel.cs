using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class EditarAcuerdoViewModel {
        // Constructores
        public EditarAcuerdoViewModel() { }

        public EditarAcuerdoViewModel(Acuerdo ModeloAcuerdo, IEnumerable<Votacion> ListaVotaciones) {
            IdAcuerdo = ModeloAcuerdo.Id;
            IdArticulo = ModeloAcuerdo.IdArticulo;
            IdEstado = ModeloAcuerdo.IdEstado;
            NombreEstado = ModeloAcuerdo.Estado.Nombre;
            CodigoEstado = ModeloAcuerdo.Estado.CodigoEstado;
            IdTipoAprobacion = ModeloAcuerdo.IdTipoAprobacion;
            NombreTipoAprobacion = ModeloAcuerdo.TipoAprobacion.Nombre;
            Titulo = ModeloAcuerdo.Titulo;
            NumeroAcuerdo = ModeloAcuerdo.NumeroAcuerdo;
            Firme = ModeloAcuerdo.Firme;
            FechaFirmeza = ModeloAcuerdo.FechaFirmeza;
            Asunto = ModeloAcuerdo.Asunto;
            NumeroVersion = ModeloAcuerdo.NumeroVersion;
            Detalle = ModeloAcuerdo.Detalle;
            Observaciones = ModeloAcuerdo.Observaciones;
            TipoSeguimiento = ModeloAcuerdo.TipoSeguimiento;
            Revision = ModeloAcuerdo.Revision;
            DetalleConsiderando = ModeloAcuerdo.DetalleConsiderando;
            DetallePorTanto = ModeloAcuerdo.DetallePorTanto;
            UrlAcuerdoFirmado = ModeloAcuerdo.UrlAcuerdoFirmado;
            ObservacionVotaciones = ModeloAcuerdo.ObservacionVotaciones;
            FechaNotificacion = ModeloAcuerdo.FechaNotificacion;
            FechaCreacion = ModeloAcuerdo.FechaCreacion;
            NombreObjeto = ModeloAcuerdo.NombreObjeto;

            UnidadesEjecucion = ModeloAcuerdo.UnidadesEjecucion.Select(u => new UnidadViewModel(u)).ToList();
            UnidadesInformacion = ModeloAcuerdo.UnidadesInformacion.Select(u => new UnidadViewModel(u)).ToList();
            CorreosAdicionales = ModeloAcuerdo.CorreosAdicionales.Select(ca => new InicioCorreoAdicionalViewModel(ca)).ToList();

            Votaciones = ListaVotaciones.Select(v => new VotacionesAcuerdoViewModel(v)).ToList();
        }

        // Propiedades
        public int IdAcuerdo { get; set; }

        [Required(ErrorMessage = "El articulo es requerido")]
        public int IdArticulo { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Codigo del estado")]
        public string CodigoEstado { get; set; }

        [Display(Name = "Acuerdo aprobado")]
        [Required(ErrorMessage = "El estado de aprobado es requerido")]
        public int IdTipoAprobacion { get; set; }

        [Display(Name = "Tipo de aporbacion")]
        public string NombreTipoAprobacion { get; set; }

        [Required(ErrorMessage = "El número es requerido.")]
        public int NumeroAcuerdo { get; set; }

        public string Titulo { get; set; }

        [Display(Name = "Firmeza del Acuerdo")]
        [Required(ErrorMessage = "El estado de firmeza es requerido")]
        public bool Firme { get; set; }

        [Display(Name = "Fecha de firmeza")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de firmeza debe ser una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        public DateTime? FechaFirmeza { get; set; }

        [Display(Name = "Asunto")]
        [Required(ErrorMessage = "El asunto es requerido")]
        public string Asunto { get; set; }

        [Display(Name = "Número de versión")]
        [Required(ErrorMessage = "El número de versión es requerido")]
        public int NumeroVersion { get; set; }

        //0 =- Sin seguimiento, 1 - Con seguimiento, 2 - Seguimiento permanente
        [Display(Name = "Tipo de seguimiento al Acuerdo")]
        [Required(ErrorMessage = "El tipo de seguimiento es requerido")]
        public int TipoSeguimiento { get; set; }

        [Display(Name = "Acuerdo en revisión")]
        [Required(ErrorMessage = "El estado de revisión es requerido")]
        public bool Revision { get; set; }

        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Considerando")]
        [Required(ErrorMessage = "El considerando es requerido")]
        [AllowHtml]
        public string DetalleConsiderando { get; set; }

        [Display(Name = "Por tanto")]
        [Required(ErrorMessage = "El por tanto es requerido")]
        [AllowHtml]
        public string DetallePorTanto { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Acuerdo firmado")]
        public string UrlAcuerdoFirmado { get; set; }

        [Display(Name = "Observaciones a las votaciones")]
        public string ObservacionVotaciones { get; set; }

        [Display(Name = "Fecha de notificaciòn")]
        public DateTime? FechaNotificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public IEnumerable<UnidadViewModel> UnidadesEjecucion { get; set; }

        public IEnumerable<UnidadViewModel> UnidadesInformacion { get; set; }

        public IEnumerable<VotacionesAcuerdoViewModel> Votaciones { get; set; }

        public IEnumerable<InicioCorreoAdicionalViewModel> CorreosAdicionales { get; set; }

        public string NombreObjeto { get; set; }

        #region Métodos
        public Acuerdo Entidad() {
            return new Acuerdo {
                Id = IdAcuerdo,
                IdArticulo = IdArticulo,
                IdEstado = IdEstado,
                IdTipoAprobacion = IdTipoAprobacion,
                Titulo = Titulo,
                NumeroAcuerdo = NumeroAcuerdo,
                Firme = Firme,
                FechaFirmeza = FechaFirmeza,
                Asunto = Asunto,
                NumeroVersion = NumeroVersion,
                Detalle = Detalle,
                Observaciones = Observaciones,
                TipoSeguimiento = TipoSeguimiento,
                Revision = Revision,
                DetalleConsiderando = DetalleConsiderando,
                DetallePorTanto = DetallePorTanto,
                UrlAcuerdoFirmado = UrlAcuerdoFirmado,
                ObservacionVotaciones = ObservacionVotaciones,
                FechaNotificacion = FechaNotificacion,
                FechaCreacion = FechaCreacion
            };
        }
        #endregion
    }
}