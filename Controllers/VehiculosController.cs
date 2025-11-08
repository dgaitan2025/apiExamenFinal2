using apiExamenFinal.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiExamenFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var resultado = _context.Vehiculos
               .FromSqlRaw("CALL sp_crud_vehiculos({0}, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'R')", 1)
               .ToList();
            return Ok(resultado);
        }

        [HttpPost("Crud")]
        public IActionResult CrudVehiculos([FromBody] VehiculoDTO request)
        {
            try
            {
                var result = _context.Set<IdResponse>()
                .FromSqlRaw("CALL sp_crud_vehiculos({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                    request.Idvehiculo,
                    request.Idcolor,
                    request.Idmarca,
                    request.Modelo,
                    request.Chasis,
                    request.Motor,
                    request.Nombre,
                    request.Activo,
                    request.Accion)
                .AsEnumerable()
                .FirstOrDefault();

                return Ok(new
                {
                    success = true,
                    message = "Vehículo creado correctamente.",
                    idGenerado = result?.Idvehiculo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
