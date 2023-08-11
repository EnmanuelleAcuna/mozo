using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Seccion {
        // Constructores
        public Seccion() { }

        // Constructor para llenar las propiedades del objeto
        public Seccion(SGJD_ADM_TAB_SECCIONES SeccionBD) {
            Id = SeccionBD.LLP_Id;
            Descripcion = SeccionBD.Descripcion;
            NombreObjeto = SeccionBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para llenar las propiedades del objeto y las propiedades de navegación hijo
        public Seccion(SGJD_ADM_TAB_SECCIONES SeccionBD, IEnumerable<SGJD_ACT_TAB_TEMAS> TemasBD) {
            Id = SeccionBD.LLP_Id;
            Descripcion = SeccionBD.Descripcion;
            NombreObjeto = SeccionBD.GetType().UnderlyingSystemType.BaseType.Name;

            Temas = TemasBD.Select(TemaBD => new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA)).ToList();
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Hijos
        public IEnumerable<Tema> Temas { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "Descripción:" + Descripcion;
        }

        public SGJD_ADM_TAB_SECCIONES BD() {
            return new SGJD_ADM_TAB_SECCIONES() {
                LLP_Id = Id,
                Descripcion = Descripcion
            };
        }
    }
}