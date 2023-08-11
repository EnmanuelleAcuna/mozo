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
    /// Lógica para realizar operaciones sobre la entidad Orden del Día
    /// </summary>
    public class OrdenDiaLogic : IOrdenDiaLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly ITemaLogic Tema;
        private readonly IEstadoLogic Estado;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly ITipoObjetoLogic TipoObjeto;

        public OrdenDiaLogic(IBitacoraLogic Bitacora, ITemaLogic Tema, IEstadoLogic Estado, IArchivoAdjuntoLogic ArchivoAdjunto, ITipoObjetoLogic TipoObjeto) {
            this.Bitacora = Bitacora;
            this.Tema = Tema;
            this.Estado = Estado;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.TipoObjeto = TipoObjeto;
        }

        // Acciones
        public async Task<bool> AgregarAsync(OrdenDia ModeloOrdenDia) {
            var OrdenDiaBD = ModeloOrdenDia.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.Add(OrdenDiaBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Orden del Día");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<OrdenDia> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaOrdenDiaBD = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.OrderByDescending(O => O.LLP_Id).ToList();
                IEnumerable<OrdenDia> ListaOrdenDia = ListaOrdenDiaBD.Select(OrdenDiaBD => new OrdenDia(OrdenDiaBD, OrdenDiaBD.SGJD_ACT_TAB_SESIONES, OrdenDiaBD.SGJD_ADM_TAB_ESTADOS)).ToList();
                return ListaOrdenDia;
            }
        }

        public Task<IEnumerable<OrdenDia>> ObtenerTodosAsync() {
            Task<IEnumerable<OrdenDia>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<OrdenDia> ObtenerPorIdAsync(int IdOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOrdenDiaBD = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.FindAsync(IdOrdenDia);
                var OrdenDiaBD = await TareaObtenerOrdenDiaBD;
                OrdenDia ModeloOrdenDia = new OrdenDia(OrdenDiaBD, OrdenDiaBD.SGJD_ACT_TAB_SESIONES, OrdenDiaBD.SGJD_ADM_TAB_ESTADOS, OrdenDiaBD.SGJD_ADM_TAB_SECCIONES);
                return ModeloOrdenDia;
            }
        }

        public async Task<OrdenDia> ObtenerPorIdSesionAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOrdenDiaBD = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.Where(O => O.LLF_IdSesion == IdSesion).FirstOrDefaultAsync();
                var OrdenDiaBD = await TareaObtenerOrdenDiaBD;
                OrdenDia ModeloOrdenDia = new OrdenDia(OrdenDiaBD, OrdenDiaBD.SGJD_ACT_TAB_SESIONES, OrdenDiaBD.SGJD_ADM_TAB_ESTADOS, OrdenDiaBD.SGJD_ADM_TAB_SECCIONES);
                return ModeloOrdenDia;
            }
        }

        public async Task<OrdenDia> ObtenerAprobadaPorIdSesionAsync(int IdSesion) {
            var TareaObtenerOrdenDia = ObtenerPorIdSesionAsync(IdSesion);
            OrdenDia OrdenDiaModel = await TareaObtenerOrdenDia;

            var TareaObtenerOrdenDiaAprobada = ObtenerAprobadaPorIdAsync(OrdenDiaModel.Id);
            return await TareaObtenerOrdenDiaAprobada;
        }

        public async Task<OrdenDia> ObtenerAprobadaPorIdAsync(int IdOrdenDia) {
            // buscar el estado del Orden del Dia aprobado
            var TareaBuscarEstadoOrdenDia = Estado.ObtenerPorCodigoAsync("OD-APROB");
            Estado EstadoOrdenDiaAprobado = await TareaBuscarEstadoOrdenDia;

            using (var ContextoBD = SGJDDBContext.Create()) {
                var OrdenDiaConSecciones = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA
                                    .Include(od => od.SGJD_ADM_TAB_SECCIONES)
                                    .Where(od => od.LLP_Id == IdOrdenDia && od.LLF_IdEstado == EstadoOrdenDiaAprobado.Id)
                                    .FirstOrDefault();

                if (OrdenDiaConSecciones != null) {
                    // Buscar el estado del Tema aprobado
                    var TareaBuscarEstadoTemaAprobado = Estado.ObtenerPorCodigoAsync("TEM-APROB");
                    Estado EstadoTemaAprobado = await TareaBuscarEstadoTemaAprobado;

                    // Buscar el estado del Tema Ejecutado
                    var TareaBuscarEstadoTemaEjecutado = Estado.ObtenerPorCodigoAsync("TEM-EJEC");
                    Estado EstadoTemaEjecutado = await TareaBuscarEstadoTemaEjecutado;

                    var TemasAprobados = ContextoBD.SGJD_ACT_TAB_TEMAS
                        .Where(t => t.LLF_IdOrdenDia == IdOrdenDia && (t.LLF_IdEstado == EstadoTemaAprobado.Id || t.LLF_IdEstado == EstadoTemaEjecutado.Id))
                        .ToList();

                    // Recorrer cada seccion del orden del dia y quitarle los temas (Vaciarlo) y agregarle los temas 
                    // aprobados si es de la seccion correspondiente
                    foreach (var SeccionBD in OrdenDiaConSecciones.SGJD_ADM_TAB_SECCIONES) {
                        SeccionBD.SGJD_ACT_TAB_TEMAS.Clear();

                        foreach (var TemaBD in TemasAprobados) {
                            if (SeccionBD.LLP_Id == TemaBD.LLF_IdSeccion) {
                                SeccionBD.SGJD_ACT_TAB_TEMAS.Add(TemaBD);
                            }
                        }
                    }

                    OrdenDia ModeloOrdenDia = new OrdenDia(OrdenDiaConSecciones, OrdenDiaConSecciones.SGJD_ACT_TAB_SESIONES, OrdenDiaConSecciones.SGJD_ADM_TAB_ESTADOS, OrdenDiaConSecciones.SGJD_ADM_TAB_SECCIONES);
                    return ModeloOrdenDia;
                }
                else {
                    return new OrdenDia();
                }
            }
        }

        public async Task<bool> ActualizarAsync(OrdenDia ModeloOrdenDia) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOrdenDia = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.FindAsync(ModeloOrdenDia.Id);
                var OrdenDiaBD = await TareaObtenerOrdenDia;

                OrdenDiaBD.Encabezado = ModeloOrdenDia.Encabezado;

                ContextoBD.Entry(OrdenDiaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Orden del Día");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        #region Seccion
        public async Task<bool> AgregarSeccionAsync(int IdOrdenDia, int IdSeccion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOrdenDia = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.FindAsync(IdOrdenDia);
                var OrdenDiaBD = await TareaObtenerOrdenDia;

                var TareaObtenerSeccion = ContextoBD.SGJD_ADM_TAB_SECCIONES.FindAsync(IdSeccion);
                var SeccionBD = await TareaObtenerSeccion;

                OrdenDiaBD.SGJD_ADM_TAB_SECCIONES.Add(SeccionBD);

                ContextoBD.Entry(OrdenDiaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar sección en Orden del Día", ValorAntiguo: "", ValorNuevo: "IdOrdenDia: " + IdOrdenDia.ToString() + ", IdSeccion: " + IdSeccion.ToString(), SeccionSistema: "Orden del Día");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarSeccionAsync(int IdOrdenDia, int IdSeccion) {
            // Obtener los temas que tiene el orden del día en la sección
            var TareaObtenerTemasPorIdSeccionIdOrdenDia = Tema.ObtenerTodosPorIdSeccionIdOrdenDia(Convert.ToInt32(IdSeccion), Convert.ToInt32(IdOrdenDia));
            IEnumerable<Tema> ListaTemas = await TareaObtenerTemasPorIdSeccionIdOrdenDia;

            // Obtener el tipo de objeto correspondiente a Temas
            var TareaObtenerTipoObjetoTema = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TEMAS");
            TipoObjeto TipoObjetoTema = await TareaObtenerTipoObjetoTema;

            // Variables de control
            bool AdjuntosEliminados = true;
            bool TemasEliminados = true;
            int RegistrosAfectados = 0;

            // Si existen temas en la sección, recorrer lista de temas y ejecutar el mpetodo [EliminarTema] de [TemaLogic]
            if (ListaTemas.Any()) {
                foreach (Tema ObjTema in ListaTemas) {
                    // Obtener todos los adjuntos del tema
                    var TareaObtenerArchivosAdjuntosTema = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(ObjTema.Id, TipoObjetoTema.Id);
                    IEnumerable<ArchivoAdjunto> ListaAdjuntosTema = await TareaObtenerArchivosAdjuntosTema;

                    // Si existen adjuntos para el tema, recorrer la lista de adjuntos del tema y eliminarlos uno por uno
                    if (ListaAdjuntosTema.Any()) {
                        foreach (ArchivoAdjunto AdjuntoTema in ListaAdjuntosTema) {
                            // Ejecutar borrado de adjunto
                            var TareaEliminarAdjunto = ArchivoAdjunto.EliminarAsync(AdjuntoTema);
                            bool AdjuntoEliminado = await TareaEliminarAdjunto;

                            if (AdjuntoEliminado == false) {
                                AdjuntosEliminados = false;
                                break;
                            }
                            else {
                                AdjuntosEliminados = true;
                            }
                        }
                    }

                    // Verificar si hubo exito al eliminar los archivos adjuntos del tema para proceder a eliminar el tema
                    if (AdjuntosEliminados == true) {
                        // Ejecutar borrado del tema
                        var TareaEliminarTemas = Tema.EliminarAsync(ObjTema.Id);
                        bool TemaEliminado = await TareaEliminarTemas;

                        if (TemaEliminado == false) {
                            TemasEliminados = false;
                            break;
                        }
                        else {
                            TemasEliminados = true;
                        }
                    }
                }
            }

            // Verificar si hubo exito al eliminar todos los temas para proceder a eliminar la sección del orden del día
            if (TemasEliminados == true) {
                using (var ContextoBD = SGJDDBContext.Create()) {
                    var TareaObtenerOrdenDia = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.FindAsync(IdOrdenDia);
                    var OrdenDiaBD = await TareaObtenerOrdenDia;

                    var SeccionDeOrdenDiaBD = OrdenDiaBD.SGJD_ADM_TAB_SECCIONES.Where(S => S.LLP_Id == IdSeccion).FirstOrDefault();

                    OrdenDiaBD.SGJD_ADM_TAB_SECCIONES.Remove(SeccionDeOrdenDiaBD);
                    ContextoBD.Entry(OrdenDiaBD).State = EntityState.Modified;
                    RegistrosAfectados = await ContextoBD.SaveChangesAsync();
                }
            }

            // Verificar si hubo exito al eliminar a sección del orden del día
            if (RegistrosAfectados >= 1) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar Sección de Orden del Día", ValorAntiguo: "", ValorNuevo: "IdOrdenDia: " + IdOrdenDia.ToString() + ", IdSeccion: " + IdSeccion.ToString(), SeccionSistema: "Orden del Día");
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        public async Task<bool> EnviarConvocatoriaDeSesion(int IdOrdenDia, int IdAviso) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                //traer el aviso por id
                var tareaConvocarSesionBD = ContextoBD.SGJD_ADM_TAB_AVISOS.FindAsync(IdAviso);
                var convocarSesionBD = await tareaConvocarSesionBD;
            }
            return true;
        }

        public async Task<bool> EnviarVistoBuenoAsync(int IdOrdenDia) {
            // Obtener el orden día por id
            var TareaObtenerOrdenDia = ObtenerPorIdAsync(Convert.ToInt32(IdOrdenDia));
            OrdenDia ModeloOrdenDia = await TareaObtenerOrdenDia;

            // Cambiar estado del orden del día de Borrador a [Visto Bueno]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloOrdenDia, "OD-VB");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloOrdenDia.ObtenerInformacion(), ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Órden del Día");

            return EstadoActualizado;
        }

        public async Task<bool> EnviarConvocarAsync(int IdOrdenDia) {
            // Obtener el orden día por id
            var TareaObtenerOrdenDia = ObtenerPorIdAsync(Convert.ToInt32(IdOrdenDia));
            OrdenDia ModeloOrdenDia = await TareaObtenerOrdenDia;

            // Cambiar estado del orden del día de visto bueno a concado
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloOrdenDia, "OD-CONV");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloOrdenDia.ObtenerInformacion(), ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Orden del Día");

            return EstadoActualizado;
        }

        public async Task<bool> AprobarAsync(int IdOrdenDia) {
            // Obtener el orden día por id
            var TareaObtenerOrdenDia = ObtenerPorIdAsync(Convert.ToInt32(IdOrdenDia));
            OrdenDia ModeloOrdenDia = await TareaObtenerOrdenDia;

            // Cambiar estado del orden del día de visto bueno a concado
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloOrdenDia, "OD-APROB");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Aprobar", ValorAntiguo: ModeloOrdenDia.ObtenerInformacion(), ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Orden del Día");

            return EstadoActualizado;
        }

        /// <summary>
        /// Cambiar el estado del orden del día según el especificado en el código de estado
        /// </summary>
        private async Task<bool> CambiarEstadoAsync(OrdenDia ModeloOrdenDia, string CodigoEstado) {
            // Obtener el estado correspondiente al codigo especificado
            var TareaTraerEstado = Estado.ObtenerPorCodigoAsync(CodigoEstado);
            Estado ModeloEstado = await TareaTraerEstado;

            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerOrdenDia = ContextoBD.SGJD_ACT_TAB_ORDENES_DIA.FindAsync(ModeloOrdenDia.Id);
                var OrdenDiaBD = await tareaObtenerOrdenDia;

                // Establecer el nuevo estado
                OrdenDiaBD.LLF_IdEstado = ModeloEstado.Id;

                // Actualizar en la base de datos
                ContextoBD.Entry(OrdenDiaBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloOrdenDia.ObtenerInformacion(), SeccionSistema: "Orden del Día");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}