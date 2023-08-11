using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class ActaAcuersoftPorPalabraClaveViewModel {
        // Constructores
        public ActaAcuersoftPorPalabraClaveViewModel() { }

        public ActaAcuersoftPorPalabraClaveViewModel(ActasAcuersoftPorPalablaClaveDTO ModeloActa) {
            IdActa = ModeloActa.IdActa;
            NumeroActa = ModeloActa.NumeroActa;
            Fecha = ModeloActa.Fecha.ToString("dd/MM/yyyy");
            Lugar = ModeloActa.Lugar;
            Preside = ModeloActa.Preside;
            Secretario = ModeloActa.Secretario;
            Detalle = ModeloActa.Detalle;           
        }

        // Propiedades
        public int IdActa { get; set; }

        [Display(Name = "Número de Acta")]
        public string NumeroActa { get; set; }

        [Display(Name = "Fecha de Sesión")]
        public string Fecha { get; set; }

        [Display(Name = "Lugar")]
        public string Lugar { get; set; }

        [Display(Name = "Preside")]
        public string Preside { get; set; }

        [Display(Name = "Secretario")]
        public string Secretario { get; set; }

        [Display(Name = "Detalle")]
        public string Detalle { get; set; }
    }
}