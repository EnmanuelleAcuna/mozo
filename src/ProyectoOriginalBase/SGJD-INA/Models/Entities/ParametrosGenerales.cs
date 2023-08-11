using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class ParametroGeneral {
        public ParametroGeneral() { }

        public ParametroGeneral(SGJD_ADM_TAB_PARAMETROS_GENERALES ParametroBD) {
            Id = ParametroBD.LLP_Id;
            Nombre = ParametroBD.Nombre;
            Valor = ParametroBD.Valor;
            Descripcion = ParametroBD.Descripcion;
            Tipo = ParametroBD.Tipo;
            NombreObjeto = ParametroBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Propiedades
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El valor es requerido.")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo es requerido.")]
        public string Tipo { get; set; }

        public string NombreObjeto { get; set; }

        public string ObtenerInformacion() {
            return "nombreobjeto: " + NombreObjeto + ",v" +
                "id:" + Id + ", " +
                "nombre:" + Nombre + ", " +
                "valor:" + Valor + ", " +
                "descripcion:" + Descripcion + ", " +
                "tipo:" + Tipo;
        }

        public SGJD_ADM_TAB_PARAMETROS_GENERALES BD() {
            return new SGJD_ADM_TAB_PARAMETROS_GENERALES() {
                LLP_Id = Id,
                Nombre = Nombre,
                Valor = Valor,
                Descripcion = Descripcion,
                Tipo = Tipo
            };
        }
    }
}