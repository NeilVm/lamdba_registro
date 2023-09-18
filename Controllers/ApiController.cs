using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using registro.Data;
using registro.Models;

namespace registro.Controllers
{

    [ApiController]
    [Route("registro")]
    public class ApiController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context) // Inyecta el contexto de la base de datos a través de la inyección de dependencias
        {
            _context = context;
        }

        [HttpPost]
        [Route("registrar")]
        public IActionResult guardarRegistro(Registro registro )
        {
            if (ModelState.IsValid)
            {
                _context.Registros.Add(registro); // Agrega el registro al contexto
                _context.SaveChanges(); // Guarda los cambios en la base de datos

                return Ok("Registro guardado exitosamente.");
            }

            return BadRequest("Los datos del registro no son válidos."); // Si la validación falla
        }
    }
}
