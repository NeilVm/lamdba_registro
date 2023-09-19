namespace registro.Data;
using registro.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Registro> Registros { get; set; }

    

}
