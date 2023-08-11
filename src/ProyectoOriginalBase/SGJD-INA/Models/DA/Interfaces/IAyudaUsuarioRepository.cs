using SGJD_INA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface IAyudaUsuarioRepository {
        /// <summary>
        /// Actualizar una ayuda al usuario en la base de datos
        /// </summary>
        Task<bool> ActualizarAsync(AyudaUsuario Modelo);

        /// <summary>
        /// Obtener un Ayuda Usuario de la base de datos por ID
        /// </summary>
        Task<AyudaUsuario> ObtenerPorIdAsync(int IdAyudaUsuario);

        /// <summary>
        /// Obtener un Ayuda Usuario de la base de datos por Abreviatura
        /// </summary>
        Task<AyudaUsuario> ObtenerPorAbreviaturaAsync(string Abreviatura);
    }
}
