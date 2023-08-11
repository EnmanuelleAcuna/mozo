using SGJD_INA.Models.Entities;

namespace SGJD_INA.Models.ViewModels {
    public class ActaTomoViewModel {
        //Constructores
        public ActaTomoViewModel(Acta ModeloActa) {
            IdActa = ModeloActa.Id;
            IdSesion = ModeloActa.IdSesion;
            CodigoEstado = ModeloActa.Estado.CodigoEstado;
            NombreEstado = ModeloActa.Estado.Nombre;
            TipoSesion = ModeloActa.Sesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloActa.Sesion.Numero.ToString();
            AnnoSesion = ModeloActa.Sesion.FechaHora.Year.ToString();
            UrlActa = ModeloActa.UrlActaFirmada;
        }

        //Atributos
        public int IdActa { get; set; }

        public int IdSesion { get; set; }

        public string CodigoEstado { get; set; }

        public string NombreEstado { get; set; }

        public string TipoSesion { get; set; }

        public string NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }

        public string UrlActa { get; set; }
    }
}