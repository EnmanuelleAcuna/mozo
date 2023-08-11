using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGJD_INA.Models.Entities {
    public class Aviso {
        public Aviso() { }

        /// <summary>
        /// Constructor para atributos
        /// </summary>
        public Aviso(SGJD_ADM_TAB_AVISOS AvisoBD) {
            Id = AvisoBD.LLP_Id;
            Nombre = AvisoBD.Nombre;
            Mensaje = AvisoBD.Mensaje;
            Tipo = AvisoBD.Tipo.Equals("Notificacion") ? TipoAviso.Notificacion : TipoAviso.Correo;
            TipoDestinatario = AvisoBD.TipoDestinatario.Equals("Unidad") ? TipoDestinatario.Unidad : TipoDestinatario.Usuarios;
            IdUnidad = AvisoBD.LLF_IdUnidad;
            NombreObjeto = AvisoBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        /// <summary>
        /// Constructor para atributos y propiedades padre
        /// </summary>
        public Aviso(SGJD_ADM_TAB_AVISOS AvisoBD, SGJD_ADM_TAB_UNIDADES UnidadBD, ICollection<SGJD_ADM_TAB_USUARIOS> UsuariosBD) {
            Id = AvisoBD.LLP_Id;
            Nombre = AvisoBD.Nombre;
            Mensaje = AvisoBD.Mensaje;
            Tipo = AvisoBD.Tipo.Equals("Notificacion") ? TipoAviso.Notificacion : TipoAviso.Correo;
            TipoDestinatario = AvisoBD.TipoDestinatario.Equals("Unidad") ? TipoDestinatario.Unidad : TipoDestinatario.Usuarios;
            IdUnidad = AvisoBD.LLF_IdUnidad;
            NombreObjeto = AvisoBD.GetType().UnderlyingSystemType.BaseType.Name;

            Unidad = AvisoBD.LLF_IdUnidad == null ? new Unidad() : new Unidad(UnidadBD);
            Usuarios = UsuariosBD.Select(u => new ApplicationUser(u)).ToList();
        }

        /// <summary>
        /// Constructor para atributos y relacion con usuario para aviso de acuerdo
        /// </summary>
        public Aviso(SGJD_ADM_TAB_AVISOS AvisoBD, SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = AvisoBD.LLP_Id;
            Nombre = AvisoBD.Nombre;
            Mensaje = AvisoBD.Mensaje;
            Tipo = AvisoBD.Tipo.Equals("Notificacion") ? TipoAviso.Notificacion : TipoAviso.Correo;
            TipoDestinatario = AvisoBD.TipoDestinatario.Equals("Unidad") ? TipoDestinatario.Unidad : TipoDestinatario.Usuarios;
            IdUnidad = AvisoBD.LLF_IdUnidad;
            NombreObjeto = AvisoBD.GetType().UnderlyingSystemType.BaseType.Name;

            UsuarioAvisoAcuerdo = new ApplicationUser(UsuarioBD);
        }

        [Display(Name = "Aviso")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El mensaje es requerido.")]
        public string Mensaje { get; set; }

        [Required(ErrorMessage = "El tipo de aviso es requerido.")]
        public TipoAviso Tipo { get; set; }

        [Required(ErrorMessage = "El tipo destinatario de la tabla es requerido.")]
        public TipoDestinatario TipoDestinatario { get; set; }

        public int? IdUnidad { get; set; }

        public string NombreObjeto { get; set; }

        // Propiedades de navegacion
        // Padres
        public Unidad Unidad { get; set; }

        public ICollection<ApplicationUser> Usuarios { get; set; }

        //Hijo
        public ApplicationUser UsuarioAvisoAcuerdo { get; set; }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "nombre:" + Nombre + ", " +
                "mensaje:" + Mensaje + ", " +
                "tipoDestinatario:" + TipoDestinatario + ", " +
                "idUnidad:" + IdUnidad + ", " +
                "tipo" + Tipo;
        }

        public SGJD_ADM_TAB_AVISOS BD() {
            return new SGJD_ADM_TAB_AVISOS() {
                LLP_Id = Id,
                Nombre = Nombre,
                Mensaje = Mensaje,
                Tipo = Tipo.ToString(),
                TipoDestinatario = TipoDestinatario.ToString(),
                LLF_IdUnidad = IdUnidad
            };
        }
    }
}