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
    
    public partial class SGJD_ACT_TAB_ARTICULOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SGJD_ACT_TAB_ARTICULOS()
        {
            this.SGJD_ACU_TAB_ACUERDOS = new HashSet<SGJD_ACU_TAB_ACUERDOS>();
            this.SGJD_ACT_TAB_USUARIOS_ARTICULO = new HashSet<SGJD_ACT_TAB_USUARIOS_ARTICULO>();
        }
    
        public int LLP_Id { get; set; }
        public int LLF_IdCapitulo { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool Confidencial { get; set; }
        public int LLF_IdTema { get; set; }
        public string Observacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ACU_TAB_ACUERDOS> SGJD_ACU_TAB_ACUERDOS { get; set; }
        public virtual SGJD_ACT_TAB_CAPITULOS SGJD_ACT_TAB_CAPITULOS { get; set; }
        public virtual SGJD_ACT_TAB_TEMAS SGJD_ACT_TAB_TEMAS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SGJD_ACT_TAB_USUARIOS_ARTICULO> SGJD_ACT_TAB_USUARIOS_ARTICULO { get; set; }
    }
}