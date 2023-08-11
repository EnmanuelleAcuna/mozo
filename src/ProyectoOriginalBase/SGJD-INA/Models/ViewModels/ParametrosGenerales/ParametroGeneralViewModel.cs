using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class ParametroGeneralViewModel {
        #region Constructor
        public ParametroGeneralViewModel(ParametroGeneral Modelo) {
            if (Modelo is null) throw new ArgumentNullException(paramName: nameof(Modelo), message: Resources.ModeloNulo);

            IdParametroGeneral = Modelo.Id;

            // Verificar si el parametro corresponde al usuario secretario o presidente de la junta directiva para colocar en el valor la descripción
            // del parametro que almacena el nombre del usuario
            if (Modelo.Nombre.Equals("IdUsuarioSecretario", StringComparison.OrdinalIgnoreCase)) {
                Valor = Modelo.Descripcion;
                Descripcion = "Usuario Secretario Técnico de la Junta Directiva";
            }
            else if (Modelo.Nombre.Equals("IdUsuarioPresidente", StringComparison.OrdinalIgnoreCase)) {
                Valor = Modelo.Descripcion;
                Descripcion = "Usuario Presidente de la Junta Directiva";
            }
            else {
                Valor = Modelo.Valor;
                Descripcion = Modelo.Descripcion;
            }
        }
        #endregion

        #region Atributos
        public int IdParametroGeneral { get; set; }

        [Display(Name = "Parámetro")]
        public string Descripcion { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }
        #endregion
    }
}