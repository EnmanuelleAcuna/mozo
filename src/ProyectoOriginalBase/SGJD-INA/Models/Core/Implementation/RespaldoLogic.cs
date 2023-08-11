//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Implementation {
    public class RespaldoLogic : IRespaldoLogic {
        #region Atributos & constructor
        private readonly IBitacoraLogic Bitacora;
        private readonly IRepositorioLogic Repositorio;

        //static readonly string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio principal
        //static readonly string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio principal y el repositorio

        //static readonly string NombreRepositorio = "RepositorioSGJD"; // Nombre de la carpeta del repositorio principal

        ////Repositorio
        //static readonly string DireccionRepositorio = Raiz + NombreRepositorio; // ~/RepositorioSGJD/

        //// Archivos, nombre de la carpeta donde se almacenan todos los archivos del repositorio 
        //static readonly string NombreCarpetaArchivos = "Archivos";
        //static readonly string DireccionArchivos = Raiz + NombreRepositorio + NombreCarpetaArchivos; // ~/RepositorioSGJD/Archivos

        ////Respaldos
        //static readonly string NombreCarpetaRespaldos = "\\Respaldos\\"; // Carpeta donde se guardan los archivos zip con respaldos, esta carpeta está ubicada dentro de RepositorioSGJD
        //static readonly string DireccionRespaldos = DireccionRepositorio + NombreCarpetaRespaldos; // ~/RepositorioSGJD/Respaldos

        ////RespaldoRepositorios
        //static readonly string NombreCarpetaRespaldosRepositorio = "Repositorio\\"; // Carpeta donde se guardan los archivos zip con respaldos, esta carpeta está ubicada dentro de RepositorioSGJD
        //static readonly string DireccionRespaldosRepositorio = DireccionRepositorio + NombreCarpetaRespaldos + NombreCarpetaRespaldosRepositorio; // ~/RepositorioSGJD/Respaldos/Repositorio

        ////Base de datos
        //static readonly string NombreCarpetaBaseDatos = "Base de Datos\\"; // Nombre de la carpeta dentro del zip donde se incluye la base de datos
        //static readonly string DireccionRespaldosBaseDatos = DireccionRepositorio + NombreCarpetaRespaldos + NombreCarpetaBaseDatos; // ~/RepositorioSGJD/Respaldos/Base de Datos

        //static readonly string NombreRespaldoBaseDatos = "SGJD.bak"; // Nombre del archivo con el respaldo de la base de datos, este será colocado en la carpeta ~/RepositorioSGJD/Respaldos/Base de Datos/SGJD.bak
        //static readonly string NombreArchivoRespaldo = "Respaldo SGJD (" + DateTime.Now.ToString("dd-MM-yyyy__HH-mm") + ").zip"; // Nombre del archivo final con el respaldo, este será colocado en la carpeta ~/RepositorioSGJD/Respaldos/*.zip

        //static readonly string Host = "http://www.datasoft.co.cr";

        static readonly string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio
        static readonly string NombreRepositorio = "RepositorioSGJD\\"; // Nombre de la carpeta del repositorio
        static readonly string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio y el repositorio
        static readonly string DireccionRepositorio = Raiz + NombreRepositorio; // ~/RepositorioSGJD/

        public RespaldoLogic(IBitacoraLogic Bitacora, IRepositorioLogic Repositorio) {
            this.Bitacora = Bitacora;
            this.Repositorio = Repositorio;
        }
        #endregion

        #region Metodos para respaldar

        public async Task<bool> Agregar() {
            // Realizar respaldo físico
            //var tareaGenerarRespaldoFisico = GenerarRespaldoBD();
            //var respaldoModel = await tareaGenerarRespaldoFisico;

            //// Realizado el respaldo fisicamente, agregar registro n la base de datos
            //var tareaRegistrarEnBD = RegistrarEnBD(respaldoModel);
            //var registrarEnBD = await tareaRegistrarEnBD;

            return true;
        }
        #endregion

        #region Operaciones en base de datos
        /// <summary>
        /// Agregar un respaldo
        /// </summary>
        public async Task<bool> RegistrarEnBD(Respaldo respaldoModel) {
            var respaldoBD = ConvertirModelo_BD(respaldoModel);
            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_RESPALDOS.Add(respaldoBD);
                await ContextoBD.SaveChangesAsync();
            }

            // Registrar en bitácora
            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: respaldoModel.ObtenerInformacion(), SeccionSistema: "Respaldo");

            return true;
        }

        /// <summary>
        /// Obtener lista con todos los elementos Respaldo
        /// </summary>
        public async Task<IEnumerable<Respaldo>> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaRespaldoBD = ContextoBD.SGJD_ADM_TAB_RESPALDOS.ToList();
                IEnumerable<Respaldo> ListaTipoObjeto = ListaRespaldoBD.Select(r => new Respaldo(r)).ToList();
                return ListaTipoObjeto;
            }
        }

        public Task<IEnumerable<Respaldo>> ObtenerTodosAsync() {
            Task<IEnumerable<Respaldo>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        /// <summary>
        /// Obtener un elemento Respaldo por id
        /// </summary>
        public async Task<Respaldo> ObtenerPorIdAsync(int id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaRespaldoBD = ContextoBD.SGJD_ADM_TAB_RESPALDOS.FindAsync(id);
                var respaldoBD = await tareaRespaldoBD;

                var respaldoModel = ConvertirBD_Modelo(respaldoBD);

                return respaldoModel;
            }
        }

        /// <summary>
        /// Obtener lista con todos los elementos Respaldo segun un rango de fechas
        /// </summary>
        public IEnumerable<RespaldoPorFechaDTO> ObtenerPorRangoFecha(DateTime FechaInicio, DateTime FechaFin) {
            FechaFin = FechaFin + new TimeSpan(0, 23, 59, 59, 999); // Establecer a la fecha fin la hora para abarcar todo el día.
            IEnumerable<RespaldoPorFechaDTO> RespaldoPorRangoFecha = new RespaldoDAO().ObtenerRespaldoPorRangoFecha(FechaInicio, FechaFin);
            return RespaldoPorRangoFecha;
        }

        public Task<IEnumerable<RespaldoPorFechaDTO>> ObtenerPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin) {
            Task<IEnumerable<RespaldoPorFechaDTO>> Tarea = Task.Run(() => ObtenerPorRangoFecha(FechaInicio, FechaFin));
            return Tarea;
        }
        #endregion

        #region Operaciones de archivos y respaldo de base de datos
        /// <summary>
        /// Funcion encargada de realizar un respaldo de la base de datos
        /// Esta función crea el respaldo físico de la base de datos y del repositorio y devuelve un objeto de tipo Respaldo con el detalle del respaldo creado
        /// </summary>
        //private async Task<Respaldo> GenerarRespaldoBD() {
        // Estructura:
        // REPOSITORIOSGJD
        //      /Archivos
        //          *Archivos y carpetas del repositorio
        //      /Respaldos
        //          /RespaldoRepositorio (Temporal)
        //              *Copia de archivos y carpetas del repositorio
        //          /RespaldoBaseDeDatos (Temporal)
        //              *SGJD.bak
        //          *Archivo .zip con /RespaldoRepositorio y /RespaldoBaseDeDatos

        // Verificar si existe la dirección de la carpeta de respaldos dentro de la carpeta de repositorio, sino existe, crear carpeta de respaldos dentro de
        // la carpeta del repositorio, si ya existe continuar con el proceso.
        //    if (!Directory.Exists(DireccionRespaldos)) {
        //        Directory.CreateDirectory(DireccionRespaldos);
        //    }

        //    // Crear la carpeta de respaldos de repositorio y la carpeta de respaldos de la base de datos dentro de la carpeta de respaldos dentro de la
        //    // carpeta del repositorio.
        //    if (Directory.Exists(DireccionRespaldos)) {
        //        Directory.CreateDirectory(DireccionRespaldosRepositorio);
        //        Directory.CreateDirectory(DireccionRespaldosBaseDatos);
        //    }

        //    // Respaldar la carpeta Archivos dentro de la carpeta RespaldoRepositorio dentro de la carpeta respaldos
        //    var tareaRespaldarRepositorio = RespaldarRepositorio(DireccionArchivos, DireccionRespaldosRepositorio);
        //    var respaldarRepositorio = await tareaRespaldarRepositorio;

        //    // Validar que el repositorio ha sido respaldado, en caso contrario lanzar excepción
        //    if (!respaldarRepositorio) {
        //        throw new Exception("Error al respaldar el repositorio");
        //    }

        //    // Respaldar la base de datos dentro de la carpeta Base de Datos dentro de la carpeta respaldos
        //    var respaldarBD = RespaldarBaseDatos(DireccionRespaldosBaseDatos);

        //    // Validar que la base de datos ha sido respaldada, en caso contrario lanzar excepción
        //    if (!respaldarBD) {
        //        throw new Exception("Error al respaldar la base de datos");
        //    }

        //    // Comprimir las carpetas de respaldo de repositorio y base de datos en un nuevo archivo .zip, localizado en la carpeta Respaldos
        //    var tareaComprimirCarpetasRespaldo = ComprimirCarpetasRespaldo();
        //    var comprimirCarpetasRespaldo = await tareaComprimirCarpetasRespaldo;

        //    // Validar que el archivo comprimido ha sido creado, en caso contrario lanzar excepción
        //    if (!comprimirCarpetasRespaldo) {
        //        throw new Exception("Error al comprimir carpetas de respaldos");
        //    }

        //    var respaldoModel = new Respaldo() {
        //        // TODO usar la recuperación de id de usuario
        //        IdUsuario = "",//User.Identity,
        //        FechaHora = DateTime.Now,
        //        NombreRespaldo = NombreArchivoRespaldo,
        //        URLArchivoBD = "/Respaldos",
        //        URLArchivoRepositorio = Host + "\\" + NombreRepositorio + NombreCarpetaRespaldos + NombreArchivoRespaldo
        //    };

        //    return respaldoModel;
        //}

        /// <summary>
        /// Copiar toda la carpeta de Archivos dentro de la carpeta del repositorio hacia la carpeta RespaldosRepositorio dentro de la carpeta respaldos
        /// Se respaldará todo lo que esté dentro de la carpeta Archivos dentro de la carpeta Repositorio
        /// </summary>
        private async Task<bool> RespaldarRepositorio(string direccionCarpetaRespaldar, string destino) {
            // Obtener los subdiretorios (carpeta) del directorio (carpeta) especificados
            DirectoryInfo dir = new DirectoryInfo(direccionCarpetaRespaldar);

            // Obtener los archivos dentro de la carpeta a respaldar y respaldarlos.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) {
                string temppath = Path.Combine(destino, file.Name);
                file.CopyTo(temppath, false);
            }

            // Obtener las subcarpetas dentro de la carpeta a respaldar y respaldarlas.
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs) {
                string temppath = Path.Combine(destino, subdir.Name);
                var tareaRespaldarCarpeta = RespaldarRepositorio(subdir.FullName, temppath);
                var respaldarCarpeta = await tareaRespaldarCarpeta;
            }

            return true;
        }

        /// <summary>
        /// Realizar el respaldo de la base de datos.
        /// Generar archivo de respaldo .bak en el servidor donde se encuentre la base de datos
        /// Descargar el archivo bak generado en la base de datos y copiarlo a la carpeta Base de Datos dentro de la carpeta Respaldos.
        /// </summary>
        private bool RespaldarBaseDatos(string destino) {
            //ServerConnection srvConn = new ServerConnection();
            //Server server = new Server(srvConn);
            //srvConn.LoginSecure = false;
            //srvConn.ConnectionString = new SGJDEntities().Database.Connection.ConnectionString;
            //Backup SGJDBD = new Backup {
            //    Database = "SGJD", // Nombre de la base de datos a respaldar
            //    Action = BackupActionType.Database // Especificar lo que se va a respaldar
            //};
            //SGJDBD.Devices.AddDevice(@"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Backup\" + NombreRespaldoBaseDatos, DeviceType.File);
            //SGJDBD.BackupSetName = "Respaldo SGJD";
            //SGJDBD.BackupSetDescription = "SGJDdatabase - Full Backup";
            //SGJDBD.Initialize = true;
            //SGJDBD.SqlBackup(server);

            //destino = destino + NombreRespaldoBaseDatos;

            //// Descargar archivo .bak a la carpeta de Base de Datos dentro de respaldos
            //new WebClient().DownloadFile("http://backup.datasoft.co.cr/sgjd.bak", destino);

            return true;
        }

        //private async Task<bool> ComprimirCarpetasRespaldo() {
        //    try {
        //        await Task.Run(() => {
        //            // Crear archivo zip que contiene las ds carpetas de respaldos, la de repositorio y lade base de datos
        //            using (ZipFile zip = new ZipFile()) {
        //                //zip.AddDirectoryByName(direccionRespaldosBaseDatos);
        //                //zip.AddDirectoryByName(nombreCarpetaBaseDatos);
        //                zip.AddDirectory(DireccionRespaldosRepositorio, DireccionRespaldosRepositorio);
        //                zip.AddDirectory(DireccionRespaldosBaseDatos, DireccionRespaldosBaseDatos);
        //                zip.Comment = "Archivo creado el " + DateTime.Now.ToString("G");
        //                zip.Save(DireccionRespaldos + NombreArchivoRespaldo);
        //            }

        //            //Borrar las carpetas de respaldo de repositorio y respaldo de base de datos.
        //            if (Directory.Exists(DireccionRespaldosRepositorio)) {
        //                Directory.Delete(DireccionRespaldosRepositorio, true);
        //            }
        //            if (Directory.Exists(DireccionRespaldosBaseDatos)) {
        //                Directory.Delete(DireccionRespaldosBaseDatos, true);
        //            }
        //        });

        //        return true;
        //    }
        //    catch (Exception) {
        //        return false;
        //    }
        //}

        public async Task<byte[]> ObtenerZip(string NombreRespaldo) {
            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Respaldos");
            Repositorio RepositorioTomo = await TareaObtenerRepositorio;

            if (RepositorioTomo == null) {
                return Array.Empty<byte>();
            }

            if (File.Exists(DireccionRepositorio + RepositorioTomo.Nombre + "\\" + NombreRespaldo)) {
                byte[] BytesArchivo = System.IO.File.ReadAllBytes(DireccionRepositorio + RepositorioTomo.Nombre + "\\" + NombreRespaldo);
                return BytesArchivo;
            }
            else {
                return Array.Empty<byte>();
            }
        }
        #endregion

        #region Metodos de conversión 
        /// <summary>
        /// Este me todo se encarga de convertir el modelo de respaldo de la base de datos en el modelo o entidad del proyecto 
        /// </summary>
        private SGJD_ADM_TAB_RESPALDOS ConvertirModelo_BD(Respaldo respaldoModel) {
            return new SGJD_ADM_TAB_RESPALDOS() {
                LLP_Id = respaldoModel.Id,
                NombreRespaldo = respaldoModel.NombreRespaldo,
                URLArchivoRepositorio = respaldoModel.URLArchivoRepositorio,
                URLArchivoBD = respaldoModel.URLArchivoBD,
                FechaHora = respaldoModel.FechaHora,
                LLF_IdUsuario = respaldoModel.IdUsuario,
            };
        }

        /// <summary>
        /// Este me todo se encarga de convertir  el modelo o entidad del proyecto en el modelo de respaldo de la base de datos
        /// </summary>
        private Respaldo ConvertirBD_Modelo(SGJD_ADM_TAB_RESPALDOS respaldoBD) {
            return new Respaldo() {
                Id = respaldoBD.LLP_Id,
                IdUsuario = respaldoBD.LLF_IdUsuario,
                FechaHora = respaldoBD.FechaHora,
                NombreRespaldo = respaldoBD.NombreRespaldo,
                URLArchivoBD = respaldoBD.URLArchivoBD,
                URLArchivoRepositorio = respaldoBD.URLArchivoRepositorio,
                Usuario = new ApplicationUser {
                    Id = respaldoBD.SGJD_ADM_TAB_USUARIOS.LLP_Id,
                    Nombre = respaldoBD.SGJD_ADM_TAB_USUARIOS.Nombre
                },
                //Extraer el nombre de la tabla (SGJD_ADM_TAB_RESPALDO)
                NombreObjeto = respaldoBD.GetType().UnderlyingSystemType.BaseType.Name
            };
        }
        #endregion
    }
}