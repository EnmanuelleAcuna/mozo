using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Votacion {
        // Constructor
        public Votacion() { }

        public Votacion(SGJD_ACU_TAB_VOTACIONES_ACUERDO VotacionBD) {
            Id = VotacionBD.LLP_Id;
            IdAsistente = VotacionBD.LLF_IdAsistente;
            IdAcuerdo = VotacionBD.LLF_IdAcuerdo;
            TipoVotacion = VotacionBD.TipoVotacion;
        }

        public Votacion(SGJD_ACU_TAB_VOTACIONES_ACUERDO VotacionBD, SGJD_ACT_TAB_ASISTENTES_SESION AsistenteBD, SGJD_ACU_TAB_ACUERDOS AcuerdoBD) {
            Id = VotacionBD.LLP_Id;
            IdAsistente = VotacionBD.LLF_IdAsistente;
            IdAcuerdo = VotacionBD.LLF_IdAcuerdo;
            TipoVotacion = VotacionBD.TipoVotacion;

            Asistente = new AsistenteSesion(AsistenteBD, AsistenteBD.SGJD_ACT_TAB_SESIONES, AsistenteBD.SGJD_ADM_TAB_USUARIOS);
            Acuerdo = new Acuerdo(AcuerdoBD);
        }

        public Votacion(int IdAsistente, int IdAcuerdo) {
            this.IdAsistente = IdAsistente;
            this.IdAcuerdo = IdAcuerdo;
            this.TipoVotacion = "A favor";
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "El Acuerdo es requerido")]
        public int IdAsistente { get; set; }

        [Required(ErrorMessage = "El Acuerdo es requerido")]
        public int IdAcuerdo { get; set; }

        [Required(ErrorMessage = "El tipo de votación es requerido")]
        public string TipoVotacion { get; set; }

        public AsistenteSesion Asistente { get; set; }

        public Acuerdo Acuerdo { get; set; }

        public string ObtenerInformacion() {
            return "Id:" + Id + ", " +
                "IdAsistente:" + IdAsistente + ", " +
                "IdAcuerdo:" + IdAcuerdo + ", " +
                "TipoVotacion:" + TipoVotacion;
        }

        public SGJD_ACU_TAB_VOTACIONES_ACUERDO BD() {
            return new SGJD_ACU_TAB_VOTACIONES_ACUERDO() {
                LLP_Id = Id,
                LLF_IdAsistente = IdAsistente,
                LLF_IdAcuerdo = IdAcuerdo,
                TipoVotacion = TipoVotacion
            };
        }
    }
}