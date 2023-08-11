using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class OtroAsistenteSesion {
        // Constructor
        public OtroAsistenteSesion() { }

        public OtroAsistenteSesion(SGJD_ACT_TAB_OTROS_ASISTENTES_SESION OtroAsistenteBD) {
            Id = OtroAsistenteBD.LLP_Id;
            IdSesion = OtroAsistenteBD.LLF_IdSesion;
            IdTipoUsuario = OtroAsistenteBD.LLF_IdTipoUsuario;
            Nombre = OtroAsistenteBD.Nombre;
            Puesto = OtroAsistenteBD.Puesto;
            HoraInicio = OtroAsistenteBD.HoraInicio;
            HoraFin = OtroAsistenteBD.HoraFin;
            NombreObjeto = OtroAsistenteBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public OtroAsistenteSesion(SGJD_ACT_TAB_OTROS_ASISTENTES_SESION OtroAsistenteBD, SGJD_ACT_TAB_SESIONES SesionBD, SGJD_ADM_TAB_TIPOS_USUARIO TipoUsuarioBD) {
            Id = OtroAsistenteBD.LLP_Id;
            IdSesion = OtroAsistenteBD.LLF_IdSesion;
            IdTipoUsuario = OtroAsistenteBD.LLF_IdTipoUsuario;
            Nombre = OtroAsistenteBD.Nombre;
            Puesto = OtroAsistenteBD.Puesto;
            HoraInicio = OtroAsistenteBD.HoraInicio;
            HoraFin = OtroAsistenteBD.HoraFin;
            NombreObjeto = OtroAsistenteBD.GetType().UnderlyingSystemType.BaseType.Name;

            Sesion = new Sesion(OtroAsistenteBD.SGJD_ACT_TAB_SESIONES);
            TipoUsuario = new TipoUsuario(OtroAsistenteBD.SGJD_ADM_TAB_TIPOS_USUARIO);
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "La sesión es requerida")]
        [Display(Name = "Sesión")]
        public int IdSesion { get; set; }

        [Required(ErrorMessage = "El tipo usuario es requerido")]
        [Display(Name = "Tipo Usuario")]
        public int IdTipoUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El puesto es requerido")]
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }

        [Required(ErrorMessage = "La hora de ingreso es requerida")]
        [Display(Name = "Hora de ingreso")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de salida es requerida")]
        [Display(Name = "Hora de salida")]
        public string HoraFin { get; set; }

        public string NombreObjeto { get; set; }

        public Sesion Sesion { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idSesion:" + IdSesion + ", " +
                "idTipoUsuario:" + IdTipoUsuario + ", " +
                "nombre:" + Nombre + ", " +
                "puesto:" + Puesto + ", " +
                "horaInicio:" + HoraInicio + ", " +
                "horaFin:" + HoraFin;
        }

        public SGJD_ACT_TAB_OTROS_ASISTENTES_SESION BD() {
            return new SGJD_ACT_TAB_OTROS_ASISTENTES_SESION() {
                LLP_Id = Id,
                LLF_IdSesion = IdSesion,
                LLF_IdTipoUsuario = IdTipoUsuario,
                Nombre = Nombre,
                Puesto = Puesto,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin
            };
        }

    }
}