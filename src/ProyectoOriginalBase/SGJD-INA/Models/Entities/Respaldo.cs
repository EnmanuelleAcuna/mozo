using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class Respaldo {
        public Respaldo() { }

        public Respaldo(SGJD_ADM_TAB_RESPALDOS RespaldoBD) {
            Id = RespaldoBD.LLP_Id;
            IdUsuario = RespaldoBD.LLF_IdUsuario;
            FechaHora = RespaldoBD.FechaHora;
            NombreRespaldo = RespaldoBD.NombreRespaldo;
            URLArchivoBD = RespaldoBD.URLArchivoBD;
            URLArchivoRepositorio = RespaldoBD.URLArchivoRepositorio;
            NombreObjeto = RespaldoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Usuario = new ApplicationUser(RespaldoBD.SGJD_ADM_TAB_USUARIOS);
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario es requerido.")]
        [Display(Name = "Usuario")]
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre")]
        public string NombreRespaldo { get; set; }

        [Required(ErrorMessage = "La URL del archivo es requerida.")]
        [Display(Name = "URLArchivoBD")]
        public string URLArchivoBD { get; set; }

        [Required(ErrorMessage = "La URL del repositorio es requerida.")]
        [Display(Name = "URLArchivoRepositorio")]
        public string URLArchivoRepositorio { get; set; }

        public string NombreObjeto { get; set; }

        public ApplicationUser Usuario { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "idUsuario:" + IdUsuario + ", " +
                "Fecha:" + Convert.ToString(FechaHora) + ", " +
                "nombreRespaldo:" + Convert.ToString(NombreRespaldo) + ", " +
                "URLArchivoBD:" + Convert.ToString(URLArchivoBD) + ", " +
                "URLArchivoRepositorio" + URLArchivoRepositorio;
        }
    }
}