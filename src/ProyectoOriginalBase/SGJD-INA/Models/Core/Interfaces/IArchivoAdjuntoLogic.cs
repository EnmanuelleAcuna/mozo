using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IArchivoAdjuntoLogic {
        /// <summary>
        /// Agregra un archivo adjunto tanto al repositorio como a la base de datos, es decir,
        /// guardar fisicamente y en la base de datos el archivo y en la carpeta fisica
        /// </summary>
        Task<ArchivoAdjunto> AgregarAsync(ArchivoAdjunto Modelo, HttpPostedFileBase Archivo);
        Task<ArchivoAdjunto> AgregarAsync(ArchivoAdjunto Modelo, bool copia, HttpPostedFileBase Archivo);

        /// <summary>
        /// Actualizar un archivo adjunto
        /// </summary>
        Task<bool> ActualizarAsync(ArchivoAdjunto Modelo);

        /// <summary>
        /// Eliminar un archivo adjunto en la base de datos y fisicamente
        /// </summary>
        Task<bool> EliminarAsync(ArchivoAdjunto Modelo);
        bool EliminarAsync(string URL);

        // Obtener lista de elementos ArchivoAdjunto
        Task<IEnumerable<ArchivoAdjunto>> ObtenerTodos();

        // Obtener un elemento ArchivoAdjunto por id
        Task<ArchivoAdjunto> ObtenerPorIdAsync(int IdArchivo);

        // Obtener un adjunto por id de objeto y nombre de objeto que es el nombre de la tabla
        Task<ArchivoAdjunto> ObtenerPorIdObjetoAsync(int IdObjeto, string NombreObjeto);

        // Obtener  todos los adjunto por id de objeto y nombre de objeto que es el nombre de la tabla
        Task<IEnumerable<ArchivoAdjunto>> ObtenerTodosPorIdObjetoAsync(int IdObjeto, string NombreObjeto);

        // Obtener todos los adjuntos por id de objeto y id de tipo de objeto
        Task<IEnumerable<ArchivoAdjunto>> ObtenerTodosPorIdObjetoIdTipoObjetoAsync(int IdObjeto, int idTipoObjeto);

        // Obtener todos los adjuntos por id de objeto
        Task<ArchivoAdjunto> ObtenerPorIdObjeto(int IdObjeto);
    }
}