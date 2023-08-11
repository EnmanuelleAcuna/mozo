using SGJD_INA.Models.Core.Interfaces;
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
    public class VistaLogic : IVistaLogic {
        #region Atributos & constructor
        private readonly IBitacoraLogic Bitacora;
        private readonly IVistaPerfilLogic VistaPerfil;

        public VistaLogic(IBitacoraLogic Bitacora, IVistaPerfilLogic VistaPerfil) {
            this.Bitacora = Bitacora;
            this.VistaPerfil = VistaPerfil;
        }
        #endregion

        #region Operaciones en base de datos
        /// <summary>
        /// Agregar un elemento Vista.
        /// </summary>
        public async Task<bool> Agregar(Vista vistaModel) {
            var vistaBD = vistaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_VISTAS.Add(vistaBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: vistaModel.ObtenerInformacion(), SeccionSistema: "Vistas");

            return true;
        }

        /// <summary>
        /// Actualizar un elemento Vista.
        /// </summary>
        public async Task<bool> Actualizar(Vista vistaModel) {
            var vistaBD = vistaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(vistaBD).State = EntityState.Modified;
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: vistaModel.ObtenerInformacion(), SeccionSistema: "Vista");

            return true;
        }

        /// <summary>
        /// Eliminar un elemento Vista.
        /// </summary>
        public async Task<bool> Eliminar(Vista vistaModel) {
            var vistaBD = vistaModel.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.SGJD_ADM_TAB_VISTAS.Attach(vistaBD);
                ContextoBD.SGJD_ADM_TAB_VISTAS.Remove(vistaBD);
                await ContextoBD.SaveChangesAsync();
            }

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: vistaModel.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Vista");

            return true;
        }

        /// <summary>
        /// Obtener todos los elementos Vista.
        /// </summary>
        public async Task<ICollection<Vista>> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerVistasBD = ContextoBD.SGJD_ADM_TAB_VISTAS.ToListAsync();
                var listaVistaBD = await tareaObtenerVistasBD;

                var listaVistaModel = new List<Vista>();
                foreach (var vistaBD in listaVistaBD) {
                    var vistaModel = new Vista(vistaBD);
                    listaVistaModel.Add(vistaModel);
                }

                return listaVistaModel;
            }
        }

        /// <summary>
        /// Obtener un elemento Vista por id
        /// </summary>
        public async Task<Vista> ObtenerPorId(int id) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerVistaBD = ContextoBD.SGJD_ADM_TAB_VISTAS.FindAsync(id);
                var vistaBD = await tareaObtenerVistaBD;

                var vistaModel = new Vista(vistaBD);

                return vistaModel;
            }
        }

        /// <summary>
        /// Obtener un elemento Vista por el valor del atributo DireccionVista
        /// </summary>
        public Vista ObtenerPorDireccionVista(string direccionVista) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var vistaBD = ContextoBD.SGJD_ADM_TAB_VISTAS.Where(n => n.DireccionVista.Equals(direccionVista)).FirstOrDefault();

                var vistaModel = new Vista(vistaBD);

                return vistaModel;
            }
        }

        /// <summary>
        /// Obtener un elemento Vista por el valor del atributo DireccionVista
        /// Función asíncrona
        /// </summary>
        public async Task<Vista> ObtenerPorDireccionVistaAsync(string direccionVista) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var tareaObtenerVistaBD = ContextoBD.SGJD_ADM_TAB_VISTAS.Where(n => n.DireccionVista.Equals(direccionVista)).FirstOrDefaultAsync();
                var vistaBD = await tareaObtenerVistaBD;

                var vistaModel = new Vista(vistaBD);

                return vistaModel;
            }
        }

        /// <summary>
        /// Obtener un elemento Vista buscando el nombre del controlador en el atributo DireccionVista
        /// En las vistas sólo existira un elemento por controlador.
        /// </summary>
        public Vista ObtenerPorNombreControlador(string nombreControlador) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var vistaBD = ContextoBD.SGJD_ADM_TAB_VISTAS.Where(n => n.DireccionVista.Contains(nombreControlador)).FirstOrDefault();

                var vistaModel = new Vista(vistaBD);

                return vistaModel;
            }
        }

        public async Task<IEnumerable<string>> ObtenerVistasProyectoSinAsignar() {
            // Obtener las vistas físicas del proyecto
            var listaVistasProyecto = ObtenerVistasProyecto();

            //Obtener las vistas registradas en la base de datos
            var tareaObtenerListaVistaBD = ObtenerTodos();
            var listaVistaBD = await tareaObtenerListaVistaBD;

            // Eliminar de la lista [listaVistasProyecto] las vistas del proyecto que ya están en la lista [listaVistaBD], 
            // esto para que las vistas ya registradas no aparezcan en la lista de selección del usuario.
            foreach (var vistaBD in listaVistaBD) {
                string vistaParaEliminar = listaVistasProyecto.SingleOrDefault(f => f == vistaBD.DireccionVista);
                if (vistaParaEliminar != null) listaVistasProyecto.Remove(vistaParaEliminar);
            }

            return listaVistasProyecto;
        }

        /// <summary>
        /// Obtener lista con todas las vistas que aún no han sido asignadas aun perfil.
        /// Esto es para que en el mantenimiento de permisos de un perfil no se muestre la vista si ha sido asignada a dicho perfil
        /// pero que si se muestre en otro perfi que no haya sido asignado
        /// </summary>
        public async Task<IEnumerable<Vista>> ObtenerVistasSinAsignar(string idPerfil) {
            // Obtener las vistas registradas en la base de datos
            var tareaObtenerListaVista = ObtenerTodos();
            var listaVistaBD = await tareaObtenerListaVista;

            // Convertir lista para posteriormente poder eliminarle elementos
            var listaVistaModel = new List<Vista>();
            foreach (var vista in listaVistaBD) {
                listaVistaModel.Add(vista);
            }

            // Obtener las vistas que han sido asignadas a un perfil
            var tareaObtenerVistasPerfil = VistaPerfil.ObtenerTodosPorIdPerfilAsync(idPerfil);
            var listaVistaDePerfilBD = await tareaObtenerVistasPerfil;

            // Guardar la lista de vistas que ha sido asignadas en una lista de vistas nueva
            var listaVistaAsignada = new List<Vista>();
            foreach (var vistaPerfil in listaVistaDePerfilBD) {
                listaVistaAsignada.Add(vistaPerfil.Vista);
            }

            // Crear lista final con las vistas del proyecto que no están en la lista de vistas de la base de datos, esto para que las vistas ya registradas
            // no aparezcan en la lista de selección del usuario.
            foreach (var vistaAsignada in listaVistaAsignada) {
                var vistaParaEliminar = listaVistaBD.SingleOrDefault(f => f.DireccionVista == vistaAsignada.DireccionVista);
                if (vistaParaEliminar != null) listaVistaModel.Remove(vistaParaEliminar);
            }
            return listaVistaModel;
        }

        /// <summary>
        /// Obtener listado con nombre de vistas físicas del proyecto
        /// </summary>
        internal ICollection<string> ObtenerVistasProyecto() {
            // Establecer la ruta donde se encuentran las vistas.
            var rutaVistas = HttpContext.Current.Request.MapPath("~" + "\\Views");

            // Obtener las carpetas que están dentro de la ruta de las vistas.
            var subCarpetas = Directory.GetDirectories(rutaVistas);

            var listaVistaProyecto = new List<string>();

            // Extraer el nombre de las vistas de la carpeta del vistas del proyecto.
            foreach (var subCarpeta in subCarpetas) {
                // Crear lista con los archivos que contienen las vistas (Los terminados en .cshtml).
                var listaVistaSubCarpeta = Directory.EnumerateFiles(subCarpeta).Where(file => file.ToLower().EndsWith("cshtml")).ToList();

                //Añadir cada vista de la subcarpeta a la lista de vistas, quitando las extensiones de la vista y agregando sólo aquellas vistas de Inicio a excepción de Administración, Acuerdos y Actas.
                foreach (var vista in listaVistaSubCarpeta) {
                    // En caso de ejecutar el sitio en desarrollo extraer a partir de la carpeta del proyecto,
                    // debido a que puede estar almacenado en una carpeta con el nombre "Actas" y la validación 
                    // no funcionará correctamente
                    var vistaTrim = vista.Substring(vista.IndexOf("Views") + 6).Replace("\\", "/");
                    if (vistaTrim.Contains("Inicio") || vistaTrim.Contains("Administracion") || vistaTrim.Contains("Actas") || vistaTrim.Contains("Acuerdos") || vistaTrim.Contains("Auditoria")) {
                        listaVistaProyecto.Add(vistaTrim.Replace(".cshtml", ""));
                    }
                }
            }
            return listaVistaProyecto;
        }
        #endregion
    }
}