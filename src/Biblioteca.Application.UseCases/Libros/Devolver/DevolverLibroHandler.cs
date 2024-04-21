using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Devolver
{
    public class DevolverLibroHandler : IRequestHandler<DevolverLibroCommand, Result<string>>
    {
        private readonly ApplicationDbContext _dbcontext;
        public DevolverLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<string>> Handle(DevolverLibroCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el libro que se desea actualizar
            var libroToUpdate = await _dbcontext.Libro.FindAsync(request.LibroId);

            if (libroToUpdate == null)
            {
                // El libro no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                return Result<string>.Failure("No se encontró libro para devolver!");
            }

            // 2. Actualizar las propiedades del libro
            libroToUpdate.Estado = "L";

            // 3. Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return Result<string>.Success("Libro devuelto correctamente!");
        }
    }
}