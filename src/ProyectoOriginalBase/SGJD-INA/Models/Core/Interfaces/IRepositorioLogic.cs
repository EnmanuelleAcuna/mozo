using SGJD_INA.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Interfaces {
    public interface IRepositorioLogic {
        //Agregar
        Task<bool> Agregar(Repositorio RepositorioModel, bool AgregarRegistroBD, bool CrearCarpetaFisica);

        //Actualizar
        Task<bool> Actualizar(Repositorio RepositorioModel, string NuevoNombreRepositorio);

        //Eliminar
        Task<bool> Eliminar(Repositorio RepositorioModel);

        /// <summary>
        /// Obtener lista de Repositorios
        /// </summary>
        Task<IEnumerable<Repositorio>> ObtenerTodosAsync();

        /// <summary>
        /// Obtener un Repositorio por su Id
        /// </summary>
        Task<Repositorio> ObtenerPorIdAsync(int Id);

        /// <summary>
        /// Obtener un Repositorio por nombre
        /// </summary>
        Task<Repositorio> ObtenerPorNombreAsync(string Nombre);

        // Guardar archivo PDF con Acta en la carpeta del Tomo correspondiente
        Task<string> GuardarActaPDFRepositorioTomo(Acta ModeloActa, HttpPostedFileBase Archivo);

        // Guardar archivo PDF con Acuerdo en la carpeta de acuerdos correspondiente
        Task<string> GuardarAcuerdoPDFRepositorioAcuerdos(Acuerdo ModeloAcuerdo, HttpPostedFileBase Archivo);

        // Guardar archivo PDF con Oficio (apertura/cierre) del Tomo en la carpeta del Tomo correspondiente
        Task<string> GuardarOficioPDFRepositorioTomo(Tomo ModeloTomo, byte[] Archivo, string NombreArchivo);

        /// <summary>
        /// Obtener un audio wav en un array de bytes, según su URL donde se encuentra ubicado
        /// </summary>
        byte[] ObtenerAudioWav(string RutaAudio);
    }
}