using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class ClasificarTemasOrdenDiaViewModel {
        //Constructor
        public ClasificarTemasOrdenDiaViewModel() { }

        public ClasificarTemasOrdenDiaViewModel(OrdenDia ModeloOrdenDia) {
            IdOrdenDia = ModeloOrdenDia.Id;
            IdSesion = ModeloOrdenDia.IdSesion;
            Titulo = string.Format("{0} {1} - {2}", ModeloOrdenDia.Sesion.TipoSesion.Descripcion, ModeloOrdenDia.Sesion.Numero, ModeloOrdenDia.Sesion.FechaHora.Year);

            Secciones = ModeloOrdenDia.Secciones.Select(S => new SeccionViewModel(S)).ToList();
        }

        //Propiedades
        [Display(Name = "Id del Orden del Día")]
        [Required(ErrorMessage = "El Orden del Día es requerido.")]
        public int IdOrdenDia { get; set; }

        [Display(Name = "Id de la Sesión")]
        [Required(ErrorMessage = "La Sesión es requerida.")]
        public int IdSesion { get; set; }

        public string Titulo { get; set; }

        public IEnumerable<SeccionViewModel> Secciones { get; set; }
    }
}