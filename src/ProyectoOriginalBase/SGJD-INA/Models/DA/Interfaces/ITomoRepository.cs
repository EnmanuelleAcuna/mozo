using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface ITomoRepository {
        /// <summary>
        /// Guardar un tomo en la base de datos
        /// </summary>
        Task<Tomo> GuardarAsync(Tomo ModeloTomo);

        /// <summary>
        /// Actualizar un tomo en la base de datos
        /// </summary>
        Task<bool> ActualizarAsync(Tomo ModeloTomo);

        /// <summary>
        /// Eliminar un tomo de la base de datos
        /// </summary
        Task<bool> EliminarAsync(int IdTomo);

        /// <summary>
        /// Obtener todos los tomos (Sin incluir actas de cada tomo)
        /// </summary>
        IEnumerable<Tomo> ObtenerTodosSinActas();

        /// <summary>
        /// Obtener todos los tomos (Incluyendo actas de cada tomo)
        /// </summary>
        IEnumerable<Tomo> ObtenerTodosConActas();

        /// <summary>
        /// Obtener un tomo de la base de datos (Sin incluir las actas del tomo)
        /// </summary>
        Tomo ObtenerPorIdSinActas(int IdTomo);

        /// <summary>
        /// Obtener un tomo de la base de datos (Incluyendo las actas del tomo)
        /// </summary
        Tomo ObtenerPorIdConActas(int IdTomo);
    }
}