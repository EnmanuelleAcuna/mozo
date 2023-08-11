using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAcuerdoViewModel {
        //Constructor
        public InicioAcuerdoViewModel(Acuerdo ModeloAcuerdo) {
            IdAcuerdo = ModeloAcuerdo.Id;
            Titulo = string.Format("{0}", ModeloAcuerdo.Titulo);
            NumeroVersion = ModeloAcuerdo.NumeroVersion;
            NombreEstado = ModeloAcuerdo.Estado.Nombre;
            CodigoEstado = ModeloAcuerdo.Estado.CodigoEstado;
            Firmeza = ModeloAcuerdo.Firme;
            FechaFirmeza = ModeloAcuerdo.Firme == true ? Convert.ToDateTime(ModeloAcuerdo.FechaFirmeza).ToString("dd/MM/yyyy") : string.Empty;
        }

        //Propiedades
        [Display(Name = "Id del acuerdo")]
        public int IdAcuerdo { get; set; }

        [Display(Name = "Acuerdo")]
        public string Titulo { get; set; }

        public int NumeroVersion { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        public string CodigoEstado { get; set; }

        [Display(Name = "Firmeza del acuerdo")]
        public bool Firmeza { get; set; }

        public string FechaFirmeza { get; set; }
    }
}