using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Tomo {
        // Constructores
        public Tomo() { }

        // Constructor para atributos
        public Tomo(SGJD_ACT_TAB_TOMOS TomoBD) {
            Id = TomoBD.LLP_Id;
            Numero = TomoBD.Numero;
            Nombre = TomoBD.Nombre;
            ObservacionApertura = TomoBD.ObservaciónApertura;
            ObservacionCierre = TomoBD.ObservacionCierre;
            IdUsuarioApertura = TomoBD.LLF_IdUsuarioApertura;
            IdUsuarioCierre = TomoBD.LLF_IdUsuarioCierre;
            FechaApertura = TomoBD.FechaApertura;
            FechaCierre = TomoBD.FechaCierre;
            IdEstado = TomoBD.LLF_IdEstado;
            Asiento = TomoBD.Asiento;
            UrlOficioApertura = TomoBD.UrlOficioApertura;
            UrlOficioCierre = TomoBD.UrlOficioCierre;
            ObservacionesMedianteOficio = TomoBD.ObservacionesMedianteOficio;
            NombreObjeto = TomoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        // Constructor para atributos y propiedades de navegación padre
        public Tomo(SGJD_ACT_TAB_TOMOS TomoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ADM_TAB_USUARIOS UsuarioAperturaBD, SGJD_ADM_TAB_USUARIOS UsuarioCierreBD) {
            Id = TomoBD.LLP_Id;
            Numero = TomoBD.Numero;
            Nombre = TomoBD.Nombre;
            ObservacionApertura = TomoBD.ObservaciónApertura;
            ObservacionCierre = TomoBD.ObservacionCierre;
            IdUsuarioApertura = TomoBD.LLF_IdUsuarioApertura;
            IdUsuarioCierre = TomoBD.LLF_IdUsuarioCierre;
            FechaApertura = TomoBD.FechaApertura;
            FechaCierre = TomoBD.FechaCierre;
            IdEstado = TomoBD.LLF_IdEstado;
            Asiento = TomoBD.Asiento;
            UrlOficioApertura = TomoBD.UrlOficioApertura;
            UrlOficioCierre = TomoBD.UrlOficioCierre;
            ObservacionesMedianteOficio = TomoBD.ObservacionesMedianteOficio;
            NombreObjeto = TomoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Estado = new Estado(EstadoBD);
            UsuarioApertura = new ApplicationUser(UsuarioAperturaBD);
            UsuarioCierre = string.IsNullOrEmpty(IdUsuarioCierre) ? null : new ApplicationUser(UsuarioCierreBD);
        }

        /// <summary>
        /// Constructor para atributos y propiedades hijo
        /// </summary>
        public Tomo(SGJD_ACT_TAB_TOMOS TomoBD, ICollection<SGJD_ACT_TAB_ACTAS> ActaBD) {
            Id = TomoBD.LLP_Id;
            Numero = TomoBD.Numero;
            Nombre = TomoBD.Nombre;
            ObservacionApertura = TomoBD.ObservaciónApertura;
            ObservacionCierre = TomoBD.ObservacionCierre;
            IdUsuarioApertura = TomoBD.LLF_IdUsuarioApertura;
            IdUsuarioCierre = TomoBD.LLF_IdUsuarioCierre;
            FechaApertura = TomoBD.FechaApertura;
            FechaCierre = TomoBD.FechaCierre;
            IdEstado = TomoBD.LLF_IdEstado;
            Asiento = TomoBD.Asiento;
            UrlOficioApertura = TomoBD.UrlOficioApertura;
            UrlOficioCierre = TomoBD.UrlOficioCierre;
            ObservacionesMedianteOficio = TomoBD.ObservacionesMedianteOficio;
            NombreObjeto = TomoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Actas = ActaBD.Select(a => new Acta(a)).ToList();
        }

        //Constructor para atributos y propiedades padre e hijo
        public Tomo(SGJD_ACT_TAB_TOMOS TomoBD, SGJD_ADM_TAB_ESTADOS EstadoBD, SGJD_ADM_TAB_USUARIOS UsuarioAperturaBD, SGJD_ADM_TAB_USUARIOS UsuarioCierreBD, ICollection<SGJD_ACT_TAB_ACTAS> ActaBD) {
            Id = TomoBD.LLP_Id;
            Numero = TomoBD.Numero;
            Nombre = TomoBD.Nombre;
            ObservacionApertura = TomoBD.ObservaciónApertura;
            ObservacionCierre = TomoBD.ObservacionCierre;
            IdUsuarioApertura = TomoBD.LLF_IdUsuarioApertura;
            IdUsuarioCierre = TomoBD.LLF_IdUsuarioCierre;
            FechaApertura = TomoBD.FechaApertura;
            FechaCierre = TomoBD.FechaCierre;
            IdEstado = TomoBD.LLF_IdEstado;
            Asiento = TomoBD.Asiento;
            UrlOficioApertura = TomoBD.UrlOficioApertura;
            UrlOficioCierre = TomoBD.UrlOficioCierre;
            ObservacionesMedianteOficio = TomoBD.ObservacionesMedianteOficio;
            NombreObjeto = TomoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Estado = new Estado(EstadoBD);
            UsuarioApertura = new ApplicationUser(UsuarioAperturaBD);
            UsuarioCierre = string.IsNullOrEmpty(IdUsuarioCierre) ? null : new ApplicationUser(UsuarioCierreBD);

            Actas = ActaBD.Select(a => new Acta(a, a.SGJD_ACT_TAB_SESIONES, a.SGJD_ACT_TAB_TOMOS, a.SGJD_ADM_TAB_ESTADOS, a.SGJD_ACT_TAB_SESIONES1, a.SGJD_ADM_TAB_USUARIOS, a.SGJD_ADM_TAB_USUARIOS1)).ToList();
        }

        /// <summary>
        /// Constructor para nuevo tomo que se está aperturando
        /// </summary>
        public Tomo(string ObservacionApertura, DateTime FechaApertura, ApplicationUser UsuarioApertura, int NumeroAsiento) {
            this.ObservacionApertura = ObservacionApertura;
            this.FechaApertura = FechaApertura;
            IdUsuarioApertura = UsuarioApertura.Id;
            Asiento = NumeroAsiento;

            this.UsuarioApertura = UsuarioApertura;
        }

        // Atributos
        public int Id { get; set; }

        public int Numero { get; set; }

        public string Nombre { get; set; }

        public string ObservacionApertura { get; set; }

        public string ObservacionCierre { get; set; }

        public string IdUsuarioApertura { get; set; }

        public string IdUsuarioCierre { get; set; }

        public DateTime FechaApertura { get; set; }

        public Nullable<DateTime> FechaCierre { get; set; }

        public int IdEstado { get; set; }

        public int Asiento { get; set; }

        public string UrlOficioApertura { get; set; }

        public string UrlOficioCierre { get; set; }

        public string ObservacionesMedianteOficio { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación
        // Padre
        public Estado Estado { get; set; }

        public ApplicationUser UsuarioApertura { get; set; }

        public ApplicationUser UsuarioCierre { get; set; }

        // Hijo
        public ICollection<Acta> Actas { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + "," +
                "Numero:" + Numero + "," +
                "Nombre:" + Nombre + "," +
                "Apertura:" + ObservacionApertura + "," +
                "Cierre:" + ObservacionCierre + "," +
                "FechaApertura:" + FechaApertura + "," +
                "FechaCierre:" + FechaCierre + "," +
                "Estdo:" + IdEstado + "," +
                "Asiento:" + Asiento + "," +
                "UrlOficioApertura:" + UrlOficioApertura + "," +
                "UrlOficioCierre:" + UrlOficioCierre + "," +
                "ObservacionesMedianteOficio:" + ObservacionesMedianteOficio;
        }

        public SGJD_ACT_TAB_TOMOS BD() {
            return new SGJD_ACT_TAB_TOMOS {
                LLP_Id = Id,
                Numero = Numero,
                Nombre = Nombre,
                ObservaciónApertura = ObservacionApertura,
                ObservacionCierre = ObservacionCierre,
                LLF_IdUsuarioApertura = IdUsuarioApertura,
                LLF_IdUsuarioCierre = IdUsuarioCierre,
                FechaApertura = FechaApertura,
                FechaCierre = FechaCierre,
                LLF_IdEstado = IdEstado,
                Asiento = Asiento,
                UrlOficioApertura = UrlOficioApertura,
                UrlOficioCierre = UrlOficioCierre,
                ObservacionesMedianteOficio = ObservacionesMedianteOficio,
            };
        }
    }
}