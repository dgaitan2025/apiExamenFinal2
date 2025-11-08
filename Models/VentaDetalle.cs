using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("venta_detalle")]
[Index("IdProducto", Name = "venta_detalle_producto_idx")]
[Index("IdVenta", Name = "venta_detalle_venta_idx")]
public partial class VentaDetalle
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_venta", TypeName = "int(11)")]
    public int? IdVenta { get; set; }

    [Column("id_producto", TypeName = "int(11)")]
    public int? IdProducto { get; set; }

    [Column("cantidad", TypeName = "int(11)")]
    public int? Cantidad { get; set; }

    [Column("precio")]
    [Precision(10, 2)]
    public decimal? Precio { get; set; }

    [Column("subtotal")]
    [Precision(10, 2)]
    public decimal? Subtotal { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("VentaDetalles")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("VentaDetalles")]
    public virtual Ventum? IdVentaNavigation { get; set; }
}
