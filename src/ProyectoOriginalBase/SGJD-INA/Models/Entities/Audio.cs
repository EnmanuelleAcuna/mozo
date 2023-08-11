using SGJD_INA.Models.DA.EntityFramework.SGJD;
using SGJD_INA.Properties;
using System;
using System.Web;

namespace SGJD_INA.Models.Entities {
    public class Audio {
        //Constructores
        public Audio() { }

        public Audio(SGJD_ADM_TAB_AUDIOS AudioBD) {
            if (AudioBD is null) {
                throw new ArgumentNullException(paramName: nameof(AudioBD), message: Resources.ModeloNulo);
            }

            Id = AudioBD.LLP_Id;
            IdActa = AudioBD.LLF_IdActa;
            Nombre = AudioBD.Nombre;
            UrlAudio = AudioBD.UrlAudio;
            Transcripcion = AudioBD.Transcripcion;
            NombreObjeto = AudioBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        //Propiedades
        public int Id { get; set; }

        public int IdActa { get; set; }

        public string Nombre { get; set; }

        public string UrlAudio { get; set; }

        public string Transcripcion { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegación 
        // Padres
        public Acta Acta { get; set; }

        public HttpPostedFileBase Archivo { get; set; }

        public Repositorio Repositorio { get; set; }

        public string NombreCarpeta { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto" + NombreObjeto + "," +
                "Id:" + Id + ", " +
                "IdActa" + IdActa + ", " +
                "Nombre" + Nombre + ", " +
                "UrlAudio" + UrlAudio;
        }

        public SGJD_ADM_TAB_AUDIOS BD() {
            return new SGJD_ADM_TAB_AUDIOS() {
                LLP_Id = this.Id,
                LLF_IdActa = this.IdActa,
                Nombre = this.Nombre,
                UrlAudio = this.UrlAudio,
                Transcripcion = this.Transcripcion
            };
        }
    }
}