using Microsoft.AspNet.Identity.EntityFramework;
using SGJD_INA.Models.Entities;
using System.Configuration;
using System.Data.Entity;

namespace SGJD_INA {
    /// <summary>
    /// Clase encargada de generar un contexto de base de datos para Identity
    /// </summary>
    public class IdentityDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> {
        public IdentityDBContext() : base("IdentityConnection") {
            //Deshabilitar code-first migrations
            Database.SetInitializer<IdentityDBContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder ModelBuilder) {
            // Es necesario antes de cualquier regla
            base.OnModelCreating(ModelBuilder);

            // Remapear las tablas que identity necesita a las creadas en la base dedatos
            // Usuarios
            ModelBuilder.Entity<ApplicationUser>().ToTable("SGJD_ADM_TAB_USUARIOS");
            ModelBuilder.Entity<ApplicationUser>().HasKey(u => u.Id);
            ModelBuilder.Entity<ApplicationUser>().Property(u => u.Id).HasColumnName("LLP_Id");
            ModelBuilder.Entity<ApplicationUser>().Property(u => u.IdTipoUsuario).HasColumnName("LLF_IdTipoUsuario");
            ModelBuilder.Entity<ApplicationUser>().Property(u => u.IdUnidad).HasColumnName("LLF_IdUnidad");
            ModelBuilder.Entity<ApplicationUser>().HasRequired(au => au.TipoUsuario).WithMany(tu => tu.Usuarios).HasForeignKey(au => au.IdTipoUsuario);
            ModelBuilder.Entity<ApplicationUser>().HasRequired(au => au.Unidad).WithMany(u => u.Usuarios).HasForeignKey(au => au.IdUnidad);

            // Roles
            ModelBuilder.Entity<ApplicationRole>().ToTable("SGJD_ADM_TAB_ROLES");

            // Roles de usuario
            ModelBuilder.Entity<IdentityUserRole>().ToTable("SGJD_ADM_TAB_ROLES_USUARIO");

            // Unidades (Para usuario)
            ModelBuilder.Entity<Unidad>().ToTable("SGJD_ADM_TAB_UNIDADES");
            ModelBuilder.Entity<Unidad>().HasKey(u => u.Id);
            ModelBuilder.Entity<Unidad>().Property(u => u.Id).HasColumnName("LLP_Id");

            // Tipos de usuario (Para usuario)
            ModelBuilder.Entity<TipoUsuario>().ToTable("SGJD_ADM_TAB_TIPOS_USUARIO");
            ModelBuilder.Entity<TipoUsuario>().HasKey(tu => tu.Id);
            ModelBuilder.Entity<TipoUsuario>().Property(tu => tu.Id).HasColumnName("LLP_Id");
        }

        public static IdentityDBContext Create() {
            return new IdentityDBContext();
        }

        public static string ConectionString() {
            return ConfigurationManager.ConnectionStrings["IdentityConnection"].ConnectionString;
        }
    }
}