using iTextSharp.text.pdf;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Implementation {
    public class ActaLogic : IActaLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly IEstadoLogic Estado;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IOrdenDiaLogic OrdenDia;
        private readonly ISesionLogic Sesion;
        private readonly IRepositorioLogic Repositorio;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly ITomoLogic Tomo;
        private readonly IParametrosGeneralesLogic ParametrosGenerales;

        public ActaLogic(IBitacoraLogic Bitacora, IEstadoLogic Estado, ITipoObjetoLogic TipoObjeto, IOrdenDiaLogic OrdenDia, ISesionLogic Sesion, IRepositorioLogic Repositorio, IArchivoAdjuntoLogic ArchivoAdjunto, ITomoLogic Tomo, IParametrosGeneralesLogic ParametrosGenerales) {
            this.Bitacora = Bitacora;
            this.Estado = Estado;
            this.TipoObjeto = TipoObjeto;
            this.OrdenDia = OrdenDia;
            this.Sesion = Sesion;
            this.Repositorio = Repositorio;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.Tomo = Tomo;
            this.ParametrosGenerales = ParametrosGenerales;
        }

        // Métodos públicos
        public async Task<string> AgregarAsync(int IdSesion, int IdOrdenDia, IEnumerable<AsistenteSesion> ListaTodosAsistentesSesion, IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes) {
            var TareaObtenerSesion = Sesion.ObtenerPorIdAsync(IdSesion);
            var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdAsync(IdOrdenDia);
            var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_ACTAS");
            var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("AC-TRANS");
            var TareaObtenerUltimoTomoAbierto = Tomo.ObtenerUltimoAbiertoAsync();
            var TareaObtenerParametrosInstitucion = ParametrosGenerales.ObtenerParametrosInstitucionAsync();

            Sesion SesionModel = await TareaObtenerSesion;
            OrdenDia OrdenDiaModel = await TareaObtenerOrdenDia;
            TipoObjeto TipoObjetoModel = await TareaObtenerTipoObjeto;
            Estado EstadoModel = await TareaObtenerEstado;
            Tomo TomoModel = await TareaObtenerUltimoTomoAbierto;
            IEnumerable<ParametroGeneral> ParametrosInstitucion = await TareaObtenerParametrosInstitucion;

            ParametroGeneral UsuarioSecretario = ParametrosInstitucion.Where(p => p.Nombre.Equals("IdUsuarioSecretario", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            ParametroGeneral UsuarioPresidente = ParametrosInstitucion.Where(p => p.Nombre.Equals("IdUsuarioPresidente", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            string EncabezadoActa = GenerarEncabezadoActa(ListaTodosAsistentesSesion, ListaOtrosAsistentes, SesionModel.TipoSesion.Descripcion, SesionModel.Numero, SesionModel.FechaHora);

            if (TomoModel.Id == 0) {
                return "No existe un Tomo disponible para asignar al Acta";
            }

            int RegistrosAfectados = 0;
            bool CapitulosConArticulosAgregados = false;
            bool OrdenDiaActualizado = false;
            bool SesionActualizada = false;

            Acta ActaModel = new Acta(SesionModel, EstadoModel, TipoObjetoModel, TomoModel, EncabezadoActa, UsuarioSecretario.Valor, UsuarioPresidente.Valor);

            using (var ContextoBD = SGJDDBContext.Create()) {
                var ActaBD = ActaModel.BD();
                ContextoBD.SGJD_ACT_TAB_ACTAS.Add(ActaBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                ActaModel.Id = ActaBD.LLP_Id;
            }

            if (RegistrosAfectados >= 0) {
                // Registrar las secciones del orden del día como capítulos del acta
                var TareaRegistrarSeccionesConTemasComoCapitulosConArticulos = AgregarSeccionesConTemasComoCapitulosConArticulosAsync(ActaModel, OrdenDiaModel);
                CapitulosConArticulosAgregados = await TareaRegistrarSeccionesConTemasComoCapitulosConArticulos;
            }

            if (CapitulosConArticulosAgregados == true) {
                // Actualizar Orden del Día
                var TareaActualizarOrdenDIa = OrdenDia.AprobarAsync(OrdenDiaModel.Id);
                OrdenDiaActualizado = await TareaActualizarOrdenDIa;
            }

            if (OrdenDiaActualizado == true) {
                // Actualizar sesión como realizada
                var TareaActualizarSesion = Sesion.RealizadaAsync(ActaModel.IdSesion);
                SesionActualizada = await TareaActualizarSesion;
            }

            if (RegistrosAfectados >= 0 && CapitulosConArticulosAgregados == true && OrdenDiaActualizado == true && SesionActualizada == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ActaModel.ObtenerInformacion(), SeccionSistema: "Actas de Sesión");
                return "ok";
            }
            else {
                return "Error al crear el Acta";
            }
        }

        public async Task<bool> ActualizarAsync(Acta ActaModel) {
            var ActaBD = ActaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(ActaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ActaModel.ObtenerInformacion(), SeccionSistema: "Actas de Sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdActa) {           
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerActaBD = ContextoBD.SGJD_ACT_TAB_ACTAS.FindAsync(IdActa);
                var ActaBD = await TareaObtenerActaBD;
                ContextoBD.SGJD_ACT_TAB_ACTAS.Attach(ActaBD);
                ContextoBD.SGJD_ACT_TAB_ACTAS.Remove(ActaBD);
                var RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdActa.ToString(), ValorNuevo: "", SeccionSistema: "Acta");
                    return true;
                }
                else {
                    return false;
                }
            }           
        }

        public async Task<bool> DarVistoBuenoAsync(int IdActa) {
            // Obtener el acta por id
            var TareaObtenerActa = ObtenerPorIdAsync(Convert.ToInt32(IdActa));
            Acta ModeloActa = await TareaObtenerActa;

            // Cambiar estado del acta de transcripción a [Visto Bueno]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloActa, "AC-VB");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloActa.ObtenerInformacion(), ValorNuevo: ModeloActa.ObtenerInformacion(), SeccionSistema: "Actas");

            return EstadoActualizado;
        }

        public async Task<bool> DarControlCalidadAsync(int IdActa) {
            // Obtener el acta por id
            var TareaObtenerActa = ObtenerPorIdAsync(Convert.ToInt32(IdActa));
            Acta ModeloActa = await TareaObtenerActa;

            // Cambiar estado del acta de visto bueno a [Control de calidad]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloActa, "AC-CC");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloActa.ObtenerInformacion(), ValorNuevo: ModeloActa.ObtenerInformacion(), SeccionSistema: "Actas");

            return EstadoActualizado;
        }

        public async Task<bool> AprobarAsync(int IdActa) {
            // Obtener el acta por id
            var TareaObtenerActa = ObtenerPorIdAsync(Convert.ToInt32(IdActa));
            Acta ModeloActa = await TareaObtenerActa;

            bool EstadoActualizado = false;

            if (ModeloActa.IdUsuarioPreside != null && ModeloActa.IdSesionAprobada != null) {
                // Cambiar estado del acta de control de calidad a [Aprobada]
                var TareaActualiazarEstado = CambiarEstadoAsync(ModeloActa, "AC-APROB");
                EstadoActualizado = await TareaActualiazarEstado;

                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloActa.ObtenerInformacion(), ValorNuevo: ModeloActa.ObtenerInformacion(), SeccionSistema: "Actas");
            }

            return EstadoActualizado;
        }

        public async Task<bool> AgregarActaFirmadaAsync(Acta ModeloActa, HttpPostedFileBase Archivo) {
            // Actualizar en base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener Acta a actualizar
                var TareaObtenerActaBD = ContextoBD.SGJD_ACT_TAB_ACTAS.FindAsync(ModeloActa.Id);

                // Obtener el estado que corresponde al acta firmada
                var TareaTraerEstado = Estado.ObtenerPorCodigoAsync("AC-FIRM");

                //Descargar pdf a carpeta de repositorio del tomo respectivo
                var TareaDescargarPDF = Repositorio.GuardarActaPDFRepositorioTomo(ModeloActa, Archivo);

                // Obtener Acta a actualizar
                var ActaBD = await TareaObtenerActaBD;

                // Obtener el estado que corresponde al acta firmada
                Estado ModeloEstado = await TareaTraerEstado;

                // Obtener URL en que se guardó el PDF del acta firmada
                string RutaPDFActaFirmada = await TareaDescargarPDF;

                // Calcuar el número de páginas del PDF subido
                int CantidadPaginas = CalcularPaginasPDF(RutaPDFActaFirmada);

                if (!String.IsNullOrEmpty(RutaPDFActaFirmada)) {
                    // Asignar el valor de la URL del archivo guardado en la columna de la tabla acuerdo
                    ActaBD.UrlActaFirmada = RutaPDFActaFirmada;

                    //Cambiar el estado del Acta a firmada
                    ActaBD.LLF_IdEstado = ModeloEstado.Id;

                    // Actualizar cantidad de paginas del PDF
                    ActaBD.NumeroPaginasPDF = CantidadPaginas;

                    // Guardar en BD
                    ContextoBD.Entry(ActaBD).State = EntityState.Modified;
                    int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                    if (RegistrosAfectados > 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarActaFirmadaAsync(Acta ModeloActa) {
            // Actualizar en base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerActaBD = ContextoBD.SGJD_ACT_TAB_ACTAS.FindAsync(ModeloActa.Id);
                var ActaBD = await TareaObtenerActaBD;

                // Eliminar el archivo fisicamente
                // La URL del acta se encuentra en la tabla ACTAS y no en la tabla ARCHIVOS_ADJUNTOS, es por eso
                // que no se ejecuta la funcion enviando el modelo ArchivoAdjunto, sino que se usa el metodo EliminarAsync que recibe la URL
                bool ArchivoEliminado = ArchivoAdjunto.EliminarAsync(ActaBD.UrlActaFirmada);

                // Limpiar el valor de la URL del archivo guardado en la columna de la tabla acuerdo
                ActaBD.UrlActaFirmada = null;

                // Guardar en BD
                ContextoBD.Entry(ActaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados > 0 & ArchivoEliminado == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Acta> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener lista de actas utilizando linq para utilizar ciertas columnas, ya que entity obtiene las relaciones inclusive, lo cual es pesado por los contenidos de la transcripción
                var ObtenerActasBD = from Actas in ContextoBD.SGJD_ACT_TAB_ACTAS
                                     select new {
                                         Actas.LLP_Id,
                                         Actas.LLF_IdSesion,
                                         Actas.LLF_IdEstado,
                                         Sesion = Actas.SGJD_ACT_TAB_SESIONES,
                                         Tipo_Sesion_De_Sesion = Actas.SGJD_ACT_TAB_SESIONES.SGJD_ADM_TAB_TIPOS_SESION,
                                         Usuario_De_Sesion = Actas.SGJD_ACT_TAB_SESIONES.SGJD_ADM_TAB_USUARIOS,
                                         Estado_De_Sesion = Actas.SGJD_ACT_TAB_SESIONES.SGJD_ADM_TAB_ESTADOS,
                                         Actas.SGJD_ADM_TAB_ESTADOS,
                                         Actas.SGJD_ACT_TAB_TOMOS
                                     };

                // Crear lista de Actas especificando los campos que se van a llenar
                List<Acta> ListaActas = ObtenerActasBD.AsEnumerable().Select(a => new Acta {
                    Id = a.LLP_Id,
                    IdSesion = a.LLF_IdSesion,
                    Sesion = new Sesion(a.Sesion, a.Tipo_Sesion_De_Sesion, a.Usuario_De_Sesion, a.Estado_De_Sesion),
                    Estado = new Estado(a.SGJD_ADM_TAB_ESTADOS),
                    Tomo = new Tomo(a.SGJD_ACT_TAB_TOMOS)
                }).ToList();

                return ListaActas;
            }
        }

        public Task<IEnumerable<Acta>> ObtenerTodosAsync() {
            Task<IEnumerable<Acta>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<ActasPorPalabraClaveDTO> ObtenerTodosPorPalabraClave(string Palabra) {
            IEnumerable<ActasPorPalabraClaveDTO> Actas = new ActaDAO().ObtenerActasPorPalabraClave(Palabra);
            return Actas;
        }

        public Task<IEnumerable<ActasPorPalabraClaveDTO>> ObtenerTodosPorPalabraClaveAsync(string Palabra) {
            Task<IEnumerable<ActasPorPalabraClaveDTO>> Tarea = Task.Run(() => ObtenerTodosPorPalabraClave(Palabra));
            return Tarea;
        }

        public async Task<Acta> ObtenerPorIdAsync(int IdActa) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerActaBD = ContextoBD.SGJD_ACT_TAB_ACTAS.FindAsync(IdActa);
                var ActaBD = await TareaObtenerActaBD;

                if (ActaBD != null) {
                    Acta ActaModel = new Acta(ActaBD, ActaBD.SGJD_ACT_TAB_SESIONES, ActaBD.SGJD_ACT_TAB_TOMOS, ActaBD.SGJD_ADM_TAB_ESTADOS, ActaBD.SGJD_ACT_TAB_SESIONES1, ActaBD.SGJD_ADM_TAB_USUARIOS, ActaBD.SGJD_ADM_TAB_USUARIOS1, ActaBD.SGJD_ACT_TAB_CAPITULOS);
                    return ActaModel;
                }
                else {
                    return null;
                }
            }
        }

        public async Task<Acta> ObtenerPorIdSesionAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerActa = ContextoBD.SGJD_ACT_TAB_ACTAS.Where(n => n.LLF_IdSesion == IdSesion).FirstOrDefaultAsync();
                var ActaBD = await TareaObtenerActa;

                if (ActaBD != null) {
                    Acta ActaModel = new Acta(ActaBD, ActaBD.SGJD_ACT_TAB_SESIONES, ActaBD.SGJD_ACT_TAB_TOMOS, ActaBD.SGJD_ADM_TAB_ESTADOS, ActaBD.SGJD_ACT_TAB_SESIONES1, ActaBD.SGJD_ADM_TAB_USUARIOS, ActaBD.SGJD_ADM_TAB_USUARIOS1, ActaBD.SGJD_ACT_TAB_CAPITULOS);
                    return ActaModel;
                }
                else {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Acta>> ObtenerTodosPorFechaAsync(DateTime fechaInicio, DateTime fechaFin) {
            // Establecer a la fecha fin la hora para abarcar todo el día.
            fechaFin = fechaFin + new TimeSpan(0, 23, 59, 59, 999);

            var listaActaBD = new List<SGJD_ACT_TAB_ACTAS>();

            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaListaeActas = ContextoBD.SGJD_ACT_TAB_ACTAS.Where(F => F.SGJD_ACT_TAB_SESIONES.FechaHora >= fechaInicio && F.SGJD_ACT_TAB_SESIONES.FechaHora <= fechaFin).ToListAsync();
                listaActaBD = await tareaListaeActas;

                var listaActaModel = new List<Acta>();
                foreach (var actaBD in listaActaBD) {
                    var actaModel = new Acta(actaBD);
                    listaActaModel.Add(actaModel);
                }

                return listaActaModel;
            }
        }

        //Actas Acuersoft
        private IEnumerable<ActasAcuersoftDTO> ObtenerActasAcuersoft() {
            IEnumerable<ActasAcuersoftDTO> ActasAcuersoft = new ActaDAO().ObtenerActasAcuersoft();
            return ActasAcuersoft;
        }

        public Task<IEnumerable<ActasAcuersoftDTO>> ObtenerActasAcuersoftAsync() {
            Task<IEnumerable<ActasAcuersoftDTO>> Tarea = Task.Run(() => ObtenerActasAcuersoft());
            return Tarea;
        }

        private IEnumerable<ActasDetalleAcuersoftDTO> ObtenerDetalleActaAcuersoft(int IdActa) {
            IEnumerable<ActasDetalleAcuersoftDTO> ActasDetalleAcuersoft = new ActaDAO().ObtenerDetalleActasAcuersoft(IdActa);
            return ActasDetalleAcuersoft;
        }

        public Task<IEnumerable<ActasDetalleAcuersoftDTO>> ObtenerDetallesActaAcuersoftAsync(int IdActa) {
            Task<IEnumerable<ActasDetalleAcuersoftDTO>> Tarea = Task.Run(() => ObtenerDetalleActaAcuersoft(IdActa));
            return Tarea;
        }

        public async Task<ActaAcuersoft> ObtenerActaAcuersoftPorIdAsync(int IdActa) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerActaBD = ContextoBD.SGJD_ACT_TAB_ACTAS_ACUERSOFT.FindAsync(IdActa);
                var ActaAcuersoftBD = await TareaObtenerActaBD;

                if (ActaAcuersoftBD != null) {
                    ActaAcuersoft ActaModel = new ActaAcuersoft(ActaAcuersoftBD);
                    return ActaModel;
                }
                else {
                    return null;
                }
            }
        }

        /// <summary>
        /// Cuenta los registros totales y la cantidad de paginas con la palabra clave correspondiente 
        /// </summary>
        private IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO> ObtenerRegistrosPorPalabraClave(string Palabra) {
            IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO> Actas = new ActaDAO().ObtenerRegistrosActasAcuersoftPorPalabraClave(Palabra);
            return Actas;
        }

        public Task<IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO>> ObtenerRegistrosAcuersoftPorPalabraClaveAsync(string Palabra) {
            Task<IEnumerable<ContarRegistrosActasAcuersoftPorPalabraDTO>> Tarea = Task.Run(() => ObtenerRegistrosPorPalabraClave(Palabra));
            return Tarea;
        }

        /// <summary>
        /// Trae los registros paginados de 10 en 10 segun la palabre clave correspondiente 
        /// </summary>
        private IEnumerable<ActasAcuersoftPorPalablaClaveDTO> ObtenerTodoPorPalabraClave(string Palabra, int Pagina) {
            IEnumerable<ActasAcuersoftPorPalablaClaveDTO> Actas = new ActaDAO().ObtenerActasAcuersoftPorPalabraClave(Palabra, Pagina);
            return Actas;
        }

        public Task<IEnumerable<ActasAcuersoftPorPalablaClaveDTO>> ObtenerTodosAcuersoftPorPalabraClaveAsync(string Palabra, int Pagina) {
            Task<IEnumerable<ActasAcuersoftPorPalablaClaveDTO>> Tarea = Task.Run(() => ObtenerTodoPorPalabraClave(Palabra, Pagina));
            return Tarea;
        }

        //////////
        // Métodos privados
        //////////

        /// <summary>
        /// Cambiar estado de un acta
        /// </summary>
        private async Task<bool> CambiarEstadoAsync(Acta ModeloActa, string CodigoEstado) {
            // Obtener el estado correspondiente al codigo especificado
            var TareaTraerEstado = Estado.ObtenerPorCodigoAsync(CodigoEstado);
            Estado ModeloEstado = await TareaTraerEstado;

            // Establecer el nuevo estado
            ModeloActa.IdEstado = ModeloEstado.Id;

            // Actualizar en la base de datos
            var TareaActualizarActa = ActualizarAsync(ModeloActa);
            bool ActaActualizado = await TareaActualizarActa;

            return ActaActualizado;
        }

        /// <summary>
        /// Registrar capítulos con artículos
        /// </summary>
        public async Task<bool> AgregarSeccionesConTemasComoCapitulosConArticulosAsync(Acta ModeloActa, OrdenDia ModeloOrdenDia) {
            IEnumerable<Capitulo> ListaCapitulos = ModeloOrdenDia.Secciones.Select(Seccion => new Capitulo(ModeloActa.Id, Seccion.Descripcion, Seccion.Temas)).ToList();

            int RegistrosAfectadosCapitulo = 0;
            int RegistrosAfectadosArticulo = 0;

            using (var ContextoBD = SGJDDBContext.Create()) {
                // Recorrer lista de capítulos para agregarlos a la base de datos
                foreach (Capitulo Capitulo in ListaCapitulos) {
                    // Validar si capítulo tiene artículos, si no tiene, lo cual se da cuando el o los temas de una sección no se aprobaron, no se guarda el capítulo en la base de datos
                    if (Capitulo.Articulos.Any()) {
                        var CapituloBD = Capitulo.BD();
                        ContextoBD.SGJD_ACT_TAB_CAPITULOS.Add(CapituloBD);
                        RegistrosAfectadosCapitulo = await ContextoBD.SaveChangesAsync();

                        Capitulo.Id = CapituloBD.LLP_Id;

                        // Recorrer lista de artículos de cada capitulo para agregarlos a la base de datos
                        foreach (Articulo Articulo in Capitulo.Articulos) {
                            // Con un id de capítulo establecido, se actualiza el id del capítulo de cada articulo del capitulo
                            Articulo.IdCapitulo = Capitulo.Id;

                            var ArticuloBD = Articulo.BD();
                            ContextoBD.SGJD_ACT_TAB_ARTICULOS.Add(ArticuloBD);
                        }

                        RegistrosAfectadosArticulo = await ContextoBD.SaveChangesAsync();
                    }
                }
            }

            if (RegistrosAfectadosCapitulo == 1 && RegistrosAfectadosArticulo >= 0) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar capítulos y artículos", ValorAntiguo: "", ValorNuevo: ModeloActa.Id.ToString(), SeccionSistema: "Actas");
                return true;
            }
            else {
                return false;
            }
        }

        private int CalcularPaginasPDF(string RutaArchivo) {
            // Obtener URL con ruta donde se ubica la carpeta del sitio, que contiene la carpeta RepositorioSGJD
            string Raiz = HttpContext.Current.Server.MapPath("~");

            RutaArchivo = RutaArchivo.Substring(1, RutaArchivo.Length -1);

            using (PdfReader PDFReader = new PdfReader(Raiz + RutaArchivo)) {
                int CantidadDePaginas = PDFReader.NumberOfPages;
                return CantidadDePaginas;
            }
        }

        private string GenerarEncabezadoActa(IEnumerable<AsistenteSesion> ListaTodosAsistentesSesion, IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes, string TipoSesion, int NumeroSesion, DateTime FechaHoraSesion) {
            string ListaDirectores = "";
            string ListaAusentes = "";
            string ListaAdministracion = "";
            string ListaAsesoriaLegal = "";
            string ListaSecretariaTecnica = "";
            string ListaExpositores = "";
            string ContenidoEncabezado = "";

            if (ListaTodosAsistentesSesion.Count() > 0) {
                foreach (AsistenteSesion AsistenteSesion in ListaTodosAsistentesSesion) {
                    if (AsistenteSesion.TipoAsistencia.Equals("Presente")) {
                        if (AsistenteSesion.Usuario.Rol.Name.Equals("Director") || AsistenteSesion.Usuario.Rol.Name.Equals("Presidencia")) {
                            ListaDirectores += AsistenteSesion.Usuario.Nombre + ", ";
                        }
                        else if (AsistenteSesion.Usuario.Rol.Name.Equals("Gerente General") || AsistenteSesion.Usuario.Rol.Name.Equals("Subgerente Tecnico")) {
                            ListaAdministracion += AsistenteSesion.Usuario.Nombre + ", ";
                        }
                        else if (AsistenteSesion.Usuario.Rol.Name.Equals("Asesor Legal")) {
                            ListaAsesoriaLegal += AsistenteSesion.Usuario.Nombre + ", ";
                        }
                        else if (AsistenteSesion.Usuario.Rol.Name.Equals("Secretario Tecnico")) {
                            ListaSecretariaTecnica += AsistenteSesion.Usuario.Nombre + ", ";
                        }
                    }
                    else {
                        ListaAusentes += AsistenteSesion.Usuario.Nombre + ", ";
                    }
                }

                foreach (OtroAsistenteSesion OtrosAsistente in ListaOtrosAsistentes) {
                    ListaExpositores += OtrosAsistente.Nombre + ", ";
                }

                if (FechaHoraSesion.Minute == 1) {
                    ContenidoEncabezado = "<p class='text-justified font-arial fz-16'>Acta de la " + TipoSesion + " <u class='text-underline font-bold'>número " + Funciones.TextToNumber(NumeroSesion) +
                        ",</u> celebrada por la Junta Directiva del Instituto Nacional de Aprendizaje, en el Edificio de Comercio y Servicios, a las " +
                        Funciones.TextToNumber(FechaHoraSesion.Hour) + " horas con un minuto del " + Funciones.TextToNumber(FechaHoraSesion.Day) + " de " + FechaHoraSesion.ToString("MMMM") +
                        " del " + Funciones.TextToNumber(FechaHoraSesion.Year) + ", con la asistencia de los siguientes </p>";
                }
                else {
                    ContenidoEncabezado = "<p class='text-justified font-arial fz-16'>Acta de la " + TipoSesion + " <u class='text-underline'>número " + Funciones.TextToNumber(NumeroSesion) +
                        ",</u> celebrada por la Junta Directiva del Instituto Nacional de Aprendizaje, en el Edificio de Comercio y Servicios, a las " +
                        Funciones.TextToNumber(FechaHoraSesion.Hour) + " horas con " + Funciones.TextToNumber(FechaHoraSesion.Minute) + " minutos del " +
                        Funciones.TextToNumber(FechaHoraSesion.Day) + " de " + FechaHoraSesion.ToString("MMMM") +
                        " del " + Funciones.TextToNumber(FechaHoraSesion.Year) + ", con la asistencia de los siguientes </p>";
                }

                if (!String.IsNullOrEmpty(ListaDirectores)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Directores presentes:</u> &nbsp" + ListaDirectores + "</p>";
                }
                if (!String.IsNullOrEmpty(ListaAusentes)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Directores ausentes:</u> &nbsp" + ListaAusentes + "</p>";
                }
                if (!String.IsNullOrEmpty(ListaAdministracion)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Por la Administración:</u> &nbsp" + ListaAdministracion + "</p>";
                }
                if (!String.IsNullOrEmpty(ListaAsesoriaLegal)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Por la Asesoría Legal:</u> &nbsp" + ListaAsesoriaLegal + "</p>";
                }
                if (!String.IsNullOrEmpty(ListaSecretariaTecnica)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Por la Secretaría Técnica:</u> &nbsp" + ListaSecretariaTecnica + "</p>";
                }
                if (!String.IsNullOrEmpty(ListaExpositores)) {
                    ContenidoEncabezado += "<p class='text-justified font-arial fz-16'><u class='text-underline'>Expositores:</u> &nbsp" + ListaExpositores + "</p>";
                }

                return ContenidoEncabezado;
            }
            else {
                return String.Empty;
            }
        }
    }
}