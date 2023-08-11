using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleSesionViewModel {
        // Constructores
        public DetalleSesionViewModel() { }

        public DetalleSesionViewModel(Sesion ModeloSesion) {
            IdSesion = ModeloSesion.Id;
            TipoSesion = ModeloSesion.TipoSesion.Descripcion;
            NumeroSesion = ModeloSesion.Numero;
            FechaSesion = ModeloSesion.FechaHora;
            NombreEstado = ModeloSesion.Estado.Nombre;
            NombreUsuario = ModeloSesion.Usuario.Nombre;
        }

        //Propiedades
        public int IdSesion { get; set; }

        [Display(Name = "Tipo de sesión")]
        public string TipoSesion { get; set; }

        [Display(Name = "Numero de sesion")]
        public int NumeroSesion { get; set; }

        [Display(Name = "Fecha")]
        public DateTime FechaSesion { get; set; }

        [Display(Name = "Estado")]
        public string NombreEstado { get; set; }

        [Display(Name = "Última modificación por")]
        public string NombreUsuario { get; set; }
    }
}