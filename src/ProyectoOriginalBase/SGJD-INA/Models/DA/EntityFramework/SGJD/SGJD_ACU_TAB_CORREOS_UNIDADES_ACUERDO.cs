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
    
    public partial class SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO
    {
        public int LLP_Id { get; set; }
        public int LLF_IdUnidad { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
    
        public virtual SGJD_ADM_TAB_UNIDADES SGJD_ADM_TAB_UNIDADES { get; set; }
    }
}