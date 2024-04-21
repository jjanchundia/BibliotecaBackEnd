using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.UseCases.Libros.ConsultarPorId
{
    public class GetAllLibroByIdHandler : IRequestHandler<GetlAllLibrosQueryById, Result<LibroDto>>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetAllLibroByIdHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<LibroDto>> Handle(GetlAllLibrosQueryById request, CancellationToken cancellationToken)
        {
            var lib = await _dbcontext.Libro.Where(x => x.LibroId == request.LibroId).FirstOrDefaultAsync();

            if(lib == null)
            {
                return Result<LibroDto>.Failure("No se encontró libro!");
            }

            var librosDto = new LibroDto
            {
                LibroId = lib.LibroId,
                Nombre = lib.Nombre,
                Descripcion = lib.Descripcion,
                Estado = lib.Estado
            };

            return Result<LibroDto>.Success(librosDto);
        }
    }
}
