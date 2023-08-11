using SGJD_INA.Models.DA.EntityFramework.SGJD;

namespace SGJD_INA.Models.Entities {
    public class ElementoRelacionado {
        //Constructores
        public ElementoRelacionado() { }

        public ElementoRelacionado(SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS ElementoRelacionadoBD) {
            Id = ElementoRelacionadoBD.LLP_Id;
            IdActa = ElementoRelacionadoBD.LLF_IdActa;
            IdAcuerdo = ElementoRelacionadoBD.LLF_IdAcuerdo;
            IdSeguimiento = ElementoRelacionadoBD.LLF_IdSeguimiento;
            TipoElemento = ElementoRelacionadoBD.TipoElemento;
            NombreObjeto = ElementoRelacionadoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para propiedades y propiedades de navegación padres
        public ElementoRelacionado(SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS ElementoRelacionadoBD, SGJD_ACU_TAB_SEGUIMIENTOS SeguimientoAcuerdoBD, SGJD_ACT_TAB_ACTAS ActaBD, SGJD_ACU_TAB_ACUERDOS AcuerdoBD) {
            Id = ElementoRelacionadoBD.LLP_Id;
            IdActa = ElementoRelacionadoBD.LLF_IdActa;
            IdAcuerdo = ElementoRelacionadoBD.LLF_IdAcuerdo;
            IdSeguimiento = ElementoRelacionadoBD.LLF_IdSeguimiento;
            TipoElemento = ElementoRelacionadoBD.TipoElemento;
            NombreObjeto = ElementoRelacionadoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Seguimiento = new SeguimientoAcuerdo(SeguimientoAcuerdoBD);
            Acta = (ActaBD == null) ? new Acta() : new Acta(ActaBD, ActaBD.SGJD_ACT_TAB_SESIONES, ActaBD.SGJD_ACT_TAB_TOMOS, ActaBD.SGJD_ADM_TAB_ESTADOS, ActaBD.SGJD_ACT_TAB_SESIONES1, ActaBD.SGJD_ADM_TAB_USUARIOS, ActaBD.SGJD_ADM_TAB_USUARIOS1);
            Acuerdo = (AcuerdoBD == null) ? new Acuerdo() : new Acuerdo(AcuerdoBD);
        }
        //Propiedades
        public int Id { get; set; }

        public int? IdActa { get; set; }

        public int? IdAcuerdo { get; set; }

        public int IdSeguimiento { get; set; }

        public string TipoElemento { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padres
        public Acta Acta { get; set; }

        public Acuerdo Acuerdo { get; set; }

        public SeguimientoAcuerdo Seguimiento { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto" + NombreObjeto + "," +
            "Id:" + Id + ", " +
            "IdActa:" + IdActa + ", " +
            "IdAcuerdo:" + IdAcuerdo + ", " +
            "IdSeguimiento:" + IdSeguimiento + ", " +
            "TipoElemento:" + TipoElemento;
        }

        /// <summary>
        /// Convertir un modelo a entidad de base de datos
        /// </summary>
        public SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS BD() {
            return new SGJD_ACU_TAB_ELEMENTOS_RELACIONADOS() {
                LLP_Id = Id,
                LLF_IdActa = IdActa,
                LLF_IdAcuerdo = IdAcuerdo,
                LLF_IdSeguimiento = IdSeguimiento,
                TipoElemento = TipoElemento
            };
        }
    }
}