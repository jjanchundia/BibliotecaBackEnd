using Biblioteca.Application.UseCases.Libros.Consultar;
using Biblioteca.Application.UseCases.Libros.Crear;
using Biblioteca.Application.UseCases.Libros.Devolver;
using Biblioteca.Application.UseCases.Libros.Eliminar;
using Biblioteca.Application.UseCases.Libros.Prestar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Biblioteca.Application.UseCases.Libros.Editar;
using Biblioteca.Application.UseCases.Libros.ConsultarPorId;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibrosController : ControllerBase
    {
        //Uso de Mediatr que implementa el patrón Mediator en C#.
        //El patrón Mediator es un patrón de diseño de comportamiento que reduce el acoplamiento entre los componentes de un programa
        private readonly IMediator _mediator;
        public LibrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LibrosController>
        [HttpGet]
        public async Task<IActionResult> ListLibros()
        {
            var response = await _mediator.Send(new GetlAllLibrosQuery());
            return Ok(response);
        }

        [HttpGet("{libroId:int}")]
        public async Task<IActionResult> LibroPorId(int libroId)
        {
            var response = await _mediator.Send(new GetlAllLibrosQueryById() { LibroId = libroId });
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateLibro(CreateLibroCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditLibro(EditLibroCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("delete/{libroId:int}")]
        public async Task<IActionResult> DeleteLibro(int libroId)
        {
            var response = await _mediator.Send(new DeleteLibroCommand() { LibroId = libroId });
            return Ok(response);
        }

        [HttpPost("prestar")]
        public async Task<IActionResult> PrestarLibro([FromBody] PrestarCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("devolver")]
        public async Task<IActionResult> Devolveribro([FromBody] DevolverLibroCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
