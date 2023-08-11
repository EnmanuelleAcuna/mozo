using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
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
    /// Lógica para realizar operaciones sobre la entidad Tema
    /// </summary>
    public class TemaLogic : ITemaLogic {
        #region Constructor ydependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly ITipoArchivoLogic TipoArchivo;
        private readonly IRepositorioLogic Repositorio;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly IEstadoLogic Estado;

        public TemaLogic(IBitacoraLogic Bitacora, IArchivoAdjuntoLogic ArchivoAdjunto, ITipoObjetoLogic TipoObjeto, ITipoArchivoLogic TipoArchivo, IRepositorioLogic Repositorio, IEstadoLogic Estado) {
            this.Bitacora = Bitacora;
            this.TipoObjeto = TipoObjeto;
            this.TipoArchivo = TipoArchivo;
            this.Repositorio = Repositorio;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.Estado = Estado;
        }
        #endregion

        #region Métodos públicos
        public async Task<int> AgregarAsync(Tema ModeloTema) {
            var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("TEM-PEND");
            Estado EstadoTema = await TareaObtenerEstado;

            ModeloTema.IdEstado = EstadoTema.Id;

            using (var ContextoBD = SGJDDBContext.Create()) {
                var TemaBD = ModeloTema.BD();
                TemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Add(TemaBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                ModeloTema.Id = TemaBD.LLP_Id;

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTema.ObtenerInformacion(), SeccionSistema: "Tema");
                    return ModeloTema.Id;
                }
                else {
                    // Si no se agregó el Tema a la BD, devolver 0 para que en la vista al validar el id del nuevo tema muestre que ocurrió un error
                    return 0;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Tema ModeloTema) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(ModeloTema.Id);
                var TemaBD = await TareaObtenerTemaBD;

                // Crear un modelo con la información original para guardar en bitácora
                Tema ModeloTemaOriginal = new Tema(TemaBD);

                // Modificar campos que deben ser actualizados
                TemaBD.Titulo = ModeloTema.Titulo;
                TemaBD.Resumen = ModeloTema.Resumen;
                TemaBD.Observacion = ModeloTema.Observacion;

                // Actualizar en BD
                ContextoBD.Entry(TemaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: ModeloTemaOriginal.ObtenerInformacion(), ValorNuevo: ModeloTema.ObtenerInformacion(), SeccionSistema: "Tema");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarEstadoAsync(Tema ModeloTema) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(ModeloTema.Id);
                var TemaBD = await TareaObtenerTemaBD;

                // Crear un modelo con la información original para guardar en bitácora
                Tema ModeloTemaOriginal = new Tema(TemaBD);

                TemaBD.LLF_IdEstado = ModeloTema.IdEstado; // Modificar el campo estado

                // Actualizar en BD
                ContextoBD.Entry(TemaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: ModeloTemaOriginal.ObtenerInformacion(), ValorNuevo: ModeloTema.ObtenerInformacion(), SeccionSistema: "Tema");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarTemaPendienteAsync(int IdTema, int IdOrdenDia, int IdSeccion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(IdTema);
                var TemaBD = await TareaObtenerTemaBD;

                // Crear un modelo con la información original para guardar en bitácora
                Tema ModeloTemaOriginal = new Tema(TemaBD);

                // Modificar campos que deben ser actualizados
                TemaBD.LLF_IdOrdenDia = IdOrdenDia;
                TemaBD.LLF_IdSeccion = IdSeccion;

                // Actualizar en BD
                ContextoBD.Entry(TemaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    Tema TemaActualizado = new Tema(TemaBD);

                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: ModeloTemaOriginal.ObtenerInformacion(), ValorNuevo: TemaActualizado.ObtenerInformacion(), SeccionSistema: "Tema");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdTema) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTema = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(IdTema);
                var TemaBD = await TareaObtenerTema;

                // Crear un modelo con la información del tema para guardar en bitácora
                Tema ModeloTemaOriginal = new Tema(TemaBD);

                // Actualizar en BD
                ContextoBD.SGJD_ACT_TAB_TEMAS.Attach(TemaBD);
                ContextoBD.SGJD_ACT_TAB_TEMAS.Remove(TemaBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: ModeloTemaOriginal.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Tema");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo) {
            string NombreTabla = ModeloArchivoAdjunto.NombreObjeto;
            string ExtensionArchivo = Path.GetExtension(Archivo.FileName);

            var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync(NombreTabla);
            var TareaObtenerTipoArchivo = TipoArchivo.ObtenerPorExtensionAsync(ExtensionArchivo);
            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("OrdenesDia");

            // Obtener la información del TipoObjeto según la propiedad NombreObjeto (Nombre de la tabla)
            TipoObjeto ModeloTipoObjetoTema = await TareaObtenerTipoObjeto;

            // Obtener la información del tipo de archivo, según la extensión del archivo subido
            TipoArchivo ModeloTipoArchivo = await TareaObtenerTipoArchivo;

            // Obtener la información del repositorio, según la extensión del archivo subido
            Repositorio ModeloRepositorio = await TareaObtenerRepositorio;

            // TODO: Agregar campo a la tabla de repositorios para que se refiera a un tipo de objeto y poder obtener el
            // repositorio por el id del tipoObjeto al que pertenece el archivo a subir
            ModeloArchivoAdjunto.IdTipoObjeto = ModeloTipoObjetoTema.Id;
            ModeloArchivoAdjunto.TipoObjeto = ModeloTipoObjetoTema;

            ModeloArchivoAdjunto.IdRepositorio = ModeloRepositorio.Id;
            ModeloArchivoAdjunto.Repositorio = ModeloRepositorio;

            ModeloArchivoAdjunto.IdTipoArchivo = ModeloTipoArchivo.Id;
            ModeloArchivoAdjunto.TipoArchivo = ModeloTipoArchivo;

            //Agrega archivo adjunto a la carpeta y la base de datos
            var TareaAgregarArchivoAdjunto = ArchivoAdjunto.AgregarAsync(ModeloArchivoAdjunto, Archivo);
            ModeloArchivoAdjunto = await TareaAgregarArchivoAdjunto;

            return true;
        }

        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerAdjuntosAsync(Tema ModeloTema) {
            int? IdTema = ModeloTema.Id;
            // Lista de adjuntos de tema
            IEnumerable<ArchivoAdjunto> ListaAdjuntosTema = new List<ArchivoAdjunto>();

            if (IdTema != null) {
                var TareaObtenerTipoObjetoTema = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloTema.NombreObjeto);
                TipoObjeto TipoObjetoTema = await TareaObtenerTipoObjetoTema;

                var TareaObtenerTema = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(Convert.ToInt32(IdTema), TipoObjetoTema.Id);
                ListaAdjuntosTema = await TareaObtenerTema;
            }
            return ListaAdjuntosTema;
        }

        public async Task<Tema> ObtenerPorIdAsync(int IdTema) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(IdTema);
                var TemaBD = await TareaObtenerTemaBD;
                Tema ModeloTema = new Tema(TemaBD);
                return ModeloTema;
            }
        }
        
        public async Task<Tema> ObtenerPorIdConOrdenDiaAsync(int IdTema) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.FindAsync(IdTema);
                var TemaBD = await TareaObtenerTemaBD;
                Tema ModeloTema = new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA);
                return ModeloTema;
            }
        }

        public async Task<IEnumerable<Tema>> ObtenerTodosPorIdSeccionIdOrdenDia(int IdSeccion, int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemasBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdSeccion == IdSeccion && T.LLF_IdOrdenDia == IdOrdenDia).ToListAsync();
                var ListaTemasBD = await TareaObtenerTemasBD;
                IEnumerable<Tema> ListaTemas = ListaTemasBD.Select(TemaBD => new Tema(TemaBD)).ToList();
                return ListaTemas;
            }
        }

        public async Task<IEnumerable<Tema>> ObtenerTodosPorIdOrdenDiaIdEstado(int IdOrdenDia, int IdEstado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemasBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdOrdenDia == IdOrdenDia && T.LLF_IdEstado == IdEstado).ToListAsync();
                var ListaTemasBD = await TareaObtenerTemasBD;
                IEnumerable<Tema> ListaTemas = ListaTemasBD.Select(TemaBD => new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA)).ToList();
                return ListaTemas;
            }
        }

        public async Task<IEnumerable<Tema>> ObtenerTodosPorIdEstadoIdOrdenDia(int IdEstado, int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemasBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdEstado == IdEstado && T.LLF_IdOrdenDia != IdOrdenDia).ToListAsync();
                var ListaTemasBD = await TareaObtenerTemasBD;
                IEnumerable<Tema> ListaTemas = ListaTemasBD.Select(TemaBD => new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA)).ToList();
                return ListaTemas;
            }
        }
        #endregion

        /// <summary>
        /// Obtener todos los temas por id de sección
        /// </summary>
        public async Task<IEnumerable<Tema>> ObtenerTodosPorIdSeccion(int IdSeccion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemasBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdSeccion == IdSeccion).ToListAsync();
                var ListaTemasBD = await TareaObtenerTemasBD;
                IEnumerable<Tema> ListaTemas = ListaTemasBD.Select(TemaBD => new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA)).ToList();
                return ListaTemas;
            }
        }

        /// <summary>
        /// Obtener todos los temas por id de orden dia
        /// </summary>
        public async Task<IEnumerable<Tema>> ObtenerTodosPorIdOrdenDia(int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTemasBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdOrdenDia == IdOrdenDia).ToListAsync();
                var ListaTemasBD = await TareaObtenerTemasBD;
                IEnumerable<Tema> ListaTemas = ListaTemasBD.Select(TemaBD => new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA)).ToList();
                return ListaTemas;
            }
        }

        /// <summary>
        ///Actualizar el tema de una sección de un orden del día para cambiar su posición y realizar auto guardado
        /// </summary>
        public async Task<bool> ActualizarPosicionTema(Tema TemaModelActual, Tema TemaModelCambio) {
            //Modificar los modelos
            var NuevotemaActualBD = ConvertirActualCambio(TemaModelActual, TemaModelCambio);
            var NuevotemaCambioBD = ConvertirCambioActual(TemaModelActual, TemaModelCambio);

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(NuevotemaActualBD).State = EntityState.Modified;
                ContextoBD.Entry(NuevotemaCambioBD).State = EntityState.Modified;
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar cambio de poscion de tema", ValorAntiguo: TemaModelActual.ObtenerInformacion(), ValorNuevo: TemaModelCambio.ObtenerInformacion(), SeccionSistema: "Tema");

            return true;
        }

        /// <summary>
        /// Obtener el ultimo tema por id de seccion y id de orden dia 
        /// </summary>
        public Tema ObtenerUltimoPorIdSeccionIdOrdenDia(int IdSeccion, int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Where(T => T.LLF_IdSeccion == IdSeccion && T.LLF_IdOrdenDia == IdOrdenDia).ToArray().Last();

                Tema ModeloTema = new Tema();

                if (TemaBD != null) {
                    ModeloTema = new Tema(TemaBD, TemaBD.SGJD_ADM_TAB_SECCIONES, TemaBD.SGJD_ADM_TAB_ESTADOS, TemaBD.SGJD_ACT_TAB_ORDENES_DIA);
                }

                return ModeloTema;
            }
        }

        /// <summary>
        /// Cambia la informacion del tema actual por la del tema a cambiar
        /// </summary>
        private SGJD_ACT_TAB_TEMAS ConvertirActualCambio(Tema temaModelActual, Tema temaModelCambio) {
            return new SGJD_ACT_TAB_TEMAS() {
                LLP_Id = temaModelActual.Id,
                LLF_IdSeccion = temaModelCambio.IdSeccion,
                LLF_IdEstado = temaModelCambio.IdEstado,
                LLF_IdOrdenDia = temaModelCambio.IdOrdenDia,
                Resumen = temaModelCambio.Resumen,
                Titulo = temaModelCambio.Titulo,
                Observacion = temaModelCambio.Observacion
            };
        }

        /// <summary>
        /// Cambia la informacion del tema a cambiar por la del tema actual
        /// </summary>
        private SGJD_ACT_TAB_TEMAS ConvertirCambioActual(Tema temaModelActual, Tema temaModelCambio) {
            return new SGJD_ACT_TAB_TEMAS() {
                LLP_Id = temaModelCambio.Id,
                LLF_IdSeccion = temaModelActual.IdSeccion,
                LLF_IdEstado = temaModelActual.IdEstado,
                LLF_IdOrdenDia = temaModelActual.IdOrdenDia,
                Resumen = temaModelActual.Resumen,
                Titulo = temaModelActual.Titulo,
                Observacion = temaModelActual.Observacion
            };
        }
    }
}