using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IVistaLogic {
        //Agregar
        Task<bool> Agregar(Vista vistaModel);

        //Actualizar
        Task<bool> Actualizar(Vista vistaModel);

        //Eliminar
        Task<bool> Eliminar(Vista vistaModel);

        //Obtener todo
        Task<ICollection<Vista>> ObtenerTodos();

        //Obtener por id
        Task<Vista> ObtenerPorId(int id);

        /// <summary>
        /// Obtener un elemento Vista por el valor del atributo DireccionVista
        /// </summary>
        Vista ObtenerPorDireccionVista(string direccionVista);

        /// <summary>
        /// Obtener un elemento Vista por el valor del atributo DireccionVista
        /// Función asíncrona
        /// </summary>
        Task<Vista> ObtenerPorDireccionVistaAsync(string direccionVista);

        /// <summary>
        /// Obtener un elemento Vista buscando el nombre del controlador en el atributo DireccionVista
        /// En las vistas sólo existira un elemento por controlador.
        /// </summary>
        Vista ObtenerPorNombreControlador(string nombreControlador);

        /// <summary>
        /// Obtener listado con nombre de vistas físicas del proyecto sin asignar en la lista de vistas de la BD.
        /// </summary>
        Task<IEnumerable<string>> ObtenerVistasProyectoSinAsignar();

        /// <summary>
        /// Obtener vistas del proyecto que no han sido asignadas a un perfil
        /// </summary>
        Task<IEnumerable<Vista>> ObtenerVistasSinAsignar(string idPerfil);
    }
}