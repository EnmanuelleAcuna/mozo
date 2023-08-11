using SGJD_INA.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class TotalAcuerdosViewModel {
        // Contructores 
        public TotalAcuerdosViewModel() { }

        public TotalAcuerdosViewModel(InformeTotalAcuerdosDTO Modelo) {
            Ano = Modelo.Ano;
            Acuerdos = Modelo.Acuerdos;
            AcuerdosProceso = Modelo.AcuerdosProceso;
            AcuerdosCumplidos = Modelo.AcuerdosCumplidos;
        }

        // Propiedades
        [Display(Name = "Año")]
        public int Ano { get; set; }

        [Display(Name = "Emitidos")]
        public int Acuerdos { get; set; }

        [Display(Name = "En ejecución")]
        public int AcuerdosProceso { get; set; }

        [Display(Name = "Ejecutados")]
        public int AcuerdosCumplidos { get; set; }
    }
}