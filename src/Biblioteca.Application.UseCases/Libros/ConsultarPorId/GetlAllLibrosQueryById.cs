using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.ConsultarPorId
{
    public class GetlAllLibrosQueryById : IRequest<Result<LibroDto>>
    {
        public int LibroId { get; set; }
    }
}