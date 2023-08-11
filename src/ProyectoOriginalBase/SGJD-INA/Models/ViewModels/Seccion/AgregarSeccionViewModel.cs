using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarSeccionViewModel {
        #region Constructores
        public AgregarSeccionViewModel() { }

        public AgregarSeccionViewModel(Seccion SeccionModelo) {
        }
        #endregion

        #region Atributos
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public Seccion Entidad() {
            return new Seccion {
                Descripcion = Descripcion
            };
        }
        #endregion
    }
}