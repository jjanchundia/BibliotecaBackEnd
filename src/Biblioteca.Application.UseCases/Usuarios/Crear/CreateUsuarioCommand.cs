using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Crear
{
    public class CreateUsuarioCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}