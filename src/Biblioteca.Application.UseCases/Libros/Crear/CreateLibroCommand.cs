using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Crear
{
    public class CreateLibroCommand : IRequest<bool>
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}