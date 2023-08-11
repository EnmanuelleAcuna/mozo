using SGJD_INA.Models.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class AgregarAcuerdoViewModel {
        public AgregarAcuerdoViewModel() { }

        public AgregarAcuerdoViewModel(SesionConArticulosParaNuevoAcuerdoDTO SesionConAcuerdos) {
            IdSesion = SesionConAcuerdos.Sesion.IdSesion;
            NombreSesion = SesionConAcuerdos.Sesion.NombreSesion;
            FechaSesion = SesionConAcuerdos.Sesion.Fecha.ToString("dd/MM/yyyy");
            CodigoEstadoActa = SesionConAcuerdos.Sesion.CodigoEstadoActa;
            Articulos = SesionConAcuerdos.Articulos.Select(Articulo => new ArticuloParaNuevoAcuerdoViewModel(Articulo));
        }

        public int IdSesion { get; set; }

        [Display(Name = "Sesión")]
        public string NombreSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public string FechaSesion { get; set; }

        public string CodigoEstadoActa { get; set; }

        [Display(Name = "Artículos del Acta de la Sesión")]
        public IEnumerable<ArticuloParaNuevoAcuerdoViewModel> Articulos { get; set; }
    }
}