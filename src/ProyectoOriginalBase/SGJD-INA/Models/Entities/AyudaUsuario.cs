using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGJD_INA.Models.Entities {
    public class AyudaUsuario {
        //Constructores
        public AyudaUsuario() { }

        public AyudaUsuario(SGJD_ADM_TAB_AYUDA_USUARIO AyudaUsuarioBD) {
            Id = AyudaUsuarioBD.LLP_Id;
            NombreModulo = AyudaUsuarioBD.NombreModulo;
            NombreVista = AyudaUsuarioBD.NombreVista;
            MensajeAyuda = AyudaUsuarioBD.MensajeAyuda;
            Abreviatura = AyudaUsuarioBD.Abreviatura;
            NombreObjeto = AyudaUsuarioBD.GetType().UnderlyingSystemType.BaseType.Name;
        }       

        //Propiedades
        public int Id { get; set; }

        public string NombreModulo { get; set; }

        public string NombreVista { get; set; }

        public string MensajeAyuda { get; set; }

        public string Abreviatura { get; set; }

        public string NombreObjeto { get; set; }

        // Métodos
        public string ObtenerInformacion() {
            return "Nombre Objeto" + NombreObjeto + "," +
                "id:" + Id + "," +
                "NombreModulo:" + NombreModulo + "," +
                "NombreVista:" + NombreVista + "," +
                "MensajeAyuda:" + MensajeAyuda + "," +
                "Abreviatura:" + Abreviatura;
        }

        /// <summary>
        /// Convertir entidad a modelo de base de datos
        /// </summary>
        public SGJD_ADM_TAB_AYUDA_USUARIO BD () {
            return new SGJD_ADM_TAB_AYUDA_USUARIO() {
                LLP_Id = Id,
                NombreModulo = NombreModulo,
                NombreVista = NombreVista,
                MensajeAyuda = MensajeAyuda,
                Abreviatura = Abreviatura
            };
        }
    }
}