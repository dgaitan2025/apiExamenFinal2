using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiExamenFinal.Models
{
    public class VehiculoDTO
    
    {
        public int Idvehiculo { get; set; }

        [Column("idcolor", TypeName = "int(11)")]
        public int Idcolor { get; set; }

        [Column("idmarca", TypeName = "int(11)")]
        public int Idmarca { get; set; }

        [Column("modelo", TypeName = "smallint(4)")]
        public short Modelo { get; set; }

        [Column("chasis")]
        [StringLength(45)]
        public string Chasis { get; set; } = null!;

        [Column("motor")]
        [StringLength(45)]
        public string Motor { get; set; } = null!;

        [Column("nombre")]
        [StringLength(45)]
        public string Nombre { get; set; } = null!;

        public string Accion { get; set; } = null!;

        [Column("activo", TypeName = "smallint(1)")]
        public short Activo { get; set; }

    }
}
