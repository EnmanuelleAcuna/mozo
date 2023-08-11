using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Clase para realizar operaciones relacionadas a la clase Repositorio
    /// </summary>
    public class RepositorioLogic : IRepositorioLogic {
        #region Atributos & constructor
        // Dependencias
        private readonly IBitacoraLogic Bitacora;

        // Propiedades
        const string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio
        const string NombreRepositorio = "RepositorioSGJD\\"; // Nombre de la carpeta del repositorio
        static readonly string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio y el repositorio

        public RepositorioLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Operaciones en base de datos
        /// <summary>
        /// Agregar un Repositorio
        /// </summary>
        public async Task<bool> Agregar(Repositorio Modelo, bool AgregarRegistroBD, bool CrearCarpetaFisica) {
            if (Modelo is null) {
                throw new ArgumentNullException(paramName: nameof(Modelo), message: Properties.Resources.ModeloNulo);
            }

            // Variables de control
            bool CarpetaCreada = true;
            int RegistrosAfectados = 0;

            if (CrearCarpetaFisica) {
                // 1: Crea carpeta física en el servidor.
                CarpetaCreada = await CrearCarpetaRepositorio(Modelo);

                // 2. Verificar que la creación de la carpeta física se ha realizado correctamente
                if (!CarpetaCreada) {
                    throw new Exception(message: Properties.Resources.ErrorCrearCarpetaFisica);
                }
            }

            if (AgregarRegistroBD) {
                // 3: Registrar información del Repositorio en la base de datos.
                using (var ContextoBD = SGJDDBContext.Create()) {
                    var RepositorioBD = Modelo.BD();
                    ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.Add(RepositorioBD);
                    RegistrosAfectados = await ContextoBD.SaveChangesAsync();
                }
            }
            else {
                RegistrosAfectados = 1;
            }

            if (CarpetaCreada && RegistrosAfectados >= 1) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Repositorios");
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Actualizar un Repositorio
        /// </summary>
        public async Task<bool> Actualizar(Repositorio Modelo, string NuevoNombreRepositorio) {
            if (Modelo is null) {
                throw new ArgumentNullException(paramName: nameof(Modelo), message: Properties.Resources.ModeloNulo);
            }

            // 1: Actualizar la carpeta fisica en el servidor.
            bool CarpetaActualizada = await ActualizarCarpetaRepositorio(Modelo, NuevoNombreRepositorio);

            // 2: Verificar que la actualización de la carpeta física se ha realizado correctamente
            if (!CarpetaActualizada) {
                throw new Exception(Properties.Resources.ErrorActualizarCarpetaFisica);
            }

            // 3: Actualizar información del Repositorio en la base de datos.
            using (var ContextoBD = SGJDDBContext.Create()) {
                var RepositorioBD = Modelo.BD();
                RepositorioBD.Nombre = NuevoNombreRepositorio;
                ContextoBD.Entry(RepositorioBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Repositorios");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Eliminar la información del Repositorio en la base de datos
        /// </summary>
        public async Task<bool> Eliminar(Repositorio Modelo) {
            if (Modelo is null) {
                throw new ArgumentNullException(paramName: nameof(Modelo), message: Properties.Resources.ModeloNulo);
            }

            // 1: Eliminar la carpeta fisicamente del serviudor.
            bool CarpetaEliminada = await EliminarCarpetaRepositorio(Modelo);

            // 2:  Verificar que la actualización de la carpeta física se ha realizado correctamente
            if (!CarpetaEliminada) {
                throw new Exception(message: Properties.Resources.ErrorEliminarCarpetaFisica);
            }

            // 3: Elimina el registro del repositorio de la base de datos.
            using (var ContextoBD = SGJDDBContext.Create()) {
                var RepositorioBD = Modelo.BD();
                ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.Attach(RepositorioBD);
                ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.Remove(RepositorioBD);
                ContextoBD.Entry(RepositorioBD).State = EntityState.Deleted;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: Modelo.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Repositorios");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Repositorio>> ObtenerTodosAsync() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerRepositorioBD = ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.ToListAsync();
                var ListaRepositoriosBD = await TareaObtenerRepositorioBD;
                IEnumerable<Repositorio> ListaRepositorios = ListaRepositoriosBD.Select(r => new Repositorio(r)).ToList();
                return ListaRepositorios;
            }
        }

        public async Task<Repositorio> ObtenerPorIdAsync(int Id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerRepositorioBD = ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.FindAsync(Id);
                var RepositorioBD = await TareaObtenerRepositorioBD;
                Repositorio Modelo = new Repositorio(RepositorioBD);
                return Modelo;
            }
        }

        public async Task<Repositorio> ObtenerPorNombreAsync(string Nombre) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerRepositorioBD = ContextoBD.SGJD_ADM_TAB_REPOSITORIOS.Where(r => r.Nombre.Equals(Nombre, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
                var RepositorioBD = await TareaObtenerRepositorioBD;
                Repositorio Modelo = new Repositorio(RepositorioBD);
                return Modelo;
            }
        }

        public async Task<string> GuardarActaPDFRepositorioTomo(Acta ModeloActa, HttpPostedFileBase Archivo) {
            if (ModeloActa is null) {
                throw new ArgumentNullException(paramName: nameof(ModeloActa), message: Properties.Resources.ModeloNulo);
            }

            if (Archivo is null) {
                throw new ArgumentNullException(paramName: nameof(Archivo), message: Properties.Resources.ModeloNulo);
            }

            var TareaObtenerRepositorioTomos = ObtenerPorNombreAsync("Tomos");
            Repositorio RepositorioTomo = await TareaObtenerRepositorioTomos;

            if (RepositorioTomo is null) {
                return "";
            }

            // Se espera por ejemplo para el tomo 2: //RepositorioSGJD/Tomos/Tomo II/Acta_Sesion_3
            string DireccionRepositorioTomo = RepositorioTomo.Ruta + ModeloActa.Tomo.Nombre + "\\" + "Acta_Sesion_" + ModeloActa.Sesion.Numero;

            // Eliminar los espacios en blanco de la ruta
            DireccionRepositorioTomo = DireccionRepositorioTomo.Replace(" ", "_");

            if (!Directory.Exists(Raiz + DireccionRepositorioTomo)) {
                Directory.CreateDirectory(Raiz + DireccionRepositorioTomo);
            }

            if (Directory.Exists(Raiz + DireccionRepositorioTomo)) {
                // Va a quedar por ejemplo para el tomo 2: //RepositorioSGJD/Tomos/Tomo II/Acta_Sesion_3/Acta de sesion ordinaria - 3.pdf
                Archivo.SaveAs(Raiz + DireccionRepositorioTomo + "\\" + "Acta de " + ModeloActa.Sesion.TipoSesion.Descripcion + " " + ModeloActa.Sesion.Numero + ".pdf");
                return DireccionRepositorioTomo + "\\" + "Acta de " + ModeloActa.Sesion.TipoSesion.Descripcion + " " + ModeloActa.Sesion.Numero + ".pdf";
            }
            else {
                return "";
            }
        }

        public async Task<string> GuardarAcuerdoPDFRepositorioAcuerdos(Acuerdo ModeloAcuerdo, HttpPostedFileBase Archivo) {
            if (ModeloAcuerdo is null) {
                throw new ArgumentNullException(paramName: nameof(ModeloAcuerdo), message: Properties.Resources.ModeloNulo);
            }

            if (Archivo is null) {
                throw new ArgumentNullException(paramName: nameof(Archivo), message: Properties.Resources.ModeloNulo);
            }

            var TareaObtenerRepositorioAcuerdos = ObtenerPorNombreAsync("Acuerdos");
            Repositorio RepositorioAcuerdos = await TareaObtenerRepositorioAcuerdos;

            if (RepositorioAcuerdos is null) {
                return "";
            }

            // Se espera por ejemplo para el acuerdo 123: //RepositorioSGJD/Acuerdos/Acuerdo_123
            string DireccionRepositorioAcuerdo = RepositorioAcuerdos.Ruta + ModeloAcuerdo.Titulo;

            // Eliminar los espacios en blanco de la ruta
            DireccionRepositorioAcuerdo = DireccionRepositorioAcuerdo.Replace(" ", "_");

            if (!Directory.Exists(Raiz + DireccionRepositorioAcuerdo)) {
                Directory.CreateDirectory(Raiz + DireccionRepositorioAcuerdo);
            }

            if (Directory.Exists(Raiz + DireccionRepositorioAcuerdo)) {
                // Va a quedar por ejemplo para el acuerdo 123: //RepositorioSGJD/Acuerdos/Acuerdo 123/Acuerdo_JD....pdf
                Archivo.SaveAs(Raiz + DireccionRepositorioAcuerdo + "\\" + "Acuerdo_" + ModeloAcuerdo.Titulo + ".pdf");
                return DireccionRepositorioAcuerdo + "\\" + "Acuerdo_" + ModeloAcuerdo.Titulo + ".pdf";
            }
            else {
                return "";
            }
        }

        public async Task<string> GuardarOficioPDFRepositorioTomo(Tomo ModeloTomo, byte[] Archivo, string NombreArchivo) {
            if (ModeloTomo is null) {
                throw new ArgumentNullException(paramName: nameof(ModeloTomo), message: Properties.Resources.ModeloNulo);
            }

            var TareaObtenerRepositorio = ObtenerPorNombreAsync("Tomos");
            Repositorio RepositorioTomo = await TareaObtenerRepositorio;

            if (RepositorioTomo is null) {
                return "";
            }

            // Se espera por ejemplo para el tomo 2: //RepositorioSGJD/Tomos/Tomo II
            string DireccionRepositorioTomo = RepositorioTomo.Ruta + "\\" + ModeloTomo.Nombre;

            // Eliminar los espacios en blanco de la ruta
            DireccionRepositorioTomo = DireccionRepositorioTomo.Replace(" ", "_");

            if (!Directory.Exists(Raiz + DireccionRepositorioTomo)) {
                Directory.CreateDirectory((Raiz + DireccionRepositorioTomo));
            }

            if (Directory.Exists((Raiz + DireccionRepositorioTomo))) {
                File.WriteAllBytes(Raiz + DireccionRepositorioTomo + "\\" + NombreArchivo + ".pdf", Archivo);
                return DireccionRepositorioTomo + "\\" + NombreArchivo + ".pdf";
            }
            else {
                return "";
            }
        }
        #endregion

        #region Operaciones físicas sobre el repositorio
        /// <summary>
        /// Crear carpeta de un repositorio fisicamente
        /// /// </summary>
        private async Task<bool> CrearCarpetaRepositorio(Repositorio Modelo) {
            string RutaNuevoRepositorio = Raiz + Modelo.Ruta + Modelo.Nombre;

            // Eliminar los espacios en blanco de la ruta
            RutaNuevoRepositorio = RutaNuevoRepositorio.Replace(" ", "_");

            if (!Directory.Exists(RutaNuevoRepositorio)) {
                Directory.CreateDirectory(RutaNuevoRepositorio);
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Crear carpeta de repositorio", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Repositorios");

            return true;
        }

        /// <summary>
        /// Actualizar el nombre de una carpeta de un repositorio fisicamente
        /// </summary>
        private async Task<bool> ActualizarCarpetaRepositorio(Repositorio Modelo, string NuevoNombre) {
            string RutaRepositorio = (Raiz + Modelo.Ruta + Modelo.Nombre).Replace(" ", "");
            string NuevaRutaRepositorio = (Raiz + Modelo.Ruta + NuevoNombre).Replace(" ", "");

            if (Directory.Exists(RutaRepositorio)) {
                Directory.Move(RutaRepositorio, NuevaRutaRepositorio);
                Directory.Delete(RutaRepositorio, false);
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar carpeta de repositorio", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Repositorios");

            return true;
        }

        /// <summary>
        /// Eliminar carpeta de un repositorio fisicamente, sólo si la carpeta está vacía
        /// </summary>
        private async Task<bool> EliminarCarpetaRepositorio(Repositorio Modelo) {
            string RutaRepositorio = Raiz + Modelo.Ruta + Modelo.Nombre;

            // Validar si la carpeta existe y no tiene archivos o carpetas dentro
            var InfoCarpeta = new DirectoryInfo(RutaRepositorio);
            if (InfoCarpeta.Exists) {
                FileInfo[] Archivos = InfoCarpeta.GetFiles();
                DirectoryInfo[] SubCarpetas = InfoCarpeta.GetDirectories();
                if (Archivos.Any() || SubCarpetas.Any()) {
                    return false;
                }
                else {
                    Directory.Delete(RutaRepositorio, false);
                }
            }

            // Si se llega a este punto, quiere decir que la carpeta no existe físicamente, por lo que no hay nada que borrar
            // o la carpeta se borró correctamente

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar carpeta de repositorio", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Repositorios");

            return true;
        }

        public byte[] ObtenerAudioWav(string RutaAudio) {
            byte[] Data;
            using (Stream InputStream = File.OpenRead(Raiz + "\\" + RutaAudio)) {
                MemoryStream MemoryStream = InputStream as MemoryStream;
                if (MemoryStream == null) {
                    MemoryStream = new MemoryStream();
                    InputStream.CopyTo(MemoryStream);
                }
                Data = MemoryStream.ToArray();
            }

            return Data;
        }
        #endregion
    }
}