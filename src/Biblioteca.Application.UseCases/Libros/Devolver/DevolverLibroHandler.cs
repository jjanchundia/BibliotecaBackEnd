using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Devolver
{
    public class DevolverLibroHandler : IRequestHandler<DevolverLibroCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public DevolverLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(DevolverLibroCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el libro que se desea actualizar
            var libroToUpdate = await _dbcontext.Libro.FindAsync(request.LibroId);

            if (libroToUpdate == null)
            {
                // El libro no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                return false;
            }

            // 2. Actualizar las propiedades del libro
            libroToUpdate.Estado = "L";

            // 3. Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}