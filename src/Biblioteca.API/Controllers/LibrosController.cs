using Biblioteca.Application.UseCases.Consultar;
using Biblioteca.Application.UseCases.Crear;
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

        // POST api/<LibrosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LibrosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
