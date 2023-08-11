using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class TipoSesion {
        // Constructores
        public TipoSesion() { }

        public TipoSesion(SGJD_ADM_TAB_TIPOS_SESION TipoSesionBD) {
            Id = TipoSesionBD.LLP_Id;
            Descripcion = TipoSesionBD.Descripcion;
            NombreObjeto = TipoSesionBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Atributos
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "descripcion:" + Descripcion;
        }

        public SGJD_ADM_TAB_TIPOS_SESION BD() {
            return new SGJD_ADM_TAB_TIPOS_SESION() {
                LLP_Id = Id,
                Descripcion = Descripcion
            };
        }
    }
}