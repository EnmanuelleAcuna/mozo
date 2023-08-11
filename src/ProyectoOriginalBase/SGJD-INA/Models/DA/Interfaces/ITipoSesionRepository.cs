using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface ITipoSesionRepository {
        /// <summary>
        /// Guardar un tipo de sesion en la base de datos
        /// </summary>
        Task<bool> GuardarAsync(TipoSesion Modelo);

        /// <summary>
        /// Actualizar un tipo de sesion en la base de datos
        /// </summary>
        Task<bool> ActualizarAsync(TipoSesion Modelo);

        /// <summary>
        /// Eliminar un tipo de sesion en la base de datos
        /// </summary>
        Task<bool> EliminarAsync(int IdTipoSesion);

        /// <summary>
        /// Obtener un tipo de sesión de la base de datos por ID
        /// </summary>
        Task<TipoSesion> ObtenerPorIdAsync(int IdTipoSesion);
    }
}