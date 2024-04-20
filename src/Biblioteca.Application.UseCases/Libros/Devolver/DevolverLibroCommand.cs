using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Devolver
{
    public class DevolverLibroCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
    }
}