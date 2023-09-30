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
            
            var user = await _dbContext.Registros.FirstOrDefaultAsync(r => r.Correo == correo);

          
            if (user == null)
            {
                return false;
            }

            
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(contrasena, user.ContrasenaHash);

            return isPasswordValid;
        }

    }
}

