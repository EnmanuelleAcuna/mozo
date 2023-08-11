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
    /// <summary>
    /// Lógica para realizar operaciones sobre la entidad Articulo
    /// </summary>
    public class ArticuloLogic : IArticuloLogic {
        // Constructor y dependencias
        private readonly IBitacoraLogic Bitacora;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly ITipoArchivoLogic TipoArchivo;
        private readonly IRepositorioLogic Repositorio;
        private readonly ITemaLogic Tema;
        private readonly IEstadoLogic Estado;
        private readonly IUsuarioArticuloLogic UsuarioArticulo;
        private readonly IOrdenDiaLogic OrdenDia;
        private readonly ISeccionLogic Seccion;

        public ArticuloLogic(IBitacoraLogic Bitacora, ITipoObjetoLogic TipoObjeto, IArchivoAdjuntoLogic ArchivoAdjunto, ITipoArchivoLogic TipoArchivo, IRepositorioLogic Repositorio, ITemaLogic Tema, IEstadoLogic Estado, IUsuarioArticuloLogic UsuarioArticulo, IOrdenDiaLogic OrdenDia, ISeccionLogic Seccion) {
            this.Bitacora = Bitacora;
            this.TipoObjeto = TipoObjeto;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.TipoArchivo = TipoArchivo;
            this.Repositorio = Repositorio;
            this.Tema = Tema;
            this.Estado = Estado;
            this.UsuarioArticulo = UsuarioArticulo;
            this.OrdenDia = OrdenDia;
            this.Seccion = Seccion;
        }

        // Métodos públicos
        public async Task<int> AgregarAsync(Articulo ModeloArticulo, int IdSesion, string NombreCapitulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaAgregarTema = AgregarTemaPorArticuloAsync(ModeloArticulo, IdSesion, NombreCapitulo);
                Tema ModeloTemaAgregado = await TareaAgregarTema;

                if (ModeloTemaAgregado != null || ModeloTemaAgregado.Id > 0) {
                    ModeloArticulo.IdTema = ModeloTemaAgregado.Id;

                    var ArticuloBD = ModeloArticulo.BD();
                    ArticuloBD = ContextoBD.SGJD_ACT_TAB_ARTICULOS.Add(ArticuloBD);
                    int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                    //Asignar el id al modelo
                    ModeloArticulo.Id = ArticuloBD.LLP_Id;

                    if (RegistrosAfectados == 1) {
                        await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloArticulo.ObtenerInformacion(), SeccionSistema: "Artículo");
                        return ModeloArticulo.Id;
                    }
                    else {
                        return 0;
                    }
                }
                else {
                    return 0;
                }
            }
        }

        private async Task<Tema> AgregarTemaPorArticuloAsync(Articulo ModeloArticulo, int IdSesion, string NombreCapitulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerOrdenDia = OrdenDia.ObtenerPorIdSesionAsync(IdSesion);
                var TareaObtenerEstadoAgregadoEnSesion = Estado.ObtenerPorCodigoAsync("TEM-AES");
                var TareaObtenerSeccion = Seccion.ObtenerPorDescripcion(NombreCapitulo);

                OrdenDia OrdenDiaModelo = await TareaObtenerOrdenDia;
                Estado EstadoModelo = await TareaObtenerEstadoAgregadoEnSesion;
                Seccion SeccionModelo = await TareaObtenerSeccion;

                Tema NuevoTema = new Tema {
                    IdSeccion = SeccionModelo.Id,
                    IdOrdenDia = OrdenDiaModelo.Id,
                    IdEstado = EstadoModelo.Id,
                    Titulo = ModeloArticulo.Titulo,
                    Resumen = "Tema agregado durante la Sesión",
                    Observacion = string.Empty
                };

                var TemaBD = NuevoTema.BD();
                TemaBD = ContextoBD.SGJD_ACT_TAB_TEMAS.Add(TemaBD);
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                //Asignar el id al modelo
                NuevoTema.Id = TemaBD.LLP_Id;

                if (RegistrosAfectados == 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: NuevoTema.ObtenerInformacion(), SeccionSistema: "Tema");
                    return NuevoTema;
                }
                else {
                    return null;
                }
            }
        }

        public async Task<bool> ActualizarAsync(Articulo ModeloArticulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArticulo = ContextoBD.SGJD_ACT_TAB_ARTICULOS.FindAsync(ModeloArticulo.Id);
                var ArticuloBD = await TareaObtenerArticulo;

                // Guardar en variable para uso en bitácora
                Articulo ModeloArticuloAnterior = new Articulo(ArticuloBD);

                // Actualizar los campos necesarios 
                ArticuloBD.Contenido = ModeloArticulo.Contenido;
                ArticuloBD.Observacion = ModeloArticulo.Observacion;
                ArticuloBD.Confidencial = ModeloArticulo.Confidencial;

                // Guardar en BD
                ContextoBD.Entry(ArticuloBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Actualizar el módelo con la transcripción y observación nueva para uso en bitácora
                ModeloArticulo = new Articulo(ArticuloBD);

                if (RegistrosAfectados >= 0) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: ModeloArticuloAnterior.ObtenerInformacion(), ValorNuevo: ModeloArticulo.ObtenerInformacion(), SeccionSistema: "Artículo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdArticulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArticulo = ContextoBD.SGJD_ACT_TAB_ARTICULOS.FindAsync(IdArticulo);
                var ArticuloBD = await TareaObtenerArticulo;
                ContextoBD.SGJD_ACT_TAB_ARTICULOS.Attach(ArticuloBD);
                ContextoBD.SGJD_ACT_TAB_ARTICULOS.Remove(ArticuloBD);
                await ContextoBD.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Articulo> ObtenerPorIdAsync(int IdArticulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArticuloBD = ContextoBD.SGJD_ACT_TAB_ARTICULOS.FindAsync(IdArticulo);
                var ArticuloBD = await TareaObtenerArticuloBD;

                Articulo ModeloArticulo = new Articulo(ArticuloBD, ArticuloBD.SGJD_ACT_TAB_CAPITULOS, ArticuloBD.SGJD_ACT_TAB_TEMAS, ArticuloBD.SGJD_ACT_TAB_USUARIOS_ARTICULO);
                ArticuloBD.Contenido = HttpUtility.UrlDecode(ModeloArticulo.Contenido);

                return ModeloArticulo;
            }
        }

        public async Task<IEnumerable<Articulo>> ObtenerTodosPorIdCapituloAsync(int IdCapitulo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerArticuloBD = ContextoBD.SGJD_ACT_TAB_ARTICULOS.Where(n => n.LLF_IdCapitulo == IdCapitulo).ToListAsync();
                var ListaArticulosBD = await TareaObtenerArticuloBD;
                IEnumerable<Articulo> ListaArticulos = ListaArticulosBD.Select(ArticuloBD => new Articulo(ArticuloBD, ArticuloBD.SGJD_ACT_TAB_CAPITULOS, ArticuloBD.SGJD_ACT_TAB_TEMAS, ArticuloBD.SGJD_ACT_TAB_USUARIOS_ARTICULO)).ToList();
                return ListaArticulos;
            }
        }

        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerArchivosRelacionadosAsync(int IdArticulo) {
            // Contenedor de todos los adjuntos
            List<ArchivoAdjunto> ListaAdjuntos = new List<ArchivoAdjunto>();

            var TeareaObtenerArticulo = ObtenerPorIdAsync(IdArticulo);
            Articulo ModeloArticulo = await TeareaObtenerArticulo;

            if (ModeloArticulo != null) {
                var TareaObtenerTipoObjetoArticulo = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloArticulo.NombreObjeto);
                TipoObjeto TipoObjetoArticulo = await TareaObtenerTipoObjetoArticulo;

                // Obtener los archivos adjuntos del articulo 
                var TareaObtenerAdjuntosArticulo = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(IdArticulo, TipoObjetoArticulo.Id);
                IEnumerable<ArchivoAdjunto> ListaAdjuntosArticulo = await TareaObtenerAdjuntosArticulo;

                // Recorrer la lista y agregar los elementos al contenedor de todos los adjuntos
                foreach (ArchivoAdjunto Adjunto in ListaAdjuntosArticulo) {
                    ListaAdjuntos.Add(Adjunto);
                }

                if (ModeloArticulo.IdTema != null) {
                    var TareaObtenerTipoObjetoTema = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloArticulo.Tema.NombreObjeto);
                    TipoObjeto TipoObjetoTema = await TareaObtenerTipoObjetoTema;

                    // Obtener los archivos adjuntos del tema del articulo
                    var TareaObtenerAdjuntosTema = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(Convert.ToInt32(ModeloArticulo.IdTema), TipoObjetoTema.Id);
                    IEnumerable<ArchivoAdjunto> ListaAdjuntosTema = await TareaObtenerAdjuntosTema;

                    // Recorrer la lista y agregar los elementos al contenedor de todos los adjuntos
                    foreach (ArchivoAdjunto Adjunto in ListaAdjuntosTema) {
                        ListaAdjuntos.Add(Adjunto);
                    }
                }
            }
            return ListaAdjuntos;
        }

        public async Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo) {
            string NombreTabla = ModeloArchivoAdjunto.NombreObjeto;
            string ExtensionArchivo = Path.GetExtension(Archivo.FileName);

            var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync(NombreTabla);
            var TareaObtenerTipoArchivo = TipoArchivo.ObtenerPorExtensionAsync(ExtensionArchivo);
            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Tomos");

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

        public async Task<bool> ExcluirArticulo(Articulo Modelo) {
            var TareaEliminarUsuarioArticulo = UsuarioArticulo.EliminarTodosPorIdArticulo(Modelo.Id);
            var ListaUsuarioArticuloEliminado = await TareaEliminarUsuarioArticulo;

            if (ListaUsuarioArticuloEliminado == true) {
                if (Modelo.Tema != null) {
                    var TareaObtenerEstadoPendiente = Estado.ObtenerPorCodigoAsync("TEM-PEND");
                    Estado EstadoPendienteModelo = await TareaObtenerEstadoPendiente;

                    // Guardar en variable para uso en bitácora
                    Tema EstadoTemaAnterior = Modelo.Tema;

                    Modelo.Tema.IdEstado = EstadoPendienteModelo.Id;

                    var TareaModificarEstadoTema = Tema.ActualizarEstadoAsync(Modelo.Tema);
                    bool EstadoTemaModificado = await TareaModificarEstadoTema;

                    if (EstadoTemaModificado == true) {
                        await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: EstadoTemaAnterior.ObtenerInformacion(), ValorNuevo: Modelo.Tema.ObtenerInformacion(), SeccionSistema: "Tema");
                    }
                    else {
                        return false;
                    }
                }

                var TareaEliminarArticulo = EliminarAsync(Modelo.Id);
                bool ArticuloEliminado = await TareaEliminarArticulo;

                if (ArticuloEliminado == true) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: Modelo.ObtenerInformacion(), ValorNuevo: "", SeccionSistema: "Artículo");
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    }
}