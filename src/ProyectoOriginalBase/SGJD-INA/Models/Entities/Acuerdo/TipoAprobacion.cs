using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.Entities {
    public class TipoAprobacion {
        public TipoAprobacion() { }

        public TipoAprobacion(SGJD_ADM_TAB_TIPOS_APROBACION TipoAprobacionBD) {
            IdTipoAprobacion = TipoAprobacionBD.LLP_Id;
            Nombre = TipoAprobacionBD.Nombre;
            Mensaje = TipoAprobacionBD.Mensaje;
        }

        // Propiedades
        public int IdTipoAprobacion { get; set; }

        [Required(ErrorMessage = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Mensaje")]
        public string Mensaje { get; set; }

        public string ObtenerInformacion() {
            return "Id:" + IdTipoAprobacion + ", " +
                "Nombre:" + Nombre + ", " +
                "Mensaje:" + Mensaje;
        }

        public SGJD_ADM_TAB_TIPOS_APROBACION BD() {
            return new SGJD_ADM_TAB_TIPOS_APROBACION() {
                LLP_Id = IdTipoAprobacion,
                Nombre = Nombre,
                Mensaje = Mensaje,
            };
        }
    }
}