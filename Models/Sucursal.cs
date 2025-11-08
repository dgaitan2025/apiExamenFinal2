using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("sucursal")]
public partial class Sucursal
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    public string Nombre { get; set; } = null!;

    [Column("direccion")]
    [StringLength(45)]
    public string Direccion { get; set; } = null!;

    [Column("activo", TypeName = "int(11)")]
    public int Activo { get; set; }

    [InverseProperty("IdSucursalNavigation")]
    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
