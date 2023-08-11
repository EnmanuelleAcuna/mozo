using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class AsistenteSesion {
        // Constructores
        public AsistenteSesion() { }

        // Constructor para nuevo asistente basado en la sesión y usuario especifico
        public AsistenteSesion(int IdSesion, string IdUsuario) {
            this.IdSesion = IdSesion;
            this.IdUsuario = IdUsuario;
            TipoAsistencia = "No presente";
            HoraInicio = null;
            HoraFin = null;
        }

        // Constructor para atributos
        public AsistenteSesion(SGJD_ACT_TAB_ASISTENTES_SESION AsistenteBD) {
            Id = AsistenteBD.LLP_Id;
            IdSesion = AsistenteBD.LLF_IdSesion;
            IdUsuario = AsistenteBD.LLF_IdUsuario;
            TipoAsistencia = AsistenteBD.Tipo;
            HoraInicio = AsistenteBD.HoraInicio;
            HoraFin = AsistenteBD.HoraFin;
            NombreObjeto = AsistenteBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación
        public AsistenteSesion(SGJD_ACT_TAB_ASISTENTES_SESION AsistenteBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = AsistenteBD.LLP_Id;
            IdSesion = AsistenteBD.LLF_IdSesion;
            IdUsuario = AsistenteBD.LLF_IdUsuario;
            TipoAsistencia = AsistenteBD.Tipo;
            HoraInicio = AsistenteBD.HoraInicio;
            HoraFin = AsistenteBD.HoraFin;
            NombreObjeto = AsistenteBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(AsistenteBD.SGJD_ACT_TAB_SESIONES);
            Usuario = new ApplicationUser(AsistenteBD.SGJD_ADM_TAB_USUARIOS, AsistenteBD.SGJD_ADM_TAB_USUARIOS.SGJD_ADM_TAB_ROLES);
        }

        // Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "La sesión es requerida")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "Tipo de Asistencia")]
        public string TipoAsistencia { get; set; }

        public string HoraInicio { get; set; }

        public string HoraFin { get; set; }

        public string NombreObjeto { get; set; }

        public Sesion Sesion { get; set; }

        public ApplicationUser Usuario { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idSesion:" + IdSesion + ", " +
                "idUsuario:" + IdUsuario + ", " +
                "tipoAsistencia:" + TipoAsistencia + ", " +
                "horaInicio:" + HoraInicio + ", " +
                "horaFin:" + HoraFin;
        }

        public SGJD_ACT_TAB_ASISTENTES_SESION BD() {
            return new SGJD_ACT_TAB_ASISTENTES_SESION() {
                LLP_Id = Id,
                LLF_IdUsuario = IdUsuario,
                LLF_IdSesion = IdSesion,
                Tipo = TipoAsistencia,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin
            };
        }
    }
}