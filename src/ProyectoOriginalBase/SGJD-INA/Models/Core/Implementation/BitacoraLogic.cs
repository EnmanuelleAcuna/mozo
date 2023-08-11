using Microsoft.AspNet.Identity;
using SGJD_INA.Models.Core.Interfaces;
using SGJD_INA.Models.DA.DAO;
using SGJD_INA.Models.DA.Implementation;
using SGJD_INA.Models.DA.Interfaces;
using SGJD_INA.Models.DTO;
using SGJD_INA.Models.Entities;
using SGJD_INA.Models.Helpers;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace SGJD_INA.Models.Core.Implementation {
    public class BitacoraLogic : IBitacoraLogic {
        private readonly IBitacoraRepository RepositorioBD;

        public BitacoraLogic(IBitacoraRepository RepositorioBD) {
            this.RepositorioBD = RepositorioBD;
        }

        // Bitácora con acciones
        public async Task<IEnumerable<Bitacora>> ObtenerTodosAsync() {
            var TareaObtenerTodos = RepositorioBD.ObtenerTodosAsync();
            IEnumerable<Bitacora> ListaRegistrosBitacora = await TareaObtenerTodos;
            return ListaRegistrosBitacora;
        }

        public IEnumerable<BitacoraPorFechaDTO> ObtenerPorRangoFecha(DateTime FechaInicio, DateTime FechaFin) {
            FechaFin = FechaFin + new TimeSpan(0, 23, 59, 59, 999); // Establecer a la fecha fin la hora para abarcar todo el día.
            IEnumerable<BitacoraPorFechaDTO> BitacoraPorRangoFecha = new BitacoraDAO().ObtenerBitacoraPorRangoFecha(FechaInicio, FechaFin);
            return BitacoraPorRangoFecha;
        }

        public Task<IEnumerable<BitacoraPorFechaDTO>> ObtenerPorRangoFechaAsync(DateTime FechaInicio, DateTime FechaFin) {
            Task<IEnumerable<BitacoraPorFechaDTO>> Tarea = Task.Run(() => ObtenerPorRangoFecha(FechaInicio, FechaFin));
            return Tarea;
        }

        public async Task<Bitacora> ObtenerPorIdAsync(int Id) {
            var TareaObtenerRegistroBitacora = RepositorioBD.ObtenerPorIdAsync(Id);
            Bitacora RegistroBitacora = await TareaObtenerRegistroBitacora;
            return RegistroBitacora;
        }

        // Métodos estáticos
        /// <summary>
        /// Agregar acción en la bitácora
        /// </summary>
        public static async Task RegistrarAccionEnBitacoraAsync(string Accion, string ValorAntiguo, string ValorNuevo, string SeccionSistema) {
            //Obtener la hora UTC-6
            TimeSpan TS = new TimeSpan(6, 0, 0);
            DateTime UTCDT = DateTime.UtcNow;
            DateTime FechaHora = UTCDT.Subtract(TS);

            if (HttpContext.Current is null) {
                // No hacer nada ya que se necesita del HttpContext para obtener el usuario conectado
            }
            else {
                Bitacora ModeloBitacora = new Bitacora() {
                    IdUsuario = HttpContext.Current.User.Identity.GetUserId(),
                    FechaHora = FechaHora,
                    Accion = Accion,
                    ValorAntiguo = ValorAntiguo,
                    ValorNuevo = ValorNuevo,
                    DireccionIP = string.Empty,
                    DescripcionDispositivo = string.Empty,
                    SeccionSistema = SeccionSistema
                };

                await BitacoraRepository.GuardarAsync(ModeloBitacora);
            }
        }

        public static void RegistrarAccionEnBitacoraAsync(string Accion, string ValorAntiguo, string ValorNuevo, string SeccionSistema, RouteData RouteData, HttpContextBase ContextoHTTP) {
            Bitacora ModeloBitacora = new Bitacora() {
                IdUsuario = string.IsNullOrEmpty(ContextoHTTP.User.Identity.GetUserId()) ? "90eff4bb-6423-46fe-b0cf-0472a1a789e9" : ContextoHTTP.User.Identity.GetUserId(),
                FechaHora = DateTime.Now, // Servidor: ContextoHTTP.Timestamp;
                Accion = string.Format(new CultureInfo(Resources.CultureInfo), "{0}, {1}/{2}", Accion, RouteData.Values["controller"], RouteData.Values["action"]),
                ValorAntiguo = ValorAntiguo,
                ValorNuevo = ValorNuevo,
                DireccionIP = Funciones.ObtenerIP(ContextoHTTP.Request),
                DescripcionDispositivo = Funciones.ObtenerInfoEquipo(ContextoHTTP.Request),
                SeccionSistema = SeccionSistema
            };

            BitacoraRepository.GuardarAsync(ModeloBitacora);
        }

        // Log con errores
        public List<String> ObtenerLog() {
            string RutaArchivoLog = HttpContext.Current.Server.MapPath("~/Log.txt");
            List<String> Log = new List<String>();
            using (StreamReader Lector = new StreamReader(RutaArchivoLog)) {
                while (Lector.Peek() > -1) {
                    string Linea = Lector.ReadLine();
                    if (!String.IsNullOrEmpty(Linea)) {
                        Log.Add(Linea);
                        Console.WriteLine(Linea);
                    }
                }
            }
            return Log;
        }

        // Métodos estáticos
        public static void RegistrarErrorEnLog(Exception Ex, string Mensaje, RouteData RouteData, HttpContextBase ContextoHTTP) {
            string MensajeError;

            if (Ex.InnerException != null) {
                MensajeError = DateTime.Now + " - " + Mensaje + " - " + RouteData.Values["controller"] + "/" + RouteData.Values["action"] + " - " + Funciones.ObtenerIP(ContextoHTTP.Request) + " - " + Funciones.ObtenerInfoEquipo(ContextoHTTP.Request) + " - " + Ex.Message + ", tipo: " + Ex.GetType().ToString() + ". Inner exception: " + Ex.InnerException.Message + ", tipo: " + Ex.InnerException.GetType().ToString();

                if (Ex.InnerException.InnerException != null) {
                    MensajeError = DateTime.Now + " - " + Mensaje + " - " + RouteData.Values["controller"] + "/" + RouteData.Values["action"] + " - " + Funciones.ObtenerIP(ContextoHTTP.Request) + " - " + Funciones.ObtenerInfoEquipo(ContextoHTTP.Request) + " - " + Ex.Message + ", tipo: " + Ex.GetType().ToString() + ". Inner exception: " + Ex.InnerException.Message + ", tipo: " + Ex.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.GetType().ToString();

                    if (Ex.InnerException.InnerException.InnerException != null) {
                        MensajeError = DateTime.Now + " - " + Mensaje + " - " + RouteData.Values["controller"] + "/" + RouteData.Values["action"] + " - " + Funciones.ObtenerIP(ContextoHTTP.Request) + " - " + Funciones.ObtenerInfoEquipo(ContextoHTTP.Request) + " - " + Ex.Message + ", tipo: " + Ex.GetType().ToString() + ". Inner exception: " + Ex.InnerException.Message + ", tipo: " + Ex.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.InnerException.GetType().ToString();

                        if (Ex.InnerException.InnerException.InnerException.InnerException != null) {
                            MensajeError = DateTime.Now + " - " + Mensaje + " - " + RouteData.Values["controller"] + "/" + RouteData.Values["action"] + " - " + Funciones.ObtenerIP(ContextoHTTP.Request) + " - " + Funciones.ObtenerInfoEquipo(ContextoHTTP.Request) + " - " + Ex.Message + ", tipo: " + Ex.GetType().ToString() + ". Inner exception: " + Ex.InnerException.Message + ", tipo: " + Ex.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.InnerException.GetType().ToString() + ". Inner exception: " + Ex.InnerException.InnerException.InnerException.InnerException.Message + ", tipo: " + Ex.InnerException.InnerException.InnerException.InnerException.GetType().ToString();
                        }
                    }
                }
            }
            else {
                MensajeError = DateTime.Now + " - " + Mensaje + " - " + RouteData.Values["controller"] + "/" + RouteData.Values["action"] + " - " + Funciones.ObtenerIP(ContextoHTTP.Request) + " - " + Funciones.ObtenerInfoEquipo(ContextoHTTP.Request) + " - " + Ex.Message + ", tipo: " + Ex.GetType().ToString();
            }

            EscribirErrorEnArchivoLog(MensajeError);
        }

        public static void RegistrarErrorEnLog(DbEntityValidationException Ex) {
            if (Ex is null) throw new ArgumentNullException(paramName: nameof(Ex), message: Resources.ModeloNulo);

            foreach (var ValidationError in Ex.EntityValidationErrors) {
                foreach (var InnerValidationError in ValidationError.ValidationErrors) {
                    EscribirErrorEnArchivoLog(ValidationError.Entry.Entity.GetType().FullName);
                    EscribirErrorEnArchivoLog(InnerValidationError.PropertyName);
                    EscribirErrorEnArchivoLog(InnerValidationError.ErrorMessage);
                }
            }
        }

        private static void EscribirErrorEnArchivoLog(string Mensaje) {
            string RutaArchivoLog = HttpContext.Current.Server.MapPath("~/Log.txt");
            using (StreamWriter writer = new StreamWriter(RutaArchivoLog, true)) {
                try {
                    writer.WriteLine(Mensaje);
                }
                catch {
                    // Error al acceder al archivo
                }
            }
        }

        // Log de errores
        public async Task<IEnumerable<Errores>> ObtenerTodosErroresAsync() {
            var TareaObtenerTodos = RepositorioBD.ObtenerTodosErroresAsync();
            IEnumerable<Errores> ListaRegistrosErrores = await TareaObtenerTodos;
            return ListaRegistrosErrores;
        }
    }
}