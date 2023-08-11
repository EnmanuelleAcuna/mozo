using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class SeguimientoViewModel {
        //Constructores
        public SeguimientoViewModel(SeguimientoAcuerdo ModeloSeguimientoAcuerdo) {
            IdSeguimiento = ModeloSeguimientoAcuerdo.Id;
            IdAcuerdo = ModeloSeguimientoAcuerdo.IdAcuerdo;
            TituloAcuerdo = ModeloSeguimientoAcuerdo.Acuerdo.Titulo;
            Avance = ModeloSeguimientoAcuerdo.PorcentajeAvance;
            CodigoEstado = ModeloSeguimientoAcuerdo.Estado.CodigoEstado;
        }

        //Propiedades
        public int IdSeguimiento { get; set; }

        public int IdAcuerdo { get; set; }

        [Display(Name = "Seguimiento")]
        public string TituloAcuerdo { get; set; }

        [Display(Name = "Ejecución")]
        public int Avance { get; set; }

        public string CodigoEstado { get; set; }      
    }
}