using Biblioteca.Application.UseCases.Usuarios.Crear;
using Biblioteca.Application.UseCases.Usuarios.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public UsuariosController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUsuario(CreateUsuarioCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);

            if (response)
            {
                // Crear claims basados en el usuario autenticado
                var claims = new[]
                {
                    new Claim(command.Username, command.Password),
                    // Puedes agregar más claims según sea necesario (por ejemplo, roles)
                };

                // Configurar la clave secreta para firmar el token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiracion = DateTime.UtcNow.AddHours(24);
                // Configurar la información del token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: expiracion,
                    signingCredentials: creds);

                // Devolver el token JWT como resultado de la autenticación exitosa
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            // Devolver un error de no autorizado si las credenciales son incorrectas
            return Unauthorized();
        }
    }
}