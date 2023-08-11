using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Repositorio {
        public Repositorio() { }

        public Repositorio(SGJD_ADM_TAB_REPOSITORIOS RepositorioBD) {
            Id = RepositorioBD.LLP_Id;
            Nombre = RepositorioBD.Nombre;
            Ruta = RepositorioBD.Ruta;
            NombreObjeto = RepositorioBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ruta es requerida.")]
        public string Ruta { get; set; }

        public string NombreObjeto { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "nombre:" + Nombre + ", " +
                "ruta:" + Ruta;
        }

        public SGJD_ADM_TAB_REPOSITORIOS BD() {
            return new SGJD_ADM_TAB_REPOSITORIOS() {
                LLP_Id = Id,
                Nombre = Nombre,
                Ruta = Ruta
            };
        }
    }
}