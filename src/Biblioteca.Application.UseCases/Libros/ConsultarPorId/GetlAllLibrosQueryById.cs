using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.ConsultarPorId
{
    public class GetlAllLibrosQueryById : IRequest<LibroDto>
    {
        public int LibroId { get; set; }
    }
}