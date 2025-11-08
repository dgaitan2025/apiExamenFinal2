using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiExamenFinal.Models;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace apiExamenFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SucursalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sucursales
        [HttpGet("Sucursales")]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
        {
            var sucursales = await _context.Sucursals.ToListAsync();
            return Ok(sucursales);
        }

        // GET: api/Sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursals.FindAsync(id);

            if (sucursal == null)
                return NotFound(new { message = "Sucursal no encontrada" });

            return Ok(sucursal);
        }

        [HttpGet("productos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        [HttpPost("venta")]
        public async Task<IActionResult> CrearVenta([FromBody] VentaCompletaRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Datos inválidos" });

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- 1️⃣ Ejecutar CRUD_VENTA ---
                var pResultadoVenta = new MySqlParameter("@presultado", MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "CALL CRUD_VENTA({0}, {1}, {2}, {3}, {4}, {5}, {6}, @presultado);",
                    request.Venta.Id,
                    request.Venta.IdSucursal,
                    request.Venta.Fecha,
                    request.Venta.Nit,
                    request.Venta.Nombre,
                    request.Venta.Totalq,
                    request.Venta.Opcion
                );

                // 🔹 Obtener ID de la venta recién creada
                var idVenta = await _context.Venta.MaxAsync(v => v.Id);

                // --- 2️⃣ Ejecutar CRUD_VENTA_DETALLE por cada item ---
                foreach (var detalle in request.Detalles)
                {
                    var pResultadoDetalle = new MySqlParameter("@presultado", MySqlDbType.Int32)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "CALL CRUD_VENTA_DETALLE({0}, {1}, {2}, {3}, {4}, {5}, {6}, @presultado);",
                        detalle.Id,
                        idVenta, // 🔹 id_venta que acabamos de crear
                        detalle.IdProducto,
                        detalle.Cantidad,
                        detalle.Precio,
                        detalle.Subtotal,
                        detalle.Opcion
                    );
                }

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Venta registrada exitosamente con sus detalles.",
                    idVenta
                });
            }
            catch (System.Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { success = false, message = "Error al registrar la venta", error = ex.Message });
            }
        }
    }
}

