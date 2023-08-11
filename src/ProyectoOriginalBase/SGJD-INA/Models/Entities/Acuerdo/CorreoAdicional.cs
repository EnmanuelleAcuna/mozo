using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class CorreoAdicional {
        //Constructores
        public CorreoAdicional() { }

        public CorreoAdicional(SGJD_ACU_TAB_CORREOS_ADICIONALES CorreoAdicionalBD) {
            Id = CorreoAdicionalBD.LLP_Id;
            Correo = CorreoAdicionalBD.Correo;
            Nombre = CorreoAdicionalBD.Nombre;
            NombreObjeto = CorreoAdicionalBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        //Propiedades
        public int Id { get; set; }

        public string Correo { get; set; }

        public string Nombre { get; set; }

        public string NombreObjeto { get; set; }

        //Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto:" + NombreObjeto + "," +
                "id:" + Id + ", " +
                "correo" + Correo + ", " +
                "nombre" + Nombre;
        }

        public SGJD_ACU_TAB_CORREOS_ADICIONALES BD() {
            return new SGJD_ACU_TAB_CORREOS_ADICIONALES() {
                LLP_Id = Id,
                Correo = Correo,
                Nombre = Nombre
            };
        }
    }
}