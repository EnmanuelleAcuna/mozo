using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class DetalleSeguimiento {
        #region Constructores
        public DetalleSeguimiento() { }

        public DetalleSeguimiento(SGJD_ACU_TAB_AVANCES DetalleBD) {
            Id = DetalleBD.LLP_Id;
            IdSeguimientoAcuerdo = DetalleBD.LLF_IdSeguimientoAcuerdo;
            Descripcion = DetalleBD.Descripcion;
            FechaModificacion = DetalleBD.FechaModificacion;
            IdUsuario = DetalleBD.LLF_IdUsuario;
            NombreObjeto = DetalleBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public DetalleSeguimiento(SGJD_ACU_TAB_AVANCES DetalleBD, SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = DetalleBD.LLP_Id;
            IdSeguimientoAcuerdo = DetalleBD.LLF_IdSeguimientoAcuerdo;
            Descripcion = DetalleBD.Descripcion;
            FechaModificacion = DetalleBD.FechaModificacion;
            IdUsuario = DetalleBD.LLF_IdUsuario;
            NombreObjeto = DetalleBD.GetType().UnderlyingSystemType.BaseType.Name;

            SeguimientoAcuerdo = new SeguimientoAcuerdo(SeguimientoBD);
            Usuario = new ApplicationUser(UsuarioBD);
        }
        #endregion

        #region Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "El seguimiento es requerido.")]
        public int IdSeguimientoAcuerdo { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha del avance es requerida.")]
        public DateTime FechaModificacion { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        public string IdUsuario { get; set; }

        public string NombreObjeto { get; set; }
        #endregion

        // Propiedades de navegación
        public SeguimientoAcuerdo SeguimientoAcuerdo;

        public ApplicationUser Usuario;

        #region Métodos
        /// <summary>
        /// Obtener información de la entidad 
        /// </summary>
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idSeguimientoAcuerdo:" + IdSeguimientoAcuerdo + ", " +
                "idUsuario:" + IdUsuario + ", " +
                "fechaModificacion:" + FechaModificacion + ", " +
                "descripcion:" + Descripcion;
        }

        /// <summary>
        /// Convertir la entidad a modelo de base de datos
        /// </summary>
        public SGJD_ACU_TAB_AVANCES BD() {
            return new SGJD_ACU_TAB_AVANCES() {
                LLP_Id = Id,
                LLF_IdSeguimientoAcuerdo = IdSeguimientoAcuerdo,
                LLF_IdUsuario = IdUsuario,
                FechaModificacion = FechaModificacion,
                Descripcion = Descripcion
            };
        }
        #endregion
    }
}