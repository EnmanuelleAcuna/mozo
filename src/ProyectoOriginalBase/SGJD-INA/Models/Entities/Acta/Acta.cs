using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Acta {
        // Constructores
        public Acta() { }

        // Constructor para atributos
        public Acta(SGJD_ACT_TAB_ACTAS ActaBD) {
            if (ActaBD is null) throw new ArgumentNullException(paramName: nameof(ActaBD), message: Resources.ModeloNulo);

            Id = ActaBD.LLP_Id;
            IdSesion = ActaBD.LLF_IdSesion;
            IdTomo = ActaBD.LLF_IdTomo;
            IdEstado = ActaBD.LLF_IdEstado;
            Encabezado = ActaBD.Encabezado;
            Observacion = ActaBD.Observacion;
            UrlActaFirmada = ActaBD.UrlActaFirmada;
            ParrafoCierre = ActaBD.ParrafoCierre;
            NumeroPaginasPDF = ActaBD.NumeroPaginasPDF;
            IdUsuarioPreside = ActaBD.LLF_IdUsuarioPreside;
            IdUsuarioSecretario = ActaBD.LLF_IdSecretario;
            IdSesionAprobada = ActaBD.LLF_IdSesionAprobada;
            NotaVotosDisidentes = ActaBD.NotaVotosDisidentes;
            NombreObjeto = ActaBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación padre
        public Acta(SGJD_ACT_TAB_ACTAS ActaBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ACT_TAB_TOMOS TomoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ACT_TAB_SESIONES SesionAprobadaBD, SGJD_ADM_TAB_USUARIOS UsuarioPresideBD, SGJD_ADM_TAB_USUARIOS UsuarioSecretarioBD) {
            if (ActaBD is null) throw new ArgumentNullException(paramName: nameof(ActaBD), message: Resources.ModeloNulo);
            if (SesionBD is null) throw new ArgumentNullException(paramName: nameof(SesionBD), message: Resources.ModeloNulo);

            Id = ActaBD.LLP_Id;
            IdSesion = ActaBD.LLF_IdSesion;
            IdTomo = ActaBD.LLF_IdTomo;
            IdEstado = ActaBD.LLF_IdEstado;
            Encabezado = ActaBD.Encabezado;
            Observacion = ActaBD.Observacion;
            UrlActaFirmada = ActaBD.UrlActaFirmada;
            ParrafoCierre = ActaBD.ParrafoCierre;
            NumeroPaginasPDF = ActaBD.NumeroPaginasPDF;
            IdUsuarioPreside = ActaBD.LLF_IdUsuarioPreside;
            IdUsuarioSecretario = ActaBD.LLF_IdSecretario;
            IdSesionAprobada = ActaBD.LLF_IdSesionAprobada;
            NotaVotosDisidentes = ActaBD.NotaVotosDisidentes;
            NombreObjeto = ActaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS);
            Tomo = new Tomo(TomoBD);
            Estado = new Estado(EstadoBD);
            SesionAprobada = (ActaBD.LLF_IdSesionAprobada > 0) ? new Sesion(SesionAprobadaBD, SesionAprobadaBD.SGJD_ADM_TAB_TIPOS_SESION, SesionAprobadaBD.SGJD_ADM_TAB_USUARIOS, SesionAprobadaBD.SGJD_ADM_TAB_ESTADOS) : new Sesion();
            UsuarioPreside = !string.IsNullOrEmpty(IdUsuarioPreside) ? new ApplicationUser(UsuarioPresideBD) : null;
            UsuarioSecretario = !string.IsNullOrEmpty(IdUsuarioSecretario) ? new ApplicationUser(UsuarioSecretarioBD) : null;
        }

        // Constructor para atributos y propiedades de navegación hijo
        public Acta(SGJD_ACT_TAB_ACTAS ActaBD, IEnumerable<SGJD_ACT_TAB_CAPITULOS> CapitulosBD) {
            if (ActaBD is null) throw new ArgumentNullException(paramName: nameof(ActaBD), message: Resources.ModeloNulo);

            Id = ActaBD.LLP_Id;
            IdSesion = ActaBD.LLF_IdSesion;
            IdTomo = ActaBD.LLF_IdTomo;
            IdEstado = ActaBD.LLF_IdEstado;
            Encabezado = ActaBD.Encabezado;
            Observacion = ActaBD.Observacion;
            UrlActaFirmada = ActaBD.UrlActaFirmada;
            ParrafoCierre = ActaBD.ParrafoCierre;
            NumeroPaginasPDF = ActaBD.NumeroPaginasPDF;
            IdUsuarioPreside = ActaBD.LLF_IdUsuarioPreside;
            IdUsuarioSecretario = ActaBD.LLF_IdSecretario;
            IdSesionAprobada = ActaBD.LLF_IdSesionAprobada;
            NotaVotosDisidentes = ActaBD.NotaVotosDisidentes;
            NombreObjeto = ActaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Capitulos = CapitulosBD.Select(CapituloBD => new Capitulo(CapituloBD)).ToList();
        }

        /// <summary>
        /// Constructor para atributos y propiedades de navegación padre e hijo
        /// </summary>
        public Acta(SGJD_ACT_TAB_ACTAS ActaBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ACT_TAB_TOMOS TomoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ACT_TAB_SESIONES SesionAprobadaBD, SGJD_ADM_TAB_USUARIOS UsuarioPresideBD, SGJD_ADM_TAB_USUARIOS UsuarioSecretarioBD, IEnumerable<SGJD_ACT_TAB_CAPITULOS> CapitulosBD) {
            if (ActaBD is null) throw new ArgumentNullException(paramName: nameof(ActaBD), message: Resources.ModeloNulo);
            if (SesionBD is null) throw new ArgumentNullException(paramName: nameof(SesionBD), message: Resources.ModeloNulo);
            if (EstadoBD is null) throw new ArgumentNullException(paramName: nameof(EstadoBD), message: Resources.ModeloNulo);

            Id = ActaBD.LLP_Id;
            IdSesion = ActaBD.LLF_IdSesion;
            IdTomo = ActaBD.LLF_IdTomo;
            IdEstado = ActaBD.LLF_IdEstado;
            Encabezado = ActaBD.Encabezado;
            Observacion = ActaBD.Observacion;
            UrlActaFirmada = ActaBD.UrlActaFirmada;
            ParrafoCierre = ActaBD.ParrafoCierre;
            NumeroPaginasPDF = ActaBD.NumeroPaginasPDF;
            IdUsuarioPreside = ActaBD.LLF_IdUsuarioPreside;
            IdUsuarioSecretario = ActaBD.LLF_IdSecretario;
            IdSesionAprobada = ActaBD.LLF_IdSesionAprobada;
            NotaVotosDisidentes = ActaBD.NotaVotosDisidentes;
            NombreObjeto = ActaBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS);
            Tomo = new Tomo(TomoBD);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
            SesionAprobada = (ActaBD.LLF_IdSesionAprobada > 0) ? new Sesion(SesionAprobadaBD, SesionAprobadaBD.SGJD_ADM_TAB_TIPOS_SESION, SesionAprobadaBD.SGJD_ADM_TAB_USUARIOS, SesionAprobadaBD.SGJD_ADM_TAB_ESTADOS) : null;
            UsuarioPreside = string.IsNullOrEmpty(IdUsuarioPreside) ? null : new ApplicationUser(UsuarioPresideBD);
            UsuarioSecretario = string.IsNullOrEmpty(IdUsuarioSecretario) ? null : new ApplicationUser(UsuarioSecretarioBD);

            Capitulos = CapitulosBD.Select(CapituloBD => new Capitulo(CapituloBD, CapituloBD.SGJD_ACT_TAB_ARTICULOS)).ToList();
        }

        // Constructor para crear un acta basado en una sesión seleccionada, incluyendo el orden del día
        public Acta(Sesion ModeloSesion, Estado ModeloEstado, TipoObjeto ModeloTipoObjeto, Tomo ModeloTomo, string EncabezadoActa, string IdUsuarioSecretario, string IdUsuarioPresidente) {
            if (ModeloSesion is null) throw new ArgumentNullException(paramName: nameof(ModeloSesion), message: Resources.ModeloNulo);
            if (ModeloEstado is null) throw new ArgumentNullException(paramName: nameof(ModeloEstado), message: Resources.ModeloNulo);
            if (ModeloTomo is null) throw new ArgumentNullException(paramName: nameof(ModeloTomo), message: Resources.ModeloNulo);
            if (ModeloTipoObjeto is null) throw new ArgumentNullException(paramName: nameof(ModeloTipoObjeto), message: Resources.ModeloNulo);

            IdSesion = ModeloSesion.Id;
            IdTomo = ModeloTomo.Id;
            IdEstado = ModeloEstado.Id;
            Encabezado = EncabezadoActa;
            Observacion = string.Empty;
            UrlActaFirmada = string.Empty;
            ParrafoCierre = string.Empty;
            NumeroPaginasPDF = 0;
            this.IdUsuarioPreside = IdUsuarioPresidente;
            this.IdUsuarioSecretario = IdUsuarioSecretario;
            IdSesionAprobada = null;
            NotaVotosDisidentes = string.Empty;
            NombreObjeto = ModeloTipoObjeto.NombreTabla;

            Sesion = ModeloSesion;
            Tomo = ModeloTomo;
            Estado = ModeloEstado;
        }

        // Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "La sesion es requerida.")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El tomo es requerido.")]
        public int IdTomo { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El encabezado es requerido.")]
        public string Encabezado { get; set; }

        public string Observacion { get; set; }

        public string UrlActaFirmada { get; set; }

        public string ParrafoCierre { get; set; }

        public int NumeroPaginasPDF { get; set; }

        public string IdUsuarioPreside { get; set; }

        public string IdUsuarioSecretario { get; set; }

        public int? IdSesionAprobada { get; set; }

        public string NotaVotosDisidentes { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegació
        // Padres
        public Sesion Sesion { get; set; }

        public Tomo Tomo { get; set; }

        public Estado Estado { get; set; }

        public Sesion SesionAprobada { get; set; }

        public ApplicationUser UsuarioPreside { get; set; }

        public ApplicationUser UsuarioSecretario { get; set; }

        // Hijos
        public IEnumerable<Capitulo> Capitulos { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idSesion:" + IdSesion + ", " +
                "idTomo:" + IdTomo + ", " +
                "idEstado:" + IdEstado + ", " +
                "encabezado:" + Encabezado + ", " +
                "observacion:" + Observacion + ", " +
                "urlActaFirmada:" + UrlActaFirmada + ", " +
                "parrafoCierre:" + ParrafoCierre + ", " +
                "numeroPaginasPDF:" + NumeroPaginasPDF + ", " +
                "idUsuarioPreside:" + IdUsuarioPreside + ", " +
                "idUsuarioSecretario:" + IdUsuarioSecretario + ", " +
                "idSesionAprobada:" + IdSesionAprobada + ", " +
                "notaVotasDisidentes" + NotaVotosDisidentes;
        }

        /// <summary>
        /// Crea una entidad de base de datos apartir de un modelo de Acta.
        /// </summary>
        public SGJD_ACT_TAB_ACTAS BD() {
            return new SGJD_ACT_TAB_ACTAS() {
                LLP_Id = Id,
                LLF_IdSesion = IdSesion,
                LLF_IdTomo = IdTomo,
                LLF_IdEstado = IdEstado,
                Encabezado = Encabezado,
                Observacion = Observacion,
                UrlActaFirmada = UrlActaFirmada,
                ParrafoCierre = ParrafoCierre,
                NumeroPaginasPDF = NumeroPaginasPDF,
                LLF_IdUsuarioPreside = IdUsuarioPreside,
                LLF_IdSecretario = IdUsuarioSecretario,
                LLF_IdSesionAprobada = IdSesionAprobada, 
                NotaVotosDisidentes = NotaVotosDisidentes
            };
        }
    }
}