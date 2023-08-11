using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IArticuloLogic {
        /// <summary>
        /// Agregar un Artículo
        /// </summary>
        Task<int> AgregarAsync(Articulo ModeloArticulo, int IdSesion, string TituloCapitulo);

        /// <summary>
        /// Actualizar un artículo
        /// </summary>
        Task<bool> ActualizarAsync(Articulo ModeloArticulo);

        /// <summary>
        /// Eliminar un articulo de un capitulo de una acta, mediante su identificador unico
        /// </summary>
        Task<bool> EliminarAsync(int IdArticulo);

        /// <summary>
        /// Obtener un artículo por su id
        /// </summary>
        Task<Articulo> ObtenerPorIdAsync(int IdArticulo);

        /// <summary>
        /// Obtener todos los Artículos por id de Capítulo
        /// </summary>
        Task<IEnumerable<Articulo>> ObtenerTodosPorIdCapituloAsync(int IdCapitulo);

        /// <summary>
        /// Obtener todos los adjuntos de los articulos y sus temas.
        /// </summary>
        Task<IEnumerable<ArchivoAdjunto>> ObtenerArchivosRelacionadosAsync(int IdArticulo);

        /// <summary>
        /// Agregar un archivo adjunto al articulo
        /// </summary>
        Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo);

        /// <summary>
        /// Excluir un artículo del Acta, cambiando estado del tema del articulo a pendiente
        /// </summary>
        Task<bool> ExcluirArticulo(Articulo Modelo);
    }
}