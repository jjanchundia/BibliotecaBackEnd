using MediatR;
using Biblioteca.Persistence;
using Biblioteca.Domain;

namespace Biblioteca.Application.UseCases.Crear
{
    public class CreateLibroHandler : IRequestHandler<CreateLibroCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public CreateLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(CreateLibroCommand request, CancellationToken cancellationToken)
        {
            var nuevo = new Libro()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "L" //L Libre, P Prestado    
            };

            await _dbcontext.Libro.AddAsync(nuevo);
            _dbcontext.SaveChanges();
            return true;
        }
    }
}