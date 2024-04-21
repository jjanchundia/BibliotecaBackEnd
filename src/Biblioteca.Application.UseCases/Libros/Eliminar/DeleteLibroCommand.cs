using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Eliminar
{
    public class DeleteLibroCommand : IRequest<Result<string>>
    {
        public int LibroId { get; set; }
    }
}