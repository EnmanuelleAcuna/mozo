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
    /// Lógica para realizar operaciones sobre la entidad TipoObjeto
    /// </summary>
    public class TipoObjetoLogic : ITipoObjetoLogic {
        // Constructor y dependencias
        private readonly IEncabezadoPiePaginaLogic EncabezadoPiePagina;
        private readonly IBitacoraLogic Bitacora;

        public TipoObjetoLogic(IEncabezadoPiePaginaLogic EncabezadoPiePagina, IBitacoraLogic Bitacora) {
            this.EncabezadoPiePagina = EncabezadoPiePagina;
            this.Bitacora = Bitacora;
        }

        // Métodos públicos
        public async Task<bool> AgregarAsync(TipoObjeto ModeloTipoObjeto) {
            var TipoObjetoBD = ModeloTipoObjeto.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Add(TipoObjetoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTipoObjeto.ObtenerInformacion(), SeccionSistema: "TipoObjeto");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarAsync(TipoObjeto ModeloTipoObjeto) {
            var TipoObjetoBD = ModeloTipoObjeto.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(TipoObjetoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloTipoObjeto.ObtenerInformacion(), SeccionSistema: "TipoObjeto");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdTipoObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjeto = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.FindAsync(IdTipoObjeto);
                var TipoObjetoBD = await TareaObtenerTipoObjeto;
                ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Attach(TipoObjetoBD);
                ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Remove(TipoObjetoBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdTipoObjeto.ToString(), ValorNuevo: "", SeccionSistema: "TipoObjeto");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<TipoObjeto> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.ToList();
                IEnumerable<TipoObjeto> ListaTipoObjeto = ListaTipoObjetoBD.Select(to => new TipoObjeto(to)).ToList();
                return ListaTipoObjeto;
            }
        }

        public Task<IEnumerable<TipoObjeto>> ObtenerTodosAsync() {
            Task<IEnumerable<TipoObjeto>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public async Task<IEnumerable<TipoObjeto>> ObtenerTodosConParametroEdicionAsync() {
            var TareaObtenerTipoObjeto = ObtenerTodosAsync();
            IEnumerable<TipoObjeto> ListaTipoObjeto = await TareaObtenerTipoObjeto;

            List<TipoObjeto> ListaTipoObjetoConParametroEdicion = new List<TipoObjeto>();
            foreach (TipoObjeto TipoObjeto in ListaTipoObjeto) {
                if (TipoObjeto.ParametroEdicion == true) {
                    ListaTipoObjetoConParametroEdicion.Add(TipoObjeto);
                }
            }

            return ListaTipoObjetoConParametroEdicion;
        }

        public async Task<IEnumerable<TipoObjeto>> ObtenerTodosConParametroEdicionSinConfigurarAsync() {
            // Obtener los elementos TipoObjeto que poseen el atributo ParametroEdicion true
            var TareaTipoObjetoConPEBD = ObtenerTodosConParametroEdicionAsync();
            IEnumerable<TipoObjeto> ListaTipoObjetoConPEBD = await TareaTipoObjetoConPEBD;

            // Convertir lo obtenido a lista para posteriormente poder eliminarle elementos
            List<TipoObjeto> ListaTipoObjetoConPE = ListaTipoObjetoConPEBD.Select(to => to).ToList();

            return ListaTipoObjetoConPE;
        }

        public async Task<IEnumerable<TipoObjeto>> ObtenerTodosConEncabezadoPiePaginaAsync() {
            var TareaObtenerTipoObjeto = ObtenerTodosAsync();
            IEnumerable<TipoObjeto> ListaTipoObjetoModel = await TareaObtenerTipoObjeto;

            List<TipoObjeto> ListaTipoObjetoConParametroEdicion = new List<TipoObjeto>();
            foreach (TipoObjeto TipoObjeto in ListaTipoObjetoModel) {
                if (TipoObjeto.ParametroEncabezadoPie == true) {
                    ListaTipoObjetoConParametroEdicion.Add(TipoObjeto);
                }
            }

            return ListaTipoObjetoConParametroEdicion;
        }

        public async Task<IEnumerable<TipoObjeto>> ObtenerTodosConEncabezadoPiePaginaSinConfigurarAsync() {
            // Obtener los elementos TipoObjeto que poseen el atributo EncabezadoPiePagina true
            var TareaTipoObjetoConEPPBD = ObtenerTodosConEncabezadoPiePaginaAsync();
            IEnumerable<TipoObjeto> ListaTipoObjetoConEPPBD = await TareaTipoObjetoConEPPBD;

            // Convertir lista para posteriormente poder eliminarle elementos
            List<TipoObjeto> ListaTipoObjetoConEPP = ListaTipoObjetoConEPPBD.Select(to => to).ToList();

            //Obtener los elementos ParametroEncabezadoPiePagina configurados
            var TareaObtenerEncabezadoPiePagina = EncabezadoPiePagina.ObtenerTodosAsync();
            IEnumerable<EncabezadoPiePagina> ListaEncabezadoPiePagina = await TareaObtenerEncabezadoPiePagina;

            // Revisar la lista de todos los elementos y remover lo que estan en la base de datos
            foreach (EncabezadoPiePagina EncabezadoPiePagina in ListaEncabezadoPiePagina) {
                TipoObjeto TipoObjetoParaEliminar = ListaTipoObjetoConEPP.SingleOrDefault(f => f.Id == EncabezadoPiePagina.IdTipoObjeto);
                if (TipoObjetoParaEliminar != null) ListaTipoObjetoConEPP.Remove(TipoObjetoParaEliminar);
            }

            return ListaTipoObjetoConEPP;
        }

        public async Task<TipoObjeto> ObtenerPorIdAsync(int IdTipoObjeto) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.FindAsync(IdTipoObjeto);
                var TipoObjetoBD = await TareaObtenerTipoObjetoBD;
                TipoObjeto ModeloTipoObjeto = new TipoObjeto(TipoObjetoBD);
                return ModeloTipoObjeto;
            }
        }

        public async Task<TipoObjeto> ObtenerPorNombreAsync(string Nombre) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Where(to => to.Descripcion.Equals(Nombre)).FirstOrDefaultAsync();
                var TipoObjetoBD = await TareaObtenerTipoObjetoBD;
                TipoObjeto ModeloTipoObjeto = new TipoObjeto(TipoObjetoBD);
                return ModeloTipoObjeto;
            }
        }

        public async Task<TipoObjeto> ObtenerPorNombreTablaAsync(string NombreTabla) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Where(n => n.NombreTabla.Equals(NombreTabla)).FirstOrDefaultAsync();
                var TipoObjetoBD = await TareaObtenerTipoObjetoBD;

                if (TipoObjetoBD == null) {
                    return new TipoObjeto();
                }
                else {
                    TipoObjeto ModeloTipoObjeto = new TipoObjeto(TipoObjetoBD);
                    return ModeloTipoObjeto;
                }
            }
        }

        public async Task<bool> AumentarConsecutivoAsync(string NombreTabla) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Where(T => T.NombreTabla.Equals(NombreTabla)).FirstOrDefaultAsync();
                var TipoObjetoBD = await TareaObtenerTipoObjetoBD;

                // Guardar consecutivo anterior para bitácora
                var ConsecutivoAntiguo = TipoObjetoBD.Consecutivo.ToString();

                // Aumentar el consecutivo en 1
                TipoObjetoBD.Consecutivo += 1;

                // Actualizar en BD
                ContextoBD.Entry(TipoObjetoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Aumentar Consecutivo", ValorAntiguo: ConsecutivoAntiguo, ValorNuevo: TipoObjetoBD.Consecutivo.ToString(), SeccionSistema: "TipoObjeto");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> BajarConsecutivoAsync(string NombreTabla) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTipoObjetoBD = ContextoBD.SGJD_ADM_TAB_TIPOS_OBJETO.Where(T => T.NombreTabla.Equals(NombreTabla)).FirstOrDefaultAsync();
                var TipoObjetoBD = await TareaObtenerTipoObjetoBD;

                // Guardar consecutivo anterior para bitácora
                var ConsecutivoAntiguo = TipoObjetoBD.Consecutivo.ToString();

                // Bajar el consecutivo en -1
                TipoObjetoBD.Consecutivo -= 1;

                // Actualizar en BD
                ContextoBD.Entry(TipoObjetoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Bajar Consecutivo", ValorAntiguo: ConsecutivoAntiguo, ValorNuevo: TipoObjetoBD.Consecutivo.ToString(), SeccionSistema: "TipoObjeto");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<SelectListItem> ObtenerTablasBDSinConfigurarParaSelect() {
            // Obtener mediante un procedimiento almacenado la lista de tablas que existen en la base de datos
            List<TablaBDDTO> ListaTablasBD = TipoObjetoDAO.ObtenerTablasBD();

            // Obtener lista de los Tipos de Objeto que se han creado
            IEnumerable<TipoObjeto> ListaTipoObjeto = ObtenerTodos();

            // Eliminar de la lista de tablas de la BD aquellos registros que ya han sido creados y están como Tipo de Objeto
            foreach (TipoObjeto TipoObjeto in ListaTipoObjeto) {
                //Buscar en la lista de tablas un registro que tenga el mismo nombre de la tabla del tipo de objeto actual
                //Si se encuentra se elimina de la lista de tablas de BD
                var TablaParaEliminar = ListaTablasBD.SingleOrDefault(x => x.Nombre.Equals(TipoObjeto.NombreTabla));
                if (TablaParaEliminar != null) ListaTablasBD.Remove(TablaParaEliminar);
            }

            IEnumerable<SelectListItem> ListaTablasBDParaSelect = ListaTablasBD.Select(t => new SelectListItem() { Value = t.Nombre, Text = t.Nombre }).ToList();

            return ListaTablasBDParaSelect;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTablasBDSinConfigurarParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTablasBDSinConfigurarParaSelect());
            return Tarea;
        }
    }
}