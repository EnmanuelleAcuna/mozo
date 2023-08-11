using Microsoft.AspNet.Identity.EntityFramework;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGJD_INA.Models.Entities {
    /// <summary>
    /// Clase ApplicationRole para ser utilizado con Identity
    /// </summary>
    public partial class ApplicationRole : IdentityRole {
        // Constructores
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }

        public ApplicationRole(SGJD_ADM_TAB_ROLES RolBD) {
            Id = RolBD.Id;
            Name = RolBD.Name;
            NombreObjeto = RolBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Propiedades
        [NotMapped]
        public string NombreObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "Id: " + Id + ", " +
                "Name: " + Name;
        }
    }
}