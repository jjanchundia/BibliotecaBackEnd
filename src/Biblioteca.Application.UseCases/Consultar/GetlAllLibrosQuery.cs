using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.UseCases.Consultar
{
    public class GetlAllLibrosQuery : IRequest<List<LibroDto>>
    {
    }
}