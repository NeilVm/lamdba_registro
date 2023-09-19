using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using registro.Data;
using registro.Models;
using registro.Services;

namespace registro.Controllers

{

    [ApiController]
    [Route("registro")]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public ApiController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        [HttpPost]
        [Route("registrar")]
        public IActionResult GuardarRegistro([FromBody] Registro registro)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya está en uso (puedes agregar validación adicional aquí)
                var usuarioExistente = _context.Registros.FirstOrDefault(r => r.Correo == registro.Correo);
                if (usuarioExistente != null)
                {
                    return BadRequest("El correo ya está registrado.");
                }

                // Generar un hash de contraseña antes de almacenarla
                string contrasenaPlana = registro.ContrasenaHash; // Aquí obtienes la contraseña proporcionada por el usuario
                string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);

                // Almacenar el hash de contraseña en el modelo de usuario
                registro.ContrasenaHash = contrasenaHash;

                _context.Registros.Add(registro); // Agrega el registro al contexto
                _context.SaveChanges(); // Guarda los cambios en la base de datos

                return Ok("Registro guardado exitosamente.");
            }

            return BadRequest("Los datos del registro no son válidos."); // Si la validación falla
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            bool isAuthenticated = await _authService.AuthenticateAsync(loginRequest.Correo, loginRequest.Contrasena);

            if (isAuthenticated)
            {
               
                return Ok("Autenticación exitosa");
            }
            else
            {
                return Unauthorized("Credenciales inválidas");
            }
        }
    }

    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
