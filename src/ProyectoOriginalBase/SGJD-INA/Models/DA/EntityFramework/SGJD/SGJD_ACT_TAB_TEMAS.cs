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
    
    public partial class SGJD_ACT_TAB_TEMAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SGJD_ACT_TAB_TEMAS()
        {
            this.SGJD_ACT_TAB_ARTICULOS = new HashSet<SGJD_ACT_TAB_ARTICULOS>();
        }
    
        public int LLP_Id { get; set; }
        public int LLF_IdSeccion { get; set; }
        public int LLF_IdEstado { get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public int LLF_IdOrdenDia { get; set; }
        public string Observacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ACT_TAB_ARTICULOS> SGJD_ACT_TAB_ARTICULOS { get; set; }
        public virtual SGJD_ACT_TAB_ORDENES_DIA SGJD_ACT_TAB_ORDENES_DIA { get; set; }
        public virtual SGJD_ADM_TAB_ESTADOS SGJD_ADM_TAB_ESTADOS { get; set; }
        public virtual SGJD_ADM_TAB_SECCIONES SGJD_ADM_TAB_SECCIONES { get; set; }
    }
}
