using MediatR;
using Biblioteca.Persistence;
using Biblioteca.Domain;
using Biblioteca.Application.Dtos;

namespace Biblioteca.Application.UseCases.Libros.Crear
{
    public class CreateLibroHandler : IRequestHandler<CreateLibroCommand, Result<LibroDto>>
    {
        private readonly ApplicationDbContext _dbcontext;
        public CreateLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<LibroDto>> Handle(CreateLibroCommand request, CancellationToken cancellationToken)
        {
            var nuevo = new Libro()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Estado = "L" //L Libre, P Prestado    
            };

            await _dbcontext.Libro.AddAsync(nuevo);
            await _dbcontext.SaveChangesAsync();

            var ultimoIdInsertado = nuevo.LibroId;

            return Result<LibroDto>.Success(new LibroDto
            {
                LibroId = ultimoIdInsertado,
                Nombre = nuevo.Nombre,
                Descripcion = nuevo.Descripcion,
                Estado = nuevo.Estado
            });
        }
    }
}