using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SGJD_INA.Models.Helpers {
    public static class Funciones {
        /// <summary>
        /// Convertir un int a su equivalente en palabras, Ej: 12 = Doce
        /// </summary>
        public static string TextToNumber(int N) {
            if (N == 0)
                return "cero";

            if (N < 0)
                return "menos " + TextToNumber(Math.Abs(N));

            string NumeroEnTexto = "";

            if ((N / 1000000) > 0) {
                if ((N / 1000000) == 1000000 || (N / 1000000) < 2000000) {
                    NumeroEnTexto += TextToNumber(N / 1000000) + " millón ";
                    N %= 1000000;
                }
                else {
                    NumeroEnTexto += TextToNumber(N / 1000000) + " millones ";
                    N %= 1000000;
                }
            }

            if ((N / 1000) > 0) {
                NumeroEnTexto += TextToNumber(N / 1000) + " mil ";
                N %= 1000;
            }

            if ((N / 100) > 0) {
                if ((N / 100) == 100) {
                    NumeroEnTexto += TextToNumber(N / 100) + " cien ";
                    N %= 100;
                }
                else if ((N / 100) > 100 && (N / 100) < 200) {
                    NumeroEnTexto += TextToNumber(N / 100) + " ciento ";
                    N %= 100;
                }
                else if ((N / 100) == 200 || ((N / 100) > 200 && (N / 100) < 300)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " doscientos ";
                    N %= 100;
                }
                else if ((N / 100) == 300 || ((N / 100) > 300 && (N / 100) < 400)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " trescientos ";
                    N %= 100;
                }
                else if ((N / 100) == 400 || ((N / 100) > 400 && (N / 100) < 500)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " cuatrocientos ";
                    N %= 100;
                }
                else if ((N / 100) == 500 || ((N / 100) > 500 && (N / 100) < 600)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " quinientos ";
                    N %= 100;
                }
                else if ((N / 100) == 600 || ((N / 100) > 600 && (N / 100) < 700)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " seiscientos ";
                    N %= 100;
                }
                else if ((N / 100) == 700 || ((N / 100) > 700 && (N / 100) < 800)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " setecientos ";
                    N %= 100;
                }
                else if ((N / 100) == 800 || ((N / 100) > 800 && (N / 100) < 900)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " ochocientos ";
                    N %= 100;
                }
                else if ((N / 100) == 900 || ((N / 100) > 900 && (N / 100) < 1000)) {
                    NumeroEnTexto += TextToNumber(N / 100) + " novecientos ";
                    N %= 100;
                }
            }

            if (N > 0) {
                var unitsMap = new[] { "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
                var tensMap = new[] { "cero", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };

                if (N < 20)
                    NumeroEnTexto += unitsMap[N];
                else {
                    NumeroEnTexto += tensMap[N / 10];
                    if ((N % 10) > 0)
                        NumeroEnTexto += " y " + unitsMap[N % 10];
                }
            }

            return NumeroEnTexto;
        }

        /// <summary>
        /// Convertir un int con número ordial a una cadena de texto con su equivalente a número romano
        /// </summary>
        public static string OrdinalToRoman(int Numero) {
            List<string> ListaNumeralRomano = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            List<int> ListaNumerales = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            var NumeralRomano = string.Empty;
            while (Numero > 0) {
                //Buscar el numeral más alto que es menor o igual a Numero
                var index = ListaNumerales.FindIndex(x => x <= Numero);
                //Sustraer su valor desde Numero
                Numero -= ListaNumerales[index];
                //Posicionarlo al final del numeral romano
                NumeralRomano += ListaNumeralRomano[index];
            }
            return NumeralRomano;
        }

        /// <summary>
        /// Verificar si una cadena de texto es un número
        /// </summary>
        public static bool IsNumber(string S) {
            return int.TryParse(S, out int _);
        }

        /// <summary>
        /// Verificar si una cadena de texto es un correo electrónico válido
        /// </summary>
        public static bool IsValidEmail(string S) {
            try {
                var addr = new System.Net.Mail.MailAddress(S);
                return addr.Address == S;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Verificar si una cadena de texto es una fecha válida
        /// </summary>
        public static bool IsDate(string S) {
            if (string.IsNullOrEmpty(S)) throw new ArgumentNullException(paramName: nameof(S), message: Resources.ModeloNulo);

            try {
                if (S.Length != 10) {
                    return false;
                }

                CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

                int Dia = Convert.ToInt16(S.Substring(0, 2), CultureInfo);
                if (Dia <= 0 || Dia > 31) {
                    return false;
                }

                int Mes = Convert.ToInt16(S.Substring(3, 2), CultureInfo);
                if (Mes <= 0 || Mes > 12) {
                    return false;
                }

                int Anno = Convert.ToInt16(S.Substring(6, 4), CultureInfo);
                if (Anno <= 1900) {
                    return false;
                }

                return DateTime.TryParse(S, out DateTime _);
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Convertir una cadena de texto a TitleCase, ej: Pablo Pérez Rojas
        /// </summary>
        public static string ToTitleCase(string S) {
            if (string.IsNullOrEmpty(S)) throw new ArgumentNullException(paramName: nameof(S), message: Resources.ModeloNulo);

            CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);
            TextInfo TextInfo = CultureInfo.TextInfo;
            S = TextInfo.ToTitleCase(S.ToLower(CultureInfo));
            return S;
        }

        /// <summary>
        /// Convertir una cadena de texto con la hora a una fecha con hora
        /// </summary>
        public static DateTime JoinStringToDateTime(DateTime Fecha, string H) {
            if (string.IsNullOrEmpty(H)) throw new ArgumentNullException(paramName: nameof(H), message: Resources.ModeloNulo);

            try {
                CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

                // Ejemplo de h: 06:00 AM
                int hora = Convert.ToInt16(H.Substring(0, 2), CultureInfo);
                int minutos = Convert.ToInt16(H.Substring(3, 2), CultureInfo);
                string ampm = H.Substring(6, 2);

                if (ampm.ToLower(CultureInfo).Contains("p")) hora += 12;

                return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, hora, minutos, 00, DateTimeKind.Local);
            }
            catch {
                return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day);
            }
        }

        /// <summary>
        /// Convertir una cadena de texto a una fecha con hora
        /// </summary>
        public static DateTime StringToDateTime(string FechaHora) {
            //if (string.IsNullOrEmpty(FechaHora)) throw new ArgumentNullException(paramName: nameof(FechaHora), message: Resources.ModeloNulo);

            try {
                CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

                // 31/01/2020 o 31/01/2020 06:00 AM
                if (FechaHora.Length == 10) {
                    int Dia = Convert.ToInt16(FechaHora.Substring(0, 2), CultureInfo);
                    int Mes = Convert.ToInt16(FechaHora.Substring(3, 2), CultureInfo);
                    int Anno = Convert.ToInt16(FechaHora.Substring(6, 4), CultureInfo);
                    return new DateTime(Anno, Mes, Dia, 0, 0, 0, DateTimeKind.Local);
                }
                else if (FechaHora.Length == 19) {
                    int Dia = Convert.ToInt16(FechaHora.Substring(0, 2), CultureInfo);
                    int Mes = Convert.ToInt16(FechaHora.Substring(3, 2), CultureInfo);
                    int Anno = Convert.ToInt16(FechaHora.Substring(6, 4), CultureInfo);
                    int Hora = Convert.ToInt16(FechaHora.Substring(11, 2), CultureInfo);
                    int Minutos = Convert.ToInt16(FechaHora.Substring(14, 2), CultureInfo);
                    string AMPM = FechaHora.Substring(17, 2);
                    if (AMPM.ToLower(CultureInfo).Contains("p")) Hora += 12;
                    return new DateTime(Anno, Mes, Dia, Hora, Minutos, 0, DateTimeKind.Local);
                }
                else {
                    return new DateTime(1990, 01, 01, 0, 0, 0, DateTimeKind.Local);
                }
            }
            catch {
                return new DateTime(1990, 01, 01, 0, 0, 0, DateTimeKind.Local);
            }
        }

        public static string ObtenerMensajeExito(string Accion, string NombreObjeto) {
            switch (Accion) {
                //Agregar o crear: se aplica en ambos casos
                case "A":
                case "C":
                    return "" + NombreObjeto + " agregado(a).";
                //Modificar
                case "M":
                    return "" + NombreObjeto + " actualizado(a).";
                //Eliminar
                case "E":
                    return "" + NombreObjeto + " eliminado(a).";
                //Busquedas
                case "B":
                    return "" + NombreObjeto + " encontrados(as).";
                //Habilitar
                case "H":
                    return "" + NombreObjeto + " habilitado(a).";
                case "I":
                    return "" + NombreObjeto + " inhabilitado(a).";
                default:
                    return "Operación realizada.";
            }
        }

        public static string ObtenerMensajeError(Exception Error, string Accion, string NombreObjeto) {
            if (Error is null) throw new ArgumentNullException(paramName: nameof(Error), message: Resources.ModeloNulo);
            //if (string.IsNullOrEmpty(Accion)) throw new ArgumentNullException(paramName: nameof(Accion), message: Resources.ModeloNulo);
            //if (string.IsNullOrEmpty(NombreObjeto)) throw new ArgumentNullException(paramName: nameof(NombreObjeto), message: Resources.ModeloNulo);

            if (string.IsNullOrEmpty(Accion) || string.IsNullOrEmpty(NombreObjeto) || Error != null) {
                return "Error al procesar su solicitud." + ", " + Error.GetType().Name;
            }

            CultureInfo CultureInfo = new CultureInfo(Resources.CultureInfo);

            if (Error.InnerException != null) {
                return "Error al " + Accion.ToLower(CultureInfo) + " " + NombreObjeto + ", " + Error.InnerException.GetType().Name;
            }

            return "Error al " + Accion.ToLower(CultureInfo) + " " + NombreObjeto + ", " + Convert.ToString(Error.GetType().Name, CultureInfo);
        }

        /// <summary>
        /// Obtener la cédula que está dentro del hash de un certificado de Firma Digital
        /// </summary>
        public static string ObtenerCedulaCertificado(string HashCertificado) {
            if (string.IsNullOrEmpty(HashCertificado)) throw new ArgumentNullException(paramName: nameof(HashCertificado), message: Resources.ModeloNulo);

            HashCertificado = HashCertificado.Replace("-----BEGIN CERTIFICATE-----", "").Replace("-----END CERTIFICATE-----", "").Replace("\n", "").Replace("\r", "");
            byte[] Bytes = Convert.FromBase64String(HashCertificado);

            using (X509Certificate2 InfoCertificado = new X509Certificate2(Bytes)) {
                string SubjectCertificado = InfoCertificado.Subject;
                string[] ArraySubjectCertificado = SubjectCertificado.Split(',');
                string Campo6 = ArraySubjectCertificado[6];
                string[] ArrayCampo6 = Campo6.Split('=');
                string Campo1 = ArrayCampo6[1];
                string Cedula = Campo1.Substring(5).Replace("-", "");
                return Cedula;
            }
        }

        /// <summary>
        /// Obtener la direccion IP del equipo remoto que hace la solicitud
        /// </summary>
        public static string ObtenerIP(HttpRequestBase Request) {
            if (Request is null) throw new ArgumentNullException(paramName: nameof(Request), message: Resources.ModeloNulo);

            string IP;

            try {
                IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(IP)) {
                    if (IP.IndexOf(",", StringComparison.OrdinalIgnoreCase) > 0) {
                        string[] IPRange = IP.Split(',');
                        int IPLenght = IPRange.Length - 1;
                        IP = IPRange[IPLenght];
                    }
                }
                else {
                    IP = Request.UserHostAddress;
                }
            }
            catch {
                IP = string.Empty;
            }

            return IP;
        }

        /// <summary>
        /// Obtener nombre del equipo remoto que hace la solicitud
        /// </summary>
        public static string ObtenerInfoEquipo(HttpRequestBase Request) {
            if (Request is null) throw new ArgumentNullException(paramName: nameof(Request), message: Resources.ModeloNulo);

            string HostInfo = string.Format(new CultureInfo(Resources.CultureInfo), "{0}, {1}, {2}, {3}", Request.Browser.Browser, Request.Browser.Platform, Request.UserHostName, Request.UserAgent);
            return HostInfo;
        }

        /// <summary>
        /// Obtener errores al ejecutar el sistema
        /// </summary>
        public static void LogError(Exception ex) {
            using (var ContextoBD = SGJDDBContext.Create()) {
                try {

                    //Capturar información de origen con la excepción generada
                    StackTrace st = new StackTrace(ex, true);
                    StackFrame frame = st.GetFrame(1);

                    MethodBase metodo = frame.GetMethod();

                    //Obtiene el nombre del archivo
                    string NombreDelArchivo = frame.GetFileName();

                    //Obtiene el nombre del metodo
                    string NombreDelMetodo = metodo.Name;

                    //Obtiene el número de linea de la pila de ejecución
                    int Linea = frame.GetFileLineNumber();

                    //Obtiene el codigo de excepción
                    string CodigoExcepcion = ex.HResult.ToString();

                    string ErrorMessage = ex.ToString();

                    var error = new SGJD_ADM_TAB_ERRORES {
                        Error = ErrorMessage,
                        Codigo_Excepcion = CodigoExcepcion,
                        Linea_Error = Linea,
                        Nombre_Archivo = NombreDelArchivo,
                        Nombre_Metodo = NombreDelMetodo,
                        Fecha = DateTime.Now
                    };

                    ContextoBD.Entry(error).State = EntityState.Added;
                    ContextoBD.SaveChanges();
                }
                catch (Exception) {
                }
            }
        }
    }
}