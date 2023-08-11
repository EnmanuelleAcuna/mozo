using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class InicioAsistenciaViewModel {
        //Constructor
        public InicioAsistenciaViewModel(Sesion ModeloSesion, IEnumerable<AsistenteSesion> ListaAsistenteSesion, IEnumerable<OtroAsistenteSesion> ListaOtroAsistenteSesion) {
            IdSesion = ModeloSesion.Id;
            Sesion = string.Format("{0} {1} - {2}", ModeloSesion.TipoSesion.Descripcion, ModeloSesion.Numero, ModeloSesion.FechaHora.Year);
            ListaTipoAsistenteSesion = ListaAsistenteSesion.Select(AsistenteSesion => new InicioAsistenteSesionViewModel(AsistenteSesion)).ToList();
            ListaTipoOtroAsistenteSesion = ListaOtroAsistenteSesion.Select(OtroAsistenteSesion => new InicioOtroAsistenteSesionViewModel(OtroAsistenteSesion)).ToList();
        }

        //Propiedades
        public int IdSesion { get; set; }

        [Display(Name = "Sesión")]
        public string Sesion { get; set; }

        public IEnumerable<InicioAsistenteSesionViewModel> ListaTipoAsistenteSesion { get; set; }

        public IEnumerable<InicioOtroAsistenteSesionViewModel> ListaTipoOtroAsistenteSesion { get; set; }
    }
}