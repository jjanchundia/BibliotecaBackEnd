using MediatR;

namespace Biblioteca.Application.UseCases.Eliminar
{
    public class DeleteLibroCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
    }
}