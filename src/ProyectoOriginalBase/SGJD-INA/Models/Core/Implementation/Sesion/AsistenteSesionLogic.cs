using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad AsistenteSesion  y OtroAsistenteSesion
    /// </summary>
    public class AsistenteSesionLogic : IAsistenteSesionLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public AsistenteSesionLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }

        // Asistencia de miembros de junta directiva 
        // Métodos públicos
        public async Task<AsistenteSesion> AgregarAsistenteAsync(AsistenteSesion ModeloAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var AsistenteSesionBD = ModeloAsistente.BD();
                ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Add(AsistenteSesionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    ModeloAsistente.Id = AsistenteSesionBD.LLP_Id;
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloAsistente.ObtenerInformacion(), SeccionSistema: "Asistente a sesión");
                }

                return ModeloAsistente;
            }
        }

        public async Task<bool> ActualizarAsistenteAsync(AsistenteSesion ModeloAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistenteBD = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.FindAsync(ModeloAsistente.Id);
                var AsistenteBD = await TareaObtenerAsistenteBD;

                if (AsistenteBD == null) {
                    return false;
                }

                // Actualizar campos necesarios
                AsistenteBD.Tipo = ModeloAsistente.TipoAsistencia;

                // Actualizar base de datos
                ContextoBD.Entry(AsistenteBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloAsistente.ObtenerInformacion(), SeccionSistema: "AsistenteSesion");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarHoraAsync(AsistenteSesion ModeloAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistente = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.FindAsync(ModeloAsistente.Id);
                var AsistenteBD = await TareaObtenerAsistente;

                // Actualizar la hora de entrada y salida del asistente
                AsistenteBD.HoraInicio = ModeloAsistente.HoraInicio;
                AsistenteBD.HoraFin = ModeloAsistente.HoraFin;

                // Guardar cambios en base dedatos
                ContextoBD.Entry(AsistenteBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloAsistente.ObtenerInformacion(), SeccionSistema: "Asistente a sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<AsistenteSesion> ObtenerPorIdAsync(int IdAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistenciaBD = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.FindAsync(IdAsistente);
                var AsistenciaBD = await TareaObtenerAsistenciaBD;
                AsistenteSesion ModeloAsistente = new AsistenteSesion(AsistenciaBD, AsistenciaBD.SGJD_ACT_TAB_SESIONES, AsistenciaBD.SGJD_ADM_TAB_USUARIOS);
                return ModeloAsistente;
            }
        }

        public async Task<AsistenteSesion> ObtenerPorIdUsuarioIdSesionAsync(int IdSesion, string IdUsuario) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistenciaBD = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Where(A => A.LLF_IdUsuario == IdUsuario && A.LLF_IdSesion == IdSesion).FirstOrDefaultAsync();
                var AsistenciaBD = await TareaObtenerAsistenciaBD;
                AsistenteSesion ModeloAsistente = new AsistenteSesion(AsistenciaBD, AsistenciaBD.SGJD_ACT_TAB_SESIONES, AsistenciaBD.SGJD_ADM_TAB_USUARIOS);
                return ModeloAsistente;
            }
        }

        public async Task<IEnumerable<AsistenteSesion>> ObtenerTodosPorIdSesionAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistenciasBD = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Where(A => A.LLF_IdSesion == IdSesion).ToListAsync();
                var ListaAsistenciasBD = await TareaObtenerAsistenciasBD;
                IEnumerable<AsistenteSesion> ListaAsistentes = ListaAsistenciasBD.Select(a => new AsistenteSesion(a, a.SGJD_ACT_TAB_SESIONES, a.SGJD_ADM_TAB_USUARIOS)).ToList();
                return ListaAsistentes;
            }
        }

        public async Task<IEnumerable<AsistenteSesion>> ObtenerTodosPresentesPorIdSesionAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAsistenciasBD = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Where(A => A.LLF_IdSesion == IdSesion && A.Tipo.Equals("Presente")).ToListAsync();
                var ListaAsistenciasBD = await TareaObtenerAsistenciasBD;
                IEnumerable<AsistenteSesion> ListaAsistentes = ListaAsistenciasBD.Select(a => new AsistenteSesion(a, a.SGJD_ACT_TAB_SESIONES, a.SGJD_ADM_TAB_USUARIOS)).ToList();
                return ListaAsistentes;
            }
        }

        public IEnumerable<SelectListItem> ObtenerTiposAsistenciaParaSelect() {
            List<SelectListItem> ListaAsistencia = new List<SelectListItem>();
            ListaAsistencia.Add(new SelectListItem { Text = "No presente", Value = "No presente" });
            ListaAsistencia.Add(new SelectListItem { Text = "Presente", Value = "Presente" });
            return ListaAsistencia;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTiposAsistenciaParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTiposAsistenciaParaSelect());
            return Tarea;
        }

        // Otro asistentes a sesión 
        public async Task<bool> AgregarOtroAsistenteAsync(OtroAsistenteSesion ModeloOtroAsistenteSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var OtroAsistenteSesionBD = ModeloOtroAsistenteSesion.BD();
                ContextoBD.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION.Add(OtroAsistenteSesionBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloOtroAsistenteSesion.ObtenerInformacion(), SeccionSistema: "Otro asistente sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarOtroAsistenteAsync(OtroAsistenteSesion ModeloOtroAsistente) {
            var OtroAsistenteBD = ModeloOtroAsistente.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(OtroAsistenteBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloOtroAsistente.ObtenerInformacion(), SeccionSistema: "Otro asistente sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarOtroAsistenteAsync(int IdOtroAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOtroAsistente = ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.FindAsync(IdOtroAsistente);
                var OtroAsistenteBD = await TareaObtenerOtroAsistente;
                ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Attach(OtroAsistenteBD);
                ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Remove(OtroAsistenteBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdOtroAsistente.ToString(), ValorNuevo: "", SeccionSistema: "Otro asistente sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<OtroAsistenteSesion>> ObtenerTodosOtroAsistentePorIdSesionAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOtroAsistenteBD = ContextoBD.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION.Where(A => A.LLF_IdSesion == IdSesion).ToListAsync();
                var ListaOtroAsistenteBD = await TareaObtenerOtroAsistenteBD;
                IEnumerable<OtroAsistenteSesion> ListaOtroAsistentes = ListaOtroAsistenteBD.Select(a => new OtroAsistenteSesion(a, a.SGJD_ACT_TAB_SESIONES, a.SGJD_ADM_TAB_TIPOS_USUARIO)).ToList();
                return ListaOtroAsistentes;
            }
        }

        public async Task<OtroAsistenteSesion> ObtenerOtroAsistentePorIdAsync(int IdOtroAsistente) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOtroAsistenteBD = ContextoBD.SGJD_ACT_TAB_OTROS_ASISTENTES_SESION.FindAsync(IdOtroAsistente);
                var OtroAsistenteBD = await TareaObtenerOtroAsistenteBD;
                OtroAsistenteSesion ModeloOtroAsistente = new OtroAsistenteSesion(OtroAsistenteBD);
                return ModeloOtroAsistente;
            }
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodosPorIdSesionParaSelectAsync(int IdSesion) {
            var TareaObtenerAsistentesSesion = ObtenerTodosPorIdSesionAsync(IdSesion);
            var TareaObtenerOtrosAsistentesSesion = ObtenerTodosOtroAsistentePorIdSesionAsync(IdSesion);

            IEnumerable<AsistenteSesion> ListaAsistentes = await TareaObtenerAsistentesSesion;
            IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes = await TareaObtenerOtrosAsistentesSesion;

            List<SelectListItem> ListaAsistentesSesion = new List<SelectListItem>();
            foreach (AsistenteSesion Asistente in ListaAsistentes) {
                ListaAsistentesSesion.Add(new SelectListItem() { Value = Asistente.IdUsuario, Text = Asistente.Usuario.Nombre });
            }

            foreach (OtroAsistenteSesion OtroAsistente in ListaOtrosAsistentes) {
                ListaAsistentesSesion.Add(new SelectListItem() { Value = OtroAsistente.Id.ToString(), Text = OtroAsistente.Nombre });
            }

            return ListaAsistentesSesion;
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerTodosPresentesPorIdSesionParaSelectAsync(int IdSesion) {
            var TareaObtenerAsistentesSesion = ObtenerTodosPresentesPorIdSesionAsync(IdSesion);
            var TareaObtenerOtrosAsistentesSesion = ObtenerTodosOtroAsistentePorIdSesionAsync(IdSesion);

            IEnumerable<AsistenteSesion> ListaAsistentes = await TareaObtenerAsistentesSesion;
            IEnumerable<OtroAsistenteSesion> ListaOtrosAsistentes = await TareaObtenerOtrosAsistentesSesion;

            List<SelectListItem> ListaAsistentesSesion = new List<SelectListItem>();
            foreach (AsistenteSesion Asistente in ListaAsistentes) {
                ListaAsistentesSesion.Add(new SelectListItem() { Value = Asistente.IdUsuario, Text = Asistente.Usuario.Nombre });
            }

            foreach (OtroAsistenteSesion OtroAsistente in ListaOtrosAsistentes) {
                ListaAsistentesSesion.Add(new SelectListItem() { Value = OtroAsistente.Id.ToString(), Text = OtroAsistente.Nombre });
            }

            return ListaAsistentesSesion;
        }
    }
}