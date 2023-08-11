using SGJD_INA.Models.DA.EntityFramework.SIRH;

namespace SGJD_INA.Models.Entities {
    public class UsuarioSIRH {
        public UsuarioSIRH() { }

        public UsuarioSIRH(V_SIRH_FUNC_SIRIA UsuarioSIRHBD) {
            Cedula = UsuarioSIRHBD.CEDULA;
            Nombre = UsuarioSIRHBD.NOMBRE_EMPLEADO;
            Correo = UsuarioSIRHBD.CORREO;
            NombreUsuario = UsuarioSIRHBD.USUARIO;
            NombreObjeto = UsuarioSIRHBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public decimal Cedula { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string NombreUsuario { get; set; }

        public string NombreObjeto { get; set; }

        public string ObtenerInformacion() {
            return "cedula:" + Cedula + "," +
                   "nombre:" + Nombre + "," +
                   "email:" + Correo + "," +
                   "userName:" + NombreUsuario + "," +
                   "Nombre Objeto: " + NombreObjeto;
        }
    }
}