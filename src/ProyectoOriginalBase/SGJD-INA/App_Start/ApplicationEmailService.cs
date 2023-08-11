using Microsoft.AspNet.Identity;
using SGJD_INA.Models.Core.Implementation;
using SGJD_INA.Models.Entities;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SGJD_INA {
    public class ApplicationEmailService : IIdentityMessageService {
        public async Task SendAsync(IdentityMessage Mensaje) {
            if (Mensaje is null) throw new ArgumentNullException(paramName: nameof(Mensaje), message: Resources.ModeloNulo);

            string CorreoEnvio = string.Empty;
            string ContrasennaCorreoEnvio = string.Empty;
            string ServidorSMTP = string.Empty;
            int PuertoSMTP = 0;
            bool UseSSL = false;

            var TareaObtenerParametrosCorreo = ParametrosGeneralesLogic.ObtenerParametrosCorreoStaticAsync();
            IEnumerable<ParametroGeneral> ParametrosCorreo = await TareaObtenerParametrosCorreo;
            foreach (ParametroGeneral ParametroCorreo in ParametrosCorreo) {
                CorreoEnvio = ParametroCorreo.Nombre == "CorreoEnvio" ? CorreoEnvio = ParametroCorreo.Valor : CorreoEnvio;
                ContrasennaCorreoEnvio = ParametroCorreo.Nombre == "Clave" ? ContrasennaCorreoEnvio = ParametroCorreo.Valor : ContrasennaCorreoEnvio;
                ServidorSMTP = ParametroCorreo.Nombre == "ServidorSMTP" ? ServidorSMTP = ParametroCorreo.Valor : ServidorSMTP;
                PuertoSMTP = ParametroCorreo.Nombre == "Puerto" ? PuertoSMTP = Convert.ToInt32(ParametroCorreo.Valor, new CultureInfo(Resources.CultureInfo)) : PuertoSMTP;
                UseSSL = ParametroCorreo.Nombre == "SSL" ? UseSSL = Convert.ToBoolean(ParametroCorreo.Valor, new CultureInfo(Resources.CultureInfo)) : UseSSL;
            }

            using (SmtpClient ClienteSMTP = new SmtpClient()) {
                ClienteSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                ClienteSMTP.EnableSsl = UseSSL;
                ClienteSMTP.Host = ServidorSMTP;
                ClienteSMTP.Port = PuertoSMTP;
                // Especificar credenciales para conectar a la cuenta que envía el correo
                ClienteSMTP.UseDefaultCredentials = true;
                ClienteSMTP.Credentials = new NetworkCredential(CorreoEnvio, ContrasennaCorreoEnvio);

                // Crear intancia de mensaje de correo nueva para asignar la propiedad IsBodyHtml true.
                using (MailMessage MensajeCorreo = new MailMessage(CorreoEnvio, Mensaje.Destination)) {
                    MensajeCorreo.Subject = Mensaje.Subject;
                    MensajeCorreo.Body = Mensaje.Body;
                    MensajeCorreo.IsBodyHtml = true;

                    await ClienteSMTP.SendMailAsync(MensajeCorreo);
                }
            }
        }

        public static async Task EnviarCorreo(string CorreoDestinatario, string Asunto, string Mensaje) {
            string CorreoEnvio = string.Empty;
            string ContrasennaCorreoEnvio = string.Empty;
            string ServidorSMTP = string.Empty;
            int PuertoSMTP = 0;
            bool UseSSL = false;

            var TareaObtenerParametrosCorreo = ParametrosGeneralesLogic.ObtenerParametrosCorreoStaticAsync();
            IEnumerable<ParametroGeneral> ParametrosCorreo = await TareaObtenerParametrosCorreo;
            foreach (ParametroGeneral ParametroCorreo in ParametrosCorreo) {
                CorreoEnvio = ParametroCorreo.Nombre == "CorreoEnvio" ? CorreoEnvio = ParametroCorreo.Valor : CorreoEnvio;
                ContrasennaCorreoEnvio = ParametroCorreo.Nombre == "Clave" ? ContrasennaCorreoEnvio = ParametroCorreo.Valor : ContrasennaCorreoEnvio;
                ServidorSMTP = ParametroCorreo.Nombre == "ServidorSMTP" ? ServidorSMTP = ParametroCorreo.Valor : ServidorSMTP;
                PuertoSMTP = ParametroCorreo.Nombre == "Puerto" ? PuertoSMTP = Convert.ToInt32(ParametroCorreo.Valor, new CultureInfo(Resources.CultureInfo)) : PuertoSMTP;
                UseSSL = ParametroCorreo.Nombre == "SSL" ? UseSSL = Convert.ToBoolean(ParametroCorreo.Valor, new CultureInfo(Resources.CultureInfo)) : UseSSL;
            }

            using (SmtpClient ClienteSMTP = new SmtpClient()) {
                ClienteSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                ClienteSMTP.EnableSsl = UseSSL;
                ClienteSMTP.Host = ServidorSMTP;
                ClienteSMTP.Port = PuertoSMTP;
                // Especificar credenciales para conectar a la cuenta que envía el correo
                ClienteSMTP.UseDefaultCredentials = true;
                ClienteSMTP.Credentials = new NetworkCredential(CorreoEnvio, ContrasennaCorreoEnvio);

                // Crear intancia de mensaje de correo nueva para asignar la propiedad IsBodyHtml true.
                using (MailMessage MensajeCorreo = new MailMessage(CorreoEnvio, CorreoDestinatario)) {
                    MensajeCorreo.Subject = Asunto;
                    MensajeCorreo.Body = Mensaje;
                    MensajeCorreo.IsBodyHtml = true;

                    await ClienteSMTP.SendMailAsync(MensajeCorreo);
                }
            }
        }

        /// <summary>
        /// Configurar el mensaje de correo para enviar
        /// </summary>
        public static string ConfigurarMensajeCorreo(string NombreDeUsuario, string Mensaje, string TextoBoton, string URL, string MensajeDetalle) {
            string RutaArchivoEnAplicacionEnServidor = HttpContext.Current.Server.MapPath("~/Content/Mail/Mail.html");

            // Configuracion de plantilla HTML
            string ContenidoCorreo = "";
            using (var reader = new StreamReader(RutaArchivoEnAplicacionEnServidor)) {
                ContenidoCorreo = reader.ReadToEnd();
            }

            // Nombre de usuario o correo de destinatario
            ContenidoCorreo = ContenidoCorreo.Replace("{UserName}", NombreDeUsuario);

            // Mensaje principal del correo
            ContenidoCorreo = ContenidoCorreo.Replace("{Contenido1}", Mensaje);
            ContenidoCorreo = ContenidoCorreo.Replace("{Contenido2}", MensajeDetalle);

            //Si se desea mostrar el boton en el correo  descomentar el siguiente codigo
            //openComment y closeComment deben ser llenados, de lo contrario, solo enviar vacio
            ContenidoCorreo = ContenidoCorreo.Replace("{OpenComment}", "");
            ContenidoCorreo = ContenidoCorreo.Replace("{CloseComment}", "");

            // Texto que se mostrará en el botón
            ContenidoCorreo = ContenidoCorreo.Replace("{TituloBotonIr}", TextoBoton);

            // URL del botón
            ContenidoCorreo = ContenidoCorreo.Replace("{Url}", URL);

            return ContenidoCorreo;
        }
    }
}