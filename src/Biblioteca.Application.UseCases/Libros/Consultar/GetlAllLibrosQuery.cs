using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Consultar
{
    public class GetlAllLibrosQuery : IRequest<List<LibroDto>>
    {
    }
}