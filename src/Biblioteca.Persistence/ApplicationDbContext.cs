using Biblioteca.Domain;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Libro> Libro { get; set; }
    }
}