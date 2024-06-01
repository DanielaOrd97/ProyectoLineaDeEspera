using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace API_Linea_Espera.Models.Entities;

public partial class SistemaDeEspera1Context : DbContext
{
    public SistemaDeEspera1Context()
    {
    }

    public SistemaDeEspera1Context(DbContextOptions<SistemaDeEspera1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cajas> Cajas { get; set; }

    public virtual DbSet<Estadosturno> Estadosturno { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Turnos> Turnos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=SistemaDeEspera1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cajas>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("PRIMARY");

            entity.ToTable("cajas");

            entity.Property(e => e.NombreCaja).HasMaxLength(50);
        });

        modelBuilder.Entity<Estadosturno>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PRIMARY");

            entity.ToTable("estadosturno");

            entity.Property(e => e.Estado).HasMaxLength(25);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Turnos>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PRIMARY");

            entity.ToTable("turnos");

            entity.HasIndex(e => e.CajaId, "CajaId");

            entity.HasIndex(e => e.EstadoId, "EstadoId");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.HasOne(d => d.Caja).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.CajaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("turnos_ibfk_2");

            entity.HasOne(d => d.Estado).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("turnos_ibfk_3");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("turnos_ibfk_1");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.IdCaja, "IdCaja");

            entity.HasIndex(e => e.IdRol, "IdRol");

            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCaja)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuarios_ibfk_2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
