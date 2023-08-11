using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleActaViewModel {
        //Contructores
        public DetalleActaViewModel() { }

        public DetalleActaViewModel(Acta ModeloActa, string Encabezado, string PiePagina) {
            IdActa = ModeloActa.Id;
            Titulo = string.Format("Acta {0} N° {1}-{2}", ModeloActa.Sesion.TipoSesion.Descripcion, ModeloActa.Sesion.Numero, ModeloActa.Sesion.FechaHora.Year);
            TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = Convert.ToString(ModeloActa.Sesion.Numero);
            EncabezadoActa = ModeloActa.Encabezado;
            ParrafoCierre = ModeloActa.ParrafoCierre;
            FechaSesion = ModeloActa.Sesion.FechaHora;
            NombreUsuarioPreside = ModeloActa.UsuarioPreside != null ? ModeloActa.UsuarioPreside.Nombre : string.Empty;
            NombreUsuarioSecretario = ModeloActa.UsuarioSecretario != null ? ModeloActa.UsuarioSecretario.Nombre : string.Empty;
            IdSesionAprobada = ModeloActa.IdSesionAprobada != null ? ModeloActa.IdSesionAprobada : null;
            SesionActaAprobada = ModeloActa.SesionAprobada != null ? string.Format("{0} N° {1}-{2}", ModeloActa.SesionAprobada.TipoSesion.Descripcion, ModeloActa.SesionAprobada.Numero, ModeloActa.SesionAprobada.FechaHora.Year) : string.Empty;
            FechaSesionAprobada = ModeloActa.SesionAprobada != null ? ModeloActa.SesionAprobada.FechaHora.ToString("dd/MM/yyyy") : null;
            NotasVotosDisidentes = ModeloActa.NotaVotosDisidentes;
            ListaCapitulos = ModeloActa.Capitulos.Select(Capitulo => new DetalleCapituloViewModel(Capitulo)).ToList();
            
            this.Encabezado = Encabezado;
            this.PiePagina = PiePagina;
        }

        public DetalleActaViewModel(Acta ModeloActa, string Encabezado, string PiePagina, OrdenDia OrdenDiaAprobadaModelo, IEnumerable<VotosDesidentesAcuerdosDTO> ListaVotosDesidentes) {
            IdActa = ModeloActa.Id;
            Titulo = string.Format("Acta {0} N° {1}-{2}", ModeloActa.Sesion.TipoSesion.Descripcion, ModeloActa.Sesion.Numero, ModeloActa.Sesion.FechaHora.Year);
            TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = Convert.ToString(ModeloActa.Sesion.Numero);
            EncabezadoActa = ModeloActa.Encabezado;
            ParrafoCierre = ModeloActa.ParrafoCierre;
            FechaSesion = ModeloActa.Sesion.FechaHora;
            NombreUsuarioPreside = ModeloActa.UsuarioPreside != null ? ModeloActa.UsuarioPreside.Nombre : string.Empty;
            NombreUsuarioSecretario = ModeloActa.UsuarioSecretario != null ? ModeloActa.UsuarioSecretario.Nombre : string.Empty;
            IdSesionAprobada = ModeloActa.IdSesionAprobada != null ? ModeloActa.IdSesionAprobada : null;
            SesionActaAprobada = ModeloActa.SesionAprobada != null ? string.Format("{0} N° {1}-{2}", ModeloActa.SesionAprobada.TipoSesion.Descripcion, ModeloActa.SesionAprobada.Numero, ModeloActa.SesionAprobada.FechaHora.Year) : string.Empty;
            FechaSesionAprobada = ModeloActa.SesionAprobada != null ? ModeloActa.SesionAprobada.FechaHora.ToString("dd/MM/yyyy") : null;
            NotasVotosDisidentes = ModeloActa.NotaVotosDisidentes;
            ListaCapitulos = ModeloActa.Capitulos.Select(Capitulo => new DetalleCapituloViewModel(Capitulo)).ToList();
            OrdenDiaViewModel = OrdenDiaAprobadaModelo.Id <= 0 ? new DetallesOrdenDiaViewModel() : new DetallesOrdenDiaViewModel(OrdenDiaAprobadaModelo);
            this.Encabezado = Encabezado;
            this.PiePagina = PiePagina;

            VotosDesidentes = ListaVotosDesidentes.Select(v => new VotosDesidentesAcuerdosViewModel(v)).ToList();
        }

        //Propiedades
        public int IdActa { get; set; }

        public string Titulo { get; set; }

        public string TipoSesion { get; set; }

        public string NumeroSesion { get; set; }

        public string EncabezadoActa { get; set; }

        public string ParrafoCierre { get; set; }

        public DateTime FechaSesion { get; set; }

        public string NombreUsuarioPreside { get; set; }

        public string NombreUsuarioSecretario { get; set; }

        public int? IdSesionAprobada { get; set; }

        public string SesionActaAprobada { get; set; }

        public string FechaSesionAprobada { get; set; }

        public string NotasVotosDisidentes { get; set; }

        public IEnumerable<DetalleCapituloViewModel> ListaCapitulos { get; set; }

        public IEnumerable<VotosDesidentesAcuerdosViewModel> VotosDesidentes { get; set; }

        public DetallesOrdenDiaViewModel OrdenDiaViewModel { get; set; }

        public string Encabezado { get; set; }

        public string PiePagina { get; set; }
    }
}