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
    /// Lógica para realizar operaciones sobre la entidad sesion
    /// </summary>
    public class SesionLogic : ISesionLogic {
        // constructor y dependencias 
        private readonly IBitacoraLogic Bitacora;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IEstadoLogic Estado;
        private readonly ITipoSesionLogic TipoSesion;
        private readonly IMiembrosJDLogic Miembros;
        private readonly IOrdenDiaLogic OrdenDia;

        public SesionLogic(IBitacoraLogic Bitacora, ITipoObjetoLogic TipoObjeto, IEstadoLogic Estado, IMiembrosJDLogic Miembros, IOrdenDiaLogic OrdenDia, ITipoSesionLogic TipoSesion) {
            this.Bitacora = Bitacora;
            this.TipoObjeto = TipoObjeto;
            this.Estado = Estado;
            this.Miembros = Miembros;
            this.OrdenDia = OrdenDia;
            this.TipoSesion = TipoSesion;
        }

        // Métodos públicos
        public async Task<bool> AgregarAsync(Sesion ModeloSesion) {
            var TareaObtenerObjetoSesion = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_SESIONES");
            var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("SES-PEND");
            var TareaObtenerTipoSesion = TipoSesion.ObtenerPorIdAsync(ModeloSesion.IdTipoSesion);

            TipoObjeto ObjetoSesion = await TareaObtenerObjetoSesion;
            Estado EstadoSesion = await TareaObtenerEstado;
            TipoSesion ObjetoTipoSesion = await TareaObtenerTipoSesion;

            ModeloSesion.Numero = ObjetoSesion.Consecutivo;
            ModeloSesion.IdEstado = EstadoSesion.Id;
            ModeloSesion.NombreObjeto = ObjetoSesion.NombreTabla;

            int RegistrosAfectados = 0;
            bool ConsecutivoAumentado = false;
            bool ODAgregada = false;
            bool AsistentesAgregados = false;

            // Guardar sesión en base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var SesionBD = ModeloSesion.BD();
                ContextoBD.SGJD_ACT_TAB_SESIONES.Add(SesionBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Actualizar Id del modelo al asignado en la entidad de EF
                ModeloSesion.Id = SesionBD.LLP_Id;
            }

            if (RegistrosAfectados >= 1) {
                // Aumentar el consecutivo de número de sesión en Tipos de Objeto
                var TareaAumentarConsecutivoSesion = TipoObjeto.AumentarConsecutivoAsync(ModeloSesion.NombreObjeto);
                ConsecutivoAumentado = await TareaAumentarConsecutivoSesion;
            }

            if (ConsecutivoAumentado == true) {
                // Agregar Orden del Día para la Sesión recien creada
                var TareaObtenerEstadoOD = Estado.ObtenerPorCodigoAsync("OD-BORR");
                var EstadoOD = await TareaObtenerEstadoOD;
                OrdenDia ModeloOrdenDia = new OrdenDia(ModeloSesion.Id, EstadoOD.Id);

                // Guardar orden del día en base de datos
                var TareaAgregarOrdenDia = OrdenDia.AgregarAsync(ModeloOrdenDia);
                ODAgregada = await TareaAgregarOrdenDia;
            }

            if (ODAgregada == true) {
                // Registrar los miembros de junta directiva como asistentes a sesion
                var TareaAgregarAsistentes = RegistrarAsistentesAsync(ModeloSesion.Id);
                AsistentesAgregados = await TareaAgregarAsistentes;
            }

            if (RegistrosAfectados >= 0 && ConsecutivoAumentado == true && ODAgregada == true && AsistentesAgregados == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloSesion.ObtenerInformacion(), SeccionSistema: "Sesión");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<bool> ActualizarAsync(Sesion ModeloSesion) {
            var SesionBD = ModeloSesion.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(SesionBD).State = EntityState.Modified;
                int Registrosafectados = await ContextoBD.SaveChangesAsync();

                if (Registrosafectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloSesion.ObtenerInformacion(), SeccionSistema: "sesión");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdSesion) {
            // Obtener la sesión por id
            var TareaObtenerSesion = ObtenerPorIdAsync(Convert.ToInt32(IdSesion));
            Sesion ModeloSesion = await TareaObtenerSesion;
            
            int RegistrosAfectados = 0;
            bool BajarConsecutivo = false;

            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSesionBD = ContextoBD.SGJD_ACT_TAB_SESIONES.FindAsync(IdSesion);
                var SesionBD = await TareaObtenerSesionBD;
                ContextoBD.SGJD_ACT_TAB_SESIONES.Attach(SesionBD);
                ContextoBD.SGJD_ACT_TAB_SESIONES.Remove(SesionBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();                
            }

            if (RegistrosAfectados >= 1) {
                // Bajar el consecutivo de número de sesión en Tipos de Objeto
                var TareaBajarConsecutivoSesion = TipoObjeto.BajarConsecutivoAsync(ModeloSesion.NombreObjeto);
                BajarConsecutivo = await TareaBajarConsecutivoSesion;              
            }

            if (RegistrosAfectados >= 1 && BajarConsecutivo == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: "", ValorNuevo: IdSesion.ToString(), SeccionSistema: "Seccion");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<bool> RealizadaAsync(int IdSesion) {
            // Obtener la sesión por id
            var TareaObtenerSesion = ObtenerPorIdAsync(Convert.ToInt32(IdSesion));
            Sesion ModeloSesion = await TareaObtenerSesion;

            // Cambiar estado del orden del día de visto bueno a concado
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloSesion, "SES-EJEC");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Realizada", ValorAntiguo: ModeloSesion.ObtenerInformacion(), ValorNuevo: ModeloSesion.ObtenerInformacion(), SeccionSistema: "Sesión de JD");

            return EstadoActualizado;
        }

        public IEnumerable<Sesion> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener lista de sesiones utilizando linq
                var ObtenerSesionesBD = from Sesiones in ContextoBD.SGJD_ACT_TAB_SESIONES
                                        select new {
                                            Sesiones.LLP_Id,
                                            Sesiones.Numero,
                                            Sesiones.FechaHora,
                                            TipoSesion = Sesiones.SGJD_ADM_TAB_TIPOS_SESION,
                                            EstadoSesion = Sesiones.SGJD_ADM_TAB_ESTADOS,
                                            OrdenDia = Sesiones.SGJD_ACT_TAB_ORDENES_DIA.FirstOrDefault(),
                                            Acta = Sesiones.SGJD_ACT_TAB_ACTAS.FirstOrDefault(),
                                        };

                // Crear lista de Sesiones especificando los campos que se van a llenar
                IEnumerable<Sesion> ListaSesiones = ObtenerSesionesBD.AsEnumerable().Select(s => new Sesion {
                    Id = s.LLP_Id,
                    Numero = s.Numero,
                    FechaHora = s.FechaHora,
                    TipoSesion = new TipoSesion(s.TipoSesion),
                    Estado = new Estado(s.EstadoSesion),
                    OrdenDia = new OrdenDia(s.OrdenDia),
                    Acta = s.Acta != null ? new Acta(s.Acta) : new Acta()
                }).ToList();

                //var TareaObtenerSesionesBD = ContextoBD.SGJD_ACT_TAB_SESIONES.OrderByDescending(s => s.LLP_Id).ToList();
                //IEnumerable<Sesion> ListaSesiones = TareaObtenerSesionesBD.Select(SesionBD => new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS, SesionBD.SGJD_ACT_TAB_ORDENES_DIA.FirstOrDefault(), SesionBD.SGJD_ACT_TAB_ACTAS.FirstOrDefault())).ToList();
                return ListaSesiones;
            }
        }

        public Task<IEnumerable<Sesion>> ObtenerTodosAsync() {
            Task<IEnumerable<Sesion>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<Sesion> ObtenerPorIdAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerSesionBD = ContextoBD.SGJD_ACT_TAB_SESIONES.FindAsync(IdSesion);
                var SesionBD = await TareaObtenerSesionBD;
                var ModeloSesion = new Sesion(SesionBD, SesionBD.SGJD_ADM_TAB_TIPOS_SESION, SesionBD.SGJD_ADM_TAB_USUARIOS, SesionBD.SGJD_ADM_TAB_ESTADOS);
                return ModeloSesion;
            }
        }

        public async Task<IEnumerable<SelectListItem>> ObtenerSesionPosteriorParaSelectAsync(int IdSesion) {
            var TareaObtenerSesion = ObtenerTodosAsync();
            var TareaObtenerTipoSesion = TipoSesion.ObtenerTodosAsync();

            IEnumerable<Sesion> ListaSesion = await TareaObtenerSesion;
            IEnumerable<TipoSesion> ListaTipoSesion = await TareaObtenerTipoSesion;

            List<SelectListItem> ListaSelectSesion = new List<SelectListItem>();
            foreach (Sesion Sesion in ListaSesion) {
                if (Sesion.Id > IdSesion) {
                    ListaSelectSesion.Add(new SelectListItem() { Value = Sesion.Id.ToString(), Text = Sesion.TipoSesion.Descripcion + " " + Sesion.Numero.ToString() + "-" + Sesion.FechaHora.Year.ToString() });
                }
            }
            return ListaSelectSesion;
        }

        public async Task<bool> VerificarFechaAsync(DateTime Fecha) {
            // Obtener todas las seiones
            var TareaObtenerSesiones = ObtenerTodosAsync();
            IEnumerable<Sesion> ListaSesiones = await TareaObtenerSesiones;

            // Recorrer la lista de sesiones para verificar si existe una sesión con fecha mayor a la especificada
            // En caso de haber una fecha mayor a la especificada, se devuelve false para indicar que la fecha no es válida.
            foreach (var Sesion in ListaSesiones) {
                if (Sesion.FechaHora > Fecha) {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Sesion> ObtenerSesionesGrafico() {
            //using (var ContextoBD = SGJDDBContext.Create()) {
            //    var ListaSesionesBD = ContextoBD.SGJD_ACT_PRO_OBTENER_SESIONES_PARA_GRAFICA().ToList();

            //    var ListaSesiones = new List<Sesion>();
            //    foreach (var SesionBD in ListaSesionesBD) {
            //        var ModeloSesion = ConvertirBD_Modelo(SesionBD);
            //        ListaSesiones.Add(ModeloSesion);
            //    }

            //    return ListaSesiones;
            //}

            return new List<Sesion>();
        }

        // Métodos privados
        /// <summary>
        /// Registrar los miembros de la junta directiva como asistentes a la sesion
        /// </summary>
        private async Task<bool> RegistrarAsistentesAsync(int IdSesion) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener los miembros de la Junta Directiva
                var TareaObtenerMiembros = Miembros.ObtenerTodos();
                IEnumerable<MiembroJD> ListaMiembrosJD = await TareaObtenerMiembros;

                foreach (MiembroJD Miembro in ListaMiembrosJD) {
                    AsistenteSesion Asistente = new AsistenteSesion(IdSesion, Miembro.IdUsuario);
                    var AsistenteBD = Asistente.BD();
                    ContextoBD.SGJD_ACT_TAB_ASISTENTES_SESION.Add(AsistenteBD);
                }

                await ContextoBD.SaveChangesAsync();
            }

            return true;
        }

        /// <summary>
        /// Cambiar el estado del orden del día según el especificado en el código de estado
        /// </summary>
        private async Task<bool> CambiarEstadoAsync(Sesion ModeloSesion, string CodigoEstado) {
            // Obtener el estado correspondiente al codigo especificado
            var TareaTraerEstado = Estado.ObtenerPorCodigoAsync(CodigoEstado);
            Estado ModeloEstado = await TareaTraerEstado;

            //Se establece el nuevo estado
            ModeloSesion.IdEstado = ModeloEstado.Id;

            // Actualizar en la base de datos
            var TareaActualizarSesion = ActualizarAsync(ModeloSesion);
            bool SesionActualizada = await TareaActualizarSesion;

            return SesionActualizada;
        }       
    }
}