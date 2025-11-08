using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.VentaDetalles).HasConstraintName("venta_detalle_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles).HasConstraintName("venta_detalle_venta");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venta_sucursal_fk");
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<IdResponse>().HasNoKey();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
