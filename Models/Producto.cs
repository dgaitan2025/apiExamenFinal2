using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("producto")]
public partial class Producto
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(500)]
    public string Descripcion { get; set; } = null!;

    [Column("precio_unitario")]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    [Column("entradas", TypeName = "int(11)")]
    public int Entradas { get; set; }

    [Column("salidas", TypeName = "int(11)")]
    public int Salidas { get; set; }

    [Column("stock", TypeName = "int(11)")]
    public int Stock { get; set; }

    [Column("activo", TypeName = "int(11)")]
    public int? Activo { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
