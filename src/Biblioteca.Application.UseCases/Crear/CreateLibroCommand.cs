using Biblioteca.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.UseCases.Crear
{
    public class CreateLibroCommand : IRequest<bool>
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}