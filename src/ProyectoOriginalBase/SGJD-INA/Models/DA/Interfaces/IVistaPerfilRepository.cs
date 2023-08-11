using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IVistaPerfilRepository {
        /// <summary>
        /// Agrgar una VistaPerfil en la base de datos
        /// </summary>
        Task<bool> AgregarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Actualizar el permiso de un elemento VistaPerfil (Vista de un Perfil)
        /// </summary>
        Task<bool> ActualizarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Eliminar una VistaPerfil en la base de datos
        /// </summary>
        Task<bool> EliminarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Obtener todos los perfiles y vistas por Id
        /// </summary>
        /// 
        Task<VistaPerfil> ObtenerPorIdAsync(string IdPerfil, int IdVista);
    }
}