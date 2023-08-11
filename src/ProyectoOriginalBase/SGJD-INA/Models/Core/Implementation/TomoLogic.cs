using Ionic.Zip;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA.Models.Core.Implementation {
    public class TomoLogic : ITomoLogic {
        //Dependencias
        // Core
        private readonly IBitacoraLogic Bitacora;
        private readonly ITipoObjetoLogic TipoObjeto;
        private readonly IEstadoLogic Estado;
        private readonly IRepositorioLogic Repositorio;
        private readonly IAvisosLogic Aviso;

        // Repo
        private readonly ITomoRepository RepositorioTomo;

        public TomoLogic(IBitacoraLogic Bitacora, ITipoObjetoLogic TipoObjeto, IEstadoLogic Estado, IRepositorioLogic Repositorio, IAvisosLogic Aviso, ITomoRepository RepositorioTomo) {
            this.Bitacora = Bitacora;
            this.TipoObjeto = TipoObjeto;
            this.Estado = Estado;
            this.Repositorio = Repositorio;
            this.Aviso = Aviso;

            this.RepositorioTomo = RepositorioTomo;
        }

        //Métodos
        public async Task<Tomo> AgregarAsync(Tomo ModeloTomo) {
            if (ModeloTomo is null) {
                throw new ArgumentNullException(paramName: nameof(ModeloTomo), message: Properties.Resources.ModeloNulo);
            }

            var TareaObtenerObjetoTomo = TipoObjeto.ObtenerPorNombreTablaAsync("SGJD_ACT_TAB_TOMOS");
            var TareaObtenerEstado = Estado.ObtenerPorCodigoAsync("TOM-ABIE");

            TipoObjeto ObjetoTomo = await TareaObtenerObjetoTomo;
            Estado EstadoTomo = await TareaObtenerEstado;

            ModeloTomo.Numero = ObjetoTomo.Consecutivo;
            ModeloTomo.Nombre = "Tomo " + Funciones.OrdinalToRoman(ObjetoTomo.Consecutivo);
            ModeloTomo.NombreObjeto = ObjetoTomo.NombreTabla;
            ModeloTomo.IdEstado = EstadoTomo.Id;

            var ConsecutivoAumentado = false;
            var CarpetaTomoCreada = false;

            var TareaGuardarTomo = RepositorioTomo.GuardarAsync(ModeloTomo);
            ModeloTomo = await TareaGuardarTomo;

            if (ModeloTomo.Id >= 0 && ModeloTomo != null) {
                var TareaAumentarConsecutivoSesion = TipoObjeto.AumentarConsecutivoAsync(ModeloTomo.NombreObjeto);
                ConsecutivoAumentado = await TareaAumentarConsecutivoSesion;
            }

            if (ConsecutivoAumentado == true) {
                var TareaObtenerRutaTomo = Repositorio.ObtenerPorNombreAsync("Tomos");
                Repositorio RepositorioTomo = await TareaObtenerRutaTomo;

                RepositorioTomo.Nombre = ModeloTomo.Nombre;

                var TareaCrearCarpetaTomo = Repositorio.Agregar(RepositorioTomo, false, true);
                CarpetaTomoCreada = await TareaCrearCarpetaTomo;
            }

            if (ModeloTomo.Id >= 0 && ModeloTomo != null && ConsecutivoAumentado == true && CarpetaTomoCreada == true) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Agregar", ValorAntiguo: "", ValorNuevo: ModeloTomo.ObtenerInformacion(), SeccionSistema: "Tomo");
                return ModeloTomo;
            }
            else {
                return null;
            }
        }

        public async Task<bool> ActualizarAsync(Tomo ModeloTomo) {
            if (ModeloTomo is null) {
                throw new NotImplementedException("El tomo se encuentra nulo");
            }

            var TareaGuardarTomo = RepositorioTomo.ActualizarAsync(ModeloTomo);
            bool TomoActualizado = await TareaGuardarTomo;

            return TomoActualizado;
        }

        public async Task<bool> EliminarAsync(int IdTomo) {
            if (IdTomo <= 0) {
                throw new ArgumentException("El id debe ser mayor a 0");
            }

            var TareaEliminarDeBD = RepositorioTomo.EliminarAsync(IdTomo);
            bool TomoEliminado = await TareaEliminarDeBD;

            return TomoEliminado;
        }

        public async Task<Tomo> CerrarAsync(Tomo TomoCerrado) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                var TareaObtenerTomoParaCerrar = ObtenerPorIdSinActasAsync(TomoCerrado.Id); // Obtener el tomo por su id, sin incluir las actas
                Tomo TomoParaCerrar = await TareaObtenerTomoParaCerrar;

                // Obtener la informacion del tomo a cerrar previo a cerrar para guardarlo en bitacora
                string informacionTomoParaCerrar = TomoParaCerrar.ObtenerInformacion();

                // Obtener el nuevo estado que se la va a establecer al tomo
                var TareaObtenerEstadoCierre = Estado.ObtenerPorCodigoAsync("TOM-CERR");
                Estado ModeloEstadoCierre = await TareaObtenerEstadoCierre;

                // Actualizar los campos necesarios correspondientes al cierre de tomo
                TomoParaCerrar.IdEstado = ModeloEstadoCierre.Id;
                TomoParaCerrar.ObservacionCierre = TomoCerrado.ObservacionCierre;
                TomoParaCerrar.FechaCierre = TomoCerrado.FechaCierre;
                TomoParaCerrar.IdUsuarioCierre = TomoCerrado.IdUsuarioCierre;
                TomoParaCerrar.ObservacionesMedianteOficio = TomoCerrado.ObservacionesMedianteOficio;

                var TareaActualizarTomoEnBD = ActualizarAsync(TomoParaCerrar);
                bool TomoActualizadoEnBD = await TareaActualizarTomoEnBD;

                if (TomoActualizadoEnBD) {
                    await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: informacionTomoParaCerrar, ValorNuevo: TomoParaCerrar.ObtenerInformacion(), SeccionSistema: "Tomos");
                    return TomoCerrado;
                }
                else {
                    return null;
                }
            }
        }

        public async Task<bool> ActualizarUrlOficioAperturaAsync(int IdTomo, string UrlOficioApertura) {
            var TareaObtenerTomo = ObtenerPorIdSinActasAsync(IdTomo);
            Tomo TomoSinActas = await TareaObtenerTomo;

            string InformacionTomoAnterior = TomoSinActas.ObtenerInformacion();

            // Actualiza el campo UrlOficioApertura en la tabla de tomos
            TomoSinActas.UrlOficioApertura = UrlOficioApertura;

            var TareaActualizarTomo = ActualizarAsync(TomoSinActas);
            bool TomoActualizado = await TareaActualizarTomo;

            if (TomoActualizado) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar URL Oficio de Apertura", ValorAntiguo: InformacionTomoAnterior, ValorNuevo: TomoSinActas.ObtenerInformacion(), SeccionSistema: "Tomo");
            }

            return TomoActualizado;
        }

        public async Task<bool> ActualizarUrlOficioCierreAsync(int IdTomo, string UrlOficioCierre) {
            var TareaObtenerTomo = ObtenerPorIdSinActasAsync(IdTomo);
            Tomo TomoSinActas = await TareaObtenerTomo;

            string InformacionTomoAnterior = TomoSinActas.ObtenerInformacion();

            // Actualiza el campo UrlOficioApertura en la tabla de tomos
            TomoSinActas.UrlOficioCierre = UrlOficioCierre;

            var TareaActualizarTomo = ActualizarAsync(TomoSinActas);
            bool TomoActualizado = await TareaActualizarTomo;

            if (TomoActualizado) {
                await BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar URL Oficio Cierre", ValorAntiguo: InformacionTomoAnterior, ValorNuevo: TomoSinActas.ObtenerInformacion(), SeccionSistema: "Tomo");
            }

            return TomoActualizado;
        }

        public async Task<bool> NotificarAsync(string NombreAviso, string Enlace, string TextoEnlace, string MensajeDetalle) {
            // Obtener el aviso que se envía a las unidades y correos adicionales
            var TareaObtenerAviso = Aviso.ObtenerPorNombreAsync(NombreAviso);
            Aviso ModeloAviso = await TareaObtenerAviso;

            // Enviar aviso a los usuarios establecidos
            var TareaEnviarAvisoUsuarios = Aviso.EnviarCorreo(ModeloAviso, TextoEnlace, Enlace, MensajeDetalle, ModeloAviso.Usuarios);
            bool AvisoEnviadoUsuarios = await TareaEnviarAvisoUsuarios;
            return AvisoEnviadoUsuarios;
        }

        public IEnumerable<Tomo> ObtenerTodosSinActas() {
            IEnumerable<Tomo> ListaTomosConActas = RepositorioTomo.ObtenerTodosSinActas();
            return ListaTomosConActas;
        }

        public Task<IEnumerable<Tomo>> ObtenerTodosSinActasAsync() {
            Task<IEnumerable<Tomo>> Tarea = Task.Run(() => ObtenerTodosSinActas());
            return Tarea;
        }

        public IEnumerable<Tomo> ObtenerTodosConActas() {
            IEnumerable<Tomo> ListaTomosConActas = RepositorioTomo.ObtenerTodosConActas();
            return ListaTomosConActas;
        }

        public Task<IEnumerable<Tomo>> ObtenerTodosConActasAsync() {
            Task<IEnumerable<Tomo>> Tarea = Task.Run(() => ObtenerTodosConActas());
            return Tarea;
        }

        public IEnumerable<Tomo> ObtenerTodosPorCodigoEstado(int IdEstado) {
            IEnumerable<Tomo> Tomos = ObtenerTodosSinActas();

            // Obtener de la lista de tomos, todos aquellos que tienen el estado abierto, enviado desde el controlador
            IEnumerable<Tomo> TomosConEstado = Tomos.Where(t => t.IdEstado == IdEstado).OrderByDescending(t => t.Id).ToList();

            return TomosConEstado;
        }

        public Task<IEnumerable<Tomo>> ObtenerTodosPorCodigoEstadoAsync(int IdEstado) {
            Task<IEnumerable<Tomo>> Tarea = Task.Run(() => ObtenerTodosPorCodigoEstado(IdEstado));
            return Tarea;
        }

        public Tomo ObtenerPorIdSinActas(int IdTomo) {
            if (IdTomo <= 0) {
                throw new ArgumentOutOfRangeException("El id del tomo debe ser mayor a 0");
            }

            Tomo Modelo = RepositorioTomo.ObtenerPorIdSinActas(IdTomo);
            return Modelo;
        }

        public Task<Tomo> ObtenerPorIdSinActasAsync(int IdTomo) {
            Task<Tomo> Tarea = Task.Run(() => ObtenerPorIdSinActas(IdTomo));
            return Tarea;
        }

        private Tomo ObtenerPorIdConActas(int IdTomo) {
            if (IdTomo <= 0) {
                throw new ArgumentOutOfRangeException("El id del tomo debe ser mayor a 0");
            }

            Tomo Modelo = RepositorioTomo.ObtenerPorIdConActas(IdTomo);
            return Modelo;
        }

        public Task<Tomo> ObtenerPorIdConActasAsync(int IdTomo) {
            Task<Tomo> Tarea = Task.Run(() => ObtenerPorIdConActas(IdTomo));
            return Tarea;
        }

        public async Task<Tomo> ObtenerUltimoAbiertoAsync() {
            var TareObtenerEstadoTomoAbierto = Estado.ObtenerPorCodigoAsync("TOM-ABIE");
            Estado EstadoTomoAbierto = await TareObtenerEstadoTomoAbierto;

            var TareaObtenerTomosAbiertos = ObtenerTodosPorCodigoEstadoAsync(EstadoTomoAbierto.Id);
            IEnumerable<Tomo> TomosAbiertos = await TareaObtenerTomosAbiertos;

            Tomo UltimoTomoAbierto = TomosAbiertos.FirstOrDefault();

            return (UltimoTomoAbierto is null) ? new Tomo() : UltimoTomoAbierto;
        }

        public async Task<int> ObtenerConsecutivoPaginaPorIdAsync(int IdTomo) {
            var TareaObtenerTomoConActas = ObtenerPorIdConActasAsync(IdTomo);
            Tomo TomoConActas = await TareaObtenerTomoConActas;

            // Recorrer las actas del tomo y sumar la cantidad de páginas de cada uno
            int Consecutivo = 1;
            foreach (Acta Acta in TomoConActas.Actas) {
                Consecutivo += Acta.NumeroPaginasPDF;
            }

            return Consecutivo;
        }

        public async Task<ZipFile> ObtenerZipAsync(string NombreTomo) {
            // Variables
            const string NombreSitio = "\\SitioSGJD"; // Nombre de la carpeta del sitio
            const string NombreRepositorio = "RepositorioSGJD\\"; // Nombre de la carpeta del repositorio
            string Raiz = HttpContext.Current.Request.MapPath("~" + NombreSitio).Replace(NombreSitio, "\\"); // Ruta donde se encuentra hosteado el sitio y el repositorio

            var TareaObtenerRepositorio = Repositorio.ObtenerPorNombreAsync("Tomos");
            Repositorio RepositorioTomo = await TareaObtenerRepositorio;

            //Crear un objeto zip
            using (var ZipFile = new ZipFile()) {
                if (RepositorioTomo == null) {
                    return ZipFile;
                }

                if (Directory.Exists(Raiz + NombreRepositorio + RepositorioTomo.Nombre + "\\" + NombreTomo)) {
                    ZipFile.AddDirectory(Raiz + NombreRepositorio + RepositorioTomo.Nombre + "\\" + NombreTomo, NombreTomo);
                    return ZipFile;
                }
                else {
                    return ZipFile;
                }
            }
        }
    }
}