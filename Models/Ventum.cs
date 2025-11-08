using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("venta")]
[Index("IdSucursal", Name = "venta_sucursal_fk_idx")]
public partial class Ventum
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_sucursal", TypeName = "int(11)")]
    public int IdSucursal { get; set; }

    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("nit")]
    [StringLength(45)]
    public string Nit { get; set; } = null!;

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Column("totalq")]
    [Precision(10, 0)]
    public decimal Totalq { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("Venta")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
