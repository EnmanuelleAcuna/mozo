using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetallesTipoObjetoViewModel {
        // Constructores
        public DetallesTipoObjetoViewModel() { }

        public DetallesTipoObjetoViewModel(TipoObjeto ModeloTipoObjeto) {
            Id = ModeloTipoObjeto.Id;
            Descripcion = ModeloTipoObjeto.Descripcion;
            NombreTabla = ModeloTipoObjeto.NombreTabla;
            Nomenclatura = ModeloTipoObjeto.Nomenclatura;
            ParametroEdicion = ModeloTipoObjeto.ParametroEdicion ? "Si" : "No";
            ParametroEncabezadoPie = ModeloTipoObjeto.ParametroEncabezadoPie ? "Si" : "No";
            Consecutivo = ModeloTipoObjeto.Consecutivo;
        }

        // Propiedades
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Descripcion { get; set; }

        [Display(Name = "Tabla asignada")]
        public string NombreTabla { get; set; }

        [Display(Name = "Parámetro de edición")]
        public string ParametroEdicion { get; set; }

        [Display(Name = "Encabezado y pie de página")]
        public string ParametroEncabezadoPie { get; set; }

        [Display(Name = "Consecutivo")]
        public int Consecutivo { get; set; }

        [Display(Name = "Nomenclatura del objeto")]
        public string Nomenclatura { get; set; }
    }
}