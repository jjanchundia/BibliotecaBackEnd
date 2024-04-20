using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Rol del usuario para autorización
    }
}