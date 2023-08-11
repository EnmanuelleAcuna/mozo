using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DTO;
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
    /// Lógica para realizar operaciones sobre la entidad Acuerdo
    /// </summary>
    public class VotoLogic : IVotoLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public VotoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }

        // Métodos públicos
        public async Task<bool> ActualizarVotoAsync(Votacion ModeloVoto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerVotoBD = ContextoBD.SGJD_ACU_TAB_VOTACIONES_ACUERDO.FindAsync(ModeloVoto.Id);
                var VotoBD = await TareaObtenerVotoBD;

                // Actualizar campo
                VotoBD.TipoVotacion = ModeloVoto.TipoVotacion;

                // Actualizar en base de datos
                ContextoBD.Entry(VotoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar voto", ValorAntiguo: "", ValorNuevo: ModeloVoto.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Votacion>> ObtenerTodosPorIdAcuerdoAsync(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerVotacionesDelAcuerdoBD = ContextoBD.SGJD_ACU_TAB_VOTACIONES_ACUERDO.Where(v => v.LLF_IdAcuerdo == IdAcuerdo).ToListAsync();
                var VotacionesDelAcuerdoBD = await TareaObtenerVotacionesDelAcuerdoBD;
                IEnumerable<Votacion> Votaciones = VotacionesDelAcuerdoBD.Select(v => new Votacion(v, v.SGJD_ACT_TAB_ASISTENTES_SESION, v.SGJD_ACU_TAB_ACUERDOS)).ToList();
                return Votaciones;
            }
        }

        public IEnumerable<SelectListItem> ObtenerTipoVotacionParaSelect() {
            List<SelectListItem> ListaTipoVotacion = new List<SelectListItem>();
            ListaTipoVotacion.Add(new SelectListItem { Text = "A favor", Value = "A favor" });
            ListaTipoVotacion.Add(new SelectListItem { Text = "En contra", Value = "En contra" });
            ListaTipoVotacion.Add(new SelectListItem { Text = "No Votó", Value = "No Votó" });
            return ListaTipoVotacion;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTipoVotacionParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTipoVotacionParaSelect());
            return Tarea;
        }

        public async Task<bool> EstablecerAprobacionPorUnanimidad(int IdAcuerdo) {
            var TareObtenerVotantes = ObtenerTodosPorIdAcuerdoAsync(IdAcuerdo);
            IEnumerable<Votacion> Votaciones = await TareObtenerVotantes;

            using (var ContextoBD = SGJDDBContext.Create()) {
                foreach (Votacion Votacion in Votaciones) {
                    // Buscar un objeto votacion con el Id de la votacion en la que se esta actualmente
                    var VotacionBD = await ContextoBD.SGJD_ACU_TAB_VOTACIONES_ACUERDO.FindAsync(Votacion.Id);

                    // Actualiza campo TipoVotacion
                    VotacionBD.TipoVotacion = "A favor";

                    // Actualizar en base de datos
                    ContextoBD.Entry(VotacionBD).State = EntityState.Modified;
                    await ContextoBD.SaveChangesAsync();
                }
            }

            return true;
        }

        public IEnumerable<VotosDesidentesAcuerdosDTO> ObtenerVotosDesidentes(int IdActa) {
            IEnumerable<VotosDesidentesAcuerdosDTO> Votos = new AcuerdoDAO().ObtenerVotosDesidentes(IdActa);
            return Votos;
        }

        public Task<IEnumerable<VotosDesidentesAcuerdosDTO>> ObtenerVotosDesidentesAsync(int IdActa) {
            Task<IEnumerable<VotosDesidentesAcuerdosDTO>> Votos = Task.Run(() => ObtenerVotosDesidentes(IdActa));
            return Votos;
        }
    }
}