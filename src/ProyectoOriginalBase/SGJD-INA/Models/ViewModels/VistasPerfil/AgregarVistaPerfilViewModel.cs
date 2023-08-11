using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarVistaPerfilViewModel {
        #region Atributos
        [Key]
        [Display(Name = "Nombre de vista")]
        [Required(ErrorMessage = "La vista es requerida.")]
        public int IdVista { get; set; }

        [Key]
        [Display(Name = "Nombre de perfil")]
        [Required(ErrorMessage = "El perfil es requerido.")]
        public string IdPerfil { get; set; }

        [Display(Name = "Permiso")]
        public int Permiso { get; set; }

        public string NombreObjeto { get; set; }

        public Vista Vista { get; set; }

        public ApplicationRole Perfil { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public VistaPerfil Entidad() {
            return new VistaPerfil {
                IdVista = IdVista,
                IdPerfil = IdPerfil,
                Permiso = Permiso,
            };
        }
        #endregion
    }
}