using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IActaLogic {
        /// <summary>
        /// Agregar elemento Acta
        /// </summary>
        Task<string> AgregarAsync(int IdSesion, int IdOrdenDia, IEnumerable<AsistenteSesion> ListaTodosAsistentesSesion, IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes);

        /// <summary>
        /// Actualizar un acta
        /// </summary>
        Task<bool> ActualizarAsync(Acta ModeloActa);

        /// <summary>
        /// Eliminar un acta
        /// </summary>
        Task<bool> EliminarAsync(int IdActa);

        /// <summary>
        /// Cambiar el estado de una acta a visto bueno
        /// </summary>
        Task<bool> DarVistoBuenoAsync(int IdActa);

        /// <summary>
        /// Cambiar el estado de una acta a con control de calidad
        /// </summary>
        Task<bool> DarControlCalidadAsync(int IdActa);

        /// <summary>
        /// Cambiar el estado de una acta a aprobarda
        /// </summary>
        Task<bool> AprobarAsync(int IdActa);

        /// <summary>
        /// Agregar archivo con el acta firmada 
        /// </summary>
        Task<bool> AgregarActaFirmadaAsync(Acta ModeloActa, HttpPostedFileBase Archivo);

        /// <summary>
        /// Eliminar archivo con el acta firmada
        /// </summary>
        Task<bool> EliminarActaFirmadaAsync(Acta ModeloActa);

        /// <summary>
        /// Obtener todos los elementos Acta
        /// </summary>
        Task<IEnumerable<Acta>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos las actas que tengan la palabra clave
        /// </summary>
        Task<IEnumerable<ActasPorPalabraClaveDTO>> ObtenerTodosPorPalabraClaveAsync(string Palabra);

        /// <summary>
        /// Obtener un elemento Acta por id
        /// </summary>
        Task<Acta> ObtenerPorIdAsync(int idActa);

        /// <summary>
        /// Obtener un elemento Acta por id de Sesión
        /// </summary>
        Task<Acta> ObtenerPorIdSesionAsync(int IdSesion);

        // Actas acuersoft

        /// <summary>
        /// Obtener todos las actas de acuersoft
        /// </summary>
        Task<IEnumerable<ActasAcuersoftDTO>> ObtenerActasAcuersoftAsync();

        /// <summary>
        /// Obtener todos los detalles de las actas de acuersoft
        /// </summary>
        Task<IEnumerable<ActasDetalleAcuersoftDTO>> ObtenerDetallesActaAcuersoftAsync(int IdActa);

        /// <summary>
        /// Obtener acta de acuersoft por id
        /// </summary>
        Task<ActaAcuersoft> ObtenerActaAcuersoftPorIdAsync(int IdActa);

        /// <summary>
        /// Obtener cantidad de registros de las actas de acuersoft que tengan la palabra clave
        /// </summary>
        Task<IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO>> ObtenerRegistrosAcuersoftPorPalabraClaveAsync(string Palabra);

        /// <summary>
        /// Obtener todos las actas de acuersoft que tengan la palabra clave
        /// </summary>
        Task<IEnumerable<ActasAcuersoftPorPalablaClaveDTO>> ObtenerTodosAcuersoftPorPalabraClaveAsync(string Palabra, int Pagina);
    }
}