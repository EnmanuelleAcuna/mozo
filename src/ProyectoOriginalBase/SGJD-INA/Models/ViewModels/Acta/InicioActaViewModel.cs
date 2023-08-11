using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioActaViewModel {
        //Constructor
        public InicioActaViewModel(Acta ModeloActa) {
            IdActa = ModeloActa.Id;
            IdSesion = ModeloActa.IdSesion;
            IdTomo = ModeloActa.Tomo.Id;

            TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloActa.Sesion.Numero;
            AnnoSesion = ModeloActa.Sesion.FechaHora.Year.ToString();
            FechaSesion = ModeloActa.Sesion.FechaHora.ToString("dd/MM/yyyy");

            Tomo = ModeloActa.Tomo.Nombre;

            DescripcionEstado = ModeloActa.Estado.Nombre;
            CodigoEstado = ModeloActa.Estado.CodigoEstado;
        }

        //Propiedades
        [Display(Name = "Id del acta")]
        public int IdActa { get; set; }

        public int IdSesion { get; set; }

        [Display(Name = "Sesión")]
        public string TipoSesion { get; set; }

        public int NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public string FechaSesion { get; set; }

        public int IdTomo { get; set; }

        [Display(Name = "Tomo")]
        public string Tomo { get; set; }

        [Display(Name = "Estado")]
        public string DescripcionEstado { get; set; }

        public string CodigoEstado { get; set; }
    }
}