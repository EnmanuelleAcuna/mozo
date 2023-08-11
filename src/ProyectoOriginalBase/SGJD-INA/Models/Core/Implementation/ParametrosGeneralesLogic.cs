using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.Implementation;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Core.Implementation {
    public class ParametrosGeneralesLogic : IParametrosGeneralesLogic {
        private readonly IBitacoraLogic Bitacora;
        private readonly IUsuarioLogic Usuario;
        private readonly IParametroGeneralRepository RepositorioBD;

        public ParametrosGeneralesLogic(IBitacoraLogic Bitacora, IUsuarioLogic Usuario, IParametroGeneralRepository RepositorioBD) {
            this.Bitacora = Bitacora;
            this.Usuario = Usuario;
            this.RepositorioBD = RepositorioBD;
        }

        public async Task<bool> ActualizarAsync(ParametroGeneral Entidad) {
            if (Entidad is null) throw new ArgumentNullException(paramName: nameof(Entidad), message: Resources.ModeloNulo);

            // Validar si el parámetro a actualizar es IdUsuarioSecretario o IdUsuarioPresidente para obtener el nombre del usuario seleccionado y
            // guardarlo como descripción
            if (Entidad.Nombre.Equals("IdUsuarioSecretario", StringComparison.OrdinalIgnoreCase) || Entidad.Nombre.Equals("IdUsuarioPresidente", StringComparison.OrdinalIgnoreCase)) {
                var TareaObtenerUsuario = Usuario.ObtenerPorIdAsync(Entidad.Valor);
                ApplicationUser ModeloUsuario = await TareaObtenerUsuario;
                Entidad.Descripcion = ModeloUsuario.Nombre;
            }

            var TareaActualizarEnBD = RepositorioBD.ActualizarAsync(Entidad);
            bool ParametroGeneralActualizado = await TareaActualizarEnBD;

            if (ParametroGeneralActualizado) {
                var TareaRegistrarEnbitacora = BitacoraLogic.RegistrarAccionEnBitacoraAsync(Accion: "Actualizar", ValorAntiguo: "", ValorNuevo: Entidad.ObtenerInformacion(), SeccionSistema: "Parámetros generales");
                await TareaRegistrarEnbitacora;
            }

            return ParametroGeneralActualizado;
        }

        public IEnumerable<ParametroGeneral> ObtenerTodos() {
            IEnumerable<ParametroGeneral> ListaParametrosGenerales = RepositorioBD.ObtenerTodos();
            return ListaParametrosGenerales;
        }

        public Task<IEnumerable<ParametroGeneral>> ObtenerTodosAsync() {
            Task<IEnumerable<ParametroGeneral>> Tarea = Task.Run(() => ObtenerTodos());
            return Tarea;
        }

        public IEnumerable<ParametroGeneral> ObtenerParametrosInstitucion() {
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesModel = RepositorioBD.ObtenerTodos();
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesInstitucion = ListaParametrosGeneralesModel.Where(pg => pg.Tipo.Equals("INSTITUCION", StringComparison.OrdinalIgnoreCase)).ToList();
            return ListaParametrosGeneralesInstitucion;
        }

        public Task<IEnumerable<ParametroGeneral>> ObtenerParametrosInstitucionAsync() {
            Task<IEnumerable<ParametroGeneral>> Tarea = Task.Run(() => ObtenerParametrosInstitucion());
            return Tarea;
        }

        public IEnumerable<ParametroGeneral> ObtenerParametrosCorreo() {
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesModel = RepositorioBD.ObtenerTodos();
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesCorreo = ListaParametrosGeneralesModel.Where(pg => pg.Tipo.Equals("CORREO", StringComparison.OrdinalIgnoreCase)).ToList();
            return ListaParametrosGeneralesCorreo;
        }

        public Task<IEnumerable<ParametroGeneral>> ObtenerParametrosCorreoAsync() {
            Task<IEnumerable<ParametroGeneral>> Tarea = Task.Run(() => ObtenerParametrosCorreo());
            return Tarea;
        }

        public async Task<ParametroGeneral> ObtenerPorIdAsync(int IdParametro) {
            var TareaObtenerParametroPorIdDeBD = RepositorioBD.ObtenerPorIdAsync(IdParametro);
            ParametroGeneral Entidad = await TareaObtenerParametroPorIdDeBD;
            return Entidad;
        }

        public async Task<ParametroGeneral> ObtenerPorNombreAsync(string Nombre) {
            var TareaObtenerParametroPorIdDeBD = RepositorioBD.ObtenerPorNombreAsync(Nombre);
            ParametroGeneral Entidad = await TareaObtenerParametroPorIdDeBD;
            return Entidad;
        }

        // Métodos estáticos
        public static IEnumerable<ParametroGeneral> ObtenerParametrosInstitucionStatic() {
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesModel = ParametroGeneralRepository.ObtenerTodosStatic();
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesInstitucion = ListaParametrosGeneralesModel.Where(pg => pg.Tipo.Equals("INSTITUCION", StringComparison.OrdinalIgnoreCase)).ToList();
            return ListaParametrosGeneralesInstitucion;
        }

        public static Task<IEnumerable<ParametroGeneral>> ObtenerParametrosInstitucionStaticAsync() {
            Task<IEnumerable<ParametroGeneral>> Tarea = Task.Run(() => ObtenerParametrosInstitucionStatic());
            return Tarea;
        }

        public static IEnumerable<ParametroGeneral> ObtenerParametrosCorreoStatic() {
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesModel = ParametroGeneralRepository.ObtenerTodosStatic();
            IEnumerable<ParametroGeneral> ListaParametrosGeneralesCorreo = ListaParametrosGeneralesModel.Where(pg => pg.Tipo.Equals("CORREO", StringComparison.OrdinalIgnoreCase)).ToList();
            return ListaParametrosGeneralesCorreo;
        }

        public static Task<IEnumerable<ParametroGeneral>> ObtenerParametrosCorreoStaticAsync() {
            Task<IEnumerable<ParametroGeneral>> Tarea = Task.Run(() => ObtenerParametrosCorreoStatic());
            return Tarea;
        }
    }
}