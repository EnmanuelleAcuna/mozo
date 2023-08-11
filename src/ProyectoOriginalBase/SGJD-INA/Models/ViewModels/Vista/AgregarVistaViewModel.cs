using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarVistaViewModel {
        #region Atributos
        [Display(Name = "Nombre de la vista")]
        [Required(ErrorMessage = "El nombre de la vista es requerido.")]
        public string NombreVista { get; set; }

        [Display(Name = "Ruta")]
        [Required(ErrorMessage = "La ruta de la vista es requerida.")]
        public string DireccionVista { get; set; }

        public string NombreObjeto { get; set; }

        public IEnumerable<VistaPerfil> VistasPerfil { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public Vista Entidad() {
            return new Vista {
                Id = 0,
                DireccionVista = DireccionVista,
                NombreVista = NombreVista
            };
        }
        #endregion
    }
}