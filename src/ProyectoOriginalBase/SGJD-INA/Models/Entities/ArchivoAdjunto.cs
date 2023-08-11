using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SGJD_INA.Models.Entities {
    public class ArchivoAdjunto {
        // Constructores
        public ArchivoAdjunto() { }

        public ArchivoAdjunto(SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS ArchivoAdjuntoBD) {
            IdArchivo = ArchivoAdjuntoBD.LLP_Id;
            IdObjeto = ArchivoAdjuntoBD.IdObjeto;
            IdRepositorio = ArchivoAdjuntoBD.LLF_IdRepositorio;
            IdTipoArchivo = (int)ArchivoAdjuntoBD.LLF_TipoArchivo;
            IdTipoObjeto = ArchivoAdjuntoBD.LLF_IdTipoObjeto;
            Observaciones = ArchivoAdjuntoBD.Observacion;
            UrlArchivo = ArchivoAdjuntoBD.UrlArchivo;
            NombreObjeto = ArchivoAdjuntoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public ArchivoAdjunto(SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS ArchivoAdjuntoBD, SGJD_ADM_TAB_REPOSITORIOS RepositorioBD, SGJD_ADM_TAB_TIPOS_ARCHIVO TipoArchivoBD, SGJD_ADM_TAB_TIPOS_OBJETO TipoObjetoBD) {
            IdArchivo = ArchivoAdjuntoBD.LLP_Id;
            IdObjeto = ArchivoAdjuntoBD.IdObjeto;
            IdRepositorio = ArchivoAdjuntoBD.LLF_IdRepositorio;
            IdTipoArchivo = (int)ArchivoAdjuntoBD.LLF_TipoArchivo;
            IdTipoObjeto = ArchivoAdjuntoBD.LLF_IdTipoObjeto;
            Observaciones = ArchivoAdjuntoBD.Observacion;
            UrlArchivo = ArchivoAdjuntoBD.UrlArchivo;
            NombreObjeto = ArchivoAdjuntoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Repositorio = new Repositorio(RepositorioBD);
            TipoArchivo = new TipoArchivo(TipoArchivoBD);
            TipoObjeto = new TipoObjeto(TipoObjetoBD);
        }

        // Propiedades
        public int IdArchivo { get; set; }

        [Required(ErrorMessage = "El tipo objeto es requerido")]
        public int IdTipoObjeto { get; set; }

        [Required(ErrorMessage = "El repositorio es requerido")]
        public int IdRepositorio { get; set; }

        [Required(ErrorMessage = "El tipo archivo es requerido")]
        public int IdTipoArchivo { get; set; }

        [Required(ErrorMessage = "El objeto es requerido")]
        public int IdObjeto { get; set; }

        [Required(ErrorMessage = "observaciones")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "El Url es requerido")]
        public string UrlArchivo { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación 
        // Padres
        public TipoObjeto TipoObjeto { get; set; }

        public TipoArchivo TipoArchivo { get; set; }

        public Repositorio Repositorio { get; set; }

        // Relacion especial para IdObjeto, ya que ese objeto puede ser un tema, un articulo, un acuerdo o un seguimiento
        public Tema Tema { get; set; }
        public Articulo Articulo { get; set; }
        public Acuerdo Acuerdo { get; set; }
        public SeguimientoAcuerdo Seguimiento { get; set; }

        // Metodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + IdArchivo + ", " +
                "idTipoObjeto:" + IdTipoObjeto + ", " +
                "idRepositorio:" + IdRepositorio + ", " +
                "idTipoArchivo:" + IdTipoArchivo + ", " +
                "idObjeto:" + IdObjeto + ", " +
                "observaciones" + Observaciones + ", " +
                "urlArchivo" + UrlArchivo;
        }

        /// <summary>
        /// Crea una entidad de base de datos apartir de un modelo de Archivo adjunto.
        /// </summary>
        public SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS BD() {
            return new SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS() {
                LLP_Id = IdArchivo,
                IdObjeto = IdObjeto,
                LLF_IdRepositorio = IdRepositorio,
                LLF_IdTipoObjeto = IdTipoObjeto,
                LLF_TipoArchivo = IdTipoArchivo,
                Observacion = Observaciones,
                UrlArchivo = UrlArchivo
            };
        }
    }
}