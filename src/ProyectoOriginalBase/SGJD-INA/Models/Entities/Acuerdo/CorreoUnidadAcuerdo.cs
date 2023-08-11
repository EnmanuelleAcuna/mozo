using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class CorreoUnidadAcuerdo {
        //Constructores
        public CorreoUnidadAcuerdo() { }

        public CorreoUnidadAcuerdo(SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO CorreoUnidadAcuerdoBD) {
            Id = CorreoUnidadAcuerdoBD.LLP_Id;
            IdUnidad = CorreoUnidadAcuerdoBD.LLF_IdUnidad;
            Nombre = CorreoUnidadAcuerdoBD.Nombre;
            Correo = CorreoUnidadAcuerdoBD.Correo;
        }

        public CorreoUnidadAcuerdo(SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO CorreoUnidadAcuerdoBD, SGJD_ADM_TAB_UNIDADES UnidadBD) {
            Id = CorreoUnidadAcuerdoBD.LLP_Id;
            IdUnidad = CorreoUnidadAcuerdoBD.LLF_IdUnidad;
            Nombre = CorreoUnidadAcuerdoBD.Nombre;
            Correo = CorreoUnidadAcuerdoBD.Correo;

            Unidad = new Unidad(UnidadBD);
        }

        //Atributos
        public int Id { get; set; }

        public int IdUnidad { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        public string Correo { get; set; }

        // Propiedades de navegación
        // Padres
        public Unidad Unidad { get; set; }

        //Metodos
        public string ObtenerInformacion() {
            return "Id:" + Id + ", " +
                "IdUnidad:" + IdUnidad + ", " +
                "Nombre:" + Nombre + ", " +
                "Correo:" + Correo;
        }

        /// <summary>
        /// Convertir un modelo a entidad de base de datos
        /// </summary>
        public SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO BD() {
            return new SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO() {
                LLP_Id = Id,
                LLF_IdUnidad = IdUnidad,
                Nombre = Nombre,
                Correo = Correo
            };
        }
    }
}