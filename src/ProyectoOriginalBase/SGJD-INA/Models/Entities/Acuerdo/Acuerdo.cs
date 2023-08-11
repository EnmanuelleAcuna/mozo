using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Acuerdo {
        // Constructores
        public Acuerdo() { }

        // Constructor para propiedades
        public Acuerdo(SGJD_ACU_TAB_ACUERDOS AcuerdoBD) {
            Id = AcuerdoBD.LLP_Id;
            IdArticulo = AcuerdoBD.LLF_IdArticulo;
            IdEstado = AcuerdoBD.LLF_IdEstado;
            IdTipoAprobacion = AcuerdoBD.LLF_IdTipoAprobacion;
            Titulo = AcuerdoBD.Titulo;
            NumeroAcuerdo = AcuerdoBD.NumeroAcuerdo;
            Firme = AcuerdoBD.Firme;
            FechaFirmeza = AcuerdoBD.FechaFirmeza;
            Asunto = AcuerdoBD.Asunto;
            NumeroVersion = AcuerdoBD.NumeroVersion;
            Detalle = AcuerdoBD.Detalle;
            Observaciones = AcuerdoBD.Observaciones;
            TipoSeguimiento = AcuerdoBD.TipoSeguimiento;
            Revision = AcuerdoBD.Revision;
            DetalleConsiderando = AcuerdoBD.DetalleConsiderando;
            DetallePorTanto = AcuerdoBD.DetallePorTanto;
            UrlAcuerdoFirmado = AcuerdoBD.UrlAcuerdoFirmado;
            ObservacionVotaciones = AcuerdoBD.ObservacionVotaciones;
            FechaNotificacion = AcuerdoBD.FechaNotificacion;
            FechaCreacion = AcuerdoBD.FechaCreacion;
            NombreObjeto = AcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para propiedades y propiedades de navegación padres
        public Acuerdo(SGJD_ACU_TAB_ACUERDOS AcuerdoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ACT_TAB_ARTICULOS ArticuloBD, SGJD_ADM_TAB_TIPOS_APROBACION TipoAprobacionBD) {
            Id = AcuerdoBD.LLP_Id;
            IdArticulo = AcuerdoBD.LLF_IdArticulo;
            IdEstado = AcuerdoBD.LLF_IdEstado;
            IdTipoAprobacion = AcuerdoBD.LLF_IdTipoAprobacion;
            Titulo = AcuerdoBD.Titulo;
            NumeroAcuerdo = AcuerdoBD.NumeroAcuerdo;
            Firme = AcuerdoBD.Firme;
            FechaFirmeza = AcuerdoBD.FechaFirmeza;
            Asunto = AcuerdoBD.Asunto;
            NumeroVersion = AcuerdoBD.NumeroVersion;
            Detalle = AcuerdoBD.Detalle;
            Observaciones = AcuerdoBD.Observaciones;
            TipoSeguimiento = AcuerdoBD.TipoSeguimiento;
            Revision = AcuerdoBD.Revision;
            DetalleConsiderando = AcuerdoBD.DetalleConsiderando;
            DetallePorTanto = AcuerdoBD.DetallePorTanto;
            UrlAcuerdoFirmado = AcuerdoBD.UrlAcuerdoFirmado;
            ObservacionVotaciones = AcuerdoBD.ObservacionVotaciones;
            FechaNotificacion = AcuerdoBD.FechaNotificacion;
            FechaCreacion = AcuerdoBD.FechaCreacion;
            NombreObjeto = AcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Articulo = new Articulo(ArticuloBD);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
            TipoAprobacion = new TipoAprobacion(TipoAprobacionBD);
        }

        // Constructor para propiedades y propiedades de navegación hijos
        public Acuerdo(SGJD_ACU_TAB_ACUERDOS AcuerdoBD, ICollection<SGJD_ACU_TAB_SEGUIMIENTOS> SeguimientosBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesEjecucionBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesInformacionBD, ICollection<SGJD_ACU_TAB_CORREOS_ADICIONALES> CorreosAdicionalesBD) {
            Id = AcuerdoBD.LLP_Id;
            IdArticulo = AcuerdoBD.LLF_IdArticulo;
            IdEstado = AcuerdoBD.LLF_IdEstado;
            IdTipoAprobacion = AcuerdoBD.LLF_IdTipoAprobacion;
            Titulo = AcuerdoBD.Titulo;
            NumeroAcuerdo = AcuerdoBD.NumeroAcuerdo;
            Firme = AcuerdoBD.Firme;
            FechaFirmeza = AcuerdoBD.FechaFirmeza;
            Asunto = AcuerdoBD.Asunto;
            NumeroVersion = AcuerdoBD.NumeroVersion;
            Detalle = AcuerdoBD.Detalle;
            Observaciones = AcuerdoBD.Observaciones;
            TipoSeguimiento = AcuerdoBD.TipoSeguimiento;
            Revision = AcuerdoBD.Revision;
            DetalleConsiderando = AcuerdoBD.DetalleConsiderando;
            DetallePorTanto = AcuerdoBD.DetallePorTanto;
            UrlAcuerdoFirmado = AcuerdoBD.UrlAcuerdoFirmado;
            ObservacionVotaciones = AcuerdoBD.ObservacionVotaciones;
            FechaNotificacion = AcuerdoBD.FechaNotificacion;
            FechaCreacion = AcuerdoBD.FechaCreacion;
            NombreObjeto = AcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Seguimientos = SeguimientosBD.Select(SeguimientoBD => new SeguimientoAcuerdo(SeguimientoBD, SeguimientoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoBD.SGJD_ADM_TAB_ESTADOS));
            UnidadesEjecucion = UnidadesEjecucionBD.Select(u => new Unidad(u, u.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
            UnidadesInformacion = UnidadesInformacionBD.Select(u => new Unidad(u, u.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
            CorreosAdicionales = CorreosAdicionalesBD.Select(ca => new CorreoAdicional(ca));
        }

        // Constructor para propiedades y propiedades de navecion padres e hijos
        public Acuerdo(SGJD_ACU_TAB_ACUERDOS AcuerdoBD, SGJD_ACT_TAB_ARTICULOS ArticuloBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ADM_TAB_TIPOS_APROBACION TipoAprobacionBD, ICollection<SGJD_ACU_TAB_SEGUIMIENTOS> SeguimientosBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesEjecucionBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesInformacionBD, ICollection<SGJD_ACU_TAB_CORREOS_ADICIONALES> CorreosAdicionalesBD) {
            Id = AcuerdoBD.LLP_Id;
            IdArticulo = AcuerdoBD.LLF_IdArticulo;
            IdEstado = AcuerdoBD.LLF_IdEstado;
            IdTipoAprobacion = AcuerdoBD.LLF_IdTipoAprobacion;
            Titulo = AcuerdoBD.Titulo;
            NumeroAcuerdo = AcuerdoBD.NumeroAcuerdo;
            Firme = AcuerdoBD.Firme;
            FechaFirmeza = AcuerdoBD.FechaFirmeza;
            Asunto = AcuerdoBD.Asunto;
            NumeroVersion = AcuerdoBD.NumeroVersion;
            Detalle = AcuerdoBD.Detalle;
            Observaciones = AcuerdoBD.Observaciones;
            TipoSeguimiento = AcuerdoBD.TipoSeguimiento;
            Revision = AcuerdoBD.Revision;
            DetalleConsiderando = AcuerdoBD.DetalleConsiderando;
            DetallePorTanto = AcuerdoBD.DetallePorTanto;
            UrlAcuerdoFirmado = AcuerdoBD.UrlAcuerdoFirmado;
            ObservacionVotaciones = AcuerdoBD.ObservacionVotaciones;
            FechaNotificacion = AcuerdoBD.FechaNotificacion;
            FechaCreacion = AcuerdoBD.FechaCreacion;
            NombreObjeto = AcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Articulo = new Articulo(ArticuloBD, ArticuloBD.SGJD_ACT_TAB_CAPITULOS, ArticuloBD.SGJD_ACT_TAB_TEMAS, ArticuloBD.SGJD_ACT_TAB_USUARIOS_ARTICULO);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
            TipoAprobacion = new TipoAprobacion(TipoAprobacionBD);

            Seguimientos = SeguimientosBD.Select(SeguimientoBD => new SeguimientoAcuerdo(SeguimientoBD)).ToList();
            UnidadesEjecucion = UnidadesEjecucionBD.Select(u => new Unidad(u, u.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
            UnidadesInformacion = UnidadesInformacionBD.Select(u => new Unidad(u, u.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
            CorreosAdicionales = CorreosAdicionalesBD.Select(ca => new CorreoAdicional(ca)).ToList();
        }

        // Constructor para crear un acuerdo basado en un articulo seleccionado
        public Acuerdo(Articulo ArticuloModel, Estado EstadoModel, TipoObjeto TipoObjetoModel) {
            IdArticulo = ArticuloModel.Id;
            IdEstado = EstadoModel.Id;
            this.NumeroAcuerdo = TipoObjetoModel.Consecutivo;
            Titulo = string.Format("{0}-{1}-{2}", TipoObjetoModel.Nomenclatura, NumeroAcuerdo, DateTime.Now.Year);
            Asunto = string.Empty;
            Firme = false;
            FechaFirmeza = null;
            NumeroVersion = 1;
            Detalle = "QUE EN LA SESIÓN ORDINARIA Nº. 9999 CELEBRADA EL 01 DE ENERO DEL 2020, LA JUNTA DIRECTIVA TOMÓ EL SIGUIENTE ACUERDO, SEGUN CONSTA EN EL ACTA DE ESA SESIÓN, EN SU ARTÍCULO IV:";
            Observaciones = null;
            TipoSeguimiento = 1;
            Revision = true;
            IdTipoAprobacion = 1;
            DetalleConsiderando = string.Empty;
            DetallePorTanto = string.Empty;
            UrlAcuerdoFirmado = null;
            ObservacionVotaciones = null;
            FechaNotificacion = null;
            FechaCreacion = DateTime.Now;
            NombreObjeto = TipoObjetoModel.NombreTabla;
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "El artículo es requerido.")]
        public int IdArticulo { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "La aprobacion del acuerdo.")]
        public int IdTipoAprobacion { get; set; }

        [Required(ErrorMessage = "El número es requerido.")]
        public int NumeroAcuerdo { get; set; }

        [Required(ErrorMessage = "El título es requerido.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "el estado de firmeza es requerido.")]
        public bool Firme { get; set; }

        public DateTime? FechaFirmeza { get; set; }

        [Required(ErrorMessage = "El asunto es requerido.")]
        public string Asunto { get; set; }

        public string Detalle { get; set; }

        [Required(ErrorMessage = "El número de versión es requerido.")]
        public int NumeroVersion { get; set; }

        public string Observaciones { get; set; }

        // TODO: EA: Actualizar campo en BD para que no sea nullable
        // 0 = Sin seguimiento, 1 = Con seguimiento, 2 = Seguimiento permanente
        [Required(ErrorMessage = "El tipo de seguimiento es requerido.")]
        public int TipoSeguimiento { get; set; }

        [Required(ErrorMessage = "El estado de revisión es requerido.")]
        public bool Revision { get; set; }

        [Required(ErrorMessage = "El considerando es requerido")]
        public string DetalleConsiderando { get; set; }

        [Required(ErrorMessage = "El por tanto es requerido")]
        public string DetallePorTanto { get; set; }

        public string UrlAcuerdoFirmado { get; set; }

        [Required(ErrorMessage = "Observacion votaciones")]
        public string ObservacionVotaciones { get; set; }

        [Required(ErrorMessage = "Fecha de notificaciòn")]
        public DateTime? FechaNotificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padres
        public Articulo Articulo { get; set; }

        public Estado Estado { get; set; }

        public TipoAprobacion TipoAprobacion { get; set; }

        // Hijos
        public IEnumerable<SeguimientoAcuerdo> Seguimientos { get; set; }

        public IEnumerable<Unidad> UnidadesEjecucion { get; set; }

        public IEnumerable<Unidad> UnidadesInformacion { get; set; }

        public IEnumerable<CorreoAdicional> CorreosAdicionales { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre objeto: " + NombreObjeto + "," +
                "Id:" + Id + ", " +
                "IdArticulo:" + IdArticulo + ", " +
                "IdEstadoo:" + IdEstado + ", " +
                "IdTipoAprobacion:" + IdTipoAprobacion + ", " +
                "NumeroAcuerdo:" + NumeroAcuerdo + ", " +
                "Titulo:" + Titulo + ", " +
                "Firme" + Firme + ", " +
                "FechaFirmeza" + FechaFirmeza + ", " +
                "Detalle" + Detalle + ", " +
                "Asunto" + Asunto + ", " +
                "NumeroVersion" + NumeroVersion + ", " +
                "Observaciones" + Observaciones + ", " +
                "TipoSeguimiento" + TipoSeguimiento + ", " +
                "Revision" + Revision + ", " +
                "Considerando" + DetalleConsiderando + ", " +
                "PorTanto" + DetallePorTanto + ", " +
                "UrlAcuerdoFirmado" + UrlAcuerdoFirmado + ", " +
                "ObservacionVotaciones" + ObservacionVotaciones + ", " +
                "FechaNotificacion" + FechaNotificacion + ", " +
                "FechaCreacion" + FechaCreacion;
        }

        /// <summary>
        /// Convertir entidad a modelo de base de datos
        /// </summary>
        public SGJD_ACU_TAB_ACUERDOS BD() {
            return new SGJD_ACU_TAB_ACUERDOS() {
                LLP_Id = Id,
                LLF_IdArticulo = IdArticulo,
                LLF_IdEstado = IdEstado,
                LLF_IdTipoAprobacion = IdTipoAprobacion,
                NumeroAcuerdo = NumeroAcuerdo,
                Titulo = Titulo,
                Firme = Firme,
                FechaFirmeza = FechaFirmeza,
                Detalle = Detalle,
                Asunto = Asunto,
                NumeroVersion = NumeroVersion,
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
    }
}