using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Prestar
{
    public class PrestarCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
        //public string? Name { get; set; }
    }
}