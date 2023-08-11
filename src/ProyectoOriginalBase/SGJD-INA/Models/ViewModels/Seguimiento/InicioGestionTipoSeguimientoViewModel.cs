using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioGestionTipoSeguimientoViewModel {
        //Contructores
        public InicioGestionTipoSeguimientoViewModel(Acuerdo AcuerdoModelo) {
            IdAcuerdo = AcuerdoModelo.Id;
            EstadoAcuerdo = AcuerdoModelo.Estado.Nombre;
            CodigoEstado = AcuerdoModelo.Estado.CodigoEstado;
            Titulo = AcuerdoModelo.Titulo;
            Asunto = AcuerdoModelo.Asunto;
            FechaFirmeza = Convert.ToDateTime(AcuerdoModelo.FechaFirmeza).ToString("dd/MM/yyyy");
            TipoSeguimiento = AcuerdoModelo.TipoSeguimiento;
            Revision = AcuerdoModelo.Revision ? "Si" : "No";
        }

        //Atributos
        public int IdAcuerdo { get; set; }

        [Display(Name = "Estado del acuerdo")]
        public string EstadoAcuerdo { get; set; }

        [Display(Name = "Título del acuerdo")]
        public string Titulo { get; set; }

        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Display(Name = "Fecha de firmeza del acuerdo")]
        public string FechaFirmeza { get; set; }

        [Display(Name = "Tipo de seguimiento")]
        public int TipoSeguimiento { get; set; }

        [Display(Name = "Acuerdo en revisión")]
        public string Revision { get; set; }

        public string CodigoEstado { get; set; }
    }
}