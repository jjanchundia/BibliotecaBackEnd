using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Editar
{
    public class EditLibroCommand: IRequest<Result<LibroDto>>
    {
        public int LibroId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}