using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SGJD_INA.Models.Entities {
    public class Unidad {
        // Constructores
        public Unidad() {
            Usuarios = new HashSet<ApplicationUser>();
        }

        public Unidad(SGJD_ADM_TAB_UNIDADES UnidadBD) {
            Id = UnidadBD.LLP_Id;
            Nombre = UnidadBD.Nombre;
            Correo = UnidadBD.Correo;
            NombreObjeto = UnidadBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public Unidad(SGJD_ADM_TAB_UNIDADES UnidadBD, ICollection<SGJD_ADM_TAB_USUARIOS> UsuariosBD) {
            Id = UnidadBD.LLP_Id;
            Nombre = UnidadBD.Nombre;
            Correo = UnidadBD.Correo;
            NombreObjeto = UnidadBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuarios = UsuariosBD.Select(U => new ApplicationUser(U)).ToList();
        }

        public Unidad(SGJD_ADM_TAB_UNIDADES UnidadBD, ICollection<SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO> CorreosUnidadAcuerdoBD) {
            Id = UnidadBD.LLP_Id;
            Nombre = UnidadBD.Nombre;
            Correo = UnidadBD.Correo;
            NombreObjeto = UnidadBD.GetType().UnderlyingSystemType.BaseType.Name;

            CorreosUnidadAcuerdo = CorreosUnidadAcuerdoBD.Select(cua => new CorreoUnidadAcuerdo(cua)).ToList();
        }

        // Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string Correo { get; set; }

        [NotMapped]
        public string NombreObjeto { get; set; }

        public ICollection<ApplicationUser> Usuarios { get; set; }

        public ICollection<CorreoUnidadAcuerdo> CorreosUnidadAcuerdo { get; set; }
        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "nombre:" + Nombre + ", " +
                "correo:" + Correo;
        }

        public string ObtenerInformacion(SGJD_ADM_TAB_UNIDADES UnidadBD) {
            string Informacion = string.Empty;
            foreach (PropertyInfo Propiedad in UnidadBD.GetType().GetProperties()) {
                Informacion += string.Format("{0}: {1};", Propiedad.Name, Propiedad.GetValue(UnidadBD));
            }
            return Informacion;
        }

        /// <summary>
        /// Convertir el modelo a entidad de base de datos.
        /// </summary>
        public SGJD_ADM_TAB_UNIDADES BD() {
            return new SGJD_ADM_TAB_UNIDADES() {
                LLP_Id = Id,
                Nombre = Nombre,
                Correo = Correo
            };
        }
    }
}