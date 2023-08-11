using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarVistaViewModel {
        #region Constructores
        public EditarVistaViewModel() { }

        /// <summary>
        /// Método  encargado de convertir una entidad de sistema a view model para vista
        /// </summary>
        public EditarVistaViewModel(Vista ModeloVista) {
            Id = ModeloVista.Id;
            NombreVista = ModeloVista.NombreVista;
            DireccionVista = ModeloVista.DireccionVista;
            NombreObjeto = ModeloVista.NombreObjeto;
        }

        #endregion

        #region atributos

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la vista es requerido.")]
        [Display(Name = "Nombre de la vista")]
        public string NombreVista { get; set; }

        [Required(ErrorMessage = "La ruta de la vista es requerida.")]
        [Display(Name = "Ruta")]
        public string DireccionVista { get; set; }

        public string NombreObjeto { get; set; }

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