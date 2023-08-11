using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IAyudaUsuarioLogic {
        /// <summary>
        /// Actualizar un ayuda a usuario
        /// </summary>
        Task<bool> ActualizarAsync(AyudaUsuario ModeloayudaUsuario);

        /// <summary>
        /// Obtener todos las vistas de ayuda de usuarios 
        /// </summary>
        IEnumerable<AyudaUsuario> ObtenerTodos();
        Task<IEnumerable<AyudaUsuario>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener una ayuda de usuario por su id
        /// </summary>
        Task<AyudaUsuario> ObtenerPorIdAsync(int IdAyudaUsuario);

        /// <summary>
        /// Obtener una ayuda de usuario por su Abreviatura
        /// </summary>
        Task<AyudaUsuario> ObtenerPorAbreviaturaAsync(string Abreviatura);
    }
}
