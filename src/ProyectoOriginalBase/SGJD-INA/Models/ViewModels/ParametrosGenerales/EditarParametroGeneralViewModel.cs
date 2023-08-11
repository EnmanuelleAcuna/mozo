using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class EditarParametroGeneralViewModel {
        #region Constructor
        public EditarParametroGeneralViewModel() { }

        public EditarParametroGeneralViewModel(ParametroGeneral ModeloParametroGeneral) {
            if (ModeloParametroGeneral is null) throw new ArgumentNullException(paramName: nameof(ModeloParametroGeneral), message: Resources.ModeloNulo);

            IdParametroGeneral = ModeloParametroGeneral.Id;
            Nombre = ModeloParametroGeneral.Nombre;

            // Verificar si el parametro corresponde al usuario secretario o presidente de la junta directiva para colocar en el valor la descripción
            // la descripción correspondiente, en lugar del nombre del usuario que corresponde al id en el valor
            if (ModeloParametroGeneral.Nombre.Equals("IdUsuarioSecretario", StringComparison.OrdinalIgnoreCase)) {
                Descripcion = "Usuario Secretario Técnico de la Junta Directiva";
            }
            else if (ModeloParametroGeneral.Nombre.Equals("IdUsuarioPresidente", StringComparison.OrdinalIgnoreCase)) {
                Descripcion = "Usuario Presidente de la Junta Directiva";
            }
            else {
                Descripcion = ModeloParametroGeneral.Descripcion;
            }

            Valor = ModeloParametroGeneral.Valor;
            Tipo = ModeloParametroGeneral.Tipo;
        }
        #endregion

        #region Atributos
        public int IdParametroGeneral { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "El valor es requerida.")]
        [StringLength(50, ErrorMessage = "El valor no puede ser mayor a {1} caracteres.")]
        public string Valor { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(100, ErrorMessage = "La descripción no puede ser mayor a {1} caracteres.")]
        public string Descripcion { get; set; }

        public string Tipo { get; set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Método encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public ParametroGeneral Entidad() {
            return new ParametroGeneral {
                Id = IdParametroGeneral,
                Nombre = Nombre,
                Descripcion = Descripcion,
                Valor = Valor,
                Tipo = Tipo
            };
        }
        #endregion
    }
}