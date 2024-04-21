using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Prestar
{
    public class PrestarHandler : IRequestHandler<PrestarCommand, Result<string>>
    {
        private readonly ApplicationDbContext _dbcontext;
        public PrestarHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<string>> Handle(PrestarCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el libro que se desea actualizar
            var libroToUpdate = await _dbcontext.Libro.FindAsync(request.LibroId);

            if (libroToUpdate == null)
            {
                // El libro no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                return Result<string>.Failure("No se encontró libro para prestar!");
            }

            if (libroToUpdate.Estado == "P")
            {
                // El libro se encuentra prestado
                return Result<string>.Failure("Libro ya se encuentra en estado Prestado!");
            }

            // 2. Actualizar las propiedades del libro
            libroToUpdate.Estado = "P";

            // 3. Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return Result<string>.Success("Libro Prestado correctamente!");
        }
    }
}