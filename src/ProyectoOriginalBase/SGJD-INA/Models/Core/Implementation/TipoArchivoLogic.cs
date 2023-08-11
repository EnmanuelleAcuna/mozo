using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad TipoObjeto
    /// </summary>
    public class TipoArchivoLogic : ITipoArchivoLogic {
        #region Atributos & constructor
        private readonly IBitacoraLogic Bitacora;

        public TipoArchivoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        public async Task<bool> AgregarAsync(TipoArchivo ModeloTipoArchivo) {
            var TipoArchivoBD = ModeloTipoArchivo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.Add(TipoArchivoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTipoArchivo.ObtenerInformacion(), SeccionSistema: "TipoArchivo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(TipoArchivo ModeloTipoArchivo) {
            var TipoArchivoBD = ModeloTipoArchivo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(TipoArchivoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloTipoArchivo.ObtenerInformacion(), SeccionSistema: "TipoArchivo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdTipoArchivo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoArchivo = ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.FindAsync(IdTipoArchivo);
                var TipoArchivoBD = await TareaObtenerTipoArchivo;
                ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.Attach(TipoArchivoBD);
                ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.Remove(TipoArchivoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdTipoArchivo.ToString(), ValorNuevo: "", SeccionSistema: "TipoArchivo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<TipoArchivo> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTipoArchivoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.ToList();
                IEnumerable<TipoArchivo> ListaTiposArchivo = ListaTipoArchivoBD.Select(ta => new TipoArchivo(ta)).ToList();
                return ListaTiposArchivo;
            }
        }

        public Task<IEnumerable<TipoArchivo>> ObtenerTodosAsync() {
            Task<IEnumerable<TipoArchivo>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<TipoArchivo> ObtenerPorIdAsync(int IdTipoArchivo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoArchivoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.FindAsync(IdTipoArchivo);
                var TipoArchivoBD = await TareaObtenerTipoArchivoBD;
                TipoArchivo ModeloTipoArchivo = new TipoArchivo(TipoArchivoBD);
                return ModeloTipoArchivo;
            }
        }

        public async Task<TipoArchivo> ObtenerPorExtensionAsync(string Extension) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoArchivoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO.Where(TA => TA.Nombre.Equals(Extension)).FirstOrDefaultAsync();
                var TipoArchivoBD = await TareaObtenerTipoArchivoBD;
                TipoArchivo ModeloTipoArchivo = new TipoArchivo(TipoArchivoBD);
                return ModeloTipoArchivo;
            }
        }

        public async Task<IEnumerable<TipoArchivo>> ObtenerListaTipoArchivoSinAgregar() {
            // Obtener lista con todos los tipos de archivos disponibles
            var ListaTiposArchivo = ObtenerListaTiposArchivo();

            // Obtener todos los tipos de archivos configurados en la base de datos
            var TareaObtenerTipoArchivoBD = ObtenerTodosAsync();
            IEnumerable<TipoArchivo> ListaTiposArchivoBD = await TareaObtenerTipoArchivoBD;

            // Revisar y eliminar de la lista construida, aquellas extensiones que ya están agregadas a la base de datos
            foreach (TipoArchivo TipoArchivoBD in ListaTiposArchivoBD) {
                TipoArchivo TipoArchivoParaEliminar = ListaTiposArchivo.SingleOrDefault(f => f.Nombre.Equals(TipoArchivoBD.Nombre));
                if (TipoArchivoParaEliminar != null) ListaTiposArchivo.Remove(TipoArchivoParaEliminar);
            }

            return ListaTiposArchivo;
        }
        #endregion

        // Métodos privados
        private ICollection<TipoArchivo> ObtenerListaTiposArchivo() {
            var ListaString = new List<TipoArchivo>();

            ListaString.Add(new TipoArchivo { Nombre = ".mp3", Descripcion = "Archivo de audio .MP3" });
            ListaString.Add(new TipoArchivo { Nombre = ".mp4", Descripcion = "Archivo de video .MP4" });
            ListaString.Add(new TipoArchivo { Nombre = ".pdf", Descripcion = "Documento .PDF" });
            ListaString.Add(new TipoArchivo { Nombre = ".png", Descripcion = "Archivo de imagen .PNG" });
            ListaString.Add(new TipoArchivo { Nombre = ".jpeg", Descripcion = "Archivo de imagen .JPEG" });
            ListaString.Add(new TipoArchivo { Nombre = ".docx", Descripcion = "Documento .DOCX" });
            ListaString.Add(new TipoArchivo { Nombre = ".xls", Descripcion = "Documento .XLS" });
            ListaString.Add(new TipoArchivo { Nombre = ".pptx", Descripcion = "Documento .PPTX" });
            ListaString.Add(new TipoArchivo { Nombre = ".wav", Descripcion = "Archivo de audio .WAV" });
            ListaString.Add(new TipoArchivo { Nombre = ".gif", Descripcion = "Archivo de imagen .GIF" });
            ListaString.Add(new TipoArchivo { Nombre = ".jpg", Descripcion = "Archivo de imagen .JPG" });

            return ListaString;
        }
    }
}