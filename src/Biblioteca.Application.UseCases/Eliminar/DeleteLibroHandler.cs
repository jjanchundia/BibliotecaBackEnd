using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Eliminar
{
    public class DeleteLibroHandler : IRequestHandler<DeleteLibroCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public DeleteLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(DeleteLibroCommand request, CancellationToken cancellationToken)
        {
            var libroToDelete = await _dbcontext.Libro.FindAsync(request.LibroId);

            if (libroToDelete == null)
            {
                // El libro no fue encontrado, puedes manejar esta situación de acuerdo a tus necesidades
                return false;
            }

            // 2. Eliminar el libro
            _dbcontext.Libro.Remove(libroToDelete);

            // 3. Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}