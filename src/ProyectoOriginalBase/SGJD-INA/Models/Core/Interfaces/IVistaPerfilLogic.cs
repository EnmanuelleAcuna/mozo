using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IVistaPerfilLogic {
        /// <summary>
        /// Agregar un elemento VistaPerfil (Vista a un Perfil)
        /// </summary>
        Task<bool> AgregarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Actualizar un permiso de perfil e una vista
        /// </summary>
        Task<bool> ActualizarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Eliminar un acceso a vista de un perfil
        /// </summary>
        Task<bool> EliminarAsync(VistaPerfil Modelo);

        /// <summary>
        /// Obtener todos los elementos VistaPerfil de un Perfil
        /// </summary>
        IEnumerable<VistaPerfil> ObtenerTodosPorIdPerfil(string IdPerfil);
        Task<IEnumerable<VistaPerfil>> ObtenerTodosPorIdPerfilAsync(string IdPerfil);

        /// <summary>
        /// Obtener todos los elementos VistaPerfil de una Vista
        /// </summary>
        IEnumerable<VistaPerfil> ObtenerTodosPorIdVista(int IdVista);
        Task<IEnumerable<VistaPerfil>> ObtenerTodosPorIdVistaAsync(int IdVista);

        /// <summary>
        /// Obtener un elemento VistaPerfil a partir del id de vista y el id del perfil
        /// </summary>
        VistaPerfil ObtenerPorId(string IdPerfil, int IdVista);
        Task<VistaPerfil> ObtenerPorIdAsync(string IdPerfil, int IdVista);

        /// <summary>
        /// Obtener todas las vistas de un perfil
        /// </summary>
        IEnumerable<Vista> ObtenerVistasPorIdPerfil(string IdPerfil);
        Task<IEnumerable<Vista>> ObtenerVistasPorIdPerfilAsync(string IdPerfil);

        /// <summary>
        /// Obtener todos los perfiles que tienen acceso a una vista
        /// </summary>
        Task<IEnumerable<ApplicationRole>> ObtenerPerfilesPorIdVista(int IdVista);

        // Asignar un valor al atributo permiso del objeto VistaPerfil
        VistaPerfil AsignarPermiso(VistaPerfil Modelo, string Crear, string Editar, string Eliminar);

        //Obtener el valor del atributo Permiso del elemento VistaPerfil
        int ObtenerPermiso(string IdPerfil, int IdVista);
    }
}