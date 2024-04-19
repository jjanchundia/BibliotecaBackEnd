using MediatR;

namespace Biblioteca.Application.UseCases.Prestar
{
    public class PrestarCommand : IRequest<bool>
    {
        public int LibroId { get; set; }
        //public string? Name { get; set; }
    }
}