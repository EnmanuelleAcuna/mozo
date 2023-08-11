using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SGJD_INA.Models.Entities {
    public class EncabezadoPiePagina {
        // Constructores
        public EncabezadoPiePagina() { }

        public EncabezadoPiePagina(SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA ModeloDA) {
            Id = ModeloDA.LLP_Id;
            IdTipoObjeto = ModeloDA.LLF_IdTipoObjeto;
            Encabezado = ModeloDA.Encabezado;
            PiePagina = ModeloDA.PiePagina;
            NombreObjeto = ModeloDA.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public EncabezadoPiePagina(SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA ModeloDA, SGJD_ADM_TAB_TIPOS_OBJETO TipoObjetoBD) {
            Id = ModeloDA.LLP_Id;
            IdTipoObjeto = ModeloDA.LLF_IdTipoObjeto;
            Encabezado = ModeloDA.Encabezado;
            PiePagina = ModeloDA.PiePagina;
            NombreObjeto = ModeloDA.GetType().UnderlyingSystemType.BaseType.Name;

            TipoObjeto = new TipoObjeto(TipoObjetoBD);
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Tipo de objeto")]
        [Required(ErrorMessage = "El tipo de objeto es requerido")]
        public int IdTipoObjeto { get; set; }

        [Display(Name = "Encabezado")]
        [Required(ErrorMessage = "El encabezado es requerido")]
        [AllowHtml]
        public string Encabezado { get; set; }

        [Display(Name = "Pie de página")]
        [Required(ErrorMessage = "El pie de página es requerido")]
        [AllowHtml]
        public string PiePagina { get; set; }

        public string NombreObjeto { get; set; }

        public TipoObjeto TipoObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre objeto: " + NombreObjeto + ", " +
                "Id: " + Id + ", " +
                "IdTipoObjeto: " + IdTipoObjeto + ", " +
                "Encabezado: " + Encabezado + ", " +
                "PiePagina: " + PiePagina;
        }

        public SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA BD() {
            return new SGJD_ADM_TAB_ENCABEZADO_PIEPAGINA() {
                LLP_Id = Id,
                LLF_IdTipoObjeto = IdTipoObjeto,
                Encabezado = Encabezado,
                PiePagina = PiePagina
            };
        }
    }
}