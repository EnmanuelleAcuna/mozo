using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUsuarioLogic {
        /// <summary>
        /// Obtener lista con todos los usuarios
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodos();
        Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los usuarios activos
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodosActivos();
        Task<IEnumerable<ApplicationUser>> ObtenerTodosActivosAsync();

        /// <summary>
        /// Obtener todos los usuarios inactivos
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodosInactivos();
        Task<IEnumerable<ApplicationUser>> ObtenerTodosInactivosAsync();

        /// <summary>
        /// Obtener todos los usuarios de una unidad
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodosPorUnidad(int IdUnidad);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosPorUnidadAsync(int IdUnidad);

        /// <summary>
        /// Obtener todos los usuarios de un tipo de usuario
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodosPorTipoUsuario(int IdTipoUsuario);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosPorTipoUsuarioAsync(int IdTipoUsuario);

        /// <summary>
        /// Obtener todos los usuarios de un rol
        /// </summary>
        IEnumerable<ApplicationUser> ObtenerTodosPorRol(string IdPerfil);
        Task<IEnumerable<ApplicationUser>> ObtenerTodosPorRolAsync(string IdPerfil);

        //Obtener todos los usuarios para DropDownList en vista
        IEnumerable<SelectListItem> ObtenerTodosParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync();

        /// <summary>
        /// Obtener usuario por id
        /// </summary>
        ApplicationUser ObtenerPorId(string IdUsuario);
        Task<ApplicationUser> ObtenerPorIdAsync(string IdUsuario);

        //Obtener usuario por correo electrónico
        ApplicationUser ObtenerPorCorreo(string CorreoUsuario);
        Task<ApplicationUser> ObtenerPorCorreoAsync(string CorreoUsuario);

        //Obtener usuario por cédula
        ApplicationUser ObtenerPorCedula(string Cedula);
        Task<ApplicationUser> ObtenerPorCedulaAsync(string Cedula);

        //Obtener usuario por nombre de usuario de Active Directory de INA
        Task<ApplicationUser> ObtenerPorNombreUsuarioADAsync(string NombreUsuarioAD);
    }
}