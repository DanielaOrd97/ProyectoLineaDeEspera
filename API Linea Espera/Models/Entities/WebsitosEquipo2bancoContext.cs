using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace API_Linea_Espera.Models.Entities;

public partial class WebsitosEquipo2bancoContext : DbContext
{
    public WebsitosEquipo2bancoContext()
    {
    }

    public WebsitosEquipo2bancoContext(DbContextOptions<WebsitosEquipo2bancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cajas> Cajas { get; set; }

    public virtual DbSet<Estadosturno> Estadosturno { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Turnos> Turnos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=65.181.111.21;user=websitos_Banco2;database=websitos_equipo2banco;password=7S%90rfg6", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.7-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Cajas>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("PRIMARY");

            entity.ToTable("cajas");

            entity.Property(e => e.IdCaja).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasColumnType("tinyint(4)");
            entity.Property(e => e.NombreCaja).HasMaxLength(50);
        });

        modelBuilder.Entity<Estadosturno>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PRIMARY");

            entity.ToTable("estadosturno");

            entity.Property(e => e.IdEstado).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasMaxLength(25);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnType("int(11)");
            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Turnos>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PRIMARY");

            entity.ToTable("turnos");

            entity.HasIndex(e => e.CajaId, "CajaId");

            entity.HasIndex(e => e.EstadoId, "EstadoId");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.IdTurno).HasColumnType("int(11)");
            entity.Property(e => e.CajaId).HasColumnType("int(11)");
            entity.Property(e => e.EstadoId).HasColumnType("int(11)");
            entity.Property(e => e.Posicion).HasColumnType("int(11)");
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

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

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
            entity.Property(e => e.IdCaja).HasColumnType("int(11)");
            entity.Property(e => e.IdRol).HasColumnType("int(11)");
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
