using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    /// <summary>
    /// Clase para administrar los correos de un usuario
    /// </summary>
    public class UsuarioCorreo {
        // Constructores
        public UsuarioCorreo() { }

        public UsuarioCorreo(SGJD_ADM_TAB_CORREOS_USUARIO ModeloBD) {
            Id = ModeloBD.LLP_Id;
            IdUsuario = ModeloBD.LLF_IdUsuario;
            Correo = ModeloBD.Correo;
            NombreObjeto = ModeloBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public UsuarioCorreo(SGJD_ADM_TAB_CORREOS_USUARIO ModeloBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = ModeloBD.LLP_Id;
            IdUsuario = ModeloBD.LLF_IdUsuario;
            Correo = ModeloBD.Correo;
            NombreObjeto = ModeloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuario = new ApplicationUser(UsuarioBD);
        }

        // Propiedades
        public int Id { get; set; }

        public string IdUsuario { get; set; }

        public string Correo { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades padre
        public ApplicationUser Usuario { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id: " + Id + ", " +
                "idUsuario: " + IdUsuario + ", " +
                "correo: " + Correo;
        }

        public SGJD_ADM_TAB_CORREOS_USUARIO BD() {
            return new SGJD_ADM_TAB_CORREOS_USUARIO() {
                LLP_Id = Id,
                LLF_IdUsuario = IdUsuario,
                Correo = Correo
            };
        }
    }
}