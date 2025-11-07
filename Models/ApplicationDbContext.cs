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

    public virtual DbSet<Colore> Colores { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<IdResponse> IdResponse { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Colore>(entity =>
        {
            entity.HasKey(e => e.Idcolor).HasName("PRIMARY");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Idmarca).HasName("PRIMARY");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(e => e.Idvehiculo).HasName("PRIMARY");

            entity.HasOne(d => d.IdcolorNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vehiculos_colores");

            entity.HasOne(d => d.IdmarcaNavigation).WithMany(p => p.Vehiculos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vehiculos_marcas");
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<IdResponse>().HasNoKey();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
