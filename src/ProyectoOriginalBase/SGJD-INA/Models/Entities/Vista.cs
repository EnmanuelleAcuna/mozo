using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Vista {
        #region Constructores
        public Vista() { }

        public Vista(SGJD_ADM_TAB_VISTAS VistaBD) {
            Id = VistaBD.LLP_Id;
            NombreVista = VistaBD.NombreVista;
            DireccionVista = VistaBD.DireccionVista;
            NombreObjeto = VistaBD.GetType().UnderlyingSystemType.BaseType.Name;
        }
        #endregion

        #region Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la vista es requerido.")]
        public string NombreVista { get; set; }

        [Required(ErrorMessage = "La ruta de la vista es requerida.")]
        public string DireccionVista { get; set; }

        public string NombreObjeto { get; set; }
        #endregion

        #region Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "nombreVista:" + NombreVista + ", " +
                "direccionVista:" + DireccionVista;
        }

        public SGJD_ADM_TAB_VISTAS BD() {
            return new SGJD_ADM_TAB_VISTAS() {
                LLP_Id = Id,
                NombreVista = NombreVista,
                DireccionVista = DireccionVista
            };
        }
        #endregion
    }
}