using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    /// <summary>
    /// Clase para administrar los números de teléfono de un usuario
    /// </summary>
    public class UsuarioTelefono {
        // Constructores
        public UsuarioTelefono() { }

        public UsuarioTelefono(SGJD_ADM_TAB_TELEFONOS_USUARIO ModeloBD) {
            Id = ModeloBD.LLP_Id;
            IdUsuario = ModeloBD.LLF_IdUsuario;
            Numero = ModeloBD.Numero;
            Extension = ModeloBD.Extension;
            NombreObjeto = ModeloBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public UsuarioTelefono(SGJD_ADM_TAB_TELEFONOS_USUARIO ModeloBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = ModeloBD.LLP_Id;
            IdUsuario = ModeloBD.LLF_IdUsuario;
            Numero = ModeloBD.Numero;
            Extension = ModeloBD.Extension;
            NombreObjeto = ModeloBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuario = new ApplicationUser(UsuarioBD);
        }

        // Propiedades
        public int Id { get; set; }

        public string IdUsuario { get; set; }

        public string Numero { get; set; }

        public string Extension { get; set; }

        public string NombreObjeto { get; set; }

        public ApplicationUser Usuario { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id: " + Id + ", " +
                "idUsuario: " + IdUsuario + ", " +
                "numero: " + Numero + ", " +
                "extension: " + Extension;
        }

        public SGJD_ADM_TAB_TELEFONOS_USUARIO BD() {
            return new SGJD_ADM_TAB_TELEFONOS_USUARIO() {
                LLP_Id = Id,
                LLF_IdUsuario = IdUsuario,
                Numero = Numero,
                Extension = Extension
            };
        }
    }
}