using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IRespaldoLogic {
        /// <summary>
        /// Agregar
        /// </summary>
        Task<bool> Agregar();

        /// <summary>
        /// Obtener todo
        /// </summary>
        Task<IEnumerable<Respaldo>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener por id
        /// </summary>
        Task<Respaldo> ObtenerPorIdAsync(int id);

        /// <summary>
        /// Obtener por rango de fechas
        /// </summary>
        Task<IEnumerable<RespaldoPorFechaDTO>> ObtenerPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin);

        /// <summary>
        /// Descargar contenido perteneciente al Respaldo
        /// </summary>
        Task<byte[]> ObtenerZip(string NombreRespaldo);
    }
}