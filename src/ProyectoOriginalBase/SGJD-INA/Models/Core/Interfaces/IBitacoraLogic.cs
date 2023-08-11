using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IBitacoraLogic {
        /// <summary>
        /// Obtener todos los registros de la bitácora
        /// Sólo se obtienen 20 registros
        /// </summary>
        Task<IEnumerable<Bitacora>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener todos los registros de la bitácora en un rango de fechas
        /// </summary>
        IEnumerable<BitacoraPorFechaDTO> ObtenerPorRangoFecha(DateTime FechaInicio, DateTime FechaFin);
        Task<IEnumerable<BitacoraPorFechaDTO>> ObtenerPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin);

        /// <summary>
        /// Obtener un registro de la bitácora por su id
        /// </summary>
        Task<Bitacora> ObtenerPorIdAsync(int Id);

        /// <summary>
        /// Obtener todos los errores del archivo log
        /// </summary>
        List<String> ObtenerLog();

        /// <summary>
        /// Obtener todos los errores de la tabla de errores
        /// </summary>
        Task<IEnumerable<Errores>> ObtenerTodosErroresAsync();
    }
}