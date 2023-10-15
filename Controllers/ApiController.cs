using k8s.KubeConfigModels;
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
                var usuarioExistente = _context.Registros.FirstOrDefault(r => r.Correo == registro.Correo);
                if (usuarioExistente != null)
                {
                    return BadRequest("El correo ya está registrado.");
                }

                string contrasenaPlana = registro.ContrasenaHash;
                string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);

                registro.ContrasenaHash = contrasenaHash;

                _context.Registros.Add(registro);
                _context.SaveChanges();

                return Ok("Registro guardado exitosamente.");
            }

            return BadRequest("Los datos del registro no son válidos.");
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

        [HttpPut]
        [Route("cambiocon")]
        public IActionResult CambiarContrasena([FromBody] CambioContrasenaRequest cambioRequest)
        {
            var usuario = _context.Registros.FirstOrDefault(r => r.Correo == cambioRequest.Correo);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (cambioRequest.Contrasena != cambioRequest.ConfirmarContrasena)
            {
                return BadRequest("Las contraseñas no coinciden");
            }

           
            string contrasenaPlana = cambioRequest.Contrasena;
            string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);

            usuario.ContrasenaHash = contrasenaHash;
            usuario.ConfirmarContrasena = contrasenaHash; 
            _context.SaveChanges();

            return Ok("Contraseñas cambiadas exitosamente");
        }


        public class LoginRequest
        {
            public string Correo { get; set; }
            public string Contrasena { get; set; }
        }

        public class CambioContrasenaRequest
        {
            public string Correo { get; set; }
            public string Contrasena { get; set; }
            public string ConfirmarContrasena { get; set; }
        }
    }
}
