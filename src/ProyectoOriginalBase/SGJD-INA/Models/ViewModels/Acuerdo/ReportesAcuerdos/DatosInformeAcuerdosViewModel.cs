using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DatosInformeAcuerdosViewModel {
        // Constructores      
        public DatosInformeAcuerdosViewModel() { }

        public DatosInformeAcuerdosViewModel(InformeAcuerdosDTO ModeloInformeAcuerdo) {
            IdAcuerdo = ModeloInformeAcuerdo.IdAcuerdo;
            Titulo = string.Format("{0}", ModeloInformeAcuerdo.Titulo);
            NumeroVersion = ModeloInformeAcuerdo.NumeroVersion;
            Asunto = ModeloInformeAcuerdo.Asunto;
            UrlAcuerdoFirmado = ModeloInformeAcuerdo.UrlAcuerdoFirmado;
            IdEstado = ModeloInformeAcuerdo.IdEstado;
            NombreEstado = ModeloInformeAcuerdo.Estado;
            TipoSeguimiento = ModeloInformeAcuerdo.TipoSeguimiento;
            Unidades = ModeloInformeAcuerdo.Unidades;
            FechaCreacion = ModeloInformeAcuerdo.FechaCreacion.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        // Propiedades
        public int IdAcuerdo { get; set; }

        [Display(Name = "Acuerdo")]
        public string Titulo { get; set; }

        public int NumeroVersion { get; set; }

        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Display(Name = "Acuerdo Firmado")]
        public string UrlAcuerdoFirmado { get; set; }

        [Display(Name = "Id del estado")]
        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Tipo de seguimiento")]
        public string TipoSeguimiento { get; set; }

        [Display(Name = "Unidades ejecutoras")]
        public string Unidades { get; set; }

        [Display(Name = "Fecha de creacion")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")] // Especificar explicitamente formato de texto
        public string FechaCreacion { get; set; }
    }
}