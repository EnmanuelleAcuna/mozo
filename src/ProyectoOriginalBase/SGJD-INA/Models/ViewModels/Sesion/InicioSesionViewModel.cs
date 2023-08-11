using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioSesionViewModel {
        //Constructor
        public InicioSesionViewModel() { }

        public InicioSesionViewModel(Sesion ModeloSesion) {
            IdSesion = ModeloSesion.Id;
            IdOrdenDia = ModeloSesion.OrdenDia.Id;
            IdActa = ModeloSesion.Acta.Id;
            TipoSesion = ModeloSesion.TipoSesion.Descripcion;
            Numero = ModeloSesion.Numero;
            Anno = ModeloSesion.FechaHora.Year.ToString();
            NombreEstado = ModeloSesion.Estado.Nombre;
            CodigoEstado = ModeloSesion.Estado.CodigoEstado;
            FechaSesion = ModeloSesion.FechaHora.ToString("dd/MM/yyyy hh:mm tt");
        }

        //Propiedades
        public int IdSesion { get; set; }

        public int IdOrdenDia { get; set; }

        public int IdActa { get; set; }

        public string TipoSesion { get; set; }

        public int Numero { get; set; }

        public string Anno { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Codigo Estado")]
        public string CodigoEstado { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public string FechaSesion { get; set; }
    }
}