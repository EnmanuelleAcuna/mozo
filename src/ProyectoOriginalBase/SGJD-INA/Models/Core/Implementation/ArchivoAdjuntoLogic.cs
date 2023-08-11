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
    public class ArchivoAdjuntoLogic : IArchivoAdjuntoLogic {
        static readonly string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio
        const string NombreRepositorio = "RepositorioSGJD\\"; // Nombre de la carpeta del repositorio
        static readonly string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio y el repositorio

        #region Operaciones en base de datos
        public async Task<ArchivoAdjunto> AgregarAsync(ArchivoAdjunto Modelo, HttpPostedFileBase Archivo) {
            // Guardar el archivo fisicamente
            Modelo = GuardarArchivo(Modelo, Archivo);

            // Agregar registro en la base de datos
            var TareaAgregarEnBD = AgregarEnBD(Modelo);
            Modelo = await TareaAgregarEnBD;

            return Modelo;
        }

        public async Task<ArchivoAdjunto> AgregarAsync(ArchivoAdjunto Modelo, bool Copia, HttpPostedFileBase Archivo) {
            // Guardar el archivo fisicamente
            if (Copia != true) {
                Modelo = GuardarArchivo(Modelo, Archivo);
            }

            // Realizado el respaldo fisicamente, agregar registro en la base de datos
            var ArchivoAdjuntolBD = Modelo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ArchivoAdjuntolBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Add(ArchivoAdjuntolBD);
                await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                Modelo.IdArchivo = ArchivoAdjuntolBD.LLP_Id;
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregado", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Archivo");

            return Modelo;
        }

        public async Task<bool> ActualizarAsync(ArchivoAdjunto Modelo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ArchivoAdjuntoBD = Modelo.BD();
                ContextoBD.Entry(ArchivoAdjuntoBD).State = EntityState.Modified;
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: Modelo.ObtenerInformacion(), SeccionSistema: "Acta");

            return true;
        }

        public async Task<bool> EliminarAsync(ArchivoAdjunto ArchivoAdjuntoModel) {
            // 1: Eliminar el archivo de la carpeta fisica del servidor.
            var eliminarArchivo = EliminarAsync(ArchivoAdjuntoModel.UrlArchivo);

            // 2: Verificar que la eliminacion del archivo en la carpeta física se ha realizado correctamente
            if (!eliminarArchivo) {
                throw new Exception("Error al eliminar el archivo físico.");
            }

            /// 3: Elimina los regitros registro del archivo de la base de datos, esto en lista se da debido a que
            /// en un acuerdo pueden haber varias versiones que comparten un mismo archivo.
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaListarArchivos = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Where(A => A.UrlArchivo == ArchivoAdjuntoModel.UrlArchivo).ToListAsync();
                var listaArchivos = await tareaListarArchivos;
                foreach (var archivoAdjuntoBD in listaArchivos) {
                    ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Attach(archivoAdjuntoBD);
                    ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Remove(archivoAdjuntoBD);
                }
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: ArchivoAdjuntoModel.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Archivos Adjuntos");

            return true;
        }

        public bool EliminarAsync(string Url) {
            // Concatenar la dirección de la raiz del proyecto con la url del archivo a borrar.
            string Ruta = Raiz + Url;
            File.Delete(Ruta);
            return true;
        }

        /// <summary>
        /// Obtener lista de elementos ArchivoAdjunto
        /// </summary>
        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.ToListAsync();
                var ListaArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;
                IEnumerable<ArchivoAdjunto> ListaArchivosAdjuntos = ListaArchivoAdjuntoBD.Select(a => new ArchivoAdjunto(a, a.SGJD_ADM_TAB_REPOSITORIOS, a.SGJD_ADM_TAB_TIPOS_ARCHIVO, a.SGJD_ADM_TAB_TIPOS_OBJETO)).ToList();
                return ListaArchivosAdjuntos;
            }
        }

        /// <summary>
        /// Obtener un Adjunto por id
        /// </summary>
        public async Task<ArchivoAdjunto> ObtenerPorIdAsync(int IdArchivo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.FindAsync(IdArchivo);
                var ArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;
                ArchivoAdjunto ArchivoAdjuntoModel = new ArchivoAdjunto(ArchivoAdjuntoBD, ArchivoAdjuntoBD.SGJD_ADM_TAB_REPOSITORIOS, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_OBJETO);
                return ArchivoAdjuntoModel;
            }
        }

        /// <summary>
        /// Obtener un adjunto por id de objeto y nombre de objeto que es el nombre de la tabla
        /// </summary>
        public async Task<ArchivoAdjunto> ObtenerPorIdObjetoAsync(int IdObjeto, string NombreObjeto) {
            ArchivoAdjunto ArchivoAdjuntoModel = new ArchivoAdjunto();
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Where(A => A.IdObjeto == IdObjeto && A.SGJD_ADM_TAB_TIPOS_OBJETO.NombreTabla == NombreObjeto).FirstOrDefaultAsync();
                var ArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;

                if (ArchivoAdjuntoBD != null) {
                    ArchivoAdjuntoModel = new ArchivoAdjunto(ArchivoAdjuntoBD, ArchivoAdjuntoBD.SGJD_ADM_TAB_REPOSITORIOS, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_OBJETO);
                }

                return ArchivoAdjuntoModel;
            }
        }

        /// <summary>
        /// Obtener todos los adjuntos por id de objeto y nombre de objeto que es el nombre de la tabla
        /// </summary>
        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerTodosPorIdObjetoAsync(int IdObjeto, string NombreObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Where(A => A.IdObjeto == IdObjeto && A.SGJD_ADM_TAB_TIPOS_OBJETO.NombreTabla == NombreObjeto).ToListAsync();
                var ListaArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;
                IEnumerable<ArchivoAdjunto> ListaArchivosAdjuntos = ListaArchivoAdjuntoBD.Select(a => new ArchivoAdjunto(a, a.SGJD_ADM_TAB_REPOSITORIOS, a.SGJD_ADM_TAB_TIPOS_ARCHIVO, a.SGJD_ADM_TAB_TIPOS_OBJETO)).ToList();
                return ListaArchivosAdjuntos;
            }
        }

        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerTodosPorIdObjetoIdTipoObjetoAsync(int IdObjeto, int IdTipoObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Where(A => A.IdObjeto == IdObjeto && A.LLF_IdTipoObjeto == IdTipoObjeto).ToListAsync();
                var ListaArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;
                IEnumerable<ArchivoAdjunto> ListaArchivosAdjuntos = ListaArchivoAdjuntoBD.Select(a => new ArchivoAdjunto(a, a.SGJD_ADM_TAB_REPOSITORIOS, a.SGJD_ADM_TAB_TIPOS_ARCHIVO, a.SGJD_ADM_TAB_TIPOS_OBJETO)).ToList();
                return ListaArchivosAdjuntos;
            }
        }

        /// <summary>
        /// Obtener un adjunto por id de objeto
        /// </summary>
        public async Task<ArchivoAdjunto> ObtenerPorIdObjeto(int IdObjeto) {
            ArchivoAdjunto ArchivoAdjuntoModel = new ArchivoAdjunto();
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArchivoAdjuntoBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Where(A => A.IdObjeto == IdObjeto).FirstOrDefaultAsync();
                var ArchivoAdjuntoBD = await TareaObtenerArchivoAdjuntoBD;

                if (ArchivoAdjuntoBD != null) {
                    ArchivoAdjuntoModel = new ArchivoAdjunto(ArchivoAdjuntoBD, ArchivoAdjuntoBD.SGJD_ADM_TAB_REPOSITORIOS, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_ARCHIVO, ArchivoAdjuntoBD.SGJD_ADM_TAB_TIPOS_OBJETO);
                }

                return ArchivoAdjuntoModel;
            }
        }
        #endregion
        /// <summary>
        /// Guardar archivo logicamente
        /// </summary>
        private async Task<ArchivoAdjunto> AgregarEnBD(ArchivoAdjunto ModeloArchivoAdjunto) {
            var ArchivoAdjuntolBD = ModeloArchivoAdjunto.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ArchivoAdjuntolBD = ContextoBD.SGJD_ADM_TAB_ARCHIVOS_ADJUNTOS.Add(ArchivoAdjuntolBD);
                await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                ModeloArchivoAdjunto.IdArchivo = ArchivoAdjuntolBD.LLP_Id;
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregado", ValorAntiguo: "", ValorNuevo: ModeloArchivoAdjunto.ObtenerInformacion(), SeccionSistema: "Archivo");

            return ModeloArchivoAdjunto;
        }

        /// <summary>
        /// Guardar archivo fisicamente
        /// </summary>
        public ArchivoAdjunto GuardarArchivo(ArchivoAdjunto Modelo, HttpPostedFileBase Archivo) {
            // Obtener el nombre del archivo y su extensión
            string NombreArchivo = Path.GetFileNameWithoutExtension(Archivo.FileName);
            string ExtensionArchivo = Path.GetExtension(Archivo.FileName);

            // Concatenar archivo.extension y elimianr los espacios en blanco del nombre
            NombreArchivo = NombreArchivo.Replace(" ", "_") + ExtensionArchivo;

            string RutaRepositorio = string.Empty;

            // Si el tipo de objeto es un tema, se debe guardar el archivo adjunto en la carpeta: //RepositorioSGJD/OrdenesDia/Sesion_1/Archivos adjuntos
            if (!(Modelo.Tema is null) && Modelo.TipoObjeto.Descripcion.ToUpper().Equals("TEMA")) {
                RutaRepositorio = Modelo.Repositorio.Ruta + Modelo.Tema.OrdenDia.Sesion.TipoSesion.Descripcion +"_" + Modelo.Tema.OrdenDia.Sesion.Numero +"_"+ Modelo.Tema.OrdenDia.Sesion.FechaHora.Year.ToString();
            }

            // Si es tipo de objeto es un articulo, se debe guardar el archivo adjunto en la carpeta: //RepositorioSGJD/Tomos/Tomo II/Acta_sesion_1/Capitulo_NombreCapitulo/Archivos adjuntos
            if (!(Modelo.Articulo is null) && Modelo.TipoObjeto.Descripcion.ToUpper().Equals("ARTICULO")) {
                RutaRepositorio = Modelo.Repositorio.Ruta + Modelo.Articulo.Capitulo.Acta.Tomo.Nombre + "\\" + "Acta_" + Modelo.Articulo.Capitulo.Acta.Sesion.TipoSesion.Descripcion +"_" + Modelo.Articulo.Capitulo.Acta.Sesion.Numero + "//" + Modelo.Articulo.Capitulo.Titulo;
            }

            // Si el tipo de objeto es un acuerdo o un seguimiento
            if ( ( !(Modelo.Acuerdo is null) || !(Modelo.Seguimiento is null)) && ( Modelo.TipoObjeto.Descripcion.ToUpper().Equals("ACUERDO") || Modelo.TipoObjeto.Descripcion.ToUpper().Equals("SEGUIMIENTO ACUERDO") )) {
                // Si es un acuerdo
                if (Modelo.TipoObjeto.Descripcion.ToUpper().Equals("ACUERDO")) {
                    RutaRepositorio = Modelo.Repositorio.Ruta + Modelo.Acuerdo.Titulo;
                }
                // Si es un seguimiento
                else if (Modelo.TipoObjeto.Descripcion.ToUpper().Equals("SEGUIMIENTO ACUERDO")) {
                    RutaRepositorio = Modelo.Repositorio.Ruta + Modelo.Seguimiento.Acuerdo.Titulo + "\\" + "Seguimientos";

                }
            }

            // Eliminar los espacios en blanco de la ruta
            RutaRepositorio = RutaRepositorio.Replace(" ", "_");

            // Construir ruta con nombre de archivo
            string RutaRepositorioArchivo = RutaRepositorio + "\\" + NombreArchivo;

            // Crear un objeto con la informacion de la carpeta.
            DirectoryInfo InfoCarpeta = new DirectoryInfo(Raiz + RutaRepositorio);

            if (InfoCarpeta.Exists) {
                // Guardar el archivo subido
                Archivo.SaveAs(Raiz + RutaRepositorioArchivo);
            }
            else {
                //Crea carpeta
                Directory.CreateDirectory(Raiz + RutaRepositorio);

                // Guardar el archivo subido
                Archivo.SaveAs(Raiz + RutaRepositorioArchivo);
            }

            // Parámetro para guardar la ruta del archivo
            Modelo.UrlArchivo = RutaRepositorioArchivo;

            return Modelo;
        }
    }
}