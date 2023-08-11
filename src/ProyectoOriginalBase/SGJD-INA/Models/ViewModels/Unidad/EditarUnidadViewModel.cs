using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarUnidadViewModel {
        #region Constructor
        public EditarUnidadViewModel() { }

        public EditarUnidadViewModel(Unidad UnidadModelo) {
            IdUnidad = UnidadModelo.Id;
            Nombre = UnidadModelo.Nombre;
            Correo = UnidadModelo.Correo;
        }
        #endregion

        #region Atributos
        public int IdUnidad { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mayor a {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Correo de unidad")]
        [Required(ErrorMessage = "El correo es requerido.")]
        public string Correo { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public Unidad Entidad() {
            return new Unidad {
                Id = IdUnidad,
                Nombre = Nombre,
                Correo = Correo
            };
        }
        #endregion
    }
}