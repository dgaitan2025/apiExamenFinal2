using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("marcas")]
public partial class Marca
{
    [Key]
    [Column("idmarca", TypeName = "int(11)")]
    public int Idmarca { get; set; }

    [Column("marca")]
    [StringLength(45)]
    public string Marca1 { get; set; } = null!;

    [Column("activo", TypeName = "smallint(1)")]
    public short Activo { get; set; }

    [InverseProperty("IdmarcaNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
