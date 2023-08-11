using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class DetalleSeguimientoLogic : IDetalleSeguimientoLogic {
        #region Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;

        public DetalleSeguimientoLogic(IBitacoraLogic Bitacora) {
            this.Bitacora = Bitacora;
        }
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Agregar elemento Detalle a seguimiento, se retorna un entero 
        /// por que en vista se necesita para modificar etiquetas del DOM.
        /// </summary>
        public async Task<int> AgregarAsync(DetalleSeguimiento ModeloDetalleSeguimiento) {
            ModeloDetalleSeguimiento.FechaModificacion = DateTime.Now;
            using (var ContextoBD = SGJDDBContext.Create()) {
                var DetalleSeguimientoBD = ModeloDetalleSeguimiento.BD();
                DetalleSeguimientoBD = ContextoBD.SGJD_ACU_TAB_AVANCES.Add(DetalleSeguimientoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                ModeloDetalleSeguimiento.Id = DetalleSeguimientoBD.LLP_Id;

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloDetalleSeguimiento.ObtenerInformacion(), SeccionSistema: "Detalle Seguimiento");
                    return ModeloDetalleSeguimiento.Id;
                }
                else {
                    // Si no se agregó el Tema a la BD, devolver 0 para que en la vista al validar el id del nuevo tema muestre que ocurrió un error
                    return 0;
                }
            }
        }

        /// <summary>
        /// Actualizar un detalle de seguimiento.
        /// </summary>
        public async Task<bool> ActualizarAsync(DetalleSeguimiento ModeloDetalleSeguimiento) {
            ModeloDetalleSeguimiento.FechaModificacion = DateTime.Now;
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerDetalleBD = ContextoBD.SGJD_ACU_TAB_AVANCES.FindAsync(ModeloDetalleSeguimiento.Id);
                var DetalleBD = await TareaObtenerDetalleBD;

                // Crear un modelo con la información original para guardar en bitácora
                DetalleSeguimiento ModeloDetalleOriginal = new DetalleSeguimiento(DetalleBD);

                // Modificar campos que deben ser actualizados
                DetalleBD.Descripcion = ModeloDetalleSeguimiento.Descripcion;                

                // Actualizar en BD
                ContextoBD.Entry(DetalleBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: ModeloDetalleOriginal.ObtenerInformacion(), ValorNuevo: ModeloDetalleSeguimiento.ObtenerInformacion(), SeccionSistema: "Avances de seguimiento");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdDetalle) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerDetalleBD = ContextoBD.SGJD_ACU_TAB_AVANCES.FindAsync(IdDetalle);
                var DetalleBD = await TareaObtenerDetalleBD;

                // Crear un modelo con la información original para guardar en bitácora
                DetalleSeguimiento ModeloDetalleOriginal = new DetalleSeguimiento(DetalleBD);

                // Actualizar en BD
                ContextoBD.SGJD_ACU_TAB_AVANCES.Attach(DetalleBD);
                ContextoBD.SGJD_ACU_TAB_AVANCES.Remove(DetalleBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: ModeloDetalleOriginal.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Detalle Seguimiento");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        #endregion
    }
}