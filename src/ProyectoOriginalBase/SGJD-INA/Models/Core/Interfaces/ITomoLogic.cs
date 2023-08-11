using Ionic.Zip;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITomoLogic {
        /// <summary>
        /// Agregar un Tomo
        /// </summary>
        Task<Tomo> AgregarAsync(Tomo ModeloTomo);

        /// <summary>
        /// Actualizar un Tomo
        /// </summary>
        Task<bool> ActualizarAsync(Tomo ModeloTomo);

        /// <summary>
        /// Eliminar un Tomo
        /// </summary>
        Task<bool> EliminarAsync(int IdTomo);

        /// <summary>
        /// Cerrar un tomo
        /// </summary>
        Task<Tomo> CerrarAsync(Tomo TomoCerrado);

        /// <summary>
        /// Actualizar la URL donde se encuentra guardado el PDF con oficio de apertura 
        /// </summary>
        Task<bool> ActualizarUrlOficioAperturaAsync(int IdTomo, string UrlOficioApertura);

        /// <summary>
        /// Actualizar la URL donde se encuentra guardado el PDF con oficio de cierre 
        /// </summary>
        Task<bool> ActualizarUrlOficioCierreAsync(int IdTomo, string UrlOficioCierre);

        /// <summary>
        /// Notificar a los usuarios pertenecientes al aviso, sobre la apertura/cierre de un tomo
        /// </summary>
        Task<bool> NotificarAsync(string NombreAviso, string Enlace, string TextoEnlace, string MensajeDetalle);

        /// <summary>
        /// Obtener todos los Tomos (Incluyendo las Actas del Tomo)
        /// </summary>
        IEnumerable<Tomo> ObtenerTodosSinActas();
        Task<IEnumerable<Tomo>> ObtenerTodosSinActasAsync();

        /// <summary>
        /// Obtener todos los Tomos (Incluyendo las Actas del Tomo)
        /// </summary>
        IEnumerable<Tomo> ObtenerTodosConActas();
        Task<IEnumerable<Tomo>> ObtenerTodosConActasAsync();

        /// <summary>
        /// Obtener todos los tomos con un estado especifico
        /// </summary>
        IEnumerable<Tomo> ObtenerTodosPorCodigoEstado(int IdEstado);
        Task<IEnumerable<Tomo>> ObtenerTodosPorCodigoEstadoAsync(int IdEstado);

        /// <summary>
        /// Obtener un Tomo por su Id sin lista de Actas 
        /// </summary>
        Tomo ObtenerPorIdSinActas(int IdTomo);
        Task<Tomo> ObtenerPorIdSinActasAsync(int IdTomo);

        /// <summary>
        /// Obtener Tomo por su Id con lista de Actas
        /// </summary>
        Task<Tomo> ObtenerPorIdConActasAsync(int IdTomo);

        /// <summary>
        /// Obtener el último tomo abierto
        /// </summary>
        Task<Tomo> ObtenerUltimoAbiertoAsync();

        /// <summary>
        /// Obtener el consecutivo actual de páginas de un Tomo por su Id
        /// </summary>
        Task<int> ObtenerConsecutivoPaginaPorIdAsync(int IdTomo);

        /// <summary>
        /// Obtener archivo zip con contenido del Tomo
        /// </summary>
        Task<ZipFile> ObtenerZipAsync(string NombreTomo);
    }
}