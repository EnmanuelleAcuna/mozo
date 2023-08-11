using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class TipoUsuario {
        // Constructores
        public TipoUsuario() {
            Usuarios = new HashSet<ApplicationUser>();
        }

        public TipoUsuario(SGJD_ADM_TAB_TIPOS_USUARIO TipoUsuarioBD) {
            Id = TipoUsuarioBD.LLP_Id;
            Nombre = TipoUsuarioBD.Nombre;
            NombreObjeto = TipoUsuarioBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public TipoUsuario(SGJD_ADM_TAB_TIPOS_USUARIO TipoUsuarioBD, IEnumerable<SGJD_ADM_TAB_USUARIOS> UsuariosBD) {
            Id = TipoUsuarioBD.LLP_Id;
            Nombre = TipoUsuarioBD.Nombre;
            NombreObjeto = TipoUsuarioBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuarios = UsuariosBD.Select(u => new ApplicationUser(u)).ToList();
        }

        // Propiedades
        public int Id { get; set; }

        public string Nombre { get; set; }

        [NotMapped]
        public string NombreObjeto { get; set; }

        public ICollection<ApplicationUser> Usuarios { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "nombre:" + Nombre;
        }

        public SGJD_ADM_TAB_TIPOS_USUARIO BD() {
            return new SGJD_ADM_TAB_TIPOS_USUARIO() {
                LLP_Id = Id,
                Nombre = Nombre
            };
        }
    }
}