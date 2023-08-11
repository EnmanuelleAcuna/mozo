using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IUsuarioSIRHLogic {
        /// <summary>
        /// Obtener todos los usuarios de SIRH del INA
        /// </summary>
        Task<IEnumerable<UsuarioSIRH>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los usuarios de SIRH por nombre de usuario
        /// </summary>
        Task<IEnumerable<UsuarioSIRH>> ObtenerTodosPorNombreUsuarioAsync(string NombreUsuario);

        /// <summary>
        /// Verificar si un usuario existe dos veces con el mismo nombre de usuario (campo UserName) dentro de SIRH
        /// Si se devuelve true significa que existe mas de un usuario en SIRH con dicho nombre de usuario, por lo 
        /// que se debería reportar al INA para solucionar el error.
        /// Si se devuelve false, significa que existe en SIRH un sólo registo con dicho nombre de usuario, lo que 
        /// quiere decir que no existirá alguna duplicidad al iniciar sesión con A.D.
        /// </summary>
        Task<bool> VerificarSiExisteDuplicadoAsync(string NombreUsuario);
    }
}