using MediatR;

namespace Biblioteca.Application.UseCases.Devolver
{
    public class DevolverLibroCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
        //public string? Name { get; set; }
    }
}