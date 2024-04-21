using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Login
{
    public class LoginCommand : IRequest<Result<UserDto>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}