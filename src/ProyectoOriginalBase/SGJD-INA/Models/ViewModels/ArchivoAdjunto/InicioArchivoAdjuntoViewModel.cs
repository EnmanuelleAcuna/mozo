using SGJD_INA.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class InicioArchivoAdjuntoViewModel {
        #region Constructor
        #endregion
        public InicioArchivoAdjuntoViewModel() { }

        public InicioArchivoAdjuntoViewModel(ArchivoAdjunto ArchivoAdjuntoBD, string NombreObjeto) {
            IdArchivoAdjunto = ArchivoAdjuntoBD.IdArchivo;
            IdObjeto = ArchivoAdjuntoBD.IdObjeto;
            Observacion = ArchivoAdjuntoBD.Observaciones;
            UrlArchivo = ArchivoAdjuntoBD.UrlArchivo;
            NombreTipoArchivo = ArchivoAdjuntoBD.TipoArchivo.Nombre;
            this.NombreObjeto = NombreObjeto;
        }

        public InicioArchivoAdjuntoViewModel(ArchivoAdjunto ArchivoAdjuntoBD) {
            IdArchivoAdjunto = ArchivoAdjuntoBD.IdArchivo;
            Observacion = ArchivoAdjuntoBD.Observaciones;
            UrlArchivo = ArchivoAdjuntoBD.UrlArchivo;
            NombreTipoArchivo = ArchivoAdjuntoBD.TipoArchivo.Nombre;
        }

        #region Atributos
        #endregion
        public int IdArchivoAdjunto { get; set; }

        public int IdObjeto { get; set; }

        [Display(Name = "Resumen")]
        public string Observacion { get; set; }

        [Display(Name = "Extensión")]
        public string NombreTipoArchivo { get; set; }

        [Display(Name = "Dirección")]
        public string UrlArchivo { get; set; }

        public string NombreObjeto { get; set; }
    }
}