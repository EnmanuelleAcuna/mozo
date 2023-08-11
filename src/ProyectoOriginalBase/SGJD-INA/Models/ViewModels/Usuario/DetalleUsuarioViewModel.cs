using SGJD_INA.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGJD_INA.Models.ViewModels {
    public class DetalleUsuarioViewModel {
        // Constructores
        public DetalleUsuarioViewModel() { }

        public DetalleUsuarioViewModel(ApplicationUser ModeloUsuario) {
            Id = ModeloUsuario.Id;
            Identificacion = ModeloUsuario.Cedula;
            Nombre = ModeloUsuario.Nombre;
            Rol = ModeloUsuario.Rol.Name;
            Unidad = ModeloUsuario.Unidad.Nombre;
            TipoUsuario = ModeloUsuario.TipoUsuario.Nombre;
            Correo = ModeloUsuario.Email;
            NombreUsuario = ModeloUsuario.UserName;
            Estado = ModeloUsuario.Activo ? "Activo" : "Inactivo";
            UltimaConexion = ModeloUsuario.UltimaSesion;
        }

        // Propiedades
        public string Id { get; set; }

        [Display(Name = "No. de identificacion")]
        public string Identificacion { get; set; }

        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Display(Name = "Unidad")]
        public string Unidad { get; set; }

        [Display(Name = "Tipo de usuario")]
        public string TipoUsuario { get; set; }

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Display(Name = "Nombre de usuario (A.D INA)")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Último inicio de sesión")]
        public DateTime? UltimaConexion { get; set; }
    }
}