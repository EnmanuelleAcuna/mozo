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
    
    public partial class SGJD_ADM_TAB_TIPOS_USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SGJD_ADM_TAB_TIPOS_USUARIO()
        {
            this.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION = new HashSet<SGJD_ACT_TAB_OTROS_ASISTENTES_SESION>();
            this.SGJD_ADM_TAB_USUARIOS = new HashSet<SGJD_ADM_TAB_USUARIOS>();
        }
    
        public int LLP_Id { get; set; }
        public string Nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ACT_TAB_OTROS_ASISTENTES_SESION> SGJD_ACT_TAB_OTROS_ASISTENTES_SESION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ADM_TAB_USUARIOS> SGJD_ADM_TAB_USUARIOS { get; set; }
    }
}
