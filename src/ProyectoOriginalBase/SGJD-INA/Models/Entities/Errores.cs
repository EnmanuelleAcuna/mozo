using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.Entities {
    public class Errores {
        //Constructores
        public Errores() { }

        public Errores(SGJD_ADM_TAB_ERRORES ErroresBD) {
            Id = ErroresBD.LLP_Id;
            Fecha = ErroresBD.Fecha;
            Error = ErroresBD.Error;
            NombreArchivo = ErroresBD.Nombre_Archivo;
            NombreMetodo = ErroresBD.Nombre_Metodo;
            CodigoExcepcion = ErroresBD.Codigo_Excepcion;
            LineaError = ErroresBD.Linea_Error;
        }

        //Propiedades
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Error { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreMetodo { get; set; }
        public string CodigoExcepcion { get; set; }
        public int? LineaError { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "id:" + Id + "," +
                "fecha:" + Fecha + "," +
                "error:" + Error + "," +
                "nombreArchivo:" + NombreArchivo + "," +
                "nombreMetodo:" + NombreMetodo + "," +
                "codigoExtencion:" + CodigoExcepcion + "," +
                "lineaError:" + LineaError;
        }

        /// <summary>
        /// Convertir entidad a modelo de base de datos
        /// </summary>
        public SGJD_ADM_TAB_ERRORES BD() {
            return new SGJD_ADM_TAB_ERRORES() {
                LLP_Id = Id,
                Fecha = Fecha,
                Error = Error,
                Nombre_Archivo = NombreArchivo,
                Nombre_Metodo = NombreMetodo,
                Codigo_Excepcion = CodigoExcepcion,
                Linea_Error = LineaError
            };
        }
    }
}