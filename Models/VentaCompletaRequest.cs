namespace apiExamenFinal.Models
{
    public class VentaCompletaRequest
    {
        public VentaRequest Venta { get; set; } = new VentaRequest();
        public List<VentaDetalleRequest> Detalles { get; set; } = new List<VentaDetalleRequest>();
    }

    public class VentaRequest
    {
        public int? Id { get; set; }
        public int IdSucursal { get; set; }
        public string? Fecha { get; set; }
        public string? Nit { get; set; }
        public string? Nombre { get; set; }
        public decimal Totalq { get; set; }
        public string? Opcion { get; set; } // 'C' o 'A'
    }

    public class VentaDetalleRequest
    {
        public int? Id { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal? Precio { get; set; } // opcional, lo toma del SP
        public decimal? Subtotal { get; set; } // opcional
        public string? Opcion { get; set; } // 'C' o 'A'
    }
}
