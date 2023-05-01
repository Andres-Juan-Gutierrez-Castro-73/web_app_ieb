using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class WebApp2Context : DbContext
{
    public WebApp2Context()
    {
    }

    public WebApp2Context(DbContextOptions<WebApp2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Seleccion> Seleccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseMySql("server=localhost;port=3306;database=web_app2;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("catalogo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ampacidad)
                .HasPrecision(5, 2)
                .HasColumnName("ampacidad");
            entity.Property(e => e.CodigoCable).HasColumnName("codigo_cable");
            entity.Property(e => e.Corriente).HasColumnName("corriente");
            entity.Property(e => e.DiametroCable).HasColumnName("diametro_cable");
            entity.Property(e => e.EspesorPantalla).HasColumnName("espesor_pantalla");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.Tension).HasColumnName("tension");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreProyecto)
                .HasMaxLength(50)
                .HasColumnName("nombre_proyecto");
            entity.Property(e => e.TipoProyecto)
                .HasMaxLength(50)
                .HasColumnName("tipo_proyecto");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Seleccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("seleccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Corriente).HasColumnName("corriente");
            entity.Property(e => e.FechaInsercion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_insercion");
            entity.Property(e => e.Instalacion)
                .HasMaxLength(50)
                .HasColumnName("instalacion");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.NombreProyecto)
                .HasMaxLength(50)
                .HasColumnName("nombre_proyecto");
            entity.Property(e => e.NombreSeleccion)
                .HasMaxLength(50)
                .HasColumnName("nombre_seleccion");
            entity.Property(e => e.Tension).HasColumnName("tension");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
