using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioTipoObjetoViewModel {
        //Constructor
        public InicioTipoObjetoViewModel(TipoObjeto TipoObjetoModel) {
            IdTipoObjeto = TipoObjetoModel.Id;
            Descripcion = TipoObjetoModel.Descripcion;
            Consecutivo = TipoObjetoModel.Consecutivo;
            ParametroEdicion = TipoObjetoModel.ParametroEdicion == true ? "Si" : "No";
            EncabezadoPiePagina = TipoObjetoModel.ParametroEncabezadoPie == true ? "Si" : "No";
        }

        //Propiedades
        public int IdTipoObjeto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Consecutivo")]
        public int Consecutivo { get; set; }

        [Display(Name = "Parametro de edición")]
        public string ParametroEdicion { get; set; }

        [Display(Name = "Encabezado y pie de pagina")]
        public string EncabezadoPiePagina { get; set; }
    }
}