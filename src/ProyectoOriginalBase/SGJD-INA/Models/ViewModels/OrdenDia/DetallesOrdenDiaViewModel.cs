using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class DetallesOrdenDiaViewModel {
        #region Constructor
        public DetallesOrdenDiaViewModel() { }

        public DetallesOrdenDiaViewModel(OrdenDia ModeloOrdenDia, string Encabezado, string PiePagina, ParametroGeneral ParametroUnidad) {
            IdOrdenDia = ModeloOrdenDia.Id;
            NombreSesion = string.Format("{0} N°{1}", ModeloOrdenDia.Sesion.TipoSesion.Descripcion, ModeloOrdenDia.Sesion.Numero);
            EncabezadoConvocatoria = ModeloOrdenDia.Encabezado;
            NombreUnidad = ParametroUnidad.Valor;
            Sesion = string.Format("{0} de Junta Directiva", ModeloOrdenDia.Sesion.TipoSesion.Descripcion);
            FechaSesion = string.Format("{0} {1} de {2} de {3}", ModeloOrdenDia.Sesion.FechaHora.ToString("dddd"), ModeloOrdenDia.Sesion.FechaHora.Day, ModeloOrdenDia.Sesion.FechaHora.ToString("MMMM"), ModeloOrdenDia.Sesion.FechaHora.Year); ;
            HoraSesion = ModeloOrdenDia.Sesion.FechaHora.ToString("hh:mm tt");

            Secciones = ModeloOrdenDia.Secciones.Select(Seccion => new SeccionViewModel(Seccion)).ToList();

            this.Encabezado = Encabezado;
            this.PiePagina = PiePagina;
        }

        public DetallesOrdenDiaViewModel(OrdenDia ModeloOrdenDia) {
            IdOrdenDia = ModeloOrdenDia.Id;
            NombreSesion = string.Format("{0} N°{1}", ModeloOrdenDia.Sesion.TipoSesion.Descripcion, ModeloOrdenDia.Sesion.Numero);
            EncabezadoConvocatoria = ModeloOrdenDia.Encabezado;
            Sesion = string.Format("{0} de Junta Directiva", ModeloOrdenDia.Sesion.TipoSesion.Descripcion);
            FechaSesion = string.Format("{0} {1} de {2} de {3}", ModeloOrdenDia.Sesion.FechaHora.ToString("dddd"), ModeloOrdenDia.Sesion.FechaHora.Day, ModeloOrdenDia.Sesion.FechaHora.ToString("MMMM"), ModeloOrdenDia.Sesion.FechaHora.Year); ;
            HoraSesion = ModeloOrdenDia.Sesion.FechaHora.ToString("hh:mm tt");

            Secciones = ModeloOrdenDia.Secciones.Select(Seccion => new SeccionViewModel(Seccion)).ToList();
        }
        #endregion

        #region Propiedades
        public int IdOrdenDia { get; set; }

        public string NombreSesion { get; set; }

        public string EncabezadoConvocatoria { get; set; }

        public string NombreUnidad { get; set; }

        public string Sesion { get; set; }

        public string FechaSesion { get; set; }

        public string HoraSesion { get; set; }

        public IEnumerable<SeccionViewModel> Secciones { get; set; }

        public string Encabezado { get; set; }

        public string PiePagina { get; set; }
        #endregion
    }
}