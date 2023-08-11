using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarSeccionViewModel {
        #region Constructores
        public EditarSeccionViewModel() { }

        public EditarSeccionViewModel(Seccion SeccionModelo) {
            IdSeccion = SeccionModelo.Id;
            Descripcion = SeccionModelo.Descripcion;
        }

        #endregion

        #region Atributos
        public int IdSeccion { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public Seccion Entidad() {
            return new Seccion {
                Id = IdSeccion,
                Descripcion = Descripcion
            };
        }
        #endregion
    }
}