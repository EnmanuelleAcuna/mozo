using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class TipoArchivo {
        // Constructores
        public TipoArchivo() { }

        public TipoArchivo(SGJD_ADM_TAB_TIPOS_ARCHIVO TipoArchivoBD) {
            Id = TipoArchivoBD.LLP_Id;
            Nombre = TipoArchivoBD.Nombre;
            Descripcion = TipoArchivoBD.Descripcion;
            NombreObjeto = TipoArchivoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Propiedades
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "NombreObjeto: " + NombreObjeto + ", " +
                "Id:" + Id + ", " +
                "Nombre:" + Nombre + ", " +
                "Descripcion:" + Descripcion;
        }

        public SGJD_ADM_TAB_TIPOS_ARCHIVO BD() {
            return new SGJD_ADM_TAB_TIPOS_ARCHIVO() {
                LLP_Id = Id,
                Nombre = Nombre,
                Descripcion = Descripcion
            };
        }
    }
}