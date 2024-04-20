using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Libros.Prestar
{
    public class PrestarHandler : IRequestHandler<PrestarCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public PrestarHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(PrestarCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el libro que se desea actualizar
            var libroToUpdate = await _dbcontext.Libro.FindAsync(request.LibroId);

            if (libroToUpdate == null)
            {
                // El libro no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                return false;
            }

            if (libroToUpdate.Estado == "P")
            {
                // El libro se encuentra prestado
                return false;
            }

            // 2. Actualizar las propiedades del libro
            libroToUpdate.Estado = "P";

            // 3. Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}