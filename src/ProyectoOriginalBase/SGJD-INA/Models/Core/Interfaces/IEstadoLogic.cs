using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IEstadoLogic {
        /// <summary>
        /// Agregar un Estado de Objeto
        /// </summary>
        Task<bool> AgregarAsync(Estado ModeloEstado);

        /// <summary>
        /// Actualizar un Estado de Objeto
        /// </summary>
        Task<bool> ActualizarAsync(Estado ModeloEstado);

        /// <summary>
        /// Eliminar un Estado de Objeto
        /// </summary>
        Task<bool> EliminarAsync(int IdEstado);

        /// <summary>
        /// Obtener todos los Estados de Objeto
        /// </summary>
        IEnumerable<Estado> ObtenerTodos();
        Task<IEnumerable<Estado>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un Estado de Objeto por su id
        /// </summary>
        Task<Estado> ObtenerPorIdAsync(int IdEstado);

        /// <summary>
        /// Obtener un Estado de Objeto por su codigo
        /// </summary>
        Task<Estado> ObtenerPorCodigoAsync(string CodigoEstado);

        /// <summary>
        /// Obtener todos los estados de Objeto por id de Tipo de Objeto
        /// </summary>
        Task<IEnumerable<Estado>> ObtenerTodosPorIdTipoObjetoAsync(int IdTipoObjeto);

        /// <summary>
        /// Obtener todos los estados de Objeto el nombre de la tabla
        /// </summary>
        Task<IEnumerable<Estado>> ObtenerTodosPorNombreObjetoAsync(string NombreTabla);

        /// <summary>
        /// Obtener todos los estados de Objeto el nombre de la tabla con aviso y su usuario para notificacion de acuerdo
        /// </summary>
        Task<IEnumerable<Estado>> ObtenerTodosPorNombreObjetoConAvisoParaAcuerdoAsync(string NombreTabla);
    }
}