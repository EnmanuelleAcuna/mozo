using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;

namespace SGJD_INA.Models.Entities {
    public class ActaAcuersoft {
        // Constructores
        public ActaAcuersoft() { }

        // Constructor para atributos
        public ActaAcuersoft(SGJD_ACT_TAB_ACTAS_ACUERSOFT ActaAcuersoftBD) {
            Id = ActaAcuersoftBD.LLP_Id;
            NumeroActa = ActaAcuersoftBD.Numero_Acta;
            Fecha = ActaAcuersoftBD.Fecha;
            Lugar = ActaAcuersoftBD.Lugar;
            TipoSesion = ActaAcuersoftBD.Tipo_Sesion;
            Preside = ActaAcuersoftBD.Preside;
            Secretario = ActaAcuersoftBD.Secretario;
            Estado = ActaAcuersoftBD.Estado;
            FechaCreacion = ActaAcuersoftBD.Fecha_creacion;
            TipoDocumento = ActaAcuersoftBD.Tipo_Documento;
        }

        // Atributos
        public int Id { get; set; }
        public string NumeroActa { get; set; }
        public DateTime? Fecha { get; set; }
        public string Lugar { get; set; }
        public int? TipoSesion { get; set; }
        public string Preside { get; set; }
        public string Secretario { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string TipoDocumento { get; set; }
    }
}