using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class UnidadPredeterminada {
        // Constructores
        public UnidadPredeterminada() { }

        public UnidadPredeterminada(int IdUnidad) {
            this.IdUnidad = IdUnidad;
        }

        public UnidadPredeterminada(SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS UnidadesPredBD) {
            IdUnidad = UnidadesPredBD.LLF_IdUnidad;
        }

        public UnidadPredeterminada(SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS UnidadPredeterminadaBD, SGJD_ADM_TAB_UNIDADES UnidadBD) {
            IdUnidad = UnidadPredeterminadaBD.LLF_IdUnidad;
            Unidad = new Unidad(UnidadBD);
        }

        // Atributos
        [Required(ErrorMessage = "El id de la unidad es requerida")]
        public int IdUnidad { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padre
        public Unidad Unidad { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "idUnidad: " + IdUnidad;
        }

        public SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS BD() {
            return new SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS() {
                LLF_IdUnidad = IdUnidad
            };
        }
    }
}