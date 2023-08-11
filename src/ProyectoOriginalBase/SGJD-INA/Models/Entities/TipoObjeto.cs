using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class TipoObjeto {
        // Constructores
        public TipoObjeto() { }

        public TipoObjeto(SGJD_ADM_TAB_TIPOS_OBJETO TipoObjetoBD) {
            Id = TipoObjetoBD.LLP_Id;
            Descripcion = TipoObjetoBD.Descripcion;
            NombreTabla = TipoObjetoBD.NombreTabla;
            ParametroEdicion = TipoObjetoBD.ParametroEdicion;
            ParametroEncabezadoPie = TipoObjetoBD.ParametroEncabezadoPie;
            Consecutivo = TipoObjetoBD.Consecutivo;
            Nomenclatura = TipoObjetoBD.Nomenclatura;
            NombreObjeto = TipoObjetoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Propiedades
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string NombreTabla { get; set; }

        public bool ParametroEdicion { get; set; }

        public bool ParametroEncabezadoPie { get; set; }

        public int Consecutivo { get; set; }

        public string Nomenclatura { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "descripcion:" + Descripcion + ", " +
                "nombreTabla:" + NombreTabla + ", " +
                "consecutivo:" + Consecutivo + ", " +
                "nomenclatura:" + Nomenclatura + ", " +
                "parametroEdicion" + ParametroEdicion;
        }

        public SGJD_ADM_TAB_TIPOS_OBJETO BD() {
            return new SGJD_ADM_TAB_TIPOS_OBJETO() {
                LLP_Id = Id,
                Descripcion = Descripcion,
                NombreTabla = NombreTabla,
                ParametroEdicion = ParametroEdicion,
                ParametroEncabezadoPie = ParametroEncabezadoPie,
                Consecutivo = Consecutivo,
                Nomenclatura = Nomenclatura
            };
        }
    }
}