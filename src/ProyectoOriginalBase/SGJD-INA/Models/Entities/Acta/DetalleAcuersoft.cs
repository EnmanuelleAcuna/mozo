using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class DetalleAcuersoft {
        // Constructor
        public DetalleAcuersoft() { }

        // Constructor para atributos
        public DetalleAcuersoft(SGJD_ACT_TAB_ACTAS_DETALLE_ACUERSOFT DetalleAcuersoftBD) {
            Id = DetalleAcuersoftBD.LLP_Id;
            IdActa = DetalleAcuersoftBD.LLF_IdActaAcuersoft;
            NumeroActa = DetalleAcuersoftBD.Numero_Acta;
            Indice = DetalleAcuersoftBD.Indice;
            Tipo = DetalleAcuersoftBD.Tipo;
            Ancestro = DetalleAcuersoftBD.Ancestro;
            DefineAcuerdo = DetalleAcuersoftBD.Define_Acuerdo;
            Texto = DetalleAcuersoftBD.Texto;
            Titulo = DetalleAcuersoftBD.Titulo;
        }

        // Atributos
        public int Id { get; set; }
        public int IdActa { get; set; }
        public string NumeroActa { get; set; }
        public int? Indice { get; set; }
        public int? Tipo { get; set; }
        public int? Ancestro { get; set; }
        public bool? DefineAcuerdo { get; set; }
        public string Texto { get; set; }
        public string Titulo { get; set; }

    }
}