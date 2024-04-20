using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Login
{
    public class LoginCommand : IRequest<bool>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}