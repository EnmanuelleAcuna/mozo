using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.ViewModels {
    public class EditarOrdenDiaViewModel {
        // Constructores
        public EditarOrdenDiaViewModel() { }

        public EditarOrdenDiaViewModel(OrdenDia ModeloOrdenDia) {
            IdOrdenDia = ModeloOrdenDia.Id;
            IdSesion = ModeloOrdenDia.IdSesion;

            Encabezado = ModeloOrdenDia.Encabezado;

            TipoSesion = ModeloOrdenDia.Sesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloOrdenDia.Sesion.Numero;
            AnnoSesion = ModeloOrdenDia.Sesion.FechaHora.Year.ToString();
            FechaSesion = ModeloOrdenDia.Sesion.FechaHora.ToString("dd/MM/yyyy");
            NombreSesion = string.Format("{0} {1} - {2}", ModeloOrdenDia.Sesion.TipoSesion.Descripcion, ModeloOrdenDia.Sesion.Numero, ModeloOrdenDia.Sesion.FechaHora.Year);

            IdEstado = ModeloOrdenDia.IdEstado;
            NombreEstado = ModeloOrdenDia.Estado.Nombre;
            CodigoEstado = ModeloOrdenDia.Estado.CodigoEstado;

            Secciones = ModeloOrdenDia.Secciones.Select(Seccion => new SeccionViewModel(Seccion)).ToList();

            NombreObjeto = ModeloOrdenDia.NombreObjeto;
        }

        // Propiedades
        public int IdOrdenDia { get; set; }

        public int IdSesion { get; set; }

        [Display(Name = "Encabezado del Orden del Día")]
        public string Encabezado { get; set; }

        [Display(Name = "Sesión")]
        public string TipoSesion { get; set; }

        public int NumeroSesion { get; set; }

        public string AnnoSesion { get; set; }

        [Display(Name = "Fecha de la Sesión")]
        public string FechaSesion { get; set; }

        public string NombreSesion { get; set; }

        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Codigo del estado")]
        public string CodigoEstado { get; set; }

        public IEnumerable<SeccionViewModel> Secciones { get; set; }

        public string NombreObjeto { get; set; }

        // Metodos
        public OrdenDia Entidad() {
            return new OrdenDia {
                Id = IdOrdenDia,
                IdSesion = IdSesion,
                IdEstado = IdEstado,
                Encabezado = Encabezado,
                NombreObjeto = NombreObjeto
            };
        }
    }
}