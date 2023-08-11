using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace SGJD_INA.Models.ViewModels {
    public class EditarActaViewModel {
        // Constructores
        public EditarActaViewModel() { }

        public EditarActaViewModel(Acta ModeloActa) {
            IdActa = ModeloActa.Id;
            IdSesion = ModeloActa.IdSesion;
            IdTomo = ModeloActa.IdTomo;
            IdEstado = ModeloActa.Estado.Id;

            TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloActa.Sesion.Numero;
            AnnoSesion = ModeloActa.Sesion.FechaHora.Year.ToString();
            FechaSesion = ModeloActa.Sesion.FechaHora;

            Titulo = string.Format("Acta de {0}  {1} - {2}", ModeloActa.Sesion.TipoSesion.Descripcion, ModeloActa.Sesion.Numero, ModeloActa.Sesion.FechaHora.Year);

            NombreEstado = ModeloActa.Estado.Nombre;
            CodigoEstado = ModeloActa.Estado.CodigoEstado;

            Encabezado = ModeloActa.Encabezado;
            Observacion = ModeloActa.Observacion;

            UrlActaFirmada = ModeloActa.UrlActaFirmada;

            ParrafoCierre = ModeloActa.ParrafoCierre;

            IdUsuarioPreside = ModeloActa.IdUsuarioPreside;
            IdUsuarioSecretario = ModeloActa.IdUsuarioSecretario;
            IdSesionAprobada = ModeloActa.IdSesionAprobada;

            NotaVotosDisidentes = ModeloActa.NotaVotosDisidentes;

            NombreObjeto = ModeloActa.NombreObjeto;

            Capitulos = ModeloActa.Capitulos.Select(Capitulo => new CapituloViewModel(Capitulo)).ToList();
        }

        // Propiedades
        [Required(ErrorMessage = "El acta es requerida.")]
        public int IdActa { get; set; }

        [Required(ErrorMessage = "La sesión es requerida.")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El tomo es requerido.")]
        public int IdTomo { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public int IdEstado { get; set; }

        [Display(Name = "Sesión")]
        public string TipoSesion { get; set; }

        public int NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public DateTime FechaSesion { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Codigo del estado")]
        public string CodigoEstado { get; set; }

        public string Titulo { get; set; }

        [Required(ErrorMessage = "El encabezado es requerido.")]
        [AllowHtml]
        public string Encabezado { get; set; }

        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        public string UrlActaFirmada { get; set; }

        [Display(Name = "Párrafo de cierre")]       
        public string ParrafoCierre { get; set; }

        public string IdUsuarioPreside { get; set; }

        public string IdUsuarioSecretario { get; set; }

        public int? IdSesionAprobada { get; set; }

        public string SesionActaAprovada { get; set; }

        [Display(Name = "Nota votos disidentes")]
        [AllowHtml]
        public string NotaVotosDisidentes { get; set; }

        public string NombreObjeto { get; set; }

        public IEnumerable<CapituloViewModel> Capitulos { get; set; }

        // Metodos
        public Acta Entidad() {
            return new Acta {
                Id = IdActa,
                IdSesion = IdSesion,
                IdTomo = IdTomo,
                IdEstado = IdEstado,
                Encabezado = Encabezado,
                Observacion = Observacion,
                UrlActaFirmada = UrlActaFirmada,
                ParrafoCierre = ParrafoCierre,
                IdUsuarioPreside = IdUsuarioPreside,
                IdUsuarioSecretario = IdUsuarioSecretario,
                IdSesionAprobada = IdSesionAprobada,
                NotaVotosDisidentes = NotaVotosDisidentes,
                NombreObjeto = NombreObjeto
            };
        }
    }
}