﻿using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.UseCases.Libros.Consultar
{
    public class GetAllLibrosHandler : IRequestHandler<GetlAllLibrosQuery, Result<List<LibroDto>>>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetAllLibrosHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<List<LibroDto>>> Handle(GetlAllLibrosQuery request, CancellationToken cancellationToken)
        {
            var libros = await _dbcontext.Libro.ToListAsync();
            var librosDto = libros.Select(libro => new LibroDto
            {
                LibroId = libro.LibroId,
                Nombre = libro.Nombre,
                Descripcion = libro.Descripcion,
                Estado = libro.Estado
            }).ToList();

            return Result<List<LibroDto>>.Success(librosDto);
        }
    }
}
