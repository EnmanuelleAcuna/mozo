using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IVotoLogic {
        /// <summary>
        /// Actualizar un voto
        /// Debido a que el voto no es una tabla intermedia, posee su propio id y no se necesita saber el id del acuerdo, sólo es necesario el id del voto y el tipo de voto
        /// </summary>
        Task<bool> ActualizarVotoAsync(Votacion ModeloVoto);

        /// <summary>
        /// Obtener todos los votos de un acuerdo
        /// </summary>
        Task<IEnumerable<Votacion>> ObtenerTodosPorIdAcuerdoAsync(int IdAcuerdo);

        /// <summary>
        /// Obtener lista de tipos de voto para un acuerdo
        /// </summary>
        IEnumerable<SelectListItem> ObtenerTipoVotacionParaSelect();
        Task<IEnumerable<SelectListItem>> ObtenerTipoVotacionParaSelectAsync();

        /// <summary>
        /// Actualiza el tipo votacion cuando el tipo de aaprobacion es por unanimidad
        /// </summary>
        Task<bool> EstablecerAprobacionPorUnanimidad(int IdAcuerdo);

        /// <summary>
        /// Muestra los votos desidentes de los miembros de junta directiva
        /// </summary>
        Task<IEnumerable<VotosDesidentesAcuerdosDTO>> ObtenerVotosDesidentesAsync(int IdActa);
    }
}