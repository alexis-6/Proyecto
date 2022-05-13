using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProyectoBL.Models
{
    public partial class BdParcialContext : DbContext
    {
        public BdParcialContext()
        {
        }

        public BdParcialContext(DbContextOptions<BdParcialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Encargado> Encargados { get; set; }
        public virtual DbSet<Prioridad> Prioridades { get; set; }
        public virtual DbSet<Requerimiento> Requerimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RFU3D2R\\SQLEXPRESS;Database=BdParcial;User ID=parcial;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.ToTable("AREA");

                entity.Property(e => e.IdArea).HasColumnName("ID_AREA");

                entity.Property(e => e.DArea)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("D_AREA");
            });

            modelBuilder.Entity<Encargado>(entity =>
            {
                entity.HasKey(e => e.IdEncargado);

                entity.ToTable("ENCARGADO");

                entity.Property(e => e.IdEncargado).HasColumnName("ID_ENCARGADO");

                entity.Property(e => e.NombreEncargado)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ENCARGADO");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad);

                entity.ToTable("PRIORIDAD");

                entity.Property(e => e.IdPrioridad).HasColumnName("ID_PRIORIDAD");

                entity.Property(e => e.DPrioridad)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("D_PRIORIDAD");
            });

            modelBuilder.Entity<Requerimiento>(entity =>
            {
                entity.HasKey(e => e.IdRequerimiento);

                entity.ToTable("REQUERIMIENTO");

                entity.Property(e => e.IdRequerimiento).HasColumnName("ID_REQUERIMIENTO");

                entity.Property(e => e.Alcance)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ALCANCE");

                entity.Property(e => e.Aplicativo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("APLICATIVO");

                entity.Property(e => e.DiasDesarrollo).HasColumnName("DIAS_DESARROLLO");

                entity.Property(e => e.FechaDesarrollo)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_DESARROLLO");

                entity.Property(e => e.FechaPrueba)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_PRUEBA");

                entity.Property(e => e.FechaSolicitud)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_SOLICITUD");

                entity.Property(e => e.IdArea).HasColumnName("ID_AREA");

                entity.Property(e => e.IdEncargado).HasColumnName("ID_ENCARGADO");

                entity.Property(e => e.IdPrioridad).HasColumnName("ID_PRIORIDAD");

                entity.HasOne(d => d.DArea)
                    .WithMany(p => p.Requerimientos)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TB_AREA");

                entity.HasOne(d => d.NombreEncargado)
                    .WithMany(p => p.Requerimientos)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBENCARGADO_TBREQUERIMIENTO");

                entity.HasOne(d => d.DPrioridad)
                    .WithMany(p => p.Requerimientos)
                    .HasForeignKey(d => d.IdPrioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBPRIORIDAD_TBREQUERIMIENTO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
