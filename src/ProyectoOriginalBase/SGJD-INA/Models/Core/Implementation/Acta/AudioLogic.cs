using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transcripcion;

namespace SGJD_INA.Models.Core.Implementation {
    public class AudioLogic : IAudioLogic {
        #region Atributos y constructor
        static readonly string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio
        static readonly string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio y el repositorio
        private readonly IAudioRepository RepositorioBD;
        private readonly IRepositorioLogic Repositorio;
        private IBitacoraLogic Bitacora;

        public AudioLogic(IAudioRepository RepositorioBD, IRepositorioLogic Repositorio, IBitacoraLogic Bitacora) {
            this.RepositorioBD = RepositorioBD;
            this.Repositorio = Repositorio;
            this.Bitacora = Bitacora;
        }
        #endregion

        // Métodos públicos
        public IEnumerable<Audio> ObtenerTodosPorIdActa(int IdActa) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaAudiosBD = ContextoBD.SGJD_ADM_TAB_AUDIOS.Where(au => au.LLF_IdActa == IdActa).ToList();
                IEnumerable<Audio> ListaAudios = ListaAudiosBD.Select(au => new Audio(au)).ToList();
                return ListaAudios;
            }
        }

        public Task<IEnumerable<Audio>> ObtenerTodosPorIdActaAsync(int IdActa) {
            Task<IEnumerable<Audio>> Tarea = Task.Run(() => ObtenerTodosPorIdActa(IdActa));
            return Tarea;
        }

        /// <summary>
        /// Obtener un Audio por id
        /// </summary>
        public async Task<Audio> ObtenerPorIdAsync(int IdAudio) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAudioBD = ContextoBD.SGJD_ADM_TAB_AUDIOS.FindAsync(IdAudio);
                var AudioBD = await TareaObtenerAudioBD;
                Audio AudioModel = new Audio(AudioBD);
                return AudioModel;
            }
        }

        public async Task<bool> AgregarAsync(Audio ModeloAudio, HttpPostedFileBase ArchivoAudio) {
            var TareObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Audios");

            // Obtener la información del repositorio, según la extensión del audio subido
            Repositorio ModeloRepositorio = await TareObtenerRepositorio;

            ModeloAudio.Repositorio = ModeloRepositorio;
            ModeloAudio.Archivo = ArchivoAudio;
            ModeloAudio.NombreCarpeta = ModeloRepositorio.Nombre;

            // Agregar audio a la carpeta y la base de datos
            // Guardar el archivo fisicamente
            ModeloAudio = GuardarAudio(ModeloAudio);

            // Agregar registro en la base de datos
            var TareaAgregarAudio = RepositorioBD.AgregarAsync(ModeloAudio);
            ModeloAudio = await TareaAgregarAudio;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregado", ValorAntiguo: "", ValorNuevo: ModeloAudio.ObtenerInformacion(), SeccionSistema: "Audio");

            return true;
        }

        public async Task<bool> ActualizarInformacionAsync(Audio Entidad) {
            if (Entidad is null) {
                throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);
            }

            // Actualizar registro en la base de datos
            var TareaActualizarEnBD = RepositorioBD.ActualizarAsync(Entidad);
            bool AudioActualizado = await TareaActualizarEnBD;

            return AudioActualizado;
        }

        /// <summary>
        /// Eliminar un audio en la base de datos
        /// </summary>
        public async Task<bool> EliminarAsync(Audio AudioModel) {
            // 1: Eliminar el audio de la carpeta fisica del servidor.
            var eliminarAudio = EliminarArchivo(AudioModel.UrlAudio);

            // 2: Verificar que la eliminacion del audio en la carpeta física se ha realizado correctamente
            if (!eliminarAudio) {
                throw new Exception("Error al eliminar el archivo físico.");
            }

            /// 3: Elimina los regitros registro del audio de la base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaListarAudios = ContextoBD.SGJD_ADM_TAB_AUDIOS.Where(au => au.UrlAudio == AudioModel.UrlAudio).ToListAsync();
                var listaAudios = await tareaListarAudios;
                foreach (var AudioBD in listaAudios) {
                    ContextoBD.SGJD_ADM_TAB_AUDIOS.Attach(AudioBD);
                    ContextoBD.SGJD_ADM_TAB_AUDIOS.Remove(AudioBD);
                }
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: AudioModel.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Audios");

            return true;
        }

        /// <summary>
        /// Eliminar un archivo fisico de la carpeta repositorios del proyecto
        /// </summary>
        public bool EliminarArchivo(string Url) {
            // Concatenar la dirección de la raiz del proyecto con la url del archivo a borrar.
            string Ruta = Raiz + Url;
            File.Delete(Ruta);
            return true;
        }

        public Task<bool> EliminarArchivoAsync(string Url) {
            Task<bool> Tarea = Task.Run(() => EliminarArchivo(Url));
            return Tarea;
        }

        /// <summary>
        /// Guardar los archivo fisicamente
        /// </summary>
        public Audio GuardarAudio(Audio ModeloAudio) {
            if (ModeloAudio is null) throw new ArgumentNullException(paramName: nameof(ModeloAudio), message: Resources.ModeloNulo);

            // Obtener el nombre del audio y su extensión
            string NombreAudio = Path.GetFileNameWithoutExtension(ModeloAudio.Archivo.FileName);
            string ExtensionAudio = Path.GetExtension(ModeloAudio.Archivo.FileName);

            // Concatenar audio.extension y eliminar los espacios en blanco del nombre
            NombreAudio = Guid.NewGuid() + ExtensionAudio;

            // ~/RepositorioSGJD/Repositorio/Carpeta/Audio.extension
            string Repositorio = Raiz + ModeloAudio.Repositorio.Ruta + ModeloAudio.Repositorio.Nombre;

            // ../Repositorio/Carpeta/Audio.extension
            var rutaAudio = ModeloAudio.Repositorio.Ruta + ModeloAudio.Repositorio.Nombre + "/" + NombreAudio;

            // Ejemplo: ~/SitioSGJD/Temporales/nombreAudio.extension
            var path = Path.Combine(HttpContext.Current.Request.MapPath(rutaAudio));

            // Parámetro para guardar la ruta del audio
            ModeloAudio.UrlAudio = rutaAudio;

            var InfoCarpeta = new DirectoryInfo(Repositorio);

            if (InfoCarpeta.Exists) {
                Repositorio = Repositorio + "\\" + NombreAudio;
                // Guardar el audio subido
                ModeloAudio.Archivo.SaveAs(path);
            }
            else {
                //Crea carpeta
                Directory.CreateDirectory(Repositorio);
                //se le añade el audio
                Repositorio = Repositorio + "\\" + NombreAudio;
                // Guardar el audio subido
                ModeloAudio.Archivo.SaveAs(path);
                if (!File.Exists(Repositorio)) {
                    // Move the file.
                    File.Move(path, Repositorio);
                    File.Delete(path);
                }
            }

            return ModeloAudio;
        }

        public async Task<bool> TranscribirAsync(int IdAudio) {
            // Obtener un audio
            var TareaObtenerAudio = ObtenerPorIdAsync(IdAudio);
            Audio ModeloAudio = await TareaObtenerAudio;

            // Obtener el array de bytes del archivo con audio
            byte[] AudioData = Repositorio.ObtenerAudioWav(ModeloAudio.UrlAudio);

            // Obtener texto de transcripción
            var TareaTranscribirAudio = Transcriptor.Transcribir(AudioData);
            string Transcripcion = await TareaTranscribirAudio;

            // Guardar transcripción de audio si se obtuvo una transcripción
            if (!string.IsNullOrEmpty(Transcripcion)) {
                ModeloAudio.Transcripcion = Transcripcion;
            }
            else {
                return false;
            }

            var TareaActualizarInfoAudio = ActualizarInformacionAsync(ModeloAudio);
            bool AudioActualizado = await TareaActualizarInfoAudio;

            return AudioActualizado;
        }
    }
}