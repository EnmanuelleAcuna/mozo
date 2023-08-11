using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Implementation.EntityFramework
{
    public partial class Mozo_DB_Context : DbContext
    {
        public Mozo_DB_Context()
        {
        }

        public Mozo_DB_Context(DbContextOptions<Mozo_DB_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TObjetos> TObjetos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=srv-mysql-ea.mysql.database.azure.com;user=azureuser;password=Talamanca08;database=Mozo;sslmode=Preferred;treattinyasboolean=true", x => x.ServerVersion("8.0.21-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TObjetos>(entity =>
            {
                entity.ToTable("t_objetos");

                entity.HasIndex(e => e.Nombre)
                    .HasName("U_NOMBRE")
                    .IsUnique();

                entity.HasIndex(e => e.Tabla)
                    .HasName("U_TABLA")
                    .IsUnique();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Nomenclatura)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Tabla)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
