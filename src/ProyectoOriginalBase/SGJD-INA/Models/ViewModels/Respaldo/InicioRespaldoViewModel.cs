using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioRespaldoViewModel {
        //Constructor
        public InicioRespaldoViewModel() { }

        public InicioRespaldoViewModel(Respaldo RespaldoModelo) {
            IdRespaldo = RespaldoModelo.Id;
            Nombre = RespaldoModelo.NombreRespaldo;
            FechaHora = RespaldoModelo.FechaHora.ToString("dd/MM/yyyy hh:mm tt");
            NombreUsuario = RespaldoModelo.Usuario.Nombre;
            UrlArchivo = RespaldoModelo.URLArchivoRepositorio;
        }

        public InicioRespaldoViewModel(RespaldoPorFechaDTO RespaldoModelo) {
            IdRespaldo = RespaldoModelo.Id;
            Nombre = RespaldoModelo.NombreRespaldo;
            FechaHora = RespaldoModelo.FechaHora.ToString("dd/MM/yyyy hh:mm tt");
            NombreUsuario = RespaldoModelo.NombreUsuario;
            UrlArchivo = RespaldoModelo.UrlArchivoRepositorio;
        }

        //Atributos
        public int IdRespaldo { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha y Hora")]
        public string FechaHora { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Url")]
        public string UrlArchivo { get; set; }
    }
}