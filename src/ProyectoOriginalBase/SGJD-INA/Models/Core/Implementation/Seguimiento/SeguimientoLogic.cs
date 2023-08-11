using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    public class SeguimientoLogic : ISeguimientoLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly ITipoArchivoLogic TipoArchivo;
        private readonly IRepositorioLogic Repositorio;
        private readonly IAcuerdoLogic Acuerdo;
        private readonly IEstadoLogic Estado;
        private readonly IAvisosLogic Aviso;
      
        public SeguimientoLogic(IBitacoraLogic Bitacora, IArchivoAdjuntoLogic ArchivoAdjunto, ITipoObjetoLogic TipoObjeto, ITipoArchivoLogic TipoArchivo, IRepositorioLogic Repositorio, IAcuerdoLogic Acuerdo, IEstadoLogic Estado, IAvisosLogic Aviso) {
            this.Bitacora = Bitacora;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.TipoObjeto = TipoObjeto;
            this.TipoArchivo = TipoArchivo;
            this.Repositorio = Repositorio;
            this.Acuerdo = Acuerdo;
            this.Estado = Estado;
            this.Aviso = Aviso;
        }
        #endregion

        #region Métodos públicos
        public async Task<int> AgregarAsync(SeguimientoAcuerdo SeguimientoAcuerdoModel) {
            // Variables de control
            int RegistrosAfectados = 0;
            bool AcuerdoActualizado = false;

            // Guardar seguimiento en la base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var SeguimientoAcuerdoBD = SeguimientoAcuerdoModel.BD();
                SeguimientoAcuerdoBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Add(SeguimientoAcuerdoBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id establecido en la base de datos al modelo
                SeguimientoAcuerdoModel.Id = SeguimientoAcuerdoBD.LLP_Id;
            }

            if (RegistrosAfectados >= 0) {
                // Actualizar el acuerdo a [En ejecución]
                var TareaActualizarAcuerdo = Acuerdo.AcuerdoEnEjecucionAsync(SeguimientoAcuerdoModel.IdAcuerdo);
                AcuerdoActualizado = await TareaActualizarAcuerdo;
            }

            if (RegistrosAfectados >= 0 && AcuerdoActualizado == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: SeguimientoAcuerdoModel.ObtenerInformacion(), SeccionSistema: "Segumiento Acuerdo");
                return SeguimientoAcuerdoModel.Id;
            }
            else {
                return 0;
            }
        }

        public async Task<bool> ActualizarAsync(SeguimientoAcuerdo SeguimientoAcuerdoModel) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var seguimientoAcuerdoModelBD = SeguimientoAcuerdoModel.BD();
                ContextoBD.Entry(seguimientoAcuerdoModelBD).State = EntityState.Modified;
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: SeguimientoAcuerdoModel.ObtenerInformacion(), SeccionSistema: "Segumiento Acuerdo");

            return true;
        }

        public async Task<bool> EliminarAsync(int IdSeguimientoAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaseguimientoAcuerdoBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.FindAsync(IdSeguimientoAcuerdo);
                var SeguimientoAcuerdoBD = await TareaseguimientoAcuerdoBD;
                ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Attach(SeguimientoAcuerdoBD);
                ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Remove(SeguimientoAcuerdoBD);
                var RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdSeguimientoAcuerdo.ToString(), ValorNuevo: "", SeccionSistema: "Seguimiento");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<SeguimientoAcuerdo> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaSeguimientosAcuerdosBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.ToList();
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientosAcuerdosModel = ListaSeguimientosAcuerdosBD.Select(SeguimientoAcuerdoBD => new SeguimientoAcuerdo(SeguimientoAcuerdoBD)).ToList();
                return ListaSeguimientosAcuerdosModel;
            }
        }

        public Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosAsync() {
            Task<IEnumerable<SeguimientoAcuerdo>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<SeguimientoAcuerdo> ObtenerPorIdAsync(int IdSeguimientoAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerseguimientoAcuerdoBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.FindAsync(IdSeguimientoAcuerdo);
                var SeguimientoAcuerdoBD = await TareaObtenerseguimientoAcuerdoBD;
                SeguimientoAcuerdo SeguimientoAcuerdoModel = new SeguimientoAcuerdo(SeguimientoAcuerdoBD, SeguimientoAcuerdoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoAcuerdoBD.SGJD_ADM_TAB_ESTADOS, SeguimientoAcuerdoBD.SGJD_ACU_TAB_AVANCES, SeguimientoAcuerdoBD.SGJD_ADM_TAB_UNIDADES);
                return SeguimientoAcuerdoModel;
            }
        }

        public async Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosPorIdAcuerdoAsync(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaseguimientoAcuerdoBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Where(S => S.LLF_IdAcuerdo == IdAcuerdo).OrderByDescending(S => S.LLP_Id).ToListAsync();
                var ListaseguimientosAcuerdoBD = await TareaseguimientoAcuerdoBD;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientosAcuerdosModel = ListaseguimientosAcuerdoBD.Select(SeguimientoAcuerdoBD => new SeguimientoAcuerdo(SeguimientoAcuerdoBD, SeguimientoAcuerdoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoAcuerdoBD.SGJD_ADM_TAB_ESTADOS)).ToList();
                return ListaSeguimientosAcuerdosModel;
            }
        }

        public async Task<SeguimientoAcuerdo> ObtenerUltimoSeguimientoAcuerdoPorAcuerdo(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObetenerSeguimientoAcuerdoBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Where(SA => SA.LLF_IdAcuerdo == IdAcuerdo).ToListAsync();
                var ListaSeguimientosAcuerdoBD = await TareaObetenerSeguimientoAcuerdoBD;
                SeguimientoAcuerdo SeguimientoAcuerdoModel = new SeguimientoAcuerdo(ListaSeguimientosAcuerdoBD.LastOrDefault());
                return SeguimientoAcuerdoModel;
            }
        }

        public async Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoNoEjecutado() {
            // Obtener seguimientos con estado [No ejecutados]
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeguimientosBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Where(Seguimiento => Seguimiento.LLF_IdEstado == 1027).ToListAsync();
                var ListaSeguimientosNoEjecutadosBD = await TareaObtenerSeguimientosBD;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientos = ListaSeguimientosNoEjecutadosBD.Select(SeguimientoAcuerdoBD => new SeguimientoAcuerdo(SeguimientoAcuerdoBD, SeguimientoAcuerdoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoAcuerdoBD.SGJD_ADM_TAB_ESTADOS)).ToList();
                return ListaSeguimientos;
            }
        }

        public async Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoEnEjecucion() {
            // Obtener seguimientos con estado [En ejecución]
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeguimientosBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Where(Seguimiento => Seguimiento.LLF_IdEstado == 1028).ToListAsync();
                var ListaSeguimientosEnEjecucionBD = await TareaObtenerSeguimientosBD;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientos = ListaSeguimientosEnEjecucionBD.Select(SeguimientoAcuerdoBD => new SeguimientoAcuerdo(SeguimientoAcuerdoBD, SeguimientoAcuerdoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoAcuerdoBD.SGJD_ADM_TAB_ESTADOS)).ToList();
                return ListaSeguimientos;
            }
        }

        public async Task<IEnumerable<SeguimientoAcuerdo>> ObtenerTodosEstadoEjecutado() {
            // Obtener seguimientos con estado [Ejecutados]
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeguimientosBD = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.Where(Seguimiento => Seguimiento.LLF_IdEstado == 1029).ToListAsync();
                var ListaSeguimientosEjecutadosBD = await TareaObtenerSeguimientosBD;
                IEnumerable<SeguimientoAcuerdo> ListaSeguimientos = ListaSeguimientosEjecutadosBD.Select(SeguimientoAcuerdoBD => new SeguimientoAcuerdo(SeguimientoAcuerdoBD, SeguimientoAcuerdoBD.SGJD_ACU_TAB_ACUERDOS, SeguimientoAcuerdoBD.SGJD_ADM_TAB_ESTADOS)).ToList();
                return ListaSeguimientos;
            }
        }

        public IEnumerable<SelectListItem> ObtenerEstadosSeguimientoParaSelect() {
            List<SelectListItem> ListaTiposSeguimiento = new List<SelectListItem>();
            ListaTiposSeguimiento.Add(new SelectListItem { Text = "No ejecutado", Value = "1027" });
            ListaTiposSeguimiento.Add(new SelectListItem { Text = "En ejecución", Value = "1028" });
            ListaTiposSeguimiento.Add(new SelectListItem { Text = "Ejecutado", Value = "1029" });
            return ListaTiposSeguimiento;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerEstadosSeguimientoParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerEstadosSeguimientoParaSelect());
            return Tarea;
        }

        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerAdjuntos(int IdSeguimiento) {
            // Obtener modelo de Seguimiento 
            var TareaObtenerSeguimiento = ObtenerPorIdAsync(IdSeguimiento);
            SeguimientoAcuerdo ModeloSeguimiento = await TareaObtenerSeguimiento;

            // Obtener modelo TipoObjeto por el nombre de objeto de un seguimiento
            var TareaObtenerTipoObjetoSeguimiento = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloSeguimiento.NombreObjeto);
            TipoObjeto TipoObjetoSegumiento = await TareaObtenerTipoObjetoSeguimiento;

            // Obtener los archivos adjuntos del acuerdo 
            var TareaObtenerAdjuntos = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(IdSeguimiento, TipoObjetoSegumiento.Id);
            IEnumerable<ArchivoAdjunto> ListaAdjuntos = await TareaObtenerAdjuntos;
            return ListaAdjuntos;
        }

        public async Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo) {
            string NombreTabla = ModeloArchivoAdjunto.NombreObjeto;
            string ExtensionArchivo = Path.GetExtension(Archivo.FileName);

            var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync(NombreTabla);
            var TareaObtenerTipoArchivo = TipoArchivo.ObtenerPorExtensionAsync(ExtensionArchivo);
            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Acuerdos");

            // Obtener la información del TipoObjeto según la propiedad NombreObjeto (Nombre de la tabla)
            TipoObjeto ModeloTipoObjetoSeguimientoAcuerdo = await TareaObtenerTipoObjeto;

            // Obtener la información del tipo de archivo, según la extensión del archivo subido
            TipoArchivo ModeloTipoArchivo = await TareaObtenerTipoArchivo;

            // Obtener la información del repositorio, según la extensión del archivo subido
            Repositorio ModeloRepositorio = await TareaObtenerRepositorio;

            // TODO: Agregar campo a la tabla de repositorios para que se refiera a un tipo de objeto y poder obtener el
            // repositorio por el id del tipoObjeto al que pertenece el archivo a subir
            ModeloArchivoAdjunto.IdTipoObjeto = ModeloTipoObjetoSeguimientoAcuerdo.Id;
            ModeloArchivoAdjunto.TipoObjeto = ModeloTipoObjetoSeguimientoAcuerdo;

            ModeloArchivoAdjunto.IdRepositorio = ModeloRepositorio.Id;
            ModeloArchivoAdjunto.Repositorio = ModeloRepositorio;

            ModeloArchivoAdjunto.IdTipoArchivo = ModeloTipoArchivo.Id;
            ModeloArchivoAdjunto.TipoArchivo = ModeloTipoArchivo;

            //Agrega archivo adjunto a la carpeta y la base de datos
            var TareaAgregarArchivoAdjunto = ArchivoAdjunto.AgregarAsync(ModeloArchivoAdjunto, Archivo);
            ModeloArchivoAdjunto = await TareaAgregarArchivoAdjunto;

            return true;
        }

        public async Task<bool> NotificarUnidadesDeSeguimientoVencido(int IdSeguimiento, string TextoEnlace, string Enlace, string MensajeDetalle, bool EnviarCorreoUsuariosDeUnidades) {
            var TareaObtenerSeguimiento = ObtenerPorIdAsync(IdSeguimiento);
            SeguimientoAcuerdo ModeloSeguimiento = await TareaObtenerSeguimiento;

            var TareaObtenerAvisoSeguimientoVencido = Aviso.ObtenerPorNombreAsync("Notificar Seguimiento Vencido");
            Aviso ModeloAviso = await TareaObtenerAvisoSeguimientoVencido;

            // Obtener las unidades del segumiento vencido
            IEnumerable<Unidad> ListaUnidadesSeguimiento = ModeloSeguimiento.Unidades;

            bool AvisoEnviadoPorCorreo = true;

            if (EnviarCorreoUsuariosDeUnidades == true) {
                // Recorrer la lista de unidades
                foreach (Unidad Unidad in ListaUnidadesSeguimiento) {
                    IEnumerable<ApplicationUser> ListaUsuariosUnidad = Unidad.Usuarios;

                    if (ListaUsuariosUnidad.Any()) {
                        var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ListaUsuariosUnidad);
                        AvisoEnviadoPorCorreo = await TareaEnviarAviso;

                        if (AvisoEnviadoPorCorreo == false) {
                            break;
                        }
                    }
                }
            }
            else {
                var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ListaUnidadesSeguimiento);
                AvisoEnviadoPorCorreo = await TareaEnviarAviso;
            }

            if (AvisoEnviadoPorCorreo == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Notificar Vencido", ValorAntiguo: ModeloSeguimiento.ObtenerInformacion(), ValorNuevo: ModeloSeguimiento.ObtenerInformacion(), SeccionSistema: "Seguimiento");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<bool> NotificarUnidadesDeSeguimiento(int IdSeguimiento, string TextoEnlace, string Enlace, string MensajeDetalle, bool EnviarCorreoUsuariosDeUnidades) {
            var TareaObtenerSeguimiento = ObtenerPorIdAsync(IdSeguimiento);
            SeguimientoAcuerdo ModeloSeguimiento = await TareaObtenerSeguimiento;

            // Cambiar estado del seguimiento a [En ejecución]
            var TareaActualizarEstadoSeguimiento = CambiarEstadoAsync(ModeloSeguimiento, "SA-EE");
            bool EstadoActualizado = await TareaActualizarEstadoSeguimiento;

            // Obtener el aviso del estado recien establecido al seguimiento para ser enviado a las unidades 
            Aviso ModeloAviso = ModeloSeguimiento.Estado.Aviso;

            // Obtener las unidades del segumiento
            IEnumerable<Unidad> ListaUnidadesSeguimiento = ModeloSeguimiento.Unidades;

            bool AvisoEnviadoPorCorreo = true;

            if (EnviarCorreoUsuariosDeUnidades == true) {
                // Recorrer la lista de unidades
                foreach (Unidad Unidad in ListaUnidadesSeguimiento) {
                    IEnumerable<ApplicationUser> ListaUsuariosUnidad = Unidad.Usuarios;

                    if (ListaUsuariosUnidad.Any()) {
                        var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ListaUsuariosUnidad);
                        AvisoEnviadoPorCorreo = await TareaEnviarAviso;

                        if (AvisoEnviadoPorCorreo == false) {
                            break;
                        }
                    }
                }
            }
            else {
                var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ListaUnidadesSeguimiento);
                AvisoEnviadoPorCorreo = await TareaEnviarAviso;
            }

            if (EstadoActualizado == true && AvisoEnviadoPorCorreo == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloSeguimiento.ObtenerInformacion(), ValorNuevo: ModeloSeguimiento.ObtenerInformacion(), SeccionSistema: "Seguimiento");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<string> SeguimientoEjecutadoAsync(int IdSeguimiento) {
            // Obtener el seguimiento por id
            var TareaObtenerSeguimiento = ObtenerPorIdAsync(IdSeguimiento);
            SeguimientoAcuerdo ModeloSeguimiento = await TareaObtenerSeguimiento;

            // Cambiar estado del seguimiento a [Ejecutado]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloSeguimiento, "SA-EJEC");
            bool EstadoActualizado = await TareaActualiazarEstado;

            // Obtener todos los seguimietnos de un acuerdo
            var TareaObtenerTodosLosSeguimientos = ObtenerTodosPorIdAcuerdoAsync(ModeloSeguimiento.IdAcuerdo);
            IEnumerable<SeguimientoAcuerdo> SeguimientosAcuerdo = await TareaObtenerTodosLosSeguimientos;

            // Revisar los seguimientos del acuerdo para saber si todos estan ejecutados
            bool SeguimientosEjecutados = true;

            foreach (SeguimientoAcuerdo Seguimiento in SeguimientosAcuerdo) {
                if (!Seguimiento.Estado.CodigoEstado.Equals("SA-EJEC")) {
                    SeguimientosEjecutados = false;
                    break;
                }
            }

            var AcuerdoActualizado = "Ok";

            if (SeguimientosEjecutados == true) {
                // Cambia el estado del acuerdo a [Ejecutado]
                var TareaCambiarEstadoAcuerdo = Acuerdo.AcuerdoEjecutadoAsync(ModeloSeguimiento.IdAcuerdo);
                AcuerdoActualizado = await TareaCambiarEstadoAcuerdo;
            }

            if (EstadoActualizado == true && AcuerdoActualizado == "Ok") {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Seguimiento ejecutado", ValorAntiguo: ModeloSeguimiento.ObtenerInformacion(), ValorNuevo: ModeloSeguimiento.ObtenerInformacion(), SeccionSistema: "Seguimiento");
                return "Ok";
            }
            else {
                return "Error";
            }
        }

        public async Task<bool> AgregarUnidadAsync(int IdSeguimiento, int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeguimiento = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.FindAsync(IdSeguimiento);
                var SeguimientoBD = await TareaObtenerSeguimiento;

                var TareaObtenerUnidad = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(IdUnidad);
                var UnidadBD = await TareaObtenerUnidad;

                SeguimientoBD.SGJD_ADM_TAB_UNIDADES.Add(UnidadBD);

                ContextoBD.Entry(SeguimientoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar Unidad a Seguimiento", ValorAntiguo: "", ValorNuevo: "IdSeguimiento: " + IdSeguimiento.ToString() + ", IdUnidad: " + IdUnidad.ToString(), SeccionSistema: "Seguimientos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarUnidadAsync(int IdSeguimiento, int IdUnidad) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSeguimiento = ContextoBD.SGJD_ACU_TAB_SEGUIMIENTOS.FindAsync(IdSeguimiento);
                var SeguimientoBD = await TareaObtenerSeguimiento;

                var TareaObtenerUnidad = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(IdUnidad);
                var UnidadBD = await TareaObtenerUnidad;

                SeguimientoBD.SGJD_ADM_TAB_UNIDADES.Remove(UnidadBD);

                ContextoBD.Entry(SeguimientoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar Unidad a Seguimiento", ValorAntiguo: "", ValorNuevo: "IdAcuerdo: " + SeguimientoBD.LLF_IdAcuerdo.ToString() + ", IdUnidad: " + IdUnidad.ToString(), SeccionSistema: "Seguimiento");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Unidad> ObtenerUnidadesEjecucion(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUnidades = ContextoBD.SGJD_ACU_TAB_ACUERDOS.Where(a => a.LLP_Id == IdAcuerdo).FirstOrDefault().SGJD_ADM_TAB_UNIDADES.ToList();
                IEnumerable<Unidad> ListaUnidadesEjecucion = ListaUnidades.Select(UnidadBD => new Unidad(UnidadBD, UnidadBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
                return ListaUnidadesEjecucion;
            }
        }

        public Task<IEnumerable<Unidad>> ObtenerUnidadesEjecucionAsync(int IdAcuerdo) {
            Task<IEnumerable<Unidad>> Tarea = Task.Run(() => ObtenerUnidadesEjecucion(IdAcuerdo));
            return Tarea;
        }
        
        private IEnumerable<UnidadesEjecucionSeguimientoDTO> ObtenerSeguimientosSegunIdUnidad(int IdUnidad) {
            IEnumerable<UnidadesEjecucionSeguimientoDTO> UnidadSeguimiento = new SeguimientosDAO().ObtenerSeguimientosSegunIdUnidad(IdUnidad);
            return UnidadSeguimiento;
        }

        public Task<IEnumerable<UnidadesEjecucionSeguimientoDTO>> ObtenerSeguimientosSegunIdUnidadAsyng(int IdUnidad) {
            Task<IEnumerable<UnidadesEjecucionSeguimientoDTO>> Tarea = Task.Run(() => ObtenerSeguimientosSegunIdUnidad(IdUnidad));
            return Tarea;
        }

        // Métodos privados
        private async Task<bool> CambiarEstadoAsync(SeguimientoAcuerdo ModeloSeguimiento, string CodigoEstado) {
            // Obtener el estado correspondiente al codigo especificado
            var TareaTraerEstado = Estado.ObtenerPorCodigoAsync(CodigoEstado);
            Estado ModeloEstado = await TareaTraerEstado;

            //Se establece el nuevo estado
            ModeloSeguimiento.IdEstado = ModeloEstado.Id;
            ModeloSeguimiento.Estado = ModeloEstado;

            // Actualizar en la base de datos
            var TareaActualizarSeguimiento = ActualizarAsync(ModeloSeguimiento);
            bool AcuerdoActualizado = await TareaActualizarSeguimiento;

            return AcuerdoActualizado;
        }             
        #endregion
    }
}