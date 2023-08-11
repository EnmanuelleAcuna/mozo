using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    /// <summary>
    /// Clase que enlista los usuarios miembros de JD
    /// </summary>
    public class MiembroJD {
        public MiembroJD() { }

        public MiembroJD(string IdUsuario) {
            this.IdUsuario = IdUsuario;
        }

        public MiembroJD(SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA MiembroJDBD) {
            IdUsuario = MiembroJDBD.LLF_IdUsuario;
            Usuario = new ApplicationUser(MiembroJDBD.SGJD_ADM_TAB_USUARIOS);
        }

        [Required(ErrorMessage = "El id del usuario es requerido")]
        [Display(Name = "Id de usuario")]
        public string IdUsuario { get; set; }

        public string NombreObjeto { get; set; }

        public ApplicationUser Usuario { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "idUsuario: " + IdUsuario;
        }

        public SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA BD() {
            return new SGJD_ACT_TAB_MIEMBROS_JUNTA_DIRECTIVA() {
                LLF_IdUsuario = IdUsuario
            };
        }
    }
}