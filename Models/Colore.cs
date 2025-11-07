using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Models;

[Table("colores")]
public partial class Colore
{
    [Key]
    [Column("idcolor", TypeName = "int(11)")]
    public int Idcolor { get; set; }

    [Column("color")]
    [StringLength(45)]
    public string Color { get; set; } = null!;

    [Column("activo", TypeName = "smallint(1)")]
    public short Activo { get; set; }

    [InverseProperty("IdcolorNavigation")]
    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
