using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface ITemaLogic {
        /// <summary>
        /// Agregar un tema a una sección de un orden del día, el estado que se asigna por defecto es pendiente
        /// </summary>
        Task<int> AgregarAsync(Tema ModeloTema);

        /// <summary>
        /// Actualizar el contenido del tema de una sección de un orden del día
        /// </summary>
        Task<bool> ActualizarAsync(Tema ModeloTema);

        /// <summary>
        /// Actualizar el estado de un tema de un orden del día
        /// </summary>
        Task<bool> ActualizarEstadoAsync(Tema ModeloTema);

        /// <summary>
        /// Actualizar Id de Orden del dia y seccion de tema pendiente
        /// </summary>
        Task<bool> ActualizarTemaPendienteAsync(int IdTema, int IdOrdenDia, int IdSeccion);

        /// <summary>
        /// Elimina un tema de una sección de un orden del día
        /// </summary>
        Task<bool> EliminarAsync(int IdTema);

        /// <summary>
        /// Agregar un archivo adjunto al tema
        /// </summary>
        Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo);

        /// <summary>
        /// Obtener todos los archivos adjuntos que pertencen a un tema
        /// </summary>
        Task<IEnumerable<ArchivoAdjunto>> ObtenerAdjuntosAsync(Tema ModeloTema);

        /// <summary>
        /// Obtener un tema por id
        /// </summary>
        Task<Tema> ObtenerPorIdAsync(int IdTema);
        
        /// <summary>
        /// Obtener un tema por id, con su respectivo Orden del Dia al que pertenece
        /// </summary>
        Task<Tema> ObtenerPorIdConOrdenDiaAsync(int IdTema);

        /// <summary>
        /// Obtener todos los temas por id de seccion y id de orden dia
        /// </summary>
        Task<IEnumerable<Tema>> ObtenerTodosPorIdSeccionIdOrdenDia(int IdSeccion, int IdOrdenDia);

        /// <summary>
        /// Obtener todos los elementos tema por id de orden día y id de estado
        /// </summary>
        Task<IEnumerable<Tema>> ObtenerTodosPorIdOrdenDiaIdEstado(int IdOrdenDia, int IdEstado);

        /// <summary>
        /// Obtener todos los temas pendientes por id estado, excepto los que pertencen al id orden día
        /// </summary>
        Task<IEnumerable<Tema>> ObtenerTodosPorIdEstadoIdOrdenDia(int IdEstado, int IdOrdenDia);

        //Obtener todos los elementos tema por id de sección orden del día
        Task<IEnumerable<Tema>> ObtenerTodosPorIdSeccion(int IdSeccion);

        //Obtener todos los elementos tema por id de orden del día
        Task<IEnumerable<Tema>> ObtenerTodosPorIdOrdenDia(int IdOrdenDia);

        // TODO: Revisar la lógica para cumplir con esta funcionalidad
        //Actualizar el tema de una sección de un orden del día para cambiar su posición y realizar auto guardado
        Task<bool> ActualizarPosicionTema(Tema TemaModelActual, Tema TemaModelCambio);

        //Obtener el ultimo elemento tema por id de sección y id de orden del día
        Tema ObtenerUltimoPorIdSeccionIdOrdenDia(int IdSeccion, int IdOrdenDia);
    }
}