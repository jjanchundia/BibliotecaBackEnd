using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Crear
{
    public class CreateUsuarioCommand : IRequest<Result<UserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}