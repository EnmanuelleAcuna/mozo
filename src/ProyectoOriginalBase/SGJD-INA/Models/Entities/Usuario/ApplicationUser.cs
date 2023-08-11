using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SGJD_INA.Models.DA.EntityFramework.SGJD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SGJD_INA.Models.Entities {
    /// <summary>
    /// Clase ApplicationUser para ser utilizado con Identity
    /// </summary>
    public class ApplicationUser : IdentityUser {
        // Constructores
        public ApplicationUser() : base() { }

        //public ApplicationUser(string UserName) : base(UserName) { }

        public ApplicationUser(string IdUsuario) {
            Id = IdUsuario;
        }

        public ApplicationUser(SGJD_ADM_TAB_USUARIOS UsuarioBD, IEnumerable<SGJD_ADM_TAB_ROLES> RolesBD) {
            Id = UsuarioBD.LLP_Id;
            Nombre = UsuarioBD.Nombre;

            Rol = new ApplicationRole(RolesBD.FirstOrDefault());
        }

        public ApplicationUser(SGJD_ADM_TAB_USUARIOS UsuarioBD) {
            Id = UsuarioBD.LLP_Id;
            UserName = UsuarioBD.UserName;
            Email = UsuarioBD.Email;
            Cedula = UsuarioBD.Cedula;
            Nombre = UsuarioBD.Nombre;
            UltimaSesion = UsuarioBD.UltimaSesion;
            Activo = UsuarioBD.Activo;
            IdUnidad = UsuarioBD.LLF_IdUnidad;
            IdTipoUsuario = UsuarioBD.LLF_IdTipoUsuario;
            NombreObjeto = UsuarioBD.GetType().UnderlyingSystemType.BaseType.Name;
        }

        public ApplicationUser(SGJD_ADM_TAB_USUARIOS UsuarioBD, SGJD_ADM_TAB_UNIDADES UnidadBD, SGJD_ADM_TAB_TIPOS_USUARIO TipoUsuarioBD, IEnumerable<SGJD_ADM_TAB_ROLES> RolesBD) {
            Id = UsuarioBD.LLP_Id;
            UserName = UsuarioBD.UserName;
            Email = UsuarioBD.Email;
            Cedula = UsuarioBD.Cedula;
            Nombre = UsuarioBD.Nombre;
            UltimaSesion = UsuarioBD.UltimaSesion;
            Activo = UsuarioBD.Activo;
            IdUnidad = UsuarioBD.LLF_IdUnidad;
            IdTipoUsuario = UsuarioBD.LLF_IdTipoUsuario;
            NombreObjeto = UsuarioBD.GetType().UnderlyingSystemType.BaseType.Name;

            Unidad = new Unidad(UnidadBD);
            TipoUsuario = new TipoUsuario(TipoUsuarioBD);
            Rol = new ApplicationRole(RolesBD.FirstOrDefault());
        }

        // Propiedades
        [Required]
        public string Cedula { get; set; }

        [Required]
        public string Nombre { get; set; }

        public DateTime? UltimaSesion { get; set; } = DateTime.Now;

        [Required]
        public bool Activo { get; set; } = true;

        [Required]
        public int IdUnidad { get; set; }

        [Required]
        public int IdTipoUsuario { get; set; }

        [NotMapped]
        public string NombreObjeto { get; set; }

        public virtual Unidad Unidad { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        // Debido a que la aplicación sólo utiliza un rol por usuario, se crea esta propiedad de navegación.
        // Con esta propiedad ayuda a la vista de Inicio y Edición de Usuario, sin necesidad de crear lógica adicional para consultar los roles del
        // usuario, cuando sólo es uno.
        [NotMapped]
        public virtual ApplicationRole Rol { get; set; }

        #region Propiedades de Identity que no deben ser mapeadas a la base de datos
        [NotMapped]
        public override bool EmailConfirmed { get; set; }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [NotMapped]
        public override DateTime? LockoutEndDateUtc { get; set; }

        [NotMapped]
        public override bool LockoutEnabled { get; set; }

        [NotMapped]
        public override int AccessFailedCount { get; set; }
        #endregion

        // Métodos
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> Manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var TareaCrearIdentity = Manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            ClaimsIdentity UserIdentity = await TareaCrearIdentity;

            // Obtener el id y nombre del rol del usuario
            string IdRol = Roles.FirstOrDefault().RoleId;
            var TareaObtenerNombresDeRolesDelUsuario = Manager.GetRolesAsync(Id);
            IEnumerable<string> NombresDeRolesDelUsuario = await TareaObtenerNombresDeRolesDelUsuario;
            string NombreRol = NombresDeRolesDelUsuario.FirstOrDefault();

            // Add custom user claims here
            UserIdentity.AddClaim(new Claim("NombreCompleto", Nombre));
            UserIdentity.AddClaim(new Claim("IdPerfil", IdRol));
            UserIdentity.AddClaim(new Claim("IdRol", IdRol));
            UserIdentity.AddClaim(new Claim("NombreRol", NombreRol));
            UserIdentity.AddClaim(new Claim("IdUnidad", Convert.ToString(IdUnidad)));
            UserIdentity.AddClaim(new Claim("NombreUnidad", Unidad.Nombre));
            UserIdentity.AddClaim(new Claim("IdTipoUsuario", Convert.ToString(IdTipoUsuario)));
            UserIdentity.AddClaim(new Claim("NombreTipoUsuario", TipoUsuario.Nombre));

            return UserIdentity;
        }

        public string ObtenerInformacion() {
            return "Nombre Objeto: " + NombreObjeto + "," +
                "id:" + Id + ", " +
                "cedula" + Cedula + ", " +
                "nombre" + Nombre + ", " +
                "correo" + Email + ", " +
                "username" + UserName + ", " +
                "ultimaSesion" + UltimaSesion + "," +
                "activo" + Activo + ", " +
                "idUnidad:" + IdUnidad + ", " +
                "idTipoUsuario" + IdTipoUsuario;
        }

        public SGJD_ADM_TAB_USUARIOS BD() {
            return new SGJD_ADM_TAB_USUARIOS() {
                LLP_Id = Id
            };
        }
    }
}