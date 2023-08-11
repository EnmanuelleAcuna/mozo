using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGJD_INA.Models.Core.Implementation {
    public class CorreoAdicionalLogic : ICorreoAdicionalLogic {
        //Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public CorreoAdicionalLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }

        //Métodos públicos
        public IEnumerable<CorreoAdicional> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaCorreoAdicionalBD = ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.ToList();
                IEnumerable<CorreoAdicional> ListaCorreoAdicional = ListaCorreoAdicionalBD.Select(ca => new CorreoAdicional(ca)).ToList();
                return ListaCorreoAdicional;
            }
        }

        public Task<IEnumerable<CorreoAdicional>> ObtenerTodosAsync() {
            Task<IEnumerable<CorreoAdicional>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<SelectListItem> ObtenerTodosParaSelect() {
            IEnumerable<CorreoAdicional> ListaCorreosAdicionales = ObtenerTodos();
            List<SelectListItem> Lista = ListaCorreosAdicionales.Select(ca => new SelectListItem { Text = string.Format("{0} ({1})", ca.Nombre, ca.Correo), Value = ca.Id.ToString() }).ToList();
            return Lista;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTodosParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTodosParaSelect());
            return Tarea;
        }


        public async Task<CorreoAdicional> ObtenerPorIdAsync(int IdCorreoAdicional) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerCorreoAdicional = ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.FindAsync(IdCorreoAdicional);
                var CorreoAdicionalBD = await TareaObtenerCorreoAdicional;
                CorreoAdicional ModeloCorreoAdicional = new CorreoAdicional(CorreoAdicionalBD);
                return ModeloCorreoAdicional;
            }
        }

        public async Task<bool> AgregarAsync(CorreoAdicional ModeloCorreoAdicional) {
            var CorreoAdicionalBD = ModeloCorreoAdicional.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Add(CorreoAdicionalBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloCorreoAdicional.ObtenerInformacion(), SeccionSistema: "CorreoAdicional");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdCorreoAdicional) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerCorreoAdicional = ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.FindAsync(IdCorreoAdicional);
                var TareaCorreoAdicionalBD = await TareaObtenerCorreoAdicional;
                ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Attach(TareaCorreoAdicionalBD);
                ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Remove(TareaCorreoAdicionalBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdCorreoAdicional.ToString(), ValorNuevo: "", SeccionSistema: "CorreoAdicional");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}