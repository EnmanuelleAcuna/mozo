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
    
    public partial class SGJD_ADM_TAB_AVISOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SGJD_ADM_TAB_AVISOS()
        {
            this.SGJD_ADM_TAB_ESTADOS = new HashSet<SGJD_ADM_TAB_ESTADOS>();
            this.SGJD_ADM_TAB_USUARIOS = new HashSet<SGJD_ADM_TAB_USUARIOS>();
            this.SGJD_ADM_TAB_USUARIOS1 = new HashSet<SGJD_ADM_TAB_USUARIOS>();
        }
    
        public int LLP_Id { get; set; }
        public Nullable<int> LLF_IdUnidad { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string TipoDestinatario { get; set; }
        public string Tipo { get; set; }
    
        public virtual SGJD_ADM_TAB_UNIDADES SGJD_ADM_TAB_UNIDADES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ADM_TAB_ESTADOS> SGJD_ADM_TAB_ESTADOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ADM_TAB_USUARIOS> SGJD_ADM_TAB_USUARIOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ADM_TAB_USUARIOS> SGJD_ADM_TAB_USUARIOS1 { get; set; }
    }
}
