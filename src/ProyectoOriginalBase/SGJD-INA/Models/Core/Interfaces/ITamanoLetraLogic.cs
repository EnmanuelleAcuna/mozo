using SGJD_INA.Models.Entities;
using System.Collections.Generic;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITamanoLetraLogic {
        /// <summary>
        /// Obtener listado de todos los tamaños de letra disponible para editor HTML
        /// </summary>
        IEnumerable<TamanoLetra> ObtenerTodos();
    }
}