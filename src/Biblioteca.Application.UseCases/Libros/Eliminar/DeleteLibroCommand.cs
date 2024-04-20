using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Eliminar
{
    public class DeleteLibroCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
    }
}