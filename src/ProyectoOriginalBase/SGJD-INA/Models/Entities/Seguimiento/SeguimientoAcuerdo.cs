using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class SeguimientoAcuerdo {
        // Constructores
        public SeguimientoAcuerdo() { }

        // Constructor para propiedades
        public SeguimientoAcuerdo(SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoAcuerdoBD) {
            Id = SeguimientoAcuerdoBD.LLP_Id;
            IdAcuerdo = SeguimientoAcuerdoBD.LLF_IdAcuerdo;
            FechaNotificacion = SeguimientoAcuerdoBD.FechaNotificacion;
            FechaVencimiento = SeguimientoAcuerdoBD.FechaVencimiento;
            Observaciones = SeguimientoAcuerdoBD.Observaciones;
            PlazoDias = SeguimientoAcuerdoBD.PlazoDias;
            PorcentajeAvance = SeguimientoAcuerdoBD.PorcentajeAvance;
            IdEstado = SeguimientoAcuerdoBD.LLF_IdEstado;
            NombreObjeto = SeguimientoAcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para propiedades y propiedades de navegación padres
        public SeguimientoAcuerdo(SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoAcuerdoBD, SGJD_ACU_TAB_ACUERDOS AcuerdoBD, SGJD_ADM_TAB_ESTADOS EstadoBD) {
            Id = SeguimientoAcuerdoBD.LLP_Id;
            IdAcuerdo = SeguimientoAcuerdoBD.LLF_IdAcuerdo;
            FechaNotificacion = SeguimientoAcuerdoBD.FechaNotificacion;
            FechaVencimiento = SeguimientoAcuerdoBD.FechaVencimiento;
            Observaciones = SeguimientoAcuerdoBD.Observaciones;
            PlazoDias = SeguimientoAcuerdoBD.PlazoDias;
            PorcentajeAvance = SeguimientoAcuerdoBD.PorcentajeAvance;
            IdEstado = SeguimientoAcuerdoBD.LLF_IdEstado;
            NombreObjeto = SeguimientoAcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Acuerdo = new Acuerdo(AcuerdoBD);
            Estado = new Estado(EstadoBD, EstadoBD.SGJD_ADM_TAB_TIPOS_OBJETO, EstadoBD.SGJD_ADM_TAB_AVISOS);
        }

        // Constructor para propiedades y propiedades de navegación hijos
        public SeguimientoAcuerdo(SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoAcuerdoBD, ICollection<SGJD_ACU_TAB_AVANCES> DetallesBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesBD) {
            Id = SeguimientoAcuerdoBD.LLP_Id;
            IdAcuerdo = SeguimientoAcuerdoBD.LLF_IdAcuerdo;
            FechaNotificacion = SeguimientoAcuerdoBD.FechaNotificacion;
            FechaVencimiento = SeguimientoAcuerdoBD.FechaVencimiento;
            Observaciones = SeguimientoAcuerdoBD.Observaciones;
            PlazoDias = SeguimientoAcuerdoBD.PlazoDias;
            PorcentajeAvance = SeguimientoAcuerdoBD.PorcentajeAvance;
            IdEstado = SeguimientoAcuerdoBD.LLF_IdEstado;
            NombreObjeto = SeguimientoAcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Detalles = DetallesBD.Select(DetalleBD => new DetalleSeguimiento(DetalleBD));
            Unidades = UnidadesBD.Select(UnidadBD => new Unidad(UnidadBD)).ToList();
        }

        // Constructor para propiedades y propiedades de navecion padres e hijos
        public SeguimientoAcuerdo(SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoAcuerdoBD, SGJD_ACU_TAB_ACUERDOS AcuerdoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, ICollection<SGJD_ACU_TAB_AVANCES> DetallesBD, ICollection<SGJD_ADM_TAB_UNIDADES> UnidadesBD) {
            Id = SeguimientoAcuerdoBD.LLP_Id;
            IdAcuerdo = SeguimientoAcuerdoBD.LLF_IdAcuerdo;
            FechaNotificacion = SeguimientoAcuerdoBD.FechaNotificacion;
            FechaVencimiento = SeguimientoAcuerdoBD.FechaVencimiento;
            Observaciones = SeguimientoAcuerdoBD.Observaciones;
            PlazoDias = SeguimientoAcuerdoBD.PlazoDias;
            PorcentajeAvance = SeguimientoAcuerdoBD.PorcentajeAvance;
            IdEstado = SeguimientoAcuerdoBD.LLF_IdEstado;
            NombreObjeto = SeguimientoAcuerdoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Acuerdo = new Acuerdo(AcuerdoBD);
            Estado = new Estado(EstadoBD);
            Detalles = DetallesBD.Select(DetalleBD => new DetalleSeguimiento(DetalleBD, DetalleBD.SGJD_ACU_TAB_SEGUIMIENTOS, DetalleBD.SGJD_ADM_TAB_USUARIOS)).ToList();
            Unidades = UnidadesBD.Select(UnidadBD => new Unidad(UnidadBD, UnidadBD.SGJD_ADM_TAB_USUARIOS)).ToList();
        }

        // Constructor para seguimiento a partir de un acuerdo seleccionado
        public SeguimientoAcuerdo(Acuerdo AcuerdoModel, Estado EstadoModel) {
            this.IdAcuerdo = AcuerdoModel.Id;
            IdEstado = EstadoModel.Id;
            PorcentajeAvance = 0;
            FechaNotificacion = AcuerdoModel.FechaNotificacion;
            FechaVencimiento = null;
            PlazoDias = 0;
            Observaciones = string.Empty;
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "El acuerdo es requerido.")]
        public int IdAcuerdo { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El porcentaje de avance es requerdido.")]
        public int PorcentajeAvance { get; set; }

        public DateTime? FechaNotificacion { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [Required(ErrorMessage = "Las observaciones son requeridas.")]
        public string Observaciones { get; set; }

        public int PlazoDias { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padres
        public Acuerdo Acuerdo { get; set; }

        public Estado Estado { get; set; }

        // Hijos
        public IEnumerable<DetalleSeguimiento> Detalles { get; set; }

        public IEnumerable<Unidad> Unidades { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "Id:" + Id + ", " +
                "IdAcuerdo:" + IdAcuerdo + ", " +
                "EstadoEjecucion" + IdEstado + ", " +
                "FechaNotificacion" + FechaNotificacion + ", " +
                "FechaVencimiento" + FechaVencimiento + ", " +
                "Observaciones" + Observaciones + ", " +
                "PlazoDias" + PlazoDias + ", " +
                "PorcentajeAvance" + PorcentajeAvance;
        }

        /// <summary>
        /// Convertir un modelo a entidad de base de datos
        /// </summary>
        public SGJD_ACU_TAB_SEGUIMIENTOS BD() {
            return new SGJD_ACU_TAB_SEGUIMIENTOS() {
                LLP_Id = Id,
                LLF_IdAcuerdo = IdAcuerdo,
                LLF_IdEstado = IdEstado,
                FechaNotificacion = FechaNotificacion,
                FechaVencimiento = FechaVencimiento,
                Observaciones = Observaciones,
                PlazoDias = PlazoDias,
                PorcentajeAvance = PorcentajeAvance
            };
        }
    }
}