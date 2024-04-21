using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Crear
{
    public class CreateLibroCommand : IRequest<Result<LibroDto>>
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}