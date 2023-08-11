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
    public class AcuerdoLogic : IAcuerdoLogic {
        // Constructor y dependencias
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IArchivoAdjuntoLogic ArchivoAdjunto;
        private readonly ITipoArchivoLogic TipoArchivo;
        private readonly IRepositorioLogic Repositorio;
        private readonly IEstadoLogic Estado;
        private readonly IVotoLogic Voto;
        private readonly IAvisosLogic Aviso;

        public Task TareaObtenerTipoUsuario { get; private set; }

        public AcuerdoLogic(ITipoObjetoLogic TipoObjeto, IArchivoAdjuntoLogic ArchivoAdjunto, ITipoArchivoLogic TipoArchivo, IRepositorioLogic Repositorio, IEstadoLogic Estado, IVotoLogic Voto, IAvisosLogic Aviso) {
            this.TipoObjeto = TipoObjeto;
            this.ArchivoAdjunto = ArchivoAdjunto;
            this.TipoArchivo = TipoArchivo;
            this.Repositorio = Repositorio;
            this.Estado = Estado;
            this.Voto = Voto;
            this.Aviso = Aviso;
        }

        // Métodos públicos
        public async Task<int> AgregarAsync(Acuerdo ModeloAcuerdo) {
            int RegistrosAfectados = 0;
            bool UnidadesAgregadas = false;
            bool ConsecutivoAumentado = false;
            bool VotantesAgregados = false;

            // Guardar el acuerdo en la base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var AcuerdoBD = ModeloAcuerdo.BD();
                ContextoBD.SGJD_ACU_TAB_ACUERDOS.Add(AcuerdoBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                // Asignar el id al modelo
                ModeloAcuerdo.Id = AcuerdoBD.LLP_Id;
            }

            if (RegistrosAfectados >= 0) {
                // Registrar unidades de informacion
                var TareaRegistrarUnidades = RegistrarUnidadesParaInformacionAsync(ModeloAcuerdo.Id);
                UnidadesAgregadas = await TareaRegistrarUnidades;
            }

            if (UnidadesAgregadas == true) {
                // Aumentar consecutivo de Acuerdos en Tipo de Objeto
                var TareaAumentarConsecutivo = TipoObjeto.AumentarConsecutivoAsync(ModeloAcuerdo.NombreObjeto);
                ConsecutivoAumentado = await TareaAumentarConsecutivo;
            }

            // Si es una nueva versión el consecutivo no aumenta pero si se deben registrar los votantes del acuerdo en su nueva versión
            if (ConsecutivoAumentado == true) {
                // Registrar los asistentes como votantes
                var TareaAgregarVotantes = RegistrarVotantesAsync(ModeloAcuerdo.Id);
                VotantesAgregados = await TareaAgregarVotantes;
            }

            if (RegistrosAfectados >= 0 && UnidadesAgregadas == true && ConsecutivoAumentado == true && VotantesAgregados == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                return ModeloAcuerdo.Id;
            }
            else {
                return 0;
            }
        }

        public async Task<bool> ActualizarAsync(Acuerdo ModeloAcuerdo) {
            var AcuerdoBD = ModeloAcuerdo.BD();

            using (var ContextoBD = SGJDDBContext.Create()) {
                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            int RegistrosAfectados = 0;
            bool BajarConsecutivo = false;

            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdoBD = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdoBD;
                ContextoBD.SGJD_ACU_TAB_ACUERDOS.Attach(AcuerdoBD);
                ContextoBD.SGJD_ACU_TAB_ACUERDOS.Remove(AcuerdoBD);
                RegistrosAfectados = await ContextoBD.SaveChangesAsync();                
            }

            if (RegistrosAfectados >= 1) {
                // Bajar el consecutivo de número de acuerdo en Tipos de Objeto
                var TareaBajarConsecutivoSesion = TipoObjeto.BajarConsecutivoAsync(ModeloAcuerdo.NombreObjeto);
                BajarConsecutivo = await TareaBajarConsecutivoSesion;
            }

            if (RegistrosAfectados >= 1 && BajarConsecutivo == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar", ValorAntiguo: IdAcuerdo.ToString(), ValorNuevo: "", SeccionSistema: "Acuerdo");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<bool> ActualizarTipoSeguimiento(string IdAcuerdo, string TipoSeguimiento) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
                var AcuerdoModelo = await TareaObtenerAcuerdo;

                AcuerdoModelo.TipoSeguimiento = Convert.ToInt32(TipoSeguimiento);

                ContextoBD.Entry(AcuerdoModelo.BD()).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: AcuerdoModelo.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EnviarVistoBuenoAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            // Cambiar estado del acuerdo de Borrador a [Visto Bueno]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdo, "ACU-VB");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloAcuerdo.ObtenerInformacion(), ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");

            return EstadoActualizado;
        }

        public async Task<bool> AgregarAcuerdoFirmadoAsync(Acuerdo ModeloAcuerdo, HttpPostedFileBase Archivo) {
            // Actualizar en base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener el acuerdo a actualizar
                var TareaObtenerAcuerdoBD = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(ModeloAcuerdo.Id);

                // Obtener el estado que corresponde al acuerdo firmado
                var TareaTraerEstado = Estado.ObtenerPorCodigoAsync("ACU-FIRM");

                //Descargar/guardar pdf a carpeta de repositorio del acuerdo respectivo
                var TareaDescargarPDF = Repositorio.GuardarAcuerdoPDFRepositorioAcuerdos(ModeloAcuerdo, Archivo);

                // Obtener el acuerdo a actualizar
                var AcuerdoBD = await TareaObtenerAcuerdoBD;

                // Obtener el estado que corresponde al acuerdo firmado
                Estado ModeloEstado = await TareaTraerEstado;

                // Obtener URL en que se guardó el PDF del acuerdo firmada
                string RutaPDFAcuerdoFirmado = await TareaDescargarPDF;

                if (!String.IsNullOrEmpty(RutaPDFAcuerdoFirmado)) {
                    // Asignar el valor de la URL del archivo guardado en la columna de la tabla acuerdo
                    AcuerdoBD.UrlAcuerdoFirmado = RutaPDFAcuerdoFirmado;

                    // Cambiar el estado del Acuerdo a firmado
                    AcuerdoBD.LLF_IdEstado = ModeloEstado.Id;

                    // Guardar en BD
                    ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                    int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                    if (RegistrosAfectados > 0) {
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

        public async Task<bool> NotificarAsync(int IdAcuerdo, string Enlace, string TextoEnlace, string MensajeDetalle) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(IdAcuerdo);
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            bool EstadoActualizado = false;
            bool AcuerdoActualizado = false;
            bool AvisoEnviadoPorCorreo = false;
            bool AvisoEnviadoCorreosAdicionales = false;

            // Enviar correos de notificación a las unidades ejecutoras y de información del acuerdo
            // Obtener las unidades ejecutoras del acuerdo
            IEnumerable<Unidad> UnidadesAcuerdo = (ModeloAcuerdo.UnidadesEjecucion ?? Enumerable.Empty<Unidad>()).Concat(ModeloAcuerdo.UnidadesInformacion ?? Enumerable.Empty<Unidad>()).ToList();

            // Obtener el aviso que se envía a las unidades y correos adicionales
            var TareaObtenerAviso = Aviso.ObtenerPorIdAsync(2158);
            Aviso ModeloAviso = await TareaObtenerAviso;

            // Recorrer cada unidad del acuerdo y enviar correo a los usuario de dicha unidad
            foreach (Unidad Unidad in UnidadesAcuerdo) {
                IEnumerable<CorreoUnidadAcuerdo> ListaCorreosUnidad = Unidad.CorreosUnidadAcuerdo;

                if (ListaCorreosUnidad.Any()) {
                    // Enviar aviso a los usuarios de las unidades del acuerdo
                    var TareaEnviarAviso = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ListaCorreosUnidad);
                    AvisoEnviadoPorCorreo = await TareaEnviarAviso;

                    if (AvisoEnviadoPorCorreo == false) {
                        break;
                    }
                }
                else {
                    AvisoEnviadoPorCorreo = true;
                }
            }

            if (AvisoEnviadoPorCorreo == true) {
                // Obtener los correos adicionales del acuerdo
                IEnumerable<CorreoAdicional> CorreosAdicionales = ModeloAcuerdo.CorreosAdicionales;

                // Enviar notificación a correos adicionales
                var TareaEnviarAvisoCorreosAdicionales = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, CorreosAdicionales);
                AvisoEnviadoCorreosAdicionales = await TareaEnviarAvisoCorreosAdicionales;
            }

            if (AvisoEnviadoPorCorreo == true && AvisoEnviadoCorreosAdicionales == true) {
                // Cambiar estado del acuerdo a [Notificado]
                var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdo, "ACU-NOTI");
                EstadoActualizado = await TareaActualiazarEstado;
            }

            if (EstadoActualizado == true) {
                // Actualizar campos necesarios 
                ModeloAcuerdo.FechaNotificacion = DateTime.Now;
                ModeloAcuerdo.Revision = false;

                var TareaActualizarAcuerdo = ActualizarAsync(ModeloAcuerdo);
                AcuerdoActualizado = await TareaActualizarAcuerdo;
            }

            if (EstadoActualizado == true && AcuerdoActualizado == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Enviar Notificación", ValorAntiguo: ModeloAcuerdo.ObtenerInformacion(), ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<int> AgregarNuevaVersionAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdoOriginal = await TareaObtenerAcuerdo;

            // Verificar que existe o se encontró un acuerdo con el id provisto
            if (ModeloAcuerdoOriginal == null) {
                return 0;
            }

            // Crear un modelo de acuerdo copia para nueva versión
            var TareaObtenerAcuerdoNuevaVersion = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdoNuevaVersion = await TareaObtenerAcuerdoNuevaVersion;

            // Variables de control
            int RegistrosAfectados = 0;
            bool VotantesCopiados = false;
            bool UnidadesCopiadas = false;
            bool AdjuntosCopiados = false;

            // Cambiar estado del acuerdo original a [Versión anterior]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdoOriginal, "ACU-VA");
            bool EstadoActualizado = await TareaActualiazarEstado;

            // Si se actualizó el estado del acuerdo original, se procede a guardar la nueva versión en bd
            if (EstadoActualizado == true) {
                // Obtener el estado correspondiente al codigo de acuerdo en borrador
                var TareaTraerEstado = Estado.ObtenerPorCodigoAsync("ACU-BORR");
                Estado ModeloEstado = await TareaTraerEstado;

                ModeloAcuerdoNuevaVersion.NumeroVersion += 1; // Aumentar versión
                ModeloAcuerdoNuevaVersion.IdEstado = ModeloEstado.Id; // Establecer estado [Borrador] al acuerdo en su nueva versión

                // Guardar el acuerdo en la base de datos
                using (var ContextoBD = SGJDDBContext.Create()) {
                    var AcuerdoBD = ModeloAcuerdoNuevaVersion.BD();
                    ContextoBD.SGJD_ACU_TAB_ACUERDOS.Add(AcuerdoBD);
                    RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                    // Asignar el id al modelo
                    ModeloAcuerdoNuevaVersion.Id = AcuerdoBD.LLP_Id;
                }
            }

            // Copiar las unidades para ejecución e información
            var TareaCopiarUnidades = CopiarUnidades(ModeloAcuerdoOriginal.Id, ModeloAcuerdoNuevaVersion.Id);
            UnidadesCopiadas = await TareaCopiarUnidades;

            // Copiar los votantes
            var TareaCopiarVotaciones = CopiarVotantesAsync(ModeloAcuerdoOriginal.Id, ModeloAcuerdoNuevaVersion.Id);
            VotantesCopiados = await TareaCopiarVotaciones;

            // Copiar los adjuntos a la nueva versión
            var TareaCopiarAdjuntos = CopiarAdjuntosAsync(ModeloAcuerdoOriginal, ModeloAcuerdoNuevaVersion);
            AdjuntosCopiados = await TareaCopiarAdjuntos;

            if (RegistrosAfectados >= 0 && UnidadesCopiadas == true && VotantesCopiados == true && AdjuntosCopiados == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar nueva versión", ValorAntiguo: ModeloAcuerdoOriginal.ObtenerInformacion(), ValorNuevo: ModeloAcuerdoNuevaVersion.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                return ModeloAcuerdoNuevaVersion.Id;
            }
            else {
                return 0;
            }
        }

        public async Task<bool> AcuerdoEnEjecucionAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            // Cambiar estado del acuerdo a [En ejecución]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdo, "ACU-EE");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Acuerdo en ejecución", ValorAntiguo: ModeloAcuerdo.ObtenerInformacion(), ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");

            return EstadoActualizado;
        }

        public async Task<string> AcuerdoEjecutadoAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            if (ModeloAcuerdo.TipoSeguimiento == 2) {
                return "El tipo de seguimiento es permanente, por lo que no se puede establecer como ejecutado";
            }

            // Cambiar estado del acuerdo a [Ejecutado]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdo, "ACU-EJEC");
            bool EstadoActualizado = await TareaActualiazarEstado;

            if (EstadoActualizado == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Acuerdo ejecutado", ValorAntiguo: ModeloAcuerdo.ObtenerInformacion(), ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");
                return "Ok";
            }
            else {
                return "Error";
            }
        }

        public async Task<bool> RevocarAcuerdoAsync(int IdAcuerdo) {
            // Obtener el acuerdo por id
            var TareaObtenerAcuerdo = ObtenerPorIdAsync(Convert.ToInt32(IdAcuerdo));
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            // Cambiar estado del acuerdo a [Revocado]
            var TareaActualiazarEstado = CambiarEstadoAsync(ModeloAcuerdo, "ACU-REV");
            bool EstadoActualizado = await TareaActualiazarEstado;

            await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Acuerdo revocado", ValorAntiguo: ModeloAcuerdo.ObtenerInformacion(), ValorNuevo: ModeloAcuerdo.ObtenerInformacion(), SeccionSistema: "Acuerdo");

            return EstadoActualizado;
        }

        public async Task<bool> AgregarArchivoAdjuntoAsync(ArchivoAdjunto ModeloArchivoAdjunto, HttpPostedFileBase Archivo) {
            string NombreTabla = ModeloArchivoAdjunto.NombreObjeto;
            string ExtensionArchivo = Path.GetExtension(Archivo.FileName);

            var TareaObtenerTipoObjeto = TipoObjeto.ObtenerPorNombreTablaAsync(NombreTabla);
            var TareaObtenerTipoArchivo = TipoArchivo.ObtenerPorExtensionAsync(ExtensionArchivo);
            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Acuerdos");

            // Obtener la información del TipoObjeto según la propiedad NombreObjeto (Nombre de la tabla)
            TipoObjeto ModeloTipoObjetoAcuerdo = await TareaObtenerTipoObjeto;

            // Obtener la información del tipo de archivo, según la extensión del archivo subido
            TipoArchivo ModeloTipoArchivo = await TareaObtenerTipoArchivo;

            // Obtener la información del repositorio, según la extensión del archivo subido
            Repositorio ModeloRepositorio = await TareaObtenerRepositorio;

            // TODO: Agregar campo a la tabla de repositorios para que se refiera a un tipo de objeto y poder obtener el
            // repositorio por el id del tipoObjeto al que pertenece el archivo a subir
            ModeloArchivoAdjunto.IdTipoObjeto = ModeloTipoObjetoAcuerdo.Id;
            ModeloArchivoAdjunto.TipoObjeto = ModeloTipoObjetoAcuerdo;

            ModeloArchivoAdjunto.IdRepositorio = ModeloRepositorio.Id;
            ModeloArchivoAdjunto.Repositorio = ModeloRepositorio;

            ModeloArchivoAdjunto.IdTipoArchivo = ModeloTipoArchivo.Id;
            ModeloArchivoAdjunto.TipoArchivo = ModeloTipoArchivo;

            //Agrega archivo adjunto a la carpeta y la base de datos
            var TareaAgregarArchivoAdjunto = ArchivoAdjunto.AgregarAsync(ModeloArchivoAdjunto, Archivo);
            await TareaAgregarArchivoAdjunto;

            return true;
        }

        public async Task<IEnumerable<ArchivoAdjunto>> ObtenerArchivosRelacionadosAsync(int IdAcuerdo) {
            // Contenedor de todos los adjuntos
            List<ArchivoAdjunto> ListaAdjuntos = new List<ArchivoAdjunto>();

            var TareaObtenerAcuerdo = ObtenerPorIdAsync(IdAcuerdo);
            Acuerdo ModeloAcuerdo = await TareaObtenerAcuerdo;

            if (ModeloAcuerdo != null) {
                var TareaObtenerTipoObjetoAcuerdo = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloAcuerdo.NombreObjeto);
                TipoObjeto TipoObjetoAcuerdo = await TareaObtenerTipoObjetoAcuerdo;

                // Obtener los archivos adjuntos del acuerdo 
                var TareaObtenerAdjuntosAcuerdo = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(IdAcuerdo, TipoObjetoAcuerdo.Id);
                IEnumerable<ArchivoAdjunto> ListaAdjuntosAcuerdo = await TareaObtenerAdjuntosAcuerdo;

                // Recorrer la lista y agregar los elementos al contenedor de todos los adjuntos
                foreach (ArchivoAdjunto Adjunto in ListaAdjuntosAcuerdo) {
                    ListaAdjuntos.Add(Adjunto);
                }

                // Obtener el artículo del acuerdo
                Articulo ModeloArticulo = ModeloAcuerdo.Articulo;

                if (ModeloArticulo != null) {
                    var TareaObtenerTipoObjetoArticulo = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloArticulo.NombreObjeto);
                    TipoObjeto TipoObjetoArticulo = await TareaObtenerTipoObjetoArticulo;

                    // Obtener los archivos adjuntos del articulo del acuerdo 
                    var TareaObtenerAdjuntosArticulo = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(ModeloArticulo.Id, TipoObjetoArticulo.Id);
                    IEnumerable<ArchivoAdjunto> ListaAdjuntosArticulo = await TareaObtenerAdjuntosArticulo;

                    // Recorrer la lista y agregar los elementos al contenedor de todos los adjuntos
                    foreach (ArchivoAdjunto Adjunto in ListaAdjuntosArticulo) {
                        ListaAdjuntos.Add(Adjunto);
                    }

                    if (ModeloArticulo.IdTema != null) {
                        var TareaObtenerTipoObjetoTema = TipoObjeto.ObtenerPorNombreTablaAsync(ModeloArticulo.Tema.NombreObjeto);
                        TipoObjeto TipoObjetoTema = await TareaObtenerTipoObjetoTema;

                        // Obtener los arhivos adjuntos del tema del articulo del acuerdo siempre y cuando el id de tema no se null,
                        // ya que en algunos casos el articulo no tiene un tema relacionado
                        var TareaObtenerAdjuntosTema = ArchivoAdjunto.ObtenerTodosPorIdObjetoIdTipoObjetoAsync(Convert.ToInt32(ModeloArticulo.IdTema), TipoObjetoTema.Id);
                        IEnumerable<ArchivoAdjunto> ListaAdjuntosTema = await TareaObtenerAdjuntosTema;

                        // Recorrer la lista y agregar los elementos al contenedor de todos los adjuntos
                        foreach (ArchivoAdjunto Adjunto in ListaAdjuntosTema) {
                            ListaAdjuntos.Add(Adjunto);
                        }
                    }
                }
            }

            return ListaAdjuntos;
        }

        public async Task<bool> EliminarAcuerdoFirmadoAsync(Acuerdo ModeloAcuerdo) {
            // Actualizar en base de datos
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdoBD = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(ModeloAcuerdo.Id);
                var AcuerdoBD = await TareaObtenerAcuerdoBD;

                // Eliminar el archivo fisicamente
                bool ArchivoEliminado = ArchivoAdjunto.EliminarAsync(AcuerdoBD.UrlAcuerdoFirmado);

                // Limpiar el valor de la URL del archivo guardado en la columna de la tabla acuerdo
                AcuerdoBD.UrlAcuerdoFirmado = null;

                // Guardar en BD
                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados > 0 & ArchivoEliminado == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<Acuerdo> ObtenerTodos() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener lista de acuerdos utilizando linq para utilziar ciertas columnas, ya que la columna de considerando y por tanto hacen la consulta pesada
                var ObtenerAcuerdosBD = from Acuerdos in ContextoBD.SGJD_ACU_TAB_ACUERDOS
                                        select new {
                                            Acuerdos.LLP_Id,
                                            Acuerdos.NumeroAcuerdo,
                                            Acuerdos.NumeroVersion,
                                            Acuerdos.Firme,
                                            Acuerdos.FechaFirmeza,
                                            Acuerdos.Asunto,
                                            Acuerdos.Titulo,
                                            Acuerdos.TipoSeguimiento,
                                            Acuerdos.Revision,
                                            Acuerdos.SGJD_ADM_TAB_ESTADOS
                                        };

                // Crear lista de Acuerdos especificando los campos que se van a llenar
                List<Acuerdo> ListaAcuerdos = ObtenerAcuerdosBD.AsEnumerable().Select(a => new Acuerdo {
                    Id = a.LLP_Id,
                    NumeroAcuerdo = a.NumeroAcuerdo,
                    NumeroVersion = a.NumeroVersion,
                    Firme = a.Firme,
                    FechaFirmeza = a.FechaFirmeza,
                    Asunto = a.Asunto,
                    Titulo = a.Titulo,
                    TipoSeguimiento = a.TipoSeguimiento,
                    Revision = a.Revision,
                    Estado = new Estado(a.SGJD_ADM_TAB_ESTADOS)
                }).OrderByDescending(a => a.NumeroAcuerdo).ThenByDescending(a => a.NumeroVersion).ToList();

                return ListaAcuerdos;
            }
        }

        public Task<IEnumerable<Acuerdo>> ObtenerTodosAsync() {
            Task<IEnumerable<Acuerdo>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<AcuerdosPorPalabraClaveDTO> ObtenerTodosPorPalabraClave(string Palabra) {
            IEnumerable<AcuerdosPorPalabraClaveDTO> Acuerdos = new AcuerdoDAO().ObtenerAcuerdosPorPalabraClave(Palabra);
            return Acuerdos;
        }

        public Task<IEnumerable<AcuerdosPorPalabraClaveDTO>> ObtenerTodosPorPalabraClaveAsync(string Palabra) {
            Task<IEnumerable<AcuerdosPorPalabraClaveDTO>> Tarea = Task.Run(() => ObtenerTodosPorPalabraClave(Palabra));
            return Tarea;
        }

        public async Task<Acuerdo> ObtenerPorIdAsync(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                //ContextoBD.Configuration.LazyLoadingEnabled = false;
                var TareaObtenerAcuerdoBD = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdoBD;
                Acuerdo ModeloAcuerdo = new Acuerdo(AcuerdoBD, AcuerdoBD.SGJD_ACT_TAB_ARTICULOS, AcuerdoBD.SGJD_ADM_TAB_ESTADOS, AcuerdoBD.SGJD_ADM_TAB_TIPOS_APROBACION, AcuerdoBD.SGJD_ACU_TAB_SEGUIMIENTOS, AcuerdoBD.SGJD_ADM_TAB_UNIDADES, AcuerdoBD.SGJD_ADM_TAB_UNIDADES1, AcuerdoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES);
                return ModeloAcuerdo;
            }
        }

        public IEnumerable<AcuerdoParaSeguimientoDTO> ObtenerAcuerdosParaSeguimiento() {
            IEnumerable<AcuerdoParaSeguimientoDTO> Acuerdos = new AcuerdoDAO().ObtenerAcuerdosParaSeguimientoSinAcuerdo();
            return Acuerdos;
        }

        public Task<IEnumerable<AcuerdoParaSeguimientoDTO>> ObtenerAcuerdosParaSeguimientoAsync() {
            Task<IEnumerable<AcuerdoParaSeguimientoDTO>> Tarea = Task.Run(() => ObtenerAcuerdosParaSeguimiento());
            return Tarea;
        }

        public IEnumerable<SesionConArticulosParaNuevoAcuerdoDTO> ObtenerSesionesConArticulosParaNuevoAcuerdo() {
            // Crear objeto que es una lista de Sesiones con sus respectivos Articulos que no poseen un Acuerdo
            List<SesionConArticulosParaNuevoAcuerdoDTO> ListaSesionesConArticulos = new List<SesionConArticulosParaNuevoAcuerdoDTO>();

            // Obtener las Sesiones que contienen Articulos sin Acuerdo
            IEnumerable<SesionConArticulosSinAcuerdoDTO> Sesiones = new AcuerdoDAO().ObtenerSesionesConArticulosSinAcuerdo();

            // Por cada una de las Sesiones obtener los Articulos que no tienen Acuerdo
            foreach (var Sesion in Sesiones) {
                IEnumerable<ArticuloSinAcuerdoDTO> Articulos = new AcuerdoDAO().ObtenerArticulosSinAcuerdo(Sesion.IdSesion);

                SesionConArticulosParaNuevoAcuerdoDTO SesionConArticulos = new SesionConArticulosParaNuevoAcuerdoDTO {
                    Sesion = Sesion,
                    Articulos = Articulos
                };

                ListaSesionesConArticulos.Add(SesionConArticulos);
            }

            return ListaSesionesConArticulos;
        }

        public async Task<bool> AgregarUnidadAsync(int IdAcuerdo, int IdUnidad, string Tipo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdo;

                var TareaObtenerUnidad = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(IdUnidad);
                var UnidadBD = await TareaObtenerUnidad;

                if (Tipo.Equals("E")) {
                    AcuerdoBD.SGJD_ADM_TAB_UNIDADES.Add(UnidadBD);
                }
                else {
                    AcuerdoBD.SGJD_ADM_TAB_UNIDADES1.Add(UnidadBD);
                }

                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar Unidad a Acuerdo", ValorAntiguo: "", ValorNuevo: "IdAcuerdo: " + IdAcuerdo.ToString() + ", IdUnidad: " + IdUnidad.ToString(), SeccionSistema: "Acuerdos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> EliminarUnidadAsync(int IdAcuerdo, int IdUnidad, string Tipo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdo;

                if (Tipo.Equals("E")) {
                    var UnidadDeAcuerdoBD = AcuerdoBD.SGJD_ADM_TAB_UNIDADES.Where(u => u.LLP_Id == IdUnidad).FirstOrDefault();
                    AcuerdoBD.SGJD_ADM_TAB_UNIDADES.Remove(UnidadDeAcuerdoBD);
                }
                else {
                    var UnidadDeAcuerdoBD = AcuerdoBD.SGJD_ADM_TAB_UNIDADES1.Where(u => u.LLP_Id == IdUnidad).FirstOrDefault();
                    AcuerdoBD.SGJD_ADM_TAB_UNIDADES1.Remove(UnidadDeAcuerdoBD);
                }

                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Eliminar Unidad a Acuerdo", ValorAntiguo: "", ValorNuevo: "IdAcuerdo: " + IdAcuerdo.ToString() + ", IdUnidad: " + IdUnidad.ToString(), SeccionSistema: "Acuerdos");
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

        public IEnumerable<Unidad> ObtenerUnidadesInformacion(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaUnidades = ContextoBD.SGJD_ACU_TAB_ACUERDOS.Where(a => a.LLP_Id == IdAcuerdo).FirstOrDefault().SGJD_ADM_TAB_UNIDADES1.ToList();
                IEnumerable<Unidad> ListaUnidadesInformacion = ListaUnidades.Select(UnidadBD => new Unidad(UnidadBD, UnidadBD.SGJD_ACU_TAB_CORREOS_UNIDADES_ACUERDO)).ToList();
                return ListaUnidadesInformacion;
            }
        }

        public Task<IEnumerable<Unidad>> ObtenerUnidadesInformacionAsync(int IdAcuerdo) {
            Task<IEnumerable<Unidad>> Tarea = Task.Run(() => ObtenerUnidadesInformacion(IdAcuerdo));
            return Tarea;
        }

        public async Task<bool> AgregarCorreoAdicionalAsync(int IdAcuerdo, int IdCorreoAdicional) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdo;

                var TareaObtenerCorreoAdicional = ContextoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.FindAsync(IdCorreoAdicional);
                var CorreoAdicionalBD = await TareaObtenerCorreoAdicional;

                AcuerdoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Add(CorreoAdicionalBD);

                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar correo adicional", ValorAntiguo: "", ValorNuevo: "IdAcuerdo: " + IdAcuerdo.ToString() + ", IdCorreoAdicional: " + IdCorreoAdicional.ToString(), SeccionSistema: "Acuerdos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public async Task<bool> QuitarCorreoAdicionalAsync(int IdAcuerdo, int IdCorreoAdicional) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                var AcuerdoBD = await TareaObtenerAcuerdo;

                var CorreoAdicionalDeAcuerdoBD = AcuerdoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Where(u => u.LLP_Id == IdCorreoAdicional).FirstOrDefault();
                AcuerdoBD.SGJD_ACU_TAB_CORREOS_ADICIONALES.Remove(CorreoAdicionalDeAcuerdoBD);

                ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                if (RegistrosAfectados >= 1) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Quitar correo adicional de Acuerdo", ValorAntiguo: "", ValorNuevo: "IdAcuerdo: " + IdAcuerdo.ToString() + ", IdCorreoAdicional: " + IdCorreoAdicional.ToString(), SeccionSistema: "Acuerdos");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public IEnumerable<SelectListItem> ObtenerTiposFirmezaParaSelect() {
            List<SelectListItem> ListaTiposSeguimiento = new List<SelectListItem> {
                new SelectListItem { Text = "No firme", Value = "False" },
                new SelectListItem { Text = "En firme", Value = "True" }
            };
            return ListaTiposSeguimiento;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTiposFirmezaParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTiposFirmezaParaSelect());
            return Tarea;
        }

        public IEnumerable<SelectListItem> ObtenerTiposRevisiónParaSelect() {
            List<SelectListItem> ListaTiposSeguimiento = new List<SelectListItem> {
                new SelectListItem { Text = "No", Value = "False" },
                new SelectListItem { Text = "Si", Value = "True" }
                };
            return ListaTiposSeguimiento;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTiposRevisiónParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTiposRevisiónParaSelect());
            return Tarea;
        }

        public IEnumerable<SelectListItem> ObtenerTiposSeguimientoParaSelect() {
            List<SelectListItem> ListaTiposSeguimiento = new List<SelectListItem> {
                new SelectListItem { Text = "Sin seguimiento", Value = "0" },
                new SelectListItem { Text = "Con seguimiento", Value = "1" },
                new SelectListItem { Text = "Seguimiento permanente", Value = "2" }
            };
            return ListaTiposSeguimiento;
        }

        public Task<IEnumerable<SelectListItem>> ObtenerTiposSeguimientoParaSelectAsync() {
            Task<IEnumerable<SelectListItem>> Tarea = Task.Run(() => ObtenerTiposSeguimientoParaSelect());
            return Tarea;
        }

        public IEnumerable<TipoAprobacion> ObtenerTipoAprobacionParaSelect() {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var ListaTipoAprobacionBD = ContextoBD.SGJD_ADM_TAB_TIPOS_APROBACION.ToList();
                IEnumerable<TipoAprobacion> ListaTipoAprobacion = ListaTipoAprobacionBD.Select(TipoAprobacionBD => new TipoAprobacion(TipoAprobacionBD)).ToList();
                return ListaTipoAprobacion;
            }
        }

        public Task<IEnumerable<TipoAprobacion>> ObtenerTipoAprobacionParaSelectAsync() {
            Task<IEnumerable<TipoAprobacion>> Tarea = Task.Run(() => ObtenerTipoAprobacionParaSelect());
            return Tarea;
        }

        //////////
        // Métodos privados
        //////////

        private IEnumerable<MiembrosPresentesPorIdAcuerdoDTO> ObtenerMiembrosPresentesPorSesion(int IdAcuerdo) {
            IEnumerable<MiembrosPresentesPorIdAcuerdoDTO> Acuerdos = new AcuerdoDAO().ObtenerMiembrosPresentesPorIdAcuerdo(IdAcuerdo);
            return Acuerdos;
        }

        private Task<IEnumerable<MiembrosPresentesPorIdAcuerdoDTO>> ObtenerMiembrosPresentesPorSesionAsync(int IdAcuerdo) {
            Task<IEnumerable<MiembrosPresentesPorIdAcuerdoDTO>> Tarea = Task.Run(() => ObtenerMiembrosPresentesPorSesion(IdAcuerdo));
            return Tarea;
        }

        private async Task<bool> CambiarEstadoAsync(Acuerdo ModeloAcuerdo, string CodigoEstado) {
            // Obtener el estado correspondiente al codigo especificado
            var TareaTraerEstado = Estado.ObtenerPorCodigoAsync(CodigoEstado);
            Estado ModeloEstado = await TareaTraerEstado;

            //Se establece el nuevo estado
            ModeloAcuerdo.IdEstado = ModeloEstado.Id;

            // Actualizar en la base de datos
            var TareaActualizarAcuerdo = ActualizarAsync(ModeloAcuerdo);
            bool AcuerdoActualizado = await TareaActualizarAcuerdo;

            return AcuerdoActualizado;
        }

        private async Task<bool> RegistrarVotantesAsync(int IdAcuerdo) {
            // Obtener los miembros presentes de la Sesión a la que pertenece el Acuerdo
            var TareaObtenerVotantes = ObtenerMiembrosPresentesPorSesionAsync(IdAcuerdo);
            IEnumerable<MiembrosPresentesPorIdAcuerdoDTO> MiembrosPresentes = await TareaObtenerVotantes;

            // Convertir la lista de miembros presentes a una lista de votantes, con el tipo de votacion por defecto
            IEnumerable<Votacion> Votantes = MiembrosPresentes.Select(mp => new Votacion(mp.IdAsistente, mp.IdAcuerdo)).ToList();

            using (var ContextoBD = SGJDDBContext.Create()) {
                foreach (Votacion Votante in Votantes) {
                    var VotanteBD = Votante.BD();
                    ContextoBD.SGJD_ACU_TAB_VOTACIONES_ACUERDO.Add(VotanteBD);
                }

                await ContextoBD.SaveChangesAsync();
            }

            return true;
        }

        private async Task<bool> RegistrarUnidadesParaInformacionAsync(int IdAcuerdo) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                // Obtener las unidades predeterminadas
                var TareaObtenerUnidadesPredeterminadas = ContextoBD.SGJD_ACU_TAB_UNIDADES_INFORMACION_PREDETERMINADAS.ToListAsync();
                var UnidadesPredeterminadasBD = await TareaObtenerUnidadesPredeterminadas;

                if (UnidadesPredeterminadasBD.Any()) {
                    // Obtener el Acuerdo al cual se le registrarán las unidades de información predeterminadas
                    var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdo);
                    var AcuerdoBD = await TareaObtenerAcuerdo;

                    foreach (var UnidadPredeterminadaBD in UnidadesPredeterminadasBD) {
                        var UnidadBD = UnidadPredeterminadaBD.SGJD_ADM_TAB_UNIDADES;
                        AcuerdoBD.SGJD_ADM_TAB_UNIDADES1.Add(UnidadBD);
                    }

                    ContextoBD.Entry(AcuerdoBD).State = EntityState.Modified;
                    int RegistrosAfectados = await ContextoBD.SaveChangesAsync();

                    if (RegistrosAfectados >= 1) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<bool> CopiarVotantesAsync(int IdAcuerdo, int IdAcuerdoNuevo) {
            // Obtener los votantes del acuerdo actual
            var TareaObtenerVotaciones = Voto.ObtenerTodosPorIdAcuerdoAsync(IdAcuerdo);
            IEnumerable<Votacion> Votaciones = await TareaObtenerVotaciones;

            if (Votaciones.Any()) {
                using (var ContextoBD = SGJDDBContext.Create()) {
                    foreach (Votacion Voto in Votaciones) {
                        Voto.IdAcuerdo = IdAcuerdoNuevo;
                        var VotoBD = Voto.BD();
                        ContextoBD.SGJD_ACU_TAB_VOTACIONES_ACUERDO.Add(VotoBD);
                    }

                    await ContextoBD.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> CopiarAdjuntosAsync(Acuerdo AcuerdoActual, Acuerdo AcuerdoNuevo) {
            var TareaObtenerAdjuntos = ArchivoAdjunto.ObtenerTodosPorIdObjetoAsync(AcuerdoActual.Id, AcuerdoActual.NombreObjeto);
            IEnumerable<ArchivoAdjunto> ListaAdjuntos = await TareaObtenerAdjuntos;

            if (ListaAdjuntos.Any()) {
                bool Copiado = false;

                // Recorrer la lista de adjuntos de la version anterior para crear en la nueva version sus adjuntos
                foreach (ArchivoAdjunto ModeloArchivoAdjunto in ListaAdjuntos) {
                    // Crear un objeto de tipo CustomHttpPostedFileBase usando la URL del archivo adjunto del acuerdo
                    HttpPostedFileBase Archivo;

                    //Leer la ruta del archivo.
                    using (FileStream fs = File.Create(Path.Combine(HttpContext.Current.Request.Path, ModeloArchivoAdjunto.UrlArchivo))) {
                        string[] RutaArchivo = ModeloArchivoAdjunto.UrlArchivo.Split('/');
                        string NombreArchivo = RutaArchivo[RutaArchivo.Length - 1];
                        Archivo = new CustomHttpPostedFile(fs, "application/pdf", "NombreArchivo");
                    }

                    ModeloArchivoAdjunto.IdObjeto = AcuerdoNuevo.Id;

                    var TareaAgregarArchivoAdjuntoNuevoAcuerdo = ArchivoAdjunto.AgregarAsync(ModeloArchivoAdjunto, true, Archivo);
                    ArchivoAdjunto ModeloArchivoAdjuntoCopiado = await TareaAgregarArchivoAdjuntoNuevoAcuerdo;

                    Copiado = true;
                }

                return Copiado;
            }
            else {
                // Si el acuerdo no contiene archivos adjuntos devolver true para indicar que la función se ejecutó correctamente
                return true;
            }
        }

        private async Task<bool> CopiarUnidades(int IdAcuerdo, int IdAcuerdoNuevo) {
            var TareaObtenerUnidadesEjecucion = ObtenerUnidadesEjecucionAsync(IdAcuerdo);
            IEnumerable<Unidad> ListaUnidadesEjecucion = await TareaObtenerUnidadesEjecucion;

            var TareaObtenerUnidadesInformacion = ObtenerUnidadesInformacionAsync(IdAcuerdo);
            IEnumerable<Unidad> ListaUnidadesInformacion = await TareaObtenerUnidadesInformacion;

            // Copiar unidades de ejecución del acuerdo
            if (ListaUnidadesEjecucion.Any()) {
                using (var ContextoBD = SGJDDBContext.Create()) {
                    // Obtener el Acuerdo al cual se le registrarán las unidades de información predeterminadas
                    var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdoNuevo);
                    var AcuerdoBD = await TareaObtenerAcuerdo;

                    foreach (Unidad Unidad in ListaUnidadesEjecucion) {
                        // Obtener desde la base de datosla unidad que se agregará al acuerdo
                        var TareaObtenerUnidad = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(Unidad.Id);
                        var UnidadBD = await TareaObtenerUnidad;

                        AcuerdoBD.SGJD_ADM_TAB_UNIDADES.Add(UnidadBD);
                    }

                    await ContextoBD.SaveChangesAsync();
                }
            }

            // Copiar unidades de información del acuerdo
            if (ListaUnidadesInformacion.Any()) {
                using (var ContextoBD = SGJDDBContext.Create()) {
                    // Obtener el Acuerdo al cual se le registrarán las unidades de información predeterminadas
                    var TareaObtenerAcuerdo = ContextoBD.SGJD_ACU_TAB_ACUERDOS.FindAsync(IdAcuerdoNuevo);
                    var AcuerdoBD = await TareaObtenerAcuerdo;

                    foreach (Unidad Unidad in ListaUnidadesInformacion) {
                        // Obtener desde la base de datosla unidad que se agregará al acuerdo
                        var TareaObtenerUnidad = ContextoBD.SGJD_ADM_TAB_UNIDADES.FindAsync(Unidad.Id);
                        var UnidadBD = await TareaObtenerUnidad;

                        AcuerdoBD.SGJD_ADM_TAB_UNIDADES1.Add(UnidadBD);
                    }

                    await ContextoBD.SaveChangesAsync();
                }
            }

            return true;
        }
        //////////
        // Fin de métodos privados
        //////////

        public async Task<IEnumerable<Acuerdo>> ObtenerTodosPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin) {
            // Establecer a la fecha fin la hora para abarcar todo el día.
            FechaFin = FechaFin + new TimeSpan(0, 23, 59, 59, 999);

            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerAcuerdosBD = ContextoBD.SGJD_ACU_TAB_ACUERDOS.Where(F => F.FechaFirmeza >= FechaInicio && F.FechaFirmeza <= FechaFin).ToListAsync();
                var ListaAcuerdosBD = await TareaObtenerAcuerdosBD;
                IEnumerable<Acuerdo> ListaModeloAcuerdo = ListaAcuerdosBD.Select(AcuerdoBD => new Acuerdo(AcuerdoBD)).ToList();
                return ListaModeloAcuerdo;
            }
        }
    }
}