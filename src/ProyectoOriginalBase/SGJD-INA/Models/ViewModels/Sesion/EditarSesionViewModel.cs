using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SGJD_INA.Models.ViewModels {
    public class EditarSesionViewModel {
        //Contructor
        public EditarSesionViewModel() { }

        public EditarSesionViewModel(Sesion Entidad) {
            if (Entidad is null) throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);

            CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

            IdSesion = Entidad.Id;
            IdTipoSesion = Entidad.TipoSesion.Id;
            IdEstado = Entidad.IdEstado;
            NumeroSesion = Entidad.Numero;
            Fecha = Entidad.FechaHora.ToString("dd/MM/yyyy", CultureInfo);
            Hora = Entidad.FechaHora.ToString("hh:mm tt", CultureInfo);
        }

        //Propiedades
        [Required(ErrorMessage = "El id de la sesión es requerido.")]
        public int IdSesion { get; set; }

        [Display(Name = "Tipo sesión")]
        [Required(ErrorMessage = "El tipo de sesión es requerido.")]
        public int IdTipoSesion { get; set; }

        [Display(Name = "Estado de la sesión")]
        [Required(ErrorMessage = "El estado es requerido.")]
        public int IdEstado { get; set; }

        [Display(Name = "Numero Consecutivo")]
        [Required(ErrorMessage = "El número de sesión es requerido.")]
        public int NumeroSesion { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        public string Fecha { get; set; }

        [Display(Name = "Hora")]
        [Required(ErrorMessage = "La hora es requerida.")]
        public string Hora { get; set; }

        public Sesion Entidad() {
            return new Sesion() {
                Id = IdSesion,
                IdTipoSesion = IdTipoSesion,
                IdEstado = IdEstado,
                Numero = NumeroSesion,
                FechaHora = Funciones.JoinStringToDateTime(Funciones.StringToDateTime(Fecha), Hora)
            };
        }
    }
}