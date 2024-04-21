using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Editar
{
    public class EditLibroCommand: IRequest<bool>
    {
        public int LibroId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}