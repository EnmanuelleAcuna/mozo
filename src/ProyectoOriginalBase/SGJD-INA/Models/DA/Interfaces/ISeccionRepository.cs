using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface ISeccionRepository {
        /// <summary>
        /// Guardar una sección en la base de datos
        /// </summary>
        Task<bool> GuardarAsync(Seccion Modelo);

        /// <summary>
        /// Actualizar una sección en la base de datos
        /// </summary>
        Task<bool> ActualizarAsync(Seccion Modelo);

        /// <summary>
        /// Eliminar una sección de la base de datos
        /// </summary>
        Task<bool> EliminarAsync(int IdSeccion);

        /// <summary>
        /// Obtener una sección de la base de datos por ID
        /// </summary>
        Task<Seccion> ObtenerPorIdAsync(int IdSeccion);
    }
}