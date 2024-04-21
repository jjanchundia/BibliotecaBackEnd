using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Prestar
{
    public class PrestarCommand : IRequest<Result<string>>
    {
        public int LibroId { get; set; }
    }
}