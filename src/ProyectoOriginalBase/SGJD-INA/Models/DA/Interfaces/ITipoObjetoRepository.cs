using SGJD_INA.Models.Entities;
using System.Threading.Tasks;

namespace SGJD_INA.Models.DA.Interfaces {
    public interface ITipoObjetoRepository {
        Task<TipoObjeto> ActualizarAsync(TipoObjeto Modelo);
    }
}
