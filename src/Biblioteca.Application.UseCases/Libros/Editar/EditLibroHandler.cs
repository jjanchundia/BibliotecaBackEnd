using Biblioteca.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.UseCases.Libros.Editar
{
    public class EditLibroHandler : IRequestHandler<EditLibroCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public EditLibroHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(EditLibroCommand request, CancellationToken cancellationToken)
        {
            var libro = await _dbcontext.Libro.Where(x => x.LibroId == request.LibroId).FirstOrDefaultAsync();
            if (libro == null)
            {
                return false;
            }

            // Actualizar los campos del libro con los valores proporcionados en la solicitud
            libro.Nombre = request.Nombre;
            libro.Descripcion = request.Descripcion;

            // Guardar los cambios en la base de datos
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}