using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarUsuarioArticuloViewModel {
        [Required]
        public int IdArticulo { get; set; }

        public string IdUsuarioOtroAsistente { get; set; }

        #region Métodos
        /// <summary>
        /// Método  encargado de convertir un modelo de base de datos en una entidad de sistema 
        /// </summary>
        public UsuarioArticulo Entidad() {
            return new UsuarioArticulo() {
                Id = 0,
                IdArticulo = IdArticulo,
                IdUsuario = Funciones.IsNumber(IdUsuarioOtroAsistente) ? null : IdUsuarioOtroAsistente,
                IdOtroAsistente = !Funciones.IsNumber(IdUsuarioOtroAsistente) ? Convert.ToInt32(IdUsuarioOtroAsistente) : Convert.ToInt16(null)
            };
        }
        #endregion
    }
}