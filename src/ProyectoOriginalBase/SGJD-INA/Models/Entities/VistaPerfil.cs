using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class VistaPerfil {
        [Display(Name = "Nombre de vista")]
        [Required(ErrorMessage = "La vista es requerida.")]
        public int IdVista { get; set; }

        [Key]
        [Display(Name = "Nombre de perfil")]
        public string IdPerfil { get; set; }

        [Display(Name = "Permiso")]
        public int Permiso { get; set; }

        public string NombreObjeto { get; set; }

        public Vista Vista { get; set; }

        public ApplicationRole Perfil { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "idVista: " + IdVista + ", " +
                "idPerfil: " + IdPerfil + ", " +
                "permiso: " + Permiso;
        }
    }
}