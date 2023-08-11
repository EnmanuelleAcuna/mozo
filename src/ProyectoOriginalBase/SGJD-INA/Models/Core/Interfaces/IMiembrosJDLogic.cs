using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IMiembrosJDLogic {
        //Agregar
        Task<bool> Agregar(string IdUsuario);

        //Eliminar
        Task<bool> Eliminar(string IdUsuario);

        //Obtener todo
        Task<IEnumerable<MiembroJD>> ObtenerTodos();

        //Obtener ausentes a una sesion
        IEnumerable<MiembroJD> ObtenerMiembrosAusentes(IEnumerable<MiembroJD> ListaMiembrosJDModel, IEnumerable<AsistenteSesion> ListaUsuarioAsistentesModel);
    }
}