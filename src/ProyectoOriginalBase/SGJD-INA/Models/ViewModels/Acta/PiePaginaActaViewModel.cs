using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.ViewModels {
    public class PiePaginaActaViewModel {
        //Contructores
        public PiePaginaActaViewModel() { }

        public PiePaginaActaViewModel (string TipoSesion, string NumeroSesion, DateTime FechaSesion, string Encabezado,  string PiePagina) {
            this.TipoSesion = TipoSesion;
            this.NumeroSesion = NumeroSesion;
            this.FechaSesion = FechaSesion;
            this.Encabezado = Encabezado;
            this.PiePagina = PiePagina;
        }

        //Propiedades
        public string TipoSesion { get; set; }

        public string NumeroSesion { get; set; }

        public DateTime FechaSesion { get; set; }

        public string Encabezado { get; set; }

        public string PiePagina { get; set; }
    }
}