//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGJD_INA.Models.DA.EntityFramework.SGJD
{
    using System;
    using System.Collections.Generic;
    
    public partial class SGJD_ACT_TAB_ACTAS_DETALLE_ACUERSOFT
    {
        public int LLP_Id { get; set; }
        public int LLF_IdActaAcuersoft { get; set; }
        public string Numero_Acta { get; set; }
        public Nullable<int> Indice { get; set; }
        public Nullable<int> Tipo { get; set; }
        public Nullable<int> Ancestro { get; set; }
        public Nullable<bool> Define_Acuerdo { get; set; }
        public string Texto { get; set; }
        public string Titulo { get; set; }
    
        public virtual SGJD_ACT_TAB_ACTAS_ACUERSOFT SGJD_ACT_TAB_ACTAS_ACUERSOFT { get; set; }
    }
}