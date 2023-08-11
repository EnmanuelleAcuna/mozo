using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class EliminarSesionViewModel {
        // Constructores
        public EliminarSesionViewModel() { }

        public EliminarSesionViewModel(Sesion ModeloSesion) {
            IdSesion = ModeloSesion.Id;
            Numero = ModeloSesion.Numero;
            FechaHora = ModeloSesion.FechaHora;
            IdTipoSesion = ModeloSesion.IdTipoSesion;
            TipoSesion = ModeloSesion.TipoSesion.Descripcion;
            IdUsuario = ModeloSesion.IdUsuario;
            IdEstado = ModeloSesion.IdEstado;
            Anno = ModeloSesion.FechaHora.Year.ToString();
        }

        // Propiedades
        public int IdSesion { get; set; }

        [Display(Name = "Numero sesion")]
        public int Numero { get; set; }

        public DateTime FechaHora { get; set; }

        public int IdTipoSesion { get; set; }

        public string TipoSesion { get; set; }

        public string IdUsuario { get; set; }

        public int IdEstado { get; set; }

        public string Anno { get; set; }

    }
}