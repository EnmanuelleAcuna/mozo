using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarTomoViewModel {
        #region Constructores
        public EditarTomoViewModel() { }

        /// <summary>
        /// Método  encargado de convertir una entidad de sistema a view model para vista
        /// </summary>
        public EditarTomoViewModel(Tomo ModeloTomo) {
            Id = ModeloTomo.Id;
            Numero = ModeloTomo.Numero;
            Nombre = ModeloTomo.Nombre;
            ObservacionApertura = ModeloTomo.ObservacionApertura;
            ObservacionCierre = ModeloTomo.ObservacionCierre;
            NombreObjeto = ModeloTomo.NombreObjeto;
        }
        #endregion

        #region Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Numero")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Apertura")]
        public string ObservacionApertura { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Cierre")]
        public string ObservacionCierre { get; set; }

        public string NombreObjeto { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public Tomo Entidad() {
            return new Tomo {
                Id = Id,
                Nombre = Nombre,
                Numero = Numero,
                ObservacionApertura = ObservacionApertura,
                ObservacionCierre = ObservacionCierre
            };
        }
        #endregion
    }
}