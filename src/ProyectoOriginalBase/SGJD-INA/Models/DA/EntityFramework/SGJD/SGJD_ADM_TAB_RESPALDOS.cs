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
    
    public partial class SGJD_ADM_TAB_RESPALDOS
    {
        public int LLP_Id { get; set; }
        public string LLF_IdUsuario { get; set; }
        public System.DateTime FechaHora { get; set; }
        public string NombreRespaldo { get; set; }
        public string URLArchivoBD { get; set; }
        public string URLArchivoRepositorio { get; set; }
    
        public virtual SGJD_ADM_TAB_USUARIOS SGJD_ADM_TAB_USUARIOS { get; set; }
    }
}