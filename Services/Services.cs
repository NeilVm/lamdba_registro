using Microsoft.EntityFrameworkCore;
using registro.Data;
using registro.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace registro.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AuthenticateAsync(string correo, string contrasena)
        {
            // Busca un registro con el correo proporcionado en la base de datos
            var user = await _dbContext.Registros.FirstOrDefaultAsync(r => r.Correo == correo);

            // Si el usuario no existe, la autenticación falla
            if (user == null)
            {
                return false;
            }

            // Verifica la contraseña proporcionada con el hash almacenado en ContrasenaHash
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(contrasena, user.ContrasenaHash);

            return isPasswordValid;
        }

    }
}

