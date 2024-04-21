using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Consultar
{
    public class GetlAllLibrosQuery : IRequest<Result<List<LibroDto>>>
    {
    }
}