using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Devolver
{
    public class DevolverLibroCommand : IRequest<Result<string>>
    {
        public int LibroId { get; set; }
    }
}