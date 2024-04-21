using Biblioteca.Application.Dtos;
using Biblioteca.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.UseCases.Libros.ConsultarPorId
{
    public class GetAllLibroByIdHandler : IRequestHandler<GetlAllLibrosQueryById, LibroDto>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetAllLibroByIdHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<LibroDto> Handle(GetlAllLibrosQueryById request, CancellationToken cancellationToken)
        {
            var lib = await _dbcontext.Libro.Where(x => x.LibroId == request.LibroId).FirstOrDefaultAsync();

            if(lib == null)
            {
                //return false;
            }

            var librosDto = new LibroDto
            {
                LibroId = lib.LibroId,
                Nombre = lib.Nombre,
                Descripcion = lib.Descripcion,
                Estado = lib.Estado
            };

            return librosDto;
        }
    }
}
