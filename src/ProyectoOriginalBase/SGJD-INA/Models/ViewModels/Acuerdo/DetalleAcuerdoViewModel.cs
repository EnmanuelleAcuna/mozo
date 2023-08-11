using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleAcuerdoViewModel {
        // Constructores
        public DetalleAcuerdoViewModel() { }

        public DetalleAcuerdoViewModel(Acuerdo ModeloAcuerdo, string Encabezado, string PiePagina, IEnumerable<Votacion> ListaVotaciones) {
            IdAcuerdo = ModeloAcuerdo.Id;
            Titulo = ModeloAcuerdo.Titulo;
            Asunto = ModeloAcuerdo.Asunto;
            NumeroVersion = ModeloAcuerdo.NumeroVersion;
            Detalle = ModeloAcuerdo.Detalle;
            FechaFirmeza = ModeloAcuerdo.FechaFirmeza.HasValue ? Convert.ToDateTime(ModeloAcuerdo.FechaFirmeza).ToString("dd/MM/yyyy") : "Acuerdo no firme";
            Considerando = ModeloAcuerdo.DetalleConsiderando;
            PorTanto = ModeloAcuerdo.DetallePorTanto;
            UnidadesEjecucion = ModeloAcuerdo.UnidadesEjecucion.Select(Unidad => new UnidadViewModel(Unidad)).ToList();
            UnidadesInformacion = ModeloAcuerdo.UnidadesInformacion.Select(Unidad => new UnidadViewModel(Unidad)).ToList();
            IdTipoAprobacion = ModeloAcuerdo.IdTipoAprobacion;
            Mensaje = ModeloAcuerdo.TipoAprobacion.Mensaje;
            NombreTipoAprobacion = ModeloAcuerdo.TipoAprobacion.Nombre;
            ObservacionVotaciones = ModeloAcuerdo.ObservacionVotaciones;

            // Valida si el usuario secretario viene nulo
            if (ModeloAcuerdo.Articulo != null) {
                if (ModeloAcuerdo.Articulo.Capitulo != null) {
                    if (ModeloAcuerdo.Articulo.Capitulo.Acta != null) {
                        if (ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioSecretario != null) {
                            NombreUsuarioSecretario = ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioSecretario.Nombre;
                        }
                        else {
                            NombreUsuarioSecretario = string.Empty;
                        }
                    }
                    else {
                        NombreUsuarioSecretario = string.Empty;
                    }
                }
                else {
                    NombreUsuarioSecretario = string.Empty;
                }
            }
            else {
                NombreUsuarioSecretario = string.Empty;
            }

            // Valida si el usuario que preside viene nulo
            if (ModeloAcuerdo.Articulo != null) {
                if (ModeloAcuerdo.Articulo.Capitulo != null) {
                    if (ModeloAcuerdo.Articulo.Capitulo.Acta != null) {
                        if (ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioPreside != null) {
                            NombreUsuarioPreside = ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioPreside.Nombre;
                        }
                        else {
                            NombreUsuarioPreside = string.Empty;
                        }
                    }
                    else {
                        NombreUsuarioPreside = string.Empty;
                    }
                }
                else {
                    NombreUsuarioPreside = string.Empty;
                }
            }
            else {
                NombreUsuarioPreside = string.Empty;
            }

            //NombreUsuarioSecretario = ModeloAcuerdo.Articulo != null ? ModeloAcuerdo.Articulo.Capitulo != null ? ModeloAcuerdo.Articulo.Capitulo.Acta != null ? ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioSecretario != null ? ModeloAcuerdo.Articulo.Capitulo.Acta.UsuarioSecretario.Nombre : string.Empty : string.Empty : string.Empty : string.Empty;

            Votaciones = ListaVotaciones.Select(v => new VotacionesAcuerdoViewModel(v)).ToList();

            this.Encabezado = Encabezado;
            this.PiePagina = PiePagina;
        }

        // Propiedades
        public int IdAcuerdo { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Display(Name = "Número de versión")]
        public int NumeroVersion { get; set; }

        [Display(Name = "Detalle")]
        public string Detalle { get; set; }

        [Display(Name = "Fecha de firmeza")]
        public string FechaFirmeza { get; set; }

        [Display(Name = "Considerando")]
        public string Considerando { get; set; }

        [Display(Name = "Por tanto")]
        public string PorTanto { get; set; }

        [Display(Name = "IdTipoAprobacion")]
        public int IdTipoAprobacion { get; set; }

        [Display(Name = "Nombre tipo aprobacion")]
        public string NombreTipoAprobacion { get; set; }

        [Display(Name = "Mensaje")]
        public string Mensaje { get; set; }

        [Display(Name = "Observaciones de votantes")]
        public string ObservacionVotaciones { get; set; }

        public string NombreUsuarioSecretario { get; set; }

        public string NombreUsuarioPreside { get; set; }

        [Display(Name = "Unidades ejecucion")]
        public IEnumerable<UnidadViewModel> UnidadesEjecucion { get; set; }

        [Display(Name = "Unidades informacion")]
        public IEnumerable<UnidadViewModel> UnidadesInformacion { get; set; }

        [Display(Name = "encabezado")]
        public string Encabezado { get; set; }

        [Display(Name = "Pie de pagina")]
        public string PiePagina { get; set; }

        public IEnumerable<VotacionesAcuerdoViewModel> Votaciones { get; set; }
    }
}