using Biblioteca.Application.UseCases.Consultar;
using Biblioteca.Application.UseCases.Crear;
using Biblioteca.Application.UseCases.Devolver;
using Biblioteca.Application.UseCases.Eliminar;
using Biblioteca.Application.UseCases.Prestar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
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

        [HttpPost("register")]
        public async Task<IActionResult> CreateLibro(CreateLibroCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("delete/{libroId:int}")]
        public async Task<IActionResult> DeleteLibro(int analisisId)
        {
            var response = await _mediator.Send(new DeleteLibroCommand() { LibroId = analisisId });
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
