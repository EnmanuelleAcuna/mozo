using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class ActaAcuersoftViewModel {
        // Constructores
        public ActaAcuersoftViewModel() { }

        public ActaAcuersoftViewModel(ActaAcuersoft ModeloActaAcuersoft, IEnumerable<ActasDetalleAcuersoftDTO> ListaDetalleActas) {
            IdActa = ModeloActaAcuersoft.Id;
            NumeroActa = ModeloActaAcuersoft.NumeroActa;
            Fecha = ModeloActaAcuersoft.Fecha;
            Lugar = ModeloActaAcuersoft.Lugar;
            Preside = ModeloActaAcuersoft.Preside;
            Secretario = ModeloActaAcuersoft.Secretario;

            DetalleActaAcuersoft = ListaDetalleActas.Select(d => new DetalleActaAcuersoftViewModel(d)).ToList();
        }

        // Propiedades
        public int IdActa { get; set; }

        public int IdDetalle { get; set; }

        [Display(Name = "Número de Acta")]
        public string NumeroActa { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] // Especificar explicitamente formato de texto
        public DateTime? Fecha { get; set; }

        [Display(Name = "Lugar")]
        public string Lugar { get; set; }

        [Display(Name = "Preside")]
        public string Preside { get; set; }

        [Display(Name = "Secretario")]
        public string Secretario { get; set; }

        [Display(Name = "Índice")]
        public int Indice { get; set; }

        [Display(Name = "Detalle")]
        public string Texto { get; set; }

        public IEnumerable<DetalleActaAcuersoftViewModel> DetalleActaAcuersoft { get; set; }
    }
}