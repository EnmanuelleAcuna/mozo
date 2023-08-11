using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarTomoViewModel {
        #region Constructores
        public AgregarTomoViewModel() { }

        public AgregarTomoViewModel(ParametroGeneral ParametroNombreLibro, ParametroGeneral ParametroUnidad, TipoObjeto TipoObjetoTomo) {
            NombreLibro = ParametroNombreLibro.Valor;
            NombreUnidad = ParametroUnidad.Valor;
            FechaApertura = DateTime.Now;
            ConsecutivoTomo = Funciones.OrdinalToRoman(TipoObjetoTomo.Consecutivo);
        }
        #endregion

        #region Atributos
        [Display(Name = "Nombre del Libro")]
        public string NombreLibro { get; set; }

        [Display(Name = "Unidad")]
        public string NombreUnidad { get; set; }

        [Display(Name = "No. Asiento")]
        [Range(1, Int32.MaxValue, ErrorMessage = "El número de asiento debe ser mayor a 0")]
        [Required(ErrorMessage = "El número de asiento es requerido")]
        public int Asiento { get; set; }

        [Display(Name = "Observación de Apertura")]
        [Required(ErrorMessage = "La observación de apertura es requerida")]
        public string ObservacionApertura { get; set; }

        [Display(Name = "Fecha de Apertura")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de firmeza debe ser una fecha válida")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaApertura { get; set; }

        [Display(Name = "Numero tomo")]
        public string ConsecutivoTomo { get; set; }
        #endregion
    }
}